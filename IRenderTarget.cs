using System;

namespace ChamberLib
{
    public interface IRenderTarget
    {
        int Width { get; }
        int Height { get; }

        void Apply();
        void Apply(Color clearColor);
        void Apply(ColorF clearColor);
        void UnApply();

        ITexture2D AsTexture();
    }
}

