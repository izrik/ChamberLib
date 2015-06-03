using System;
using System.IO;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using System.Diagnostics;
using ChamberLib.Content;

namespace ChamberLib.OpenTK
{
    public class ShaderAdapter : IShader
    {
        public ShaderAdapter(ShaderContent shader, string[] bindattrs)
        {
            VertexShaderSource = shader.VertexShaderSource;
            FragmentShaderSource = shader.FragmentShaderSource;
            if (bindattrs != null)
            {
                BindAttributes.AddRange(bindattrs);
            }

            name = shader.Name ?? "";
        }

        public readonly string VertexShaderSource;
        public readonly string FragmentShaderSource;
        public int ProgramID;
        public int VertexShaderID;
        public int FragmentShaderID;
        public List<string> BindAttributes = new List<string>();

        readonly string name;
        public string Name { get { return name; } }

        public void Apply()
        {
            if (ProgramID <= 0)
            {
                MakeReady();
            }

            GL.UseProgram(ProgramID);
            GLHelper.CheckError();

            GL.ValidateProgram(ProgramID);
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
            var programInfo = GL.GetProgramInfoLog(prog);
            GLHelper.CheckError();
            Debug.WriteLine(programInfo);

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

        Dictionary<string,int> uniformLocationCache = new Dictionary<string, int>();
        int GetUniformLocation(string name)
        {
            if (uniformLocationCache.ContainsKey(name))
            {
                return uniformLocationCache[name];
            }

            var location = GL.GetUniformLocation(ProgramID, name);
            uniformLocationCache[name] = location;
            return location;
        }
        public void SetUniform(string name, float value)
        {
            Apply();
            var location = GetUniformLocation(name);
            GL.Uniform1(location, value);
            GLHelper.CheckError();
        }
        public void SetUniform(string name, Vector2 value)
        {
            Apply();
            var location = GetUniformLocation(name);
            GL.Uniform2(location, value.ToOpenTK());
            GLHelper.CheckError();
        }
        public void SetUniform(string name, Vector3 value)
        {
            Apply();
            var location = GetUniformLocation(name);
            GL.Uniform3(location, value.ToOpenTK());
            GLHelper.CheckError();
        }
        public void SetUniform(string name, Vector4 value)
        {
            Apply();
            var location = GetUniformLocation(name);
            GL.Uniform4(location, value.ToOpenTK());
            GLHelper.CheckError();
        }
        public void SetUniform(string name, Matrix value)
        {
            Apply();
            var location = GetUniformLocation(name);
            var value2 = value.ToOpenTK();
            GL.UniformMatrix4(location, false, ref value2);
            GLHelper.CheckError();
        }
        public void SetUniform(string name, bool value)
        {
            Apply();
            var location = GetUniformLocation(name);
            GL.Uniform1(location, (value ? 1 : 0));
            GLHelper.CheckError();
        }

        public Matrix GetUniformMatrix(string name)
        {
            Apply();
            var location = GetUniformLocation(name);
            var values = new float[16];
            GL.GetUniform(ProgramID, location, values);
            return new Matrix(
                values[0], values[1], values[2], values[3], 
                values[4], values[5], values[6], values[7], 
                values[8], values[9], values[10], values[11], 
                values[12], values[13], values[14], values[15]);
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
        public void SetUniform(string name, long value)
        {
            throw new NotImplementedException();
        }
        public void SetUniform(string name, ulong value)
        {
            throw new NotImplementedException();
        }
        public void SetUniform(string name, double value)
        {
            throw new NotImplementedException();
        }
        public bool GetUniformBool(string name)
        {
            throw new NotImplementedException();
        }
        public byte GetUniformByte(string name)
        {
            throw new NotImplementedException();
        }
        public sbyte GetUniformSByte(string name)
        {
            throw new NotImplementedException();
        }
        public short GetUniformShort(string name)
        {
            throw new NotImplementedException();
        }
        public ushort GetUniformUShort(string name)
        {
            throw new NotImplementedException();
        }
        public int GetUniformInt(string name)
        {
            throw new NotImplementedException();
        }
        public uint GetUniformUInt(string name)
        {
            throw new NotImplementedException();
        }
        public long GetUniformLong(string name)
        {
            throw new NotImplementedException();
        }
        public ulong GetUniformULong(string name)
        {
            throw new NotImplementedException();
        }
        public float GetUniformSingle(string name)
        {
            throw new NotImplementedException();
        }
        public double GetUniformDouble(string name)
        {
            throw new NotImplementedException();
        }
        public Vector2 GetUniformVector2(string name)
        {
            throw new NotImplementedException();
        }
        public Vector3 GetUniformVector3(string name)
        {
            throw new NotImplementedException();
        }
        public Vector4 GetUniformVector4(string name)
        {
            throw new NotImplementedException();
        }
    }
}

