using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChamberLib
{
    public struct Overrides
    {
        public Overrides(
            IMaterial fragmentMaterial=null,
            IMaterial vertexMaterial=null,
            float? alpha=null,
            ShaderUniforms uniforms=null)
        {
            this.FragmentMaterial = fragmentMaterial;
            this.VertexMaterial = vertexMaterial;
            this.Alpha = alpha;
            this.Uniforms = uniforms;
        }

        public static Overrides FromPrototype(
            Overrides prototype,
            IMaterial fragmentMaterial = null,
            IMaterial vertexMaterial = null,
            float? alpha = null,
            ShaderUniforms uniforms = null)
        {
            return new Overrides(
                fragmentMaterial: fragmentMaterial ?? prototype.FragmentMaterial,
                vertexMaterial: vertexMaterial ?? prototype.VertexMaterial,
                alpha: alpha ?? prototype.Alpha,
                uniforms: uniforms ?? prototype.Uniforms);
        }

        public IMaterial FragmentMaterial;
        public IMaterial GetFragmentMaterial(IMaterial defaultValue)
        {
            return this.FragmentMaterial ?? defaultValue;
        }
        public IMaterial VertexMaterial;
        public IMaterial GetVertexMaterial(IMaterial defaultValue)
        {
            return this.VertexMaterial ?? defaultValue;
        }
        public float? Alpha;
        public float GetAlpha(float defaultValue)
        {
            if (this.Alpha.HasValue) return this.Alpha.Value;
            if (this.FragmentMaterial == null) return defaultValue;
            return this.FragmentMaterial.Alpha;
        }

        public ShaderUniforms Uniforms;
        public ShaderUniforms GetUniforms(ShaderUniforms defaultValue = null)
        {
            return this.Uniforms ?? defaultValue;
        }
    }
}
