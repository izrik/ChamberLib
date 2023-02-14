
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
using OpenGL = global::OpenTK.Graphics.OpenGL;

namespace ChamberLib.OpenTK.Materials
{
    public static class ShaderTypeHelper
    {
        public static ChamberLib.ShaderType ToChamber(this OpenGL.ShaderType shtype)
        {
            switch (shtype)
            {
            case OpenGL.ShaderType.VertexShader: return ShaderType.Vertex;
            case OpenGL.ShaderType.FragmentShader: return ShaderType.Fragment;
            }

            throw new ArgumentOutOfRangeException("shtype",
                "Unknown shader type: " + shtype.ToString());
        }

        public static OpenGL.ShaderType ToOpenTK(this ChamberLib.ShaderType shtype)
        {
            switch (shtype)
            {
            case ShaderType.Vertex: return OpenGL.ShaderType.VertexShader;
            case ShaderType.Fragment: return OpenGL.ShaderType.FragmentShader;
            }

            throw new ArgumentOutOfRangeException("shtype",
                "Unknown shader type: " + shtype.ToString());
        }
    }
}

