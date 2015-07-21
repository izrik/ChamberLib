using System;
using System.IO;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using System.Diagnostics;
using ChamberLib.Content;

namespace ChamberLib.OpenTK
{
    public class ShaderProgram : IShaderProgram
    {
        public ShaderProgram(ShaderContent shader, string[] bindattrs)
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

            ApplyUniformValues();
        }

        public void UnApply()
        {
            GL.UseProgram(0);
            GLHelper.CheckError();
        }

        public bool IsApplied
        {
            get
            {
                if (ProgramID == 0) return false;
                return (GL.GetInteger(GetPName.CurrentProgram) == ProgramID);
            }
        }

        public class ShaderStage : IShaderStage
        {
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
                ShaderID = GL.CreateShader(ShaderType);
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

        public ShaderStage VertexShader;
        public ShaderStage FragmentShader;

        public void MakeReady()
        {
            if (ProgramID != 0)
            {
                return;
            }

            int prog;
            int result;

            if (!string.IsNullOrEmpty(Name))
            {
                Debug.WriteLine(string.Format("Readying shader \"{0}\"", Name));
            }

            GLHelper.CheckError();
            prog = GL.CreateProgram();
            GLHelper.CheckError();


            VertexShader = new ShaderStage(VertexShaderSource,
                ShaderType.VertexShader);

            VertexShader.MakeReady();

            GL.AttachShader(prog, VertexShader.ShaderID);
            GLHelper.CheckError();

            FragmentShader = new ShaderStage(FragmentShaderSource,
                ShaderType.FragmentShader);

            FragmentShader.MakeReady();

            GL.AttachShader(prog, FragmentShader.ShaderID);
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

            GL.DeleteShader(VertexShader.ShaderID);
            GLHelper.CheckError();
            GL.DeleteShader(FragmentShader.ShaderID);
            GLHelper.CheckError();

            ProgramID = prog;
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
            SetUniform(name, value, UniformType.Single);
        }
        public void SetUniform(string name, Vector2 value)
        {
            SetUniform(name, value, UniformType.Vector2);
        }
        public void SetUniform(string name, Vector3 value)
        {
            SetUniform(name, value, UniformType.Vector3);
        }
        public void SetUniform(string name, Vector4 value)
        {
            SetUniform(name, value, UniformType.Vector4);
        }
        public void SetUniform(string name, Matrix value)
        {
            SetUniform(name, value, UniformType.Matrix);
        }
        public void SetUniform(string name, bool value)
        {
            SetUniform(name, value, UniformType.Bool);
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
            SetUniform(name, value, UniformType.Byte);
        }

        public void SetUniform(string name, sbyte value)
        {
            SetUniform(name, value, UniformType.SByte);
        }

        public void SetUniform(string name, short value)
        {
            SetUniform(name, value, UniformType.Short);
        }

        public void SetUniform(string name, ushort value)
        {
            SetUniform(name, value, UniformType.UShort);
        }

        public void SetUniform(string name, int value)
        {
            SetUniform(name, value, UniformType.Int);
        }

        public void SetUniform(string name, uint value)
        {
            SetUniform(name, value, UniformType.UInt);
        }

        public void SetUniform(string name, double value)
        {
            SetUniform(name, value, UniformType.Double);
        }

        public bool GetUniformBool(string name)
        {
            Apply();
            var location = GetUniformLocation(name);
            int value;
            GL.GetUniform(ProgramID, location, out value);
            return (value != 0);
        }

        public byte GetUniformByte(string name)
        {
            Apply();
            var location = GetUniformLocation(name);
            uint value;
            GL.GetUniform((uint)ProgramID, location, out value);
            return (byte)value;
        }

        public sbyte GetUniformSByte(string name)
        {
            Apply();
            var location = GetUniformLocation(name);
            int value;
            GL.GetUniform(ProgramID, location, out value);
            return (sbyte)value;
        }

        public short GetUniformShort(string name)
        {
            Apply();
            var location = GetUniformLocation(name);
            int value;
            GL.GetUniform(ProgramID, location, out value);
            return (short)value;
        }

        public ushort GetUniformUShort(string name)
        {
            Apply();
            var location = GetUniformLocation(name);
            uint value;
            GL.GetUniform((uint)ProgramID, location, out value);
            return (ushort)value;
        }

