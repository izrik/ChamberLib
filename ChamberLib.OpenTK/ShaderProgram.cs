using System;
using System.IO;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using System.Diagnostics;
using ChamberLib.Content;
using System.Collections.ObjectModel;

namespace ChamberLib.OpenTK
{
    public class ShaderProgram
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

        static Dictionary<STuple<ShaderStage, ShaderStage>, ShaderProgram> cache = new Dictionary<STuple<ShaderStage, ShaderStage>, ShaderProgram>();
        public static ShaderProgram GetShaderProgram(ShaderStage vertexShader,
            ShaderStage fragmentShader, string name=null)
        {
            if (vertexShader.ShaderType != ShaderType.Vertex)
                throw new ArgumentException("Wrong shader type", "vertexShader");
            if (fragmentShader.ShaderType != ShaderType.Fragment)
                throw new ArgumentException("Wrong shader type", "fragmentShader");

            var stuple = new STuple<ShaderStage, ShaderStage>(vertexShader, fragmentShader);
            if (!cache.ContainsKey(stuple))
            {
                var shader = new ShaderProgram(vertexShader, fragmentShader, name);
                cache[stuple] = shader;
            }

            return cache[stuple];
        }

        public int ProgramID;

        readonly string _name;
        public string Name { get { return _name; } }

        public readonly ShaderStage VertexShader;
        public readonly ShaderStage FragmentShader;

        public void Apply(Overrides overrides=default(Overrides))
        {
            if (ProgramID <= 0)
            {
                MakeReady();
            }

            ApplyBase(overrides.GetUniforms(uniforms));
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
            foreach (var attr in VertexShader.BindAttributes)
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

        protected void ApplyUniformValues(ShaderUniforms uniformsOverride)
        {
            foreach (var entry in VertexShader.Uniforms.GetEntries())
            {
                ApplyUniform(entry.Name, VertexShader.Uniforms);
            }
            foreach (var entry in FragmentShader.Uniforms.GetEntries())
            {
                ApplyUniform(entry.Name, FragmentShader.Uniforms);
            }
            if (uniformsOverride != null)
            {
                foreach (var entry in uniformsOverride.GetEntries())
                {
                    ApplyUniform(entry.Name, uniformsOverride);
                }
            }
            foreach (var entry in uniforms.GetEntries())
            {
                ApplyUniform(entry.Name, uniformsOverride);
            }
        }

        protected void ApplyUniform(string name, ShaderUniforms uniformsOverride=null)
        {
            ShaderUniforms source = uniforms;
            if (uniformsOverride != null &&
                uniformsOverride.ContainsName(name))
            {
                source = uniformsOverride;
            }
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

