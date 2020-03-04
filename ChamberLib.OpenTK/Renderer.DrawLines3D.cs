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
        static readonly VertexAttribute[] __DrawLines3D_vattr = new[]{
            new VertexAttribute(3, VertexAttribPointerType.Float)};
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

            _DrawLines3D_shader_vert_stage.SetBindAttributes(new [] {
                "in_position" });
            _DrawLines3D_shader =
                ShaderProgram.GetShaderProgram(
                    _DrawLines3D_shader_vert_stage,
                    _DrawLines3D_shader_frag_stage,
                    name);

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
                __DrawLines3D_vattr);
            _DrawLines3D_indexBuffer = new MutableIndexBuffer();
            _DrawLines3D_indexBuffer.SetIndexData(indexes);
            _DrawLines3D_renderData = new RenderBundle(_DrawLines3D_vertexBuffer, _DrawLines3D_indexBuffer);

            _DrawLines3D_isReady = true;
        }

        ushort[] __DrawLines3D_indexes;
        void _DrawLines3D_SetVertices(_OpenTK.Vector3[] vertexes)
        {
            if (__DrawLines3D_indexes == null)
            {
                __DrawLines3D_indexes = new ushort[4] { 0, 1, 2, 3 };
            }
            if (__DrawLines3D_indexes.Length < vertexes.Length)
            {
                var temp = new ushort[__DrawLines3D_indexes.Length * 2];
                int i;
                for (i = 0; i < temp.Length; i++)
                {
                    temp[i] = (ushort)i;
                }
                __DrawLines3D_indexes = temp;
            }

            _DrawLines3D_vertexBuffer.SetVertexData(vertexes, _OpenTK.Vector3.SizeInBytes, __DrawLines3D_vattr);
            _DrawLines3D_indexBuffer.SetIndexData(__DrawLines3D_indexes, vertexes.Length);
        }

        _OpenTK.Vector3[] __DrawLines_3d_opentk_points;
        public void DrawLines(Vector3 color, Matrix world, Matrix view, Matrix projection, Vector3[] vs)
        {
            DrawLines(color, world, view, projection, vs, vs.Length);
        }
        public void DrawLines(Vector3 color, Matrix world, Matrix view, Matrix projection, Vector3[] vs, int numPoints)
        {
            if (!_DrawLines3D_isReady)
            {
                _DrawLines3D_MakeReady();
            }

            Reset3D();

            if (__DrawLines_3d_opentk_points == null ||
                __DrawLines_3d_opentk_points.Length < numPoints)
            {
                __DrawLines_3d_opentk_points = new _OpenTK.Vector3[numPoints];
            }
            int i;
            for (i = 0; i < numPoints; i++)
            {
                __DrawLines_3d_opentk_points[i] = vs[i].ToOpenTK();
            }

            _DrawLines3D_shader.Apply();
            GLHelper.CheckError();

            var worldViewProj = (world * view * projection).ToOpenTK();
            GL.UniformMatrix4(_DrawLines3D_worldViewProjLocation, false, ref worldViewProj);
            GLHelper.CheckError();
            GL.Uniform4(_DrawLines3D_fragmentColorLocation, color.ToVector4(1).ToOpenTK());
            GLHelper.CheckError();

            _DrawLines3D_SetVertices(__DrawLines_3d_opentk_points);

            _DrawLines3D_renderData.Apply();
            _DrawLines3D_renderData.Draw(PrimitiveType.LineStrip, numPoints, 0);
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

