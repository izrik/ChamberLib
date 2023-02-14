
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
using ChamberLib.OpenTK.Materials;

namespace ChamberLib.OpenTK.Text
{
    public partial class FontAdapter
    {
        public static ShaderStage _DrawString_shader_vert_stage { get; protected set;}
        public static readonly string _DrawString_shader_vert_source = @"
#version 140

precision highp float;

uniform vec2 scale;
uniform vec2 character_size;
uniform vec2 offset;
uniform float screenWidth = 1;
uniform float screenHeight = 1;

in vec2 in_position;

void main()
{
    vec2 initial = vec2(in_position.x, in_position.y);
    vec2 flipped = vec2(initial.x, 1 - initial.y);
    vec2 scaled = vec2(flipped.x * character_size.x * scale.x, flipped.y * character_size.y * scale.y);
    vec2 positioned = scaled + offset;
    vec2 final = vec2((2 * positioned.x / screenWidth) - 1,
                      (-2 * positioned.y / screenHeight) + 1);

    gl_Position = vec4(final, 0.5f,  1);
}
";

        public static ShaderStage _DrawString_shader_frag_stage { get; protected set;}
        public static readonly string _DrawString_shader_frag_source = @"
#version 140

precision highp float;

uniform vec4 fragment_color = vec4(1, 1, 1, 1);

out vec4 out_frag_color;

void main()
{
    out_frag_color = fragment_color;
}
";
    }
}

