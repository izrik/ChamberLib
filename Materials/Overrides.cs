
//
// ChamberLib, a cross-platform game engine
// Copyright (C) 2021 izrik and Metaphysics Industries, Inc.
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301
// USA
//

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
