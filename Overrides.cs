using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChamberLib
{
    public struct Overrides
    {
        public Overrides(
            IMaterial material=null,
            float? alpha=null,
            IShaderProgram shaderProgram=null,
            IShaderStage vertexShader=null,
            IShaderStage fragmentShader=null,
            ShaderUniforms uniforms=null)
        {
            this.Material = material;
            this.Alpha = alpha;
            this.ShaderProgram = shaderProgram;
            this.VertexShader = vertexShader;
            this.FragmentShader = fragmentShader;
            this.Uniforms = uniforms;
        }

        public static Overrides FromPrototype(
            Overrides prototype,
            IMaterial material = null,
            float? alpha = null,
            IShaderProgram shaderProgram = null,
            IShaderStage vertexShader = null,
            IShaderStage fragmentShader = null,
            ShaderUniforms uniforms = null)
        {
            return new Overrides(
                material: material ?? prototype.Material,
                alpha: alpha ?? prototype.Alpha,
                shaderProgram: shaderProgram ?? prototype.ShaderProgram,
                vertexShader: vertexShader ?? prototype.VertexShader,
                fragmentShader: fragmentShader ?? prototype.FragmentShader,
                uniforms: uniforms ?? prototype.Uniforms);
        }

        public IMaterial Material;
        public IMaterial GetMaterial(IMaterial defaultValue)
        {
            return this.Material ?? defaultValue;
        }
        public float? Alpha;
        public float GetAlpha(float defaultValue)
        {
            if (this.Alpha.HasValue) return this.Alpha.Value;
            if (this.Material == null) return defaultValue;
            return this.Material.Alpha;
        }

        public IShaderProgram ShaderProgram;
        public IShaderProgram GetShaderProgram(IShaderProgram defaultValue)
        {
            return this.ShaderProgram ?? defaultValue;
        }

        public IShaderStage VertexShader;
        public IShaderStage GetVertexShader(IShaderStage defaultValue)
        {
            return this.VertexShader ?? defaultValue;
        }

        public IShaderStage FragmentShader;
        public IShaderStage GetFragmentShader(IShaderStage defaultValue)
        {
            return this.FragmentShader ?? defaultValue;
        }

        public ShaderUniforms Uniforms;
        public ShaderUniforms GetUniforms(ShaderUniforms defaultValue = null)
        {
            return this.Uniforms ?? defaultValue;
        }
    }
}
