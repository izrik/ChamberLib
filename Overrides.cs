using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChamberLib
{
    public class Overrides
    {
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
