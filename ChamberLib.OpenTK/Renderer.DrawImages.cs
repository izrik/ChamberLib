using System;
using OpenTK.Graphics.OpenGL;
using ChamberLib.Content;

using _OpenTK = global::OpenTK;

namespace ChamberLib.OpenTK
{
    public partial class Renderer
    {
        static bool _DrawImages_isReady = false;
        static ShaderProgram _DrawImages_shader;
        static int _DrawImages_vao = -1;

        static int _DrawImages_elementCount;

        static int _DrawImages_spritePositionLocation;
        static int _DrawImages_spriteSizeLocation;
        static int _DrawImages_viewportSizeLocation;
        static int _DrawImages_fragmentColorLocation;

        static ITexture2D _DrawImages_lastTexture;

        static void _DrawImages_MakeReady()
        {
            /*
             * Shaders
             * 
             */

            var name = "$draw images";

            _DrawImages_shader_vert_stage = new ShaderStage(
                _DrawImages_shader_vert_source, ShaderType.Vertex, name);
            _DrawImages_shader_frag_stage = new ShaderStage(
                _DrawImages_shader_frag_source, ShaderType.Fragment, name);

            _DrawImages_shader =
                new ShaderProgram(
                    _DrawImages_shader_vert_stage,
                    _DrawImages_shader_frag_stage,
                    name);
            _DrawImages_shader.SetBindAttributes(new[] { "in_position" });

            _DrawImages_shader.MakeReady();
            GLHelper.CheckError();
            _DrawImages_shader.Apply();
            GLHelper.CheckError();

            _DrawImages_spritePositionLocation = GL.GetUniformLocation(_DrawImages_shader.ProgramID, "sprite_position");
            GLHelper.CheckError();
            _DrawImages_spriteSizeLocation = GL.GetUniformLocation(_DrawImages_shader.ProgramID, "sprite_size");
            GLHelper.CheckError();
            _DrawImages_viewportSizeLocation = GL.GetUniformLocation(_DrawImages_shader.ProgramID, "viewport_size");
            GLHelper.CheckError();
            _DrawImages_fragmentColorLocation = GL.GetUniformLocation(_DrawImages_shader.ProgramID, "fragment_color");
            GLHelper.CheckError();

            _DrawImages_shader.UnApply();
            GLHelper.CheckError();


            /*
             * VBOs
             *
             */

            _OpenTK.Vector3[] vertexes = {
                new _OpenTK.Vector3(1,1,1),
                new _OpenTK.Vector3(1,0,1),
                new _OpenTK.Vector3(0,1,1),
                new _OpenTK.Vector3(0,0,1),
            };

            int vb = GL.GenBuffer();
            GLHelper.CheckError();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vb);
            GLHelper.CheckError();
            GL.BufferData<_OpenTK.Vector3>(BufferTarget.ArrayBuffer,
                new IntPtr(vertexes.Length * _OpenTK.Vector3.SizeInBytes),
                vertexes, BufferUsageHint.StaticDraw);
            GLHelper.CheckError();

            short[] indexes = { 
                0, 1, 2,
                2, 1, 3,
            };

            _DrawImages_elementCount = indexes.Length;

            int ib = GL.GenBuffer();
            GLHelper.CheckError();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ib);
            GLHelper.CheckError();
            GL.BufferData(BufferTarget.ElementArrayBuffer,
                new IntPtr(sizeof(ushort) * indexes.Length),
                indexes, BufferUsageHint.StaticDraw);
            GLHelper.CheckError();

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GLHelper.CheckError();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GLHelper.CheckError();

            /* 
             * VAOs
             *
             */

            // GL3 allows us to store the vertex layout in a "vertex array object" (VAO).
            // This means we do not have to re-issue VertexAttribPointer calls
            // every time we try to use a different vertex layout - these calls are
            // stored in the VAO so we simply need to bind the correct VAO.

            _DrawImages_vao = GL.GenVertexArray();
            GLHelper.CheckError();
            GL.BindVertexArray(_DrawImages_vao);
            GLHelper.CheckError();

            GL.EnableVertexAttribArray(0);
            GLHelper.CheckError();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vb);
            GLHelper.CheckError();
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, true, _OpenTK.Vector3.SizeInBytes, 0);
            GLHelper.CheckError();

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ib);
            GLHelper.CheckError();

            GL.BindVertexArray(0);
            GLHelper.CheckError();

            _DrawImages_isReady = true;
        }

        public void DrawImage(ITexture2D texture, RectangleI destinationRectangle, Color color)
        {
            DrawImages(new DrawImagesEntry(texture, destinationRectangle, color));
        }
        public void DrawImages(params DrawImagesEntry[] entries)
        {
            if (!_DrawImages_isReady)
            {
                _DrawImages_MakeReady();
            }

            Reset2D();

            _DrawImages_shader.Apply();
            GLHelper.CheckError();

            GL.Uniform2(_DrawImages_viewportSizeLocation, Viewport.Size.ToOpenTK());
            GLHelper.CheckError();

            GL.BindVertexArray(_DrawImages_vao);
            GLHelper.CheckError();

            foreach (var entry in entries)
            {
                _DrawImages_lastTexture = entry.Texture;

                GL.Uniform2(_DrawImages_spritePositionLocation, entry.DestinationRectangle.TopLeft.ToVector2().ToOpenTK());
                GLHelper.CheckError();
                GL.Uniform2(_DrawImages_spriteSizeLocation, entry.DestinationRectangle.Size.ToVector2().ToOpenTK());
                GLHelper.CheckError();
                GL.Uniform4(_DrawImages_fragmentColorLocation, entry.Color.ToVector4().ToOpenTK());
                GLHelper.CheckError();

                entry.Texture.Apply();

                GL.DrawElements(PrimitiveType.Triangles, _DrawImages_elementCount,
                    DrawElementsType.UnsignedShort, IntPtr.Zero);
                GLHelper.CheckError();

                entry.Texture.UnApply();
            }

            GL.BindVertexArray(0);
            GLHelper.CheckError();

            _DrawImages_shader.UnApply();
            GLHelper.CheckError();
        }

        public static ShaderStage _DrawImages_shader_vert_stage { get; protected set; }
        public static readonly string _DrawImages_shader_vert_source = @"
#version 140

precision highp float;

uniform vec2 sprite_position;
uniform vec2 sprite_size;
uniform vec2 viewport_size;

in vec3 in_position;
out vec2 texture_coords;

void main(void)
{
    vec2 initial = vec2(in_position.x, in_position.y);
    vec2 scaled = vec2(initial.x * sprite_size.x, initial.y * sprite_size.y);
    vec2 offset = scaled + sprite_position;
    vec2 placed = vec2((2 * offset.x / viewport_size.x) - 1,
                       (-2 * offset.y / viewport_size.y) + 1);

    texture_coords = in_position.xy;

    gl_Position = vec4(placed, in_position.z, 1);
}";

        public static ShaderStage _DrawImages_shader_frag_stage { get; protected set; }
        public static readonly string _DrawImages_shader_frag_source = @"
#version 140

precision highp float;

uniform sampler2D texture0;
uniform vec4 fragment_color;

in vec2 texture_coords;
out vec4 out_frag_color;

void main(void)
{
  vec4 diffuse = texture(texture0, texture_coords);
  vec4 modulated = diffuse * fragment_color;
  out_frag_color = modulated;
}";


    }
}

