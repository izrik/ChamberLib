using System;
using System.IO;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using System.Diagnostics;

namespace ChamberLib
{
    public class ShaderAdapter : IShader
    {
        public ShaderAdapter(string vs, string fs, string[] bindAttributes=null)
        {
            if (string.IsNullOrEmpty(vs)) throw new ArgumentNullException("vs");
            if (string.IsNullOrEmpty(fs)) throw new ArgumentNullException("fs");

            VertexShaderSource = vs;
            FragmentShaderSource = fs;
            if (bindAttributes != null)
            {
                BindAttributes.AddRange(bindAttributes);
            }
        }

        public readonly string VertexShaderSource;
        public readonly string FragmentShaderSource;
        public int ProgramID;
        public int VertexShaderID;
        public int FragmentShaderID;
        public List<string> BindAttributes = new List<string>();

        public string Name;

        public void Apply()
        {
            if (ProgramID == 0)
            {
                MakeReady();
            }

            GL.UseProgram(ProgramID);
            GLHelper.CheckError();

            GL.ValidateProgram(ProgramID);
            GLHelper.CheckError();

            GL.ActiveTexture(TextureUnit.Texture0);
            GLHelper.CheckError();
            int texture_location = GL.GetUniformLocation(ProgramID, "tex0");
            GLHelper.CheckError();
            GL.Uniform1(texture_location, 0);
            GLHelper.CheckError();

        }

        public void UnApply()
        {
            GL.UseProgram(0);
            GLHelper.CheckError();
        }

        public void MakeReady()
        {
            if (ProgramID != 0)
            {
                return;
            }

            int prog;
            int vs = 0;
            int fs = 0;

            ErrorCode error;

            GLHelper.CheckError();
            prog = GL.CreateProgram();
            GLHelper.CheckError();

            GLHelper.CheckError();
            vs = GL.CreateShader(ShaderType.VertexShader);
            GLHelper.CheckError();
            GL.ShaderSource(vs, VertexShaderSource);
            GLHelper.CheckError();
            GL.CompileShader(vs);
            GLHelper.CheckError();
            GL.AttachShader(prog, vs);
            GLHelper.CheckError();

            if (!string.IsNullOrEmpty(Name))
            {
                Debug.WriteLine(string.Format("Readying shader \"{0}\"", Name));
            }

            int result;
            GL.GetShader(vs, ShaderParameter.CompileStatus, out result);
            Debug.WriteLine("Vertex shader compile status: {0}", result);
            GLHelper.CheckError();
            Debug.WriteLine("vertex shader info:");
            Debug.WriteLine(GL.GetShaderInfoLog(vs));
            GLHelper.CheckError();

            GLHelper.CheckError();
            fs = GL.CreateShader(ShaderType.FragmentShader);
            GLHelper.CheckError();
            GL.ShaderSource(fs, FragmentShaderSource);
            GLHelper.CheckError();
            GL.CompileShader(fs);
            GLHelper.CheckError();
            GL.AttachShader(prog, fs);
            GLHelper.CheckError();
            Debug.WriteLine("fragment shader info:");
            Debug.WriteLine(GL.GetShaderInfoLog(fs));
            GLHelper.CheckError();

            int i = 0;
            foreach (var attr in BindAttributes)
            {
                if (string.IsNullOrEmpty(attr)) continue;

                GL.BindAttribLocation(prog, i, attr);
                i++;
            }

            GL.LinkProgram(prog);
            GLHelper.CheckError();
            GL.GetProgram(prog, GetProgramParameterName.LinkStatus, out result);
            GLHelper.CheckError();
            Debug.WriteLine("Program Link Status: {0}", result);
            Debug.WriteLine("Program info:");
            Debug.WriteLine(GL.GetProgramInfoLog(prog));
            GLHelper.CheckError();

            string programInfoLog;
            GL.GetProgramInfoLog( prog, out programInfoLog );
            GLHelper.CheckError();

            GL.DeleteShader(vs);
            GLHelper.CheckError();
            GL.DeleteShader(fs);
            GLHelper.CheckError();

            ProgramID = prog;
            VertexShaderID = vs;
            FragmentShaderID = fs;
        }
    }
}

