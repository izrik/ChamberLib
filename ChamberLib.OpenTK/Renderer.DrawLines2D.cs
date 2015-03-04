using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using System.Linq;
using System.Diagnostics;
using ChamberLib.Content;

namespace ChamberLib
{
    public partial class Renderer
    {
        static bool _DrawLines2D_isReady = false;
        static ShaderAdapter _DrawLines2D_shader;
        static int _DrawLines2D_viewportSizeLocation;
        static int _DrawLines2D_fragmentColorLocation;
        static RenderBundle _DrawLines2D_renderData;
        static MutableVertexBuffer _DrawLines2D_vertexData;
        static MutableIndexBuffer _DrawLines2D_indexData;

        static void _DrawLines2D_MakeReady()
        {
            /*
             * Shaders
             * 
             */

            _DrawLines2D_shader = 
                new ShaderAdapter(
                    new ShaderContent(
                        _DrawLines2D_shader_vert,
                        _DrawLines2D_shader_frag,
                        new [] { "in_position" }));

            _DrawLines2D_shader.MakeReady();
            GLHelper.CheckError();
            _DrawLines2D_shader.Apply();
            GLHelper.CheckError();

            _DrawLines2D_viewportSizeLocation = GL.GetUniformLocation(_DrawLines2D_shader.ProgramID, "viewport_size");
            GLHelper.CheckError();
            _DrawLines2D_fragmentColorLocation = GL.GetUniformLocation(_DrawLines2D_shader.ProgramID, "fragment_color");
            GLHelper.CheckError();

            _DrawLines2D_shader.UnApply();
            GLHelper.CheckError();


            /*
             * Render buffers
             *
             */

            OpenTK.Vector3[] vertexes = {
                OpenTK.Vector3.Zero,
                OpenTK.Vector3.Zero,
                OpenTK.Vector3.Zero,
                OpenTK.Vector3.Zero,
                OpenTK.Vector3.Zero,
                OpenTK.Vector3.Zero,
                OpenTK.Vector3.Zero,
                OpenTK.Vector3.Zero,
            };
            short[] indexes = { 
                0, 1, 2, 3, 4, 5, 6, 7,
            };

            _DrawLines2D_vertexData = new MutableVertexBuffer();
            _DrawLines2D_vertexData.SetVertexData(
                vertexes,
                OpenTK.Vector3.SizeInBytes,
                VertexAttribPointerType.Float,
                3);
            _DrawLines2D_indexData = new MutableIndexBuffer();
            _DrawLines2D_indexData.SetIndexData(indexes);
            _DrawLines2D_renderData = new RenderBundle(_DrawLines2D_vertexData, _DrawLines2D_indexData);

            _DrawLines2D_isReady = true;
        }

        static void _DrawLines2D_SetVertices(OpenTK.Vector3[] vertexes)
        {
            _DrawLines2D_vertexData.SetVertexData(vertexes, OpenTK.Vector3.SizeInBytes, VertexAttribPointerType.Float, 3);
            _DrawLines2D_indexData.SetIndexData(Enumerable.Range(0, vertexes.Length).Select(i => (ushort)i).ToArray());
        }

        public void DrawLines(Color color, IEnumerable<Vector2> vs)
        {
            if (!_DrawLines2D_isReady)
            {
                _DrawLines2D_MakeReady();
            }

            Reset2D();

            var list = vs.ToList();

            _DrawLines2D_shader.Apply();
            GLHelper.CheckError();

            GL.Uniform2(_DrawLines2D_viewportSizeLocation, Viewport.Size.ToOpenTK());
            GLHelper.CheckError();
            GL.Uniform4(_DrawLines2D_fragmentColorLocation, color.ToVector4().ToOpenTK());
            GLHelper.CheckError();

            _DrawLines2D_SetVertices(list.Select(v => new OpenTK.Vector3(v.X, v.Y, 0.5f)).ToArray());

            _DrawLines2D_renderData.Apply();
            _DrawLines2D_renderData.Draw(PrimitiveType.LineStrip, list.Count, 0);
            _DrawLines2D_renderData.UnApply();

            _DrawLines2D_shader.UnApply();
            GLHelper.CheckError();
        }
        public void DrawLine(Color color, Vector2 v1, Vector2 v2)
        {
            DrawLines(color, new [] { v1, v2 });
        }

        public static readonly string _DrawLines2D_shader_vert = @"
#version 140

precision highp float;

uniform vec2 viewport_size;

in vec3 in_position;

void main(void)
{
    vec3 position = in_position;

    vec2 placed = vec2((2 * position.x / viewport_size.x) - 1,
                       (-2 * position.y / viewport_size.y) + 1);

    gl_Position = vec4(placed, position.z, 1);
}";

        public static readonly string _DrawLines2D_shader_frag = @"
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

