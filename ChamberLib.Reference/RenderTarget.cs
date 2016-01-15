using System;

namespace ChamberLib
{
    public class RenderTarget : IRenderTarget
    {
        public RenderTarget()
        {
        }

        #region IRenderTarget implementation

        public void Apply()
        {
            throw new NotImplementedException();
        }

        public void UnApply()
        {
            throw new NotImplementedException();
        }

        public ITexture2D AsTexture()
        {
            throw new NotImplementedException();
        }

        public int Width
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int Height
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}

