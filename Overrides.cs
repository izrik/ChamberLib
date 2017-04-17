using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChamberLib
{
    public class Overrides
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

        public LightingData? Lighting;

        public IMaterial Material;
        public float? Alpha;

        public IShaderProgram ShaderProgram;
        public IShaderStage VertexShader;
        public IShaderStage FragmentShader;
        public ShaderUniforms Uniforms;
    }

    public static class OverridesHelper
    {
        public static LightingData? GetLighting(this Overrides overrides, LightingData? defaultValue)
        {
            if (overrides == null) return defaultValue;
            return overrides.Lighting ?? defaultValue;
        }

        public static IMaterial GetMaterial(this Overrides overrides, IMaterial defaultValue)
        {
            if (overrides == null) return defaultValue;
            return overrides.Material ?? defaultValue;
        }
        public static float GetAlpha(this Overrides overrides, float defaultValue)
        {
            if (overrides == null) return defaultValue;
            if (overrides.Alpha.HasValue) return overrides.Alpha.Value;
            if (overrides.Material == null) return defaultValue;
            return overrides.Material.Alpha;
        }

        public static IShaderProgram GetShaderProgram(this Overrides overrides, IShaderProgram defaultValue)
        {
            if (overrides == null) return defaultValue;
            return overrides.ShaderProgram ?? defaultValue;
        }
        public static IShaderStage GetVertexShader(this Overrides overrides, IShaderStage defaultValue)
        {
            if (overrides == null) return defaultValue;
            return overrides.VertexShader ?? defaultValue;
        }
        public static IShaderStage GetFragmentShader(this Overrides overrides, IShaderStage defaultValue)
        {
            if (overrides == null) return defaultValue;
            return overrides.FragmentShader ?? defaultValue;
        }
        public static ShaderUniforms GetUniforms(this Overrides overrides, ShaderUniforms defaultValue=null)
        {
            if (overrides == null) return defaultValue;
            return overrides.Uniforms ?? defaultValue;
        }
    }
}
