using System;

namespace ChamberLib
{
    public class Texture2D : ITexture2D
    {
        public Texture2D()
        {
        }

        #region ITexture2D implementation

        public Vector2 GetSize()
        {
            throw new NotImplementedException();
        }

        public void Apply(int textureSlot=0)
        {
            throw new NotImplementedException();
        }

        public void UnApply()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

