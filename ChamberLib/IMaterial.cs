using System;

namespace ChamberLib
{
    public interface IMaterial
    {
        Vector3 Diffuse { get; set; }
        ITexture2D Texture { get; set; }
    }
}

