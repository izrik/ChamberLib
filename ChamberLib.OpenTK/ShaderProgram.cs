using System;
using System.IO;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using System.Diagnostics;
using ChamberLib.Content;
using System.Collections.ObjectModel;

namespace ChamberLib.OpenTK
{
    public class ShaderProgram : IShaderProgram
    {
        protected ShaderProgram(ShaderStage vertexShader, ShaderStage fragmentShader,
            string name=null)
        {
            VertexShader = vertexShader;
            FragmentShader = fragmentShader;

            if (name != null)
            {
                _name = name;
            }
            else
            {
                _name = string.Format("{0},{1}", vertexShader.Name,
                    fragmentShader.Name);
            }
        }

        static Cache2P<ShaderStage, ShaderStage, string, ShaderProgram> cache =
            new Cache2P<ShaderStage, ShaderStage, string, ShaderProgram>(MakeShaderProgramImpl);
        public static ShaderProgram MakeShaderProgram(ShaderStage vertexShader,
            ShaderStage fragmentShader, string name=null)
        {
            if (vertexShader.ShaderType != ShaderType.Vertex)
                throw new ArgumentException("Wrong shader type", "vertexShader");
            if (fragmentShader.ShaderType != ShaderType.Fragment)
                throw new ArgumentException("Wrong shader type", "fragmentShader");

            return cache.Call(vertexShader, fragmentShader, name);
        }
        protected static ShaderProgram MakeShaderProgramImpl(ShaderStage vertexShader,
            ShaderStage fragmentShader, string name=null)
        {
            if (vertexShader.ShaderType != ShaderType.Vertex)
                throw new ArgumentException("Wrong shader type", "vertexShader");
            if (fragmentShader.ShaderType != ShaderType.Fragment)
                throw new ArgumentException("Wrong shader type", "fragmentShader");

            var shader = new ShaderProgram(vertexShader, fragmentShader, name);

            return shader;
        }

        public int ProgramID;

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

        readonly string _name;
        public string Name { get { return _name; } }

        public readonly ShaderStage VertexShader;
        public readonly ShaderStage FragmentShader;

        IShaderStage IShaderProgram.VertexShader { get { return VertexShader; } }
        IShaderStage IShaderProgram.FragmentShader { get { return FragmentShader; } }

        public void Apply(Overrides overrides=default(Overrides))
        {
            if (ProgramID <= 0)
            {
                MakeReady();
            }

            var vertexShader = overrides.GetVertexShader(VertexShader);
            var fragmentShader = overrides.GetFragmentShader(FragmentShader);

            if ((vertexShader != null && vertexShader != VertexShader) ||
                (fragmentShader != null && fragmentShader != FragmentShader))
            {
                var effectiveProgram = MakeShaderProgram(
                    (ShaderStage)vertexShader,
                    (ShaderStage)fragmentShader);

                effectiveProgram.ApplyBase(overrides.GetUniforms());
            }
            else
            {
                ApplyBase(overrides.GetUniforms());
            }
        }

        protected void ApplyBase(ShaderUniforms uniformsOverride)
        {
            GL.UseProgram(ProgramID);
            GLHelper.CheckError();

            ApplyUniformValues(uniformsOverride);
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

        public void MakeReady()
        {
            if (ProgramID != 0)
            {
                return;
            }

            int result;

            if (!string.IsNullOrEmpty(Name))
            {
                Debug.WriteLine(string.Format("Readying shader \"{0}\"", Name));
            }

            GLHelper.CheckError();
            ProgramID = GL.CreateProgram();
            GLHelper.CheckError();

            VertexShader.MakeReady();

            GL.AttachShader(ProgramID, VertexShader.ShaderID);
            GLHelper.CheckError();

            FragmentShader.MakeReady();

            GL.AttachShader(ProgramID, FragmentShader.ShaderID);
            GLHelper.CheckError();

            int i = 0;
            foreach (var attr in BindAttributes)
            {
                if (string.IsNullOrEmpty(attr)) continue;

                GL.BindAttribLocation(ProgramID, i, attr);
                i++;
            }

            GL.LinkProgram(ProgramID);
            GLHelper.CheckError();
            GL.GetProgram(ProgramID, GetProgramParameterName.LinkStatus, out result);
            GLHelper.CheckError();
            Debug.WriteLine("Program Link Status: {0}", result);
            Debug.WriteLine("Program info:");
            var programInfo = GL.GetProgramInfoLog(ProgramID);
            GLHelper.CheckError();
            Debug.WriteLine(programInfo);

            GL.ValidateProgram(ProgramID);
            GLHelper.CheckError();

            string programInfoLog;
            GL.GetProgramInfoLog(ProgramID, out programInfoLog );
            GLHelper.CheckError();
        }

        readonly Dictionary<string,int> uniformLocationCache = new Dictionary<string, int>();
        readonly ShaderUniforms uniforms = new ShaderUniforms();

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
            uniforms.SetValue(name, value);
            if (IsApplied) ApplyUniform(name);
        }
        public void SetUniform(string name, Vector2 value)
        {
            uniforms.SetValue(name, value);
            if (IsApplied) ApplyUniform(name);
        }
        public void SetUniform(string name, Vector3 value)
        {
            uniforms.SetValue(name, value);
            if (IsApplied) ApplyUniform(name);
        }
        public void SetUniform(string name, Vector4 value)
        {
            uniforms.SetValue(name, value);
            if (IsApplied) ApplyUniform(name);
        }
        public void SetUniform(string name, Matrix value)
        {
            uniforms.SetValue(name, value);
            if (IsApplied) ApplyUniform(name);
        }
        public void SetUniform(string name, bool value)
        {
            uniforms.SetValue(name, value);
            if (IsApplied) ApplyUniform(name);
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
            uniforms.SetValue(name, value);
            if (IsApplied) ApplyUniform(name);
        }

