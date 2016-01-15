using System;
using System.Collections.Generic;

namespace ChamberLib
{
    public partial class Renderer : IRenderer
    {
        public Renderer()
        {
        }

        #region IRenderer implementation

        public void DrawLine(Color color, Vector2 v1, Vector2 v2)
        {
            throw new NotImplementedException();
        }

        public void DrawLines(Color color, IEnumerable<Vector2> vs)
        {
            throw new NotImplementedException();
        }

        public void DrawString(IFont font, string text, Vector2 position,
                               Color color, float rotation=0f,
                               Vector2 origin=default(Vector2), float scale=1f)
        {
            throw new NotImplementedException();
        }

        public void DrawImage(ITexture2D texture,
                              RectangleI destinationRectangle, Color color)
        {
            throw new NotImplementedException();
        }

        public void DrawImages(params DrawImagesEntry[] entries)
        {
            throw new NotImplementedException();
        }

        public void Clear(Color color)
        {
            throw new NotImplementedException();
        }

        public void Reset3D()
        {
            throw new NotImplementedException();
        }

        public void DrawLines(Vector3 color, Matrix world, Matrix view,
                              Matrix projection, IEnumerable<Vector3> vs)
        {
            throw new NotImplementedException();
        }

        public void SetViewport(Viewport value, bool windowed=true)
        {
            throw new NotImplementedException();
        }

        public Viewport Viewport
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}

