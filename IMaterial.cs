using System;

namespace ChamberLib
{
    public interface IMaterial
    {
        Vector3 Diffuse { get; set; }
        Vector3 EmissiveColor { get; set; }
        Vector3 SpecularColor { get; set; }
        float SpecularPower { get; set; }
        float Alpha { get; set; }
        ITexture2D Texture { get; set; }

        IShader Shader { get; set; }
    }
}

