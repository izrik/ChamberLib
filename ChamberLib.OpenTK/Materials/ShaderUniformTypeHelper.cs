
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
using OpenTK.Graphics.OpenGL;

namespace ChamberLib.OpenTK.Materials
{
    public static class ShaderUniformTypeHelper
    {
        public static ShaderUniformType ToChamber(this ActiveUniformType from)
        {
            switch (from)
            {
                case ActiveUniformType.Int:
                    return ShaderUniformType.Int;
                case ActiveUniformType.UnsignedInt:
                    return ShaderUniformType.UInt;
                case ActiveUniformType.Float:
                    return ShaderUniformType.Single;
                case ActiveUniformType.Double:
                    return ShaderUniformType.Double;
                case ActiveUniformType.FloatVec2:
                    return ShaderUniformType.Vector2;
                case ActiveUniformType.FloatVec3:
                    return ShaderUniformType.Vector3;
                case ActiveUniformType.FloatVec4:
                    return ShaderUniformType.Vector4;
                case ActiveUniformType.Bool:
                    return ShaderUniformType.Bool;
                case ActiveUniformType.FloatMat4:
                    return ShaderUniformType.Matrix;

                case ActiveUniformType.IntVec2:
                case ActiveUniformType.IntVec3:
                case ActiveUniformType.IntVec4:
                case ActiveUniformType.BoolVec2:
                case ActiveUniformType.BoolVec3:
                case ActiveUniformType.BoolVec4:
                case ActiveUniformType.FloatMat2:
                case ActiveUniformType.FloatMat3:
                case ActiveUniformType.Sampler1D:
                case ActiveUniformType.Sampler2D:
                case ActiveUniformType.Sampler3D:
                case ActiveUniformType.SamplerCube:
                case ActiveUniformType.Sampler1DShadow:
                case ActiveUniformType.Sampler2DShadow:
                case ActiveUniformType.Sampler2DRect:
                case ActiveUniformType.Sampler2DRectShadow:
                case ActiveUniformType.FloatMat2x3:
                case ActiveUniformType.FloatMat2x4:
                case ActiveUniformType.FloatMat3x2:
                case ActiveUniformType.FloatMat3x4:
                case ActiveUniformType.FloatMat4x2:
                case ActiveUniformType.FloatMat4x3:
                case ActiveUniformType.Sampler1DArray:
                case ActiveUniformType.Sampler2DArray:
                case ActiveUniformType.SamplerBuffer:
                case ActiveUniformType.Sampler1DArrayShadow:
                case ActiveUniformType.Sampler2DArrayShadow:
                case ActiveUniformType.SamplerCubeShadow:
                case ActiveUniformType.UnsignedIntVec2:
                case ActiveUniformType.UnsignedIntVec3:
                case ActiveUniformType.UnsignedIntVec4:
                case ActiveUniformType.IntSampler1D:
                case ActiveUniformType.IntSampler2D:
                case ActiveUniformType.IntSampler3D:
                case ActiveUniformType.IntSamplerCube:
                case ActiveUniformType.IntSampler2DRect:
                case ActiveUniformType.IntSampler1DArray:
                case ActiveUniformType.IntSampler2DArray:
                case ActiveUniformType.IntSamplerBuffer:
                case ActiveUniformType.UnsignedIntSampler1D:
                case ActiveUniformType.UnsignedIntSampler2D:
                case ActiveUniformType.UnsignedIntSampler3D:
                case ActiveUniformType.UnsignedIntSamplerCube:
                case ActiveUniformType.UnsignedIntSampler2DRect:
                case ActiveUniformType.UnsignedIntSampler1DArray:
                case ActiveUniformType.UnsignedIntSampler2DArray:
                case ActiveUniformType.UnsignedIntSamplerBuffer:
                case ActiveUniformType.DoubleVec2:
                case ActiveUniformType.DoubleVec3:
                case ActiveUniformType.DoubleVec4:
                case ActiveUniformType.SamplerCubeMapArray:
                case ActiveUniformType.SamplerCubeMapArrayShadow:
                case ActiveUniformType.IntSamplerCubeMapArray:
                case ActiveUniformType.UnsignedIntSamplerCubeMapArray:
                case ActiveUniformType.Image1D:
                case ActiveUniformType.Image2D:
                case ActiveUniformType.Image3D:
                case ActiveUniformType.Image2DRect:
                case ActiveUniformType.ImageCube:
                case ActiveUniformType.ImageBuffer:
                case ActiveUniformType.Image1DArray:
                case ActiveUniformType.Image2DArray:
                case ActiveUniformType.ImageCubeMapArray:
                case ActiveUniformType.Image2DMultisample:
                case ActiveUniformType.Image2DMultisampleArray:
                case ActiveUniformType.IntImage1D:
                case ActiveUniformType.IntImage2D:
                case ActiveUniformType.IntImage3D:
                case ActiveUniformType.IntImage2DRect:
                case ActiveUniformType.IntImageCube:
                case ActiveUniformType.IntImageBuffer:
                case ActiveUniformType.IntImage1DArray:
                case ActiveUniformType.IntImage2DArray:
                case ActiveUniformType.IntImageCubeMapArray:
                case ActiveUniformType.IntImage2DMultisample:
                case ActiveUniformType.IntImage2DMultisampleArray:
                case ActiveUniformType.UnsignedIntImage1D:
                case ActiveUniformType.UnsignedIntImage2D:
                case ActiveUniformType.UnsignedIntImage3D:
                case ActiveUniformType.UnsignedIntImage2DRect:
                case ActiveUniformType.UnsignedIntImageCube:
                case ActiveUniformType.UnsignedIntImageBuffer:
                case ActiveUniformType.UnsignedIntImage1DArray:
                case ActiveUniformType.UnsignedIntImage2DArray:
                case ActiveUniformType.UnsignedIntImageCubeMapArray:
                case ActiveUniformType.UnsignedIntImage2DMultisample:
                case ActiveUniformType.UnsignedIntImage2DMultisampleArray:
                case ActiveUniformType.Sampler2DMultisample:
                case ActiveUniformType.IntSampler2DMultisample:
                case ActiveUniformType.UnsignedIntSampler2DMultisample:
                case ActiveUniformType.Sampler2DMultisampleArray:
                case ActiveUniformType.IntSampler2DMultisampleArray:
                case ActiveUniformType.UnsignedIntSampler2DMultisampleArray:
                case ActiveUniformType.UnsignedIntAtomicCounter:
                default:
                    throw new NotSupportedException("No comparable ChamberLib type.");
            }
        }

        public static ActiveUniformType ToOpenTK(this ShaderUniformType from)
        {
            switch (from)
            {
                case ShaderUniformType.Bool:
                    return ActiveUniformType.Bool;
                case ShaderUniformType.Int:
                    return ActiveUniformType.Int;
                case ShaderUniformType.UInt:
                    return ActiveUniformType.UnsignedInt;
                case ShaderUniformType.Single:
                    return ActiveUniformType.Float;
                case ShaderUniformType.Double:
                    return ActiveUniformType.Double;
                case ShaderUniformType.Vector2:
                    return ActiveUniformType.FloatVec2;
                case ShaderUniformType.Vector3:
                    return ActiveUniformType.FloatVec3;
                case ShaderUniformType.Vector4:
                    return ActiveUniformType.FloatVec4;
                case ShaderUniformType.Matrix:
                    return ActiveUniformType.FloatMat4;

                case ShaderUniformType.Byte:
                case ShaderUniformType.SByte:
                case ShaderUniformType.Short:
                case ShaderUniformType.UShort:
                default:
                    throw new NotSupportedException("No comparable OpenTK type.");
            }
        }
    }
}
