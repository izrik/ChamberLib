using System;
using OpenTK.Graphics.OpenGL;
using System.Diagnostics;
using ChamberLib.Content;
using _OpenTK = global::OpenTK;

namespace ChamberLib.OpenTK
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
                Debug.WriteLine(GL.GetShaderInfoLog(ShaderID));
                GLHelper.CheckError();

                IsCompiled = (result == 1);
            }
        }
    }
}

