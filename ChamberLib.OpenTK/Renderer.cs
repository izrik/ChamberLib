using System;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;

namespace ChamberLib
{
    public class Renderer : IRenderer
    {
        public Renderer(OpenTKSubsystem subsystem)
        {
            if (subsystem == null) throw new ArgumentNullException("subsystem");

            _subsystem = subsystem;

            _viewport = new Viewport(0, 0, _subsystem.Window.Width, _subsystem.Window.Height);
        }

        readonly OpenTKSubsystem _subsystem;

        #region IRenderer implementation
        public void DrawLine(float width, Color color, Vector2 v1, Vector2 v2)
        {
        }
        public void DrawLine(Color color, Vector2 v1, Vector2 v2)
        {
            var viewport = Viewport;
            float w = viewport.Width;
            float h = viewport.Height;
            var proj = Matrix.CreateOrthographicOffCenter(0, w, 0, h, 0, 1);
            SetMatrices(Matrix.Identity, Matrix.Identity, proj);

            GL.Begin(PrimitiveType.LineStrip);

            var color2 = color.ToVector3();
            GL.Color3(color2.X, color2.Y, color2.Z);

            GL.Vertex3(v1.X, h - v1.Y, 0.5f);
            GL.Vertex3(v2.X, h - v2.Y, 0.5f);

            GL.End();
        }
        public void DrawString(IFont font, string text, Vector2 position, Color color, float rotation = 0f, Vector2 origin = default(Vector2), float scale = 1f)
        {
        }
        public void DrawImage(ITexture2D texture, RectangleI destinationRectangle, Color color)
        {
        }
        public void DrawImages(params DrawImagesEntry[] entries)
        {
        }
        public void Clear(Color color)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }
        public void Reset3D()
        {
        }
        public void DrawLines(Vector3 color, Matrix world, Matrix view, Matrix projection, IEnumerable<Vector3> points)
        {
        }

        Viewport _viewport;
        public Viewport Viewport
        {
            get
            {
                return _viewport;
            }
            set
            {
                _viewport = value;

                GL.Viewport(
                    value.X,
                    value.Y,
                    value.Width,
                    value.Height
                );
                GL.DepthRange(value.MinDepth, value.MaxDepth);
            }
        }
        #endregion

        public void SetMatrices(Matrix? world, Matrix? view, Matrix? projection)
        {
            if (world.HasValue && view.HasValue)
            {
                var m = (world * view).Value.ToOpenTK();
                GL.MatrixMode(MatrixMode.Modelview);
                GL.LoadMatrix(ref m);
            }
            else if (world.HasValue)
            {
                var m = world.Value.ToOpenTK();
                GL.MatrixMode(MatrixMode.Modelview);
                GL.LoadMatrix(ref m);
            }
            else if (view.HasValue)
            {
                var m = view.Value.ToOpenTK();
                GL.MatrixMode(MatrixMode.Modelview);
                GL.LoadMatrix(ref m);
            }

            if (projection.HasValue)
            {
                var m = projection.Value.ToOpenTK();
                GL.MatrixMode(MatrixMode.Projection);
                GL.LoadMatrix(ref m);
            }
        }
    }
}

