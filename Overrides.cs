using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChamberLib
{
    public class Overrides
    {
        public Overrides(
            Overrides prototype=null,
            LightingData? lighting=null,
            IMaterial material=null,
            float? alpha=null,
            IShaderProgram shaderProgram=null,
            IShaderStage vertexShader=null,
            IShaderStage fragmentShader=null,
            ShaderUniforms uniforms=null)
        {
            this.Prototype = prototype;
            this.Lighting = lighting ?? prototype.GetLighting(null);
            this.Material = material ?? prototype.GetMaterial(null);
            this.Alpha = alpha ?? (prototype != null ? prototype.Alpha : null);
            this.ShaderProgram = shaderProgram ?? prototype.GetShaderProgram(null);
            this.VertexShader = vertexShader ?? prototype.GetVertexShader(null);
            this.FragmentShader = fragmentShader ?? prototype.GetFragmentShader(null);
            this.Uniforms = uniforms ?? prototype.GetUniforms(null) ?? new ShaderUniforms();
        }

        public readonly Overrides Prototype;

        public LightingData? Lighting;
        public LightingData? GetLighting_(LightingData? defaultValue)
        {
            return this.Lighting ?? defaultValue;
        }

        public IMaterial Material;
        public IMaterial GetMaterial_(IMaterial defaultValue)
        {
            return this.Material ?? defaultValue;
        }
        public float? Alpha;
        public float GetAlpha_(float defaultValue)
        {
            if (this.Alpha.HasValue) return this.Alpha.Value;
            if (this.Material == null) return defaultValue;
            return this.Material.Alpha;
        }

        public IShaderProgram ShaderProgram;
        public IShaderProgram GetShaderProgram_(IShaderProgram defaultValue)
        {
            return this.ShaderProgram ?? defaultValue;
        }

        public IShaderStage VertexShader;
        public IShaderStage GetVertexShader_(IShaderStage defaultValue)
        {
            return this.VertexShader ?? defaultValue;
        }

        public IShaderStage FragmentShader;
        public IShaderStage GetFragmentShader_(IShaderStage defaultValue)
        {
            return this.FragmentShader ?? defaultValue;
        }

        public ShaderUniforms Uniforms;
        public ShaderUniforms GetUniforms_(ShaderUniforms defaultValue = null)
        {
            return this.Uniforms ?? defaultValue;
        }
    }

    public static class OverridesHelper
    {
        public static LightingData? GetLighting(this Overrides overrides, LightingData? defaultValue)
        {
            if (overrides == null) return defaultValue;
            return overrides.GetLighting_(defaultValue);
        }

        public static IMaterial GetMaterial(this Overrides overrides, IMaterial defaultValue)
        {
            if (overrides == null) return defaultValue;
            return overrides.GetMaterial_(defaultValue);
        }
        public static float GetAlpha(this Overrides overrides, float defaultValue)
        {
            if (overrides == null) return defaultValue;
            return overrides.GetAlpha_(defaultValue);
        }

        public static IShaderProgram GetShaderProgram(this Overrides overrides, IShaderProgram defaultValue)
        {
            if (overrides == null) return defaultValue;
            return overrides.GetShaderProgram_(defaultValue);
        }
        public static IShaderStage GetVertexShader(this Overrides overrides, IShaderStage defaultValue)
        {
            if (overrides == null) return defaultValue;
            return overrides.GetVertexShader_(defaultValue);
        }
        public static IShaderStage GetFragmentShader(this Overrides overrides, IShaderStage defaultValue)
        {
            if (overrides == null) return defaultValue;
            return overrides.GetFragmentShader_(defaultValue);
        }
        public static ShaderUniforms GetUniforms(this Overrides overrides, ShaderUniforms defaultValue=null)
        {
            if (overrides == null) return defaultValue;
            return overrides.GetUniforms_(defaultValue);
        }
    }
}
