using System;
using ChamberLib.Content;

namespace ChamberLib.OpenTK
{
    public class FragmentMaterial : Material, IFragmentMaterial
    {
        public FragmentMaterial(FragmentMaterialContent material, ContentResolver resolver, IContentProcessor processor)
            : base(material, resolver, processor)
        {
            this.Diffuse = material.DiffuseColor;
            this.EmissiveColor = material.EmissiveColor;
            this.SpecularColor = material.SpecularColor;
            this.SpecularPower = material.SpecularPower;
            this.Alpha = material.Alpha;

            if (material.Texture != null)
            {
                this.Texture = processor.ProcessTexture2D(material.Texture);
            }

            if (material.FragmentShader != null)
            {
                var fragment =
                    processor.ProcessShaderStage(material.FragmentShader, processor);
                this.FragmentShader = fragment;
            }
        }

        void IFragmentMaterial.Apply(GameTime gameTime, Matrix world,
            ComponentCollection components, IShaderStage fragmentShader,
            Overrides overrides=default(Overrides))
        {
            ApplyFragmentShader(fragmentShader, gameTime, world, components,
                overrides);
        }
    }
}