        public int GetUniformInt(string name)
        {
            Apply();
            var location = GetUniformLocation(name);
            int value;
            GL.GetUniform(ProgramID, location, out value);
            return value;
        }

        public uint GetUniformUInt(string name)
        {
            Apply();
            var location = GetUniformLocation(name);
            uint value;
            GL.GetUniform((uint)ProgramID, location, out value);
            return value;
        }

        public float GetUniformSingle(string name)
        {
            Apply();
            var location = GetUniformLocation(name);
            float value;
            GL.GetUniform(ProgramID, location, out value);
            return value;
        }

        public double GetUniformDouble(string name)
        {
            Apply();
            var location = GetUniformLocation(name);
            double value;
            GL.GetUniform(ProgramID, location, out value);
            return value;
        }

        public Vector2 GetUniformVector2(string name)
        {
            Apply();
            var location = GetUniformLocation(name);
            float [] values = new float[2];
            GL.GetUniform(ProgramID, location, values);
            return new Vector2(values[0], values[1]);
        }

        public Vector3 GetUniformVector3(string name)
        {
            Apply();
            var location = GetUniformLocation(name);
            float [] values = new float[3];
            GL.GetUniform(ProgramID, location, values);
            return new Vector3(values[0], values[1], values[2]);
        }

        public Vector4 GetUniformVector4(string name)
        {
            Apply();
            var location = GetUniformLocation(name);
            float [] values = new float[4];
            GL.GetUniform(ProgramID, location, values);
            return new Vector4(values[0], values[1], values[2], values[3]);
        }

        protected enum UniformType
        {
            Bool,
            Byte,
            SByte,
            Short,
            UShort,
            Int,
            UInt,
            Single,
            Double,
            Vector2,
            Vector3,
            Vector4,
            Matrix,
        }

        protected void SetUniform(string name, object value, UniformType type)
        {
            uniformValues[name] = value;
            uniformTypes[name] = type;

            if (IsApplied)
            {
                ApplyUniform(name);
            }
        }

        protected Dictionary<string, object> uniformValues = new Dictionary<string, object>();
        protected Dictionary<string, UniformType> uniformTypes = new System.Collections.Generic.Dictionary<string, UniformType>();

        protected void ApplyUniformValues()
        {
            foreach (var name in uniformValues.Keys)
            {
                ApplyUniform(name);
            }
        }

        public object GetUniformValue(string name)
        {
            return uniformValues[name];
        }

        protected void ApplyUniform(string name)
        {
            var value = uniformValues[name];
            var type = uniformTypes[name];
            var location = GetUniformLocation(name);

            switch (type)
            {
            case UniformType.Bool:
                GL.Uniform1(location, ((bool)value ? 1 : 0));
                break;
            case UniformType.Byte:
                GL.Uniform1(location, (byte)value);
                break;
            case UniformType.SByte:
                GL.Uniform1(location, (sbyte)value);
                break;
            case UniformType.Short:
                GL.Uniform1(location, (short)value);
                break;
            case UniformType.UShort:
                GL.Uniform1(location, (ushort)value);
                break;
            case UniformType.Int:
                GL.Uniform1(location, (int)value);
                break;
            case UniformType.UInt:
                GL.Uniform1(location, (uint)value);
                break;
            case UniformType.Single:
                GL.Uniform1(location, (float)value);
                break;
            case UniformType.Double:
                GL.Uniform1(location, (double)value);
                break;
            case UniformType.Vector2:
                GL.Uniform2(location, ((Vector2)value).ToOpenTK());
                break;
            case UniformType.Vector3:
                GL.Uniform3(location, ((Vector3)value).ToOpenTK());
                break;
            case UniformType.Vector4:
                GL.Uniform4(location, ((Vector4)value).ToOpenTK());
                break;
            case UniformType.Matrix:
                var value2 = ((Matrix)value).ToOpenTK();
                GL.UniformMatrix4(location, false, ref value2);
                break;
            default:
                throw new ArgumentOutOfRangeException(
                    "type",
                    "Unknown uniform type: " + type.ToString());
            }
            GLHelper.CheckError();
        }
    }
}