        public void SetUniform(string name, sbyte value)
        {
            uniforms.SetValue(name, value);
            if (IsApplied) ApplyUniform(name);
        }

        public void SetUniform(string name, short value)
        {
            uniforms.SetValue(name, value);
            if (IsApplied) ApplyUniform(name);
        }

        public void SetUniform(string name, ushort value)
        {
            uniforms.SetValue(name, value);
            if (IsApplied) ApplyUniform(name);
        }

        public void SetUniform(string name, int value)
        {
            uniforms.SetValue(name, value);
            if (IsApplied) ApplyUniform(name);
        }

        public void SetUniform(string name, uint value)
        {
            uniforms.SetValue(name, value);
            if (IsApplied) ApplyUniform(name);
        }

        public void SetUniform(string name, double value)
        {
            uniforms.SetValue(name, value);
            if (IsApplied) ApplyUniform(name);
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

        protected void ApplyUniformValues(ShaderUniforms uniformsOverride)
        {
            if (uniformsOverride != null)
            {
                foreach (var name in uniformsOverride.GetUniformNames())
                {
                    ApplyUniform(name, uniformsOverride);
                }
            }
            foreach (var name in uniforms.GetUniformNames())
            {
                ApplyUniform(name, uniformsOverride);
            }
        }

        public object GetUniformValue(string name)
        {
            return uniforms.GetValue(name);
        }

        protected void ApplyUniform(string name, ShaderUniforms uniformsOverride=null)
        {
            ShaderUniforms source = uniforms;
            if (uniformsOverride != null &&
                uniformsOverride.ContainsName(name))
            {
                source = uniformsOverride;
            }
            var value = source.GetValue(name);
            var type = source.GetType(name);
            var location = GetUniformLocation(name);

            switch (type)
            {
            case ShaderUniformType.Bool:
                GL.Uniform1(location, (source.GetValueBool(name) ? 1 : 0));
                break;
            case ShaderUniformType.Byte:
                GL.Uniform1(location, source.GetValueByte(name));
                break;
            case ShaderUniformType.SByte:
                GL.Uniform1(location, source.GetValueSByte(name));
                break;
            case ShaderUniformType.Short:
                GL.Uniform1(location, source.GetValueShort(name));
                break;
            case ShaderUniformType.UShort:
                GL.Uniform1(location, source.GetValueUShort(name));
                break;
            case ShaderUniformType.Int:
                GL.Uniform1(location, source.GetValueInt(name));
                break;
            case ShaderUniformType.UInt:
                GL.Uniform1(location, source.GetValueUInt(name));
                break;
            case ShaderUniformType.Single:
                GL.Uniform1(location, source.GetValueSingle(name));
                break;
            case ShaderUniformType.Double:
                GL.Uniform1(location, source.GetValueDouble(name));
                break;
            case ShaderUniformType.Vector2:
                GL.Uniform2(location, (source.GetValueVector2(name)).ToOpenTK());
                break;
            case ShaderUniformType.Vector3:
                GL.Uniform3(location, (source.GetValueVector3(name)).ToOpenTK());
                break;
            case ShaderUniformType.Vector4:
                GL.Uniform4(location, (source.GetValueVector4(name)).ToOpenTK());
                break;
            case ShaderUniformType.Matrix:
                var value2 = (source.GetValueMatrix(name)).ToOpenTK();
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

