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

            GL.UseProgram(ProgramID);
            GLHelper.CheckError();

            foreach (var entry in VertexShader.Uniforms.GetEntries())
            {
                ApplyUniform(entry.Name, VertexShader.Uniforms);
            }
            foreach (var entry in FragmentShader.Uniforms.GetEntries())
            {
                ApplyUniform(entry.Name, FragmentShader.Uniforms);
            }
            var uniformOverrides = overrides.GetUniforms(null);
            if (uniformOverrides != null)
            {
                foreach (var entry in uniformOverrides.GetEntries())
                {
                    ApplyUniform(entry.Name, uniformOverrides);
                }
            }
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

            int numUniforms;
            GL.GetProgram(ProgramID, GetProgramParameterName.ActiveUniforms, out numUniforms);
            GLHelper.CheckError();
            for (i = 0; i < numUniforms; i++)
            {
                int size;
                ActiveUniformType type;
                var name = GL.GetActiveUniform(ProgramID, i, out size, out type);
                GLHelper.CheckError();
                var location = GL.GetUniformLocation(ProgramID, name);
                GLHelper.CheckError();
                var au = new ActiveUniform(name, size, type, location);
                activeUniformIndexByToken[au.Token] = activeUniforms.Count;
                activeUniforms.Add(au);
            }
        }

        struct ActiveUniform
        {
            public ActiveUniform(string name, int size, ActiveUniformType gltype, int location)
            {
                Name = name;
                Token = ShaderUniforms.GetTokenForName(name);
                Size = size;
                GLType = gltype;
                if (gltype == ActiveUniformType.Sampler2D)
                    Type = ShaderUniformType.Int;
                else
                    Type = gltype.ToChamber();
                Location = location;
            }

            public readonly string Name;
            public readonly int Token;
            public readonly int Size;
            public readonly ActiveUniformType GLType;
            public readonly ShaderUniformType Type;
            public readonly int Location;
        }

        readonly Dictionary<string,int> uniformLocationCache = new Dictionary<string, int>();
        List<ActiveUniform> activeUniforms = new List<ActiveUniform>();
        Dictionary<int, int> activeUniformIndexByToken = new Dictionary<int, int>();

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

        protected void ApplyUniform(string name, ShaderUniforms uniforms)
        {
            var type = uniforms.GetType(name);
            var location = GetUniformLocation(name);

            switch (type)
            {
            case ShaderUniformType.Bool:
                GL.Uniform1(location, (uniforms.GetValueBool(name) ? 1 : 0));
                break;
            case ShaderUniformType.Byte:
                GL.Uniform1(location, uniforms.GetValueByte(name));
                break;
            case ShaderUniformType.SByte:
                GL.Uniform1(location, uniforms.GetValueSByte(name));
                break;
            case ShaderUniformType.Short:
                GL.Uniform1(location, uniforms.GetValueShort(name));
                break;
            case ShaderUniformType.UShort:
                GL.Uniform1(location, uniforms.GetValueUShort(name));
                break;
            case ShaderUniformType.Int:
                GL.Uniform1(location, uniforms.GetValueInt(name));
                break;
            case ShaderUniformType.UInt:
                GL.Uniform1(location, uniforms.GetValueUInt(name));
                break;
            case ShaderUniformType.Single:
                GL.Uniform1(location, uniforms.GetValueSingle(name));
                break;
            case ShaderUniformType.Double:
                GL.Uniform1(location, uniforms.GetValueDouble(name));
                break;
            case ShaderUniformType.Vector2:
                GL.Uniform2(location, (uniforms.GetValueVector2(name)).ToOpenTK());
                break;
            case ShaderUniformType.Vector3:
                GL.Uniform3(location, (uniforms.GetValueVector3(name)).ToOpenTK());
                break;
            case ShaderUniformType.Vector4:
                GL.Uniform4(location, (uniforms.GetValueVector4(name)).ToOpenTK());
                break;
            case ShaderUniformType.Matrix:
                var value2 = (uniforms.GetValueMatrix(name)).ToOpenTK();
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

