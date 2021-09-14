using System;

namespace ChamberLib
{
    public interface ITexture2D
    {
        Vector2 GetSize();

        void Apply(int textureSlot=0);
        void UnApply();
    }
}

