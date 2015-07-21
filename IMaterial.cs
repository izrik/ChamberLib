using System;

namespace ChamberLib
{
    public interface IMaterial
    {
        string Name { get; }

        void Apply(Matrix world, Matrix view, Matrix projection,
                    LightingData lighting);
        void UnApply();

        Vector3 Diffuse { get; set; }
        Vector3 EmissiveColor { get; set; }
        Vector3 SpecularColor { get; set; }
        float SpecularPower { get; set; }
        float Alpha { get; set; }
        ITexture2D Texture { get; set; }

        IShaderProgram Shader { get; set; }
    }
}

