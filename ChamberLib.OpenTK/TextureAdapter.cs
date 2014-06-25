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
            return new Vector2(25, 25);
        }

        #endregion
    }
}

