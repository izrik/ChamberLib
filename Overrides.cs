using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChamberLib
{
    public struct Overrides
    {
        public Overrides(
            LightingData? lighting=null,
            IMaterial material=null,
            float? alpha=null,
            IShaderProgram shaderProgram=null,
            IShaderStage vertexShader=null,
            IShaderStage fragmentShader=null,
            ShaderUniforms uniforms=null)
        {
            this.Lighting = lighting;
            this.Material = material;
            this.Alpha = alpha;
            this.ShaderProgram = shaderProgram;
            this.VertexShader = vertexShader;
            this.FragmentShader = fragmentShader;
            this.Uniforms = uniforms ?? new ShaderUniforms();
        }

        public static Overrides FromPrototype(
            Overrides prototype,
            LightingData? lighting = null,
            IMaterial material = null,
            float? alpha = null,
            IShaderProgram shaderProgram = null,
            IShaderStage vertexShader = null,
            IShaderStage fragmentShader = null,
            ShaderUniforms uniforms = null)
        {
            return new Overrides(
                lighting: lighting ?? prototype.Lighting,
                material: material ?? prototype.Material,
                alpha: alpha ?? prototype.Alpha,
                shaderProgram: shaderProgram ?? prototype.ShaderProgram,
                vertexShader: vertexShader ?? prototype.VertexShader,
                fragmentShader: fragmentShader ?? prototype.FragmentShader,
                uniforms: uniforms ?? prototype.Uniforms);
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
