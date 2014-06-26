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
            DrawLine(color, v1, v2);
        }
        public void DrawLine(Color color, Vector2 v1, Vector2 v2)
        {
            Reset2D();

            float h = Viewport.Height;

            GL.Begin(PrimitiveType.LineStrip);

            var color2 = color.ToVector3();
            GL.Color3(color2.X, color2.Y, color2.Z);

            GL.Vertex3(v1.X, h - v1.Y, 0.5f);
            GL.Vertex3(v2.X, h - v2.Y, 0.5f);

            GL.End();
        }
        public void DrawLines(Color color, params Vector2[] vs)
        {
            DrawLines(color, (IEnumerable<Vector2>)vs);
        }
        public void DrawLines(Color color, IEnumerable<Vector2> vs)
        {
            Reset2D();

            float h = Viewport.Height;

            GL.Begin(PrimitiveType.LineStrip);

            var color2 = color.ToVector3();
            GL.Color3(color2.X, color2.Y, color2.Z);

            foreach (var v in vs)
            {
                GL.Vertex3(v.X, h - v.Y, 0.5f);
            }

            GL.End();
        }
        public void DrawString(IFont font, string text, Vector2 position, Color color, float rotation = 0f, Vector2 origin = default(Vector2), float scale = 1f)
        {
            position -= origin;
            var font2 = (FontAdapter)font;
            DrawLine(color, position, position + new Vector2(text.Length * font2.CharacterWidth * scale, 0));
        }
        public void DrawImage(ITexture2D texture, RectangleI destinationRectangle, Color color)
        {
            DrawImages(new DrawImagesEntry(texture, destinationRectangle, color));
        }
        public void DrawImages(params DrawImagesEntry[] entries)
        {
            Reset2D();
            GL.Enable(EnableCap.Texture2D);
            float h = Viewport.Height;

            foreach (var entry in entries)
            {
                var texture = (TextureAdapter)entry.Texture;
                if (texture.Bitmap == null) continue;

                var rect = entry.DestinationRectangle;

                texture.Bind();

                GL.Begin(PrimitiveType.Quads);

                GL.Color3(entry.Color.ToVector3().ToOpenTK());

                GL.TexCoord2(0, 0); GL.Vertex3(rect.Left,  h - rect.Top, 0.4f);
                GL.TexCoord2(1, 0); GL.Vertex3(rect.Right, h - rect.Top, 0.4f);
                GL.TexCoord2(1, 1); GL.Vertex3(rect.Right, h - rect.Bottom, 0.4f);
                GL.TexCoord2(0, 1); GL.Vertex3(rect.Left,  h - rect.Bottom, 0.4f);

                GL.End();

                TextureAdapter.Unbind();
            }
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
            SetMatrices(world, view, projection);
            GL.Begin(PrimitiveType.LineStrip);

            GL.Color3(color.X, color.Y, color.Z);

            foreach (var p in points)
            {
                GL.Vertex3(p.X, p.Y, p.Z);
            }

            GL.End();
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

        void Reset2D()
        {
            var viewport = Viewport;
            float w = viewport.Width;
            float h = viewport.Height;
            var proj = Matrix.CreateOrthographicOffCenter(0, w, 0, h, 0, 1);
            SetMatrices(Matrix.Identity, Matrix.Identity, proj);
        }
    }
}

