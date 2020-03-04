using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChamberLib
{
    public struct Overrides
    {
        public Overrides(
            IVertexMaterial vertexMaterial =null,
            IFragmentMaterial fragmentMaterial =null,
            float? alpha=null,
            ShaderUniforms uniforms=null)
        {
            this.VertexMaterial = vertexMaterial;
            this.FragmentMaterial = fragmentMaterial;
            this.Alpha = alpha;
            this.Uniforms = uniforms;
        }

        public static Overrides FromPrototype(
            Overrides prototype,
            IVertexMaterial vertexMaterial =null,
            IFragmentMaterial fragmentMaterial =null,
            float? alpha=null,
            ShaderUniforms uniforms=null)
        {
            return new Overrides(
                vertexMaterial: vertexMaterial ?? prototype.VertexMaterial,
                fragmentMaterial: fragmentMaterial ?? prototype.FragmentMaterial,
                alpha: alpha ?? prototype.Alpha,
                uniforms: uniforms ?? prototype.Uniforms);
        }

        public IVertexMaterial VertexMaterial;
        public IVertexMaterial GetVertexMaterial(IVertexMaterial defaultValue)
        {
            return this.VertexMaterial ?? defaultValue;
        }
        public IFragmentMaterial FragmentMaterial;
        public IFragmentMaterial GetFragmentMaterial(IFragmentMaterial defaultValue)
        {
            return this.FragmentMaterial ?? defaultValue;
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
