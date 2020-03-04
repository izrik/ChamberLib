using System;
using OpenTK.Graphics.OpenGL;
using ChamberLib.Content;

namespace ChamberLib.OpenTK
{
    public abstract class Material : IMaterial, IVertexMaterial, IFragmentMaterial
    {
        protected Material(MaterialContent material, ContentResolver resolver, IContentProcessor processor)
        {
            this.Name = material.Name;
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
            ApplyVertexShader(vertexShader, gameTime, world, components,
                overrides);

            ApplyFragmentShader(fragmentShader, gameTime, world, components,
                overrides);
        }

        public void ApplyVertexShader(IShaderStage vertexShader,
                                        GameTime gameTime, Matrix world,
                                        ComponentCollection components,
                                        Overrides overrides=default(Overrides))
        {
            if (vertexShader == null) throw new InvalidOperationException("No vertex shader specified");

            var camera = components?.Get<ICamera>();
            var view = camera?.View ?? Matrix.Identity;
            var projection = camera?.Projection ?? __Apply_defaultProjection;

            vertexShader.SetUniform("worldViewProj", world * view * projection);
            vertexShader.SetUniform("worldView", world * view);
            vertexShader.SetUniform("viewProj", view * projection);
            vertexShader.SetUniform("view", view);
            vertexShader.SetUniform("world", world);
        }

        void IVertexMaterial.Apply(GameTime gameTime, Matrix world,
                                    ComponentCollection components,
                                    IShaderStage vertexShader,
                                    Overrides overrides=default(Overrides))
        {
            ApplyVertexShader(vertexShader, gameTime, world, components,
                overrides);
        }

        public void ApplyFragmentShader(IShaderStage fragmentShader,
                                        GameTime gameTime, Matrix world,
                                        ComponentCollection components,
                                        Overrides overrides=default(Overrides))
        {
            if (fragmentShader == null) throw new InvalidOperationException("No fragment shader specified");

            var ambient = components?.Get<AmbientLight>();
            fragmentShader.SetUniform("light_ambient", ambient?.Color ?? Vector3.Zero);

            var light = components?.Get<DirectionalLight>();
            fragmentShader.SetUniform("light_direction_ws", light?.Direction.Normalized() ?? -Vector3.UnitY);
            fragmentShader.SetUniform("light_diffuse_color", light?.DiffuseColor ?? Vector3.One);
            fragmentShader.SetUniform("light_specular_color", light?.SpecularColor ?? Vector3.One);

            var camera = components?.Get<ICamera>();
            var view = camera?.View ?? Matrix.Identity;
            fragmentShader.SetUniform("camera_position_ws", view.Inverted().Translation);

            fragmentShader.SetUniform("use_texture", (Texture != null));
            fragmentShader.SetUniform("material_diffuse_color", Diffuse);
            fragmentShader.SetUniform("material_emissive_color", EmissiveColor);
            fragmentShader.SetUniform("material_specular_color", SpecularColor);
            fragmentShader.SetUniform("material_specular_power", SpecularPower);
            float alpha = overrides.GetAlpha(Alpha);
            fragmentShader.SetUniform("material_alpha", alpha);

            if (Texture != null)
            {
                Texture.Apply();
            }
        }

        void IFragmentMaterial.Apply(GameTime gameTime, Matrix world,
            ComponentCollection components, IShaderStage fragmentShader,
            Overrides overrides=default(Overrides))
        {
            ApplyFragmentShader(fragmentShader, gameTime, world, components,
                overrides);
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

