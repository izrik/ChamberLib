using System;

namespace ChamberLib
{
    public class Renderer : IRenderer
    {
        #region IRenderer implementation
        public void Begin()
        {
        }
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
        public void End()
        {
        }
        public void Clear(Color color)
        {
        }
        public void DrawCircleXZ(Vector3 color, Matrix? world = default(Matrix?), Matrix? view = default(Matrix?), Matrix? projection = default(Matrix?))
        {
        }
        public void Reset3D()
        {
        }
        public void DrawLines(Vector3 color, Matrix view, Matrix projection, System.Collections.Generic.IEnumerable<Vector3> points)
        {
        }
        public void DrawLine(Vector3 color, Matrix view, Matrix projection, Vector3 p1, Vector3 p2)
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

