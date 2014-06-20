using System;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;

namespace ChamberLib
{
    public class Renderer : IRenderer
    {
        #region IRenderer implementation
        public void DrawLine(float width, Color color, Vector2 v1, Vector2 v2)
        {
        }
        public void DrawLine(Color color, Vector2 v1, Vector2 v2)
        {
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
        public Viewport Viewport
        {
            get
            {
                return new Viewport();
            }
            set
            {
            }
        }
        #endregion
    }
}

