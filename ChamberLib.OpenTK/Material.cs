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

            if (material.Shader != null)
            {
                this.Shader =
                    processor.ProcessShader(
                                        material.Shader,
                                        processor,
                                        new [] {
                                            "in_position",
                                            "in_normal",
                                            "in_texture_coords",
                                            "in_blend_indices",
                                            "in_blend_weights",
                                        });
            }
        }

        public string Name { get; set; }

        public Vector3 Diffuse { get; set; }
        public Vector3 EmissiveColor { get; set; }
        public Vector3 SpecularColor { get; set; }
        public float SpecularPower { get; set; }
        public float Alpha { get; set; }
        public ITexture2D Texture { get; set; }

        public IShader Shader { get; set; }

        public void Apply(Matrix world, Matrix view, Matrix projection,
                            LightingData lighting)
        {
            if (Shader == null) throw new InvalidOperationException();

            Shader.Apply();

            Shader.SetUniform("worldViewProj", world * view * projection);
            Shader.SetUniform("worldView", world * view);
            Shader.SetUniform("view", view);
            Shader.SetUniform("world", world);
            Shader.SetUniform("use_texture", (Texture != null));
            Shader.SetUniform("material_diffuse_color", Diffuse);
            Shader.SetUniform("material_emissive_color", lighting.EmissiveColor);
            Shader.SetUniform("material_specular_color", SpecularColor);
            Shader.SetUniform("material_specular_power", SpecularPower);
            Shader.SetUniform("material_alpha", Alpha);
            Shader.SetUniform("light_ambient", lighting.AmbientLightColor);
            Shader.SetUniform("light_direction_ws", lighting.DirectionalLight.Direction.Normalized());
            Shader.SetUniform("light_diffuse_color", lighting.DirectionalLight.DiffuseColor);
            Shader.SetUniform("light_specular_color", lighting.DirectionalLight.SpecularColor);
            Shader.SetUniform("camera_position_ws", view.Inverted().ToOpenTK().ExtractTranslation().ToChamber());

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

            if (Shader != null)
            {
                Shader.UnApply();
            }
        }
    }
}

