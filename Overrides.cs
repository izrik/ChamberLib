using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChamberLib
{
    public struct Overrides
    {
        public Overrides(
            Overrides prototype = default(Overrides),
            LightingData? lighting=null,
            IMaterial material=null,
            float? alpha=null,
            IShaderProgram shaderProgram=null,
            IShaderStage vertexShader=null,
            IShaderStage fragmentShader=null,
            ShaderUniforms uniforms=null)
        {
            this.Lighting = lighting ?? prototype.GetLighting(null);
            this.Material = material ?? prototype.GetMaterial(null);
            this.Alpha = alpha ?? prototype.Alpha;
            this.ShaderProgram = shaderProgram ?? prototype.GetShaderProgram(null);
            this.VertexShader = vertexShader ?? prototype.GetVertexShader(null);
            this.FragmentShader = fragmentShader ?? prototype.GetFragmentShader(null);
            this.Uniforms = uniforms ?? prototype.GetUniforms(null) ?? new ShaderUniforms();
        }

        public LightingData? Lighting;
        public LightingData? GetLighting(LightingData? defaultValue)
        {
            return this.Lighting ?? defaultValue;
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
