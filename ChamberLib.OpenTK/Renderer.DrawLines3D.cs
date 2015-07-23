using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using System.Linq;
using ChamberLib.Content;

using _OpenTK = global::OpenTK;

namespace ChamberLib.OpenTK
{
    public partial class Renderer
    {
        static bool _DrawLines3D_isReady = false;
        static ShaderProgram _DrawLines3D_shader;
        static int _DrawLines3D_worldViewProjLocation;
        static int _DrawLines3D_fragmentColorLocation;
        static RenderBundle _DrawLines3D_renderData;
        static MutableVertexBuffer _DrawLines3D_vertexBuffer;
        static MutableIndexBuffer _DrawLines3D_indexBuffer;

        static void _DrawLines3D_MakeReady()
        {
            /*
             * Shaders
             * 
             */

            var name = "$draw lines 3d";

            _DrawLines3D_shader_vert_stage = new ShaderStage(
                _DrawLines3D_shader_vert_source, ShaderType.Vertex, name);
            _DrawLines3D_shader_frag_stage = new ShaderStage(
                _DrawLines3D_shader_frag_source, ShaderType.Fragment, name);

            _DrawLines3D_shader =
                new ShaderProgram(
                    _DrawLines3D_shader_vert_stage,
                    _DrawLines3D_shader_frag_stage,
                    name);
            _DrawLines3D_shader.SetBindAttributes(new [] { "in_position" });

            _DrawLines3D_shader.MakeReady();
            GLHelper.CheckError();
            _DrawLines3D_shader.Apply();
            GLHelper.CheckError();

            _DrawLines3D_worldViewProjLocation = GL.GetUniformLocation(_DrawLines3D_shader.ProgramID, "worldViewProj");
            GLHelper.CheckError();
            _DrawLines3D_fragmentColorLocation = GL.GetUniformLocation(_DrawLines3D_shader.ProgramID, "fragment_color");
            GLHelper.CheckError();

            _DrawLines3D_shader.UnApply();
            GLHelper.CheckError();


            /*
             * Render buffers
             *
             */

            _OpenTK.Vector3[] vertexes = {
                new _OpenTK.Vector3(0, 1, 0),
                new _OpenTK.Vector3(1, 0, 0),
                new _OpenTK.Vector3(0, 0, 1),
                new _OpenTK.Vector3(-1, 0, 0),
                new _OpenTK.Vector3(0, 0, -1),
                new _OpenTK.Vector3(0, -1, 0),
            };
            short[] indexes = { 
                0, 1, 2, 3, 4, 5,
            };

            _DrawLines3D_vertexBuffer = new MutableVertexBuffer();
            _DrawLines3D_vertexBuffer.SetVertexData(
                vertexes,
                _OpenTK.Vector3.SizeInBytes,
                VertexAttribPointerType.Float,
                3);
            _DrawLines3D_indexBuffer = new MutableIndexBuffer();
            _DrawLines3D_indexBuffer.SetIndexData(indexes);
            _DrawLines3D_renderData = new RenderBundle(_DrawLines3D_vertexBuffer, _DrawLines3D_indexBuffer);

            _DrawLines3D_isReady = true;
        }

        static void _DrawLines3D_SetVertices(_OpenTK.Vector3[] vertexes)
        {
            _DrawLines3D_vertexBuffer.SetVertexData(vertexes, _OpenTK.Vector3.SizeInBytes, VertexAttribPointerType.Float, 3);
            _DrawLines3D_indexBuffer.SetIndexData(Enumerable.Range(0, vertexes.Length).Select(i => (ushort)i).ToArray());
        }

        public void DrawLines(Vector3 color, Matrix world, Matrix view, Matrix projection, IEnumerable<Vector3> vs)
        {
            if (!_DrawLines3D_isReady)
            {
                _DrawLines3D_MakeReady();
            }

            Reset3D();

            var list = vs.ToList();

            _DrawLines3D_shader.Apply();
            GLHelper.CheckError();

            var worldViewProj = (world * view * projection).ToOpenTK();
            GL.UniformMatrix4(_DrawLines3D_worldViewProjLocation, false, ref worldViewProj);
            GLHelper.CheckError();
            GL.Uniform4(_DrawLines3D_fragmentColorLocation, color.ToVector4(1).ToOpenTK());
            GLHelper.CheckError();

            _DrawLines3D_SetVertices(list.Select(v => v.ToOpenTK()).ToArray());

            _DrawLines3D_renderData.Apply();
            _DrawLines3D_renderData.Draw(PrimitiveType.LineStrip, list.Count, 0);
            _DrawLines3D_renderData.UnApply();

            _DrawLines3D_shader.UnApply();
            GLHelper.CheckError();
        }

        public static ShaderStage _DrawLines3D_shader_vert_stage { get; protected set; }
        public static readonly string _DrawLines3D_shader_vert_source = @"
#version 140

precision highp float;

uniform vec3 vertex_positions[8];
uniform mat4 worldViewProj;

in vec3 in_position;

void main(void)
{
    vec3 position = in_position;

    vec4 transformed = worldViewProj * vec4(position, 1);

    gl_Position = transformed;
}";

        public static ShaderStage _DrawLines3D_shader_frag_stage { get; protected set; }
        public static readonly string _DrawLines3D_shader_frag_source = @"
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

