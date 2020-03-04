using System;
using ChamberLib.Content;

namespace ChamberLib.OpenTK
{
    public class FragmentMaterial : Material
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
    }
}
