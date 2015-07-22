using System;

namespace ChamberLib.OpenTK
{
    public partial class FontAdapter
    {
        public static ShaderStage _DrawString_shader_vert_stage { get; protected set;}
        public static readonly string _DrawString_shader_vert_source = @"
#version 140

precision highp float;

uniform float scale;
uniform vec2 character_size;
uniform vec2 offset;
uniform float screenWidth = 1;
uniform float screenHeight = 1;

in vec2 in_position;

void main()
{
    vec2 initial = vec2(in_position.x, in_position.y);
    vec2 flipped = vec2(initial.x, 1 - initial.y);
    vec2 scaled = vec2(flipped.x * character_size.x, flipped.y * character_size.y) * scale;
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

