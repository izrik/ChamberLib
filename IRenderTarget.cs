using System;

namespace ChamberLib
{
    public interface IRenderTarget
    {
        int Width { get; }
        int Height { get; }

        void Apply();
        void UnApply();

        ITexture2D AsTexture();
    }
}

