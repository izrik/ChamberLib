using System;
using System.Collections.Generic;

namespace ChamberLib
{
    public class ShaderProgram : IShaderProgram
    {
        public ShaderProgram()
        {
        }

        #region IShaderProgram implementation

        public void Apply(Overrides overrides = null)
        {
            throw new NotImplementedException();
        }

        public void UnApply()
        {
            throw new NotImplementedException();
        }

        public void SetBindAttributes(IEnumerable<string> bindattrs)
        {
            throw new NotImplementedException();
        }

        public void SetUniform(string name, bool value)
        {
            throw new NotImplementedException();
        }

        public void SetUniform(string name, byte value)
        {
            throw new NotImplementedException();
        }

        public void SetUniform(string name, sbyte value)
        {
            throw new NotImplementedException();
        }

        public void SetUniform(string name, short value)
        {
            throw new NotImplementedException();
        }

        public void SetUniform(string name, ushort value)
        {
            throw new NotImplementedException();
        }

        public void SetUniform(string name, int value)
        {
            throw new NotImplementedException();
        }

        public void SetUniform(string name, uint value)
        {
            throw new NotImplementedException();
        }

        public void SetUniform(string name, float value)
        {
            throw new NotImplementedException();
        }

        public void SetUniform(string name, double value)
        {
            throw new NotImplementedException();
        }

        public void SetUniform(string name, Vector2 value)
        {
            throw new NotImplementedException();
        }

        public void SetUniform(string name, Vector3 value)
        {
            throw new NotImplementedException();
        }

        public void SetUniform(string name, Vector4 value)
        {
            throw new NotImplementedException();
        }

        public void SetUniform(string name, Matrix value)
        {
            throw new NotImplementedException();
        }

        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IShaderStage VertexShader
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IShaderStage FragmentShader
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<string> BindAttributes
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}

