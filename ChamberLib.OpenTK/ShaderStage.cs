using System;
using OpenTK.Graphics.OpenGL;
using System.Diagnostics;
using ChamberLib.Content;

namespace ChamberLib.OpenTK
{
    public class ShaderStage : IShaderStage
    {
        public ShaderStage(ShaderContent content)
            : this(content.Type == ShaderType.Vertex ?
                    content.VertexShaderSource :
                    content.FragmentShaderSource,
                content.Type)
        {
        }
        public ShaderStage(string source, ShaderType shaderType)
        {
            Source = source;
            ShaderType = shaderType;
        }

        public int ShaderID { get; protected set; }
        public string Source { get; protected set; }
        public ShaderType ShaderType { get; protected set; }

        public void MakeReady()
        {
            if (ShaderID != 0) return;

            GLHelper.CheckError();
            ShaderID = GL.CreateShader(ShaderType.ToOpenTK());
            GLHelper.CheckError();
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
        }
    }
}

