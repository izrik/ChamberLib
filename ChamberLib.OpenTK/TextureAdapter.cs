using System;

namespace ChamberLib
{
    public class TextureAdapter : ITexture2D
    {
        public TextureAdapter()
        {
        }

        #region ITexture2D implementation

        public Vector2 GetSize()
        {
            return Vector2.Zero;
        }

        #endregion
    }
}

