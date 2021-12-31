using System;
using OpenTK.Graphics.OpenGL;
using System.Diagnostics;
using ChamberLib.Content;
using _OpenTK = global::OpenTK;
using System.Collections.Generic;

namespace ChamberLib.OpenTK.Materials
{
    public class ShaderStage : IShaderStage
    {
        public ShaderStage(ShaderContent content)
            : this(content.Source, content.Type, content.Name)
        {
        }
        public ShaderStage(string source, ShaderType shaderType, string name)
        {
            Source = source;
            ShaderType = shaderType;
            Name = name;
        }

        public string Name { get; protected set; }
        public int ShaderID { get; protected set; }
        public string Source { get; protected set; }
        public ShaderType ShaderType { get; protected set; }

        public bool IsCompiled { get; protected set; }

        public void MakeReady()
        {
            GLHelper.CheckError();
            if (ShaderID == 0)
            {
                ShaderID = GL.CreateShader(ShaderType.ToOpenTK());
                GLHelper.CheckError();
                IsCompiled = false;
            }

            if (!IsCompiled)
            {
                GL.ShaderSource(ShaderID, Source);
                GLHelper.CheckError();
                GL.CompileShader(ShaderID);
                GLHelper.CheckError();

                int result;
                GL.GetShader(ShaderID, ShaderParameter.CompileStatus, out result);
                Debug.WriteLine("{1} compile status: {0}", result, ShaderType);
                GLHelper.CheckError();
                Debug.WriteLine("{0} info:", ShaderType);
                var infoLog = GL.GetShaderInfoLog(ShaderID);
                Debug.WriteLine(infoLog);
                GLHelper.CheckError();

                IsCompiled = (result == 1);
            }
        }

        List<string> bindAttributes = new List<string>();
        public IEnumerable<string> BindAttributes
        {
            get { return bindAttributes; }
        }
        public void SetBindAttributes(IEnumerable<string> bindattrs)
        {
            bindAttributes.Clear();
            bindAttributes.AddRange(bindattrs);
        }

        public ShaderUniforms Uniforms = new ShaderUniforms();
        public void SetUniform(string name, float value)
        {
            Uniforms.SetValue(name, value);
        }
        public void SetUniform(string name, Vector2 value)
        {
            Uniforms.SetValue(name, value);
        }
        public void SetUniform(string name, Vector3 value)
        {
            Uniforms.SetValue(name, value);
        }
        public void SetUniform(string name, Vector4 value)
        {
            Uniforms.SetValue(name, value);
        }
        public void SetUniform(string name, Matrix value)
        {
            Uniforms.SetValue(name, value);
        }
        public void SetUniform(string name, Matrix[] value)
        {
            Uniforms.SetValue(name, value);
        }
        public void SetUniform(string name, bool value)
        {
            Uniforms.SetValue(name, value);
        }
        public void SetUniform(string name, byte value)
        {
            Uniforms.SetValue(name, value);
        }
        public void SetUniform(string name, sbyte value)
        {
            Uniforms.SetValue(name, value);
        }
        public void SetUniform(string name, short value)
        {
            Uniforms.SetValue(name, value);
        }
        public void SetUniform(string name, ushort value)
        {
            Uniforms.SetValue(name, value);
        }
        public void SetUniform(string name, int value)
        {
            Uniforms.SetValue(name, value);
        }
        public void SetUniform(string name, uint value)
        {
            Uniforms.SetValue(name, value);
        }
        public void SetUniform(string name, double value)
        {
            Uniforms.SetValue(name, value);
        }
    }
}

