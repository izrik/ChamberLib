﻿using System;

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
            throw new NotImplementedException();
        }

        #endregion
    }
}

