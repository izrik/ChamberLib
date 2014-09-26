using System;

namespace ChamberLib
{
    public interface ITexture2D
    {
        Vector2 GetSize();

        void Apply();
        void UnApply();
    }


}

