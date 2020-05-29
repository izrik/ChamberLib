using System;
using ChamberLib.Content;

namespace ChamberLib.OpenTK
{
    public class FragmentMaterial : IFragmentMaterial
    {
        public FragmentMaterial(FragmentMaterialContent material,
            ContentResolver resolver, IContentProcessor processor)
            : this(material.DiffuseColor, material.EmissiveColor,
                  material.SpecularColor, material.SpecularPower,
                  material.Alpha,
                  material.Texture != null ?
                    processor.ProcessTexture2D(material.Texture) : null,
                  material.FragmentShader != null ?
                    processor.ProcessShaderStage(material.FragmentShader,
                      processor) : null,
                  material.Name)
        {
        }
        public FragmentMaterial(Vector3 diffuse, Vector3 emissive,
            Vector3 specular, float specularPower, float alpha,
            ITexture2D texture, IShaderStage fragmentShader, string name)
        {
            this.Name = name;

            this.Diffuse = diffuse;
            this.EmissiveColor = emissive;
            this.SpecularColor = specular;
            this.SpecularPower = specularPower;
            this.Alpha = alpha;

            this.Texture = texture;
            this.FragmentShader = fragmentShader;
        }

        public string Name { get; set; }

        public void Apply(GameTime gameTime, Matrix world,
            ComponentCollection components, IShaderStage fragmentShader,
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

        public void UnApply()
        {
            if (Texture != null)
            {
                Texture.UnApply();
            }
        }

        public Vector3 Diffuse { get; set; }
        public Vector3 EmissiveColor { get; set; }
        public Vector3 SpecularColor { get; set; }
        public float SpecularPower { get; set; }
        public float Alpha { get; set; }

        public ITexture2D Texture { get; set; }

        public IShaderStage FragmentShader { get; set; }

    }
}
