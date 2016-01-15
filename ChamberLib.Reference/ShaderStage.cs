using System;

namespace ChamberLib
{
    public class ShaderStage : IShaderStage
    {
        public ShaderStage()
        {
        }

        #region IShaderStage implementation

        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Source
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ShaderType ShaderType
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}

