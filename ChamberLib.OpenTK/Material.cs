using System;
using OpenTK.Graphics.OpenGL;
using ChamberLib.Content;

namespace ChamberLib.OpenTK
{
    public class Material : IMaterial
    {
        public Material(MaterialContent material, ContentResolver resolver, IContentProcessor processor)
        {
            this.Name = material.Name;
            this.Diffuse = material.DiffuseColor;
            this.EmissiveColor = material.EmissiveColor;
            this.SpecularColor = material.SpecularColor;
            this.SpecularPower = material.SpecularPower;
            this.Alpha = material.Alpha;

            if (material.Texture != null)
            {
                this.Texture = processor.ProcessTexture2D(material.Texture);
            }

            if (material.VertexShader != null ||
                material.FragmentShader != null)
            {
                var vertex =
                    processor.ProcessShaderStage(material.VertexShader, processor);
                var fragment =
                    processor.ProcessShaderStage(material.FragmentShader, processor);
                vertex.SetBindAttributes(
                    new[] {
                        "in_position",
                        "in_normal",
                        "in_texture_coords",
                        "in_blend_indices",
                        "in_blend_weights",
                    });
                this.VertexShader = vertex;
                this.FragmentShader = fragment;
            }
        }

        public string Name { get; set; }

        public Vector3 Diffuse { get; set; }
        public Vector3 EmissiveColor { get; set; }
        public Vector3 SpecularColor { get; set; }
        public float SpecularPower { get; set; }
        public float Alpha { get; set; }

        public ITexture2D Texture { get; set; }

        public IShaderStage VertexShader { get; set; }
        public IShaderStage FragmentShader { get; set; }

        readonly Matrix __Apply_defaultProjection =
            Matrix.CreateOrthographic(2, 2, 0, 1);
        public void Apply(GameTime gameTime,
                            Matrix world,
                            ComponentCollection components,
                            IShaderStage vertexShader,
                            IShaderStage fragmentShader,
                            Overrides overrides =default(Overrides))
        {
            if (vertexShader == null) throw new InvalidOperationException("No vertex shader specified");
            if (fragmentShader == null) throw new InvalidOperationException("No fragment shader specified");

            var camera = components?.Get<ICamera>();
            var view = camera?.View ?? Matrix.Identity;
            var projection = camera?.Projection ?? __Apply_defaultProjection;

            vertexShader.SetUniform("worldViewProj", world * view * projection);
            vertexShader.SetUniform("worldView", world * view);
            vertexShader.SetUniform("viewProj", view * projection);
            vertexShader.SetUniform("view", view);
            vertexShader.SetUniform("world", world);

            var ambient = components?.Get<AmbientLight>();
            vertexShader.SetUniform("light_ambient", ambient?.Color ?? Vector3.Zero);

            var light = components?.Get<DirectionalLight>();
            vertexShader.SetUniform("light_direction_ws", light?.Direction.Normalized() ?? -Vector3.UnitY);
            vertexShader.SetUniform("light_diffuse_color", light?.DiffuseColor ?? Vector3.One);
            vertexShader.SetUniform("light_specular_color", light?.SpecularColor ?? Vector3.One);

            vertexShader.SetUniform("camera_position_ws", view.Inverted().Translation);

            vertexShader.SetUniform("use_texture", (Texture != null));
            vertexShader.SetUniform("material_diffuse_color", Diffuse);
            vertexShader.SetUniform("material_emissive_color", EmissiveColor);
            vertexShader.SetUniform("material_specular_color", SpecularColor);
            vertexShader.SetUniform("material_specular_power", SpecularPower);
            float alpha = overrides.GetAlpha(Alpha);
            vertexShader.SetUniform("material_alpha", alpha);

            if (Texture != null)
            {
                Texture.Apply();
            }
        }

        public void UnApply()
        {
            if (Texture != null)
            {
                Texture.UnApply();
            }
        }
    }
}

