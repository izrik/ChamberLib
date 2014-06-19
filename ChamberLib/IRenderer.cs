using System;
using System.Collections.Generic;

namespace ChamberLib
{
    public interface IRenderer
    {
        void DrawLine(float width, Color color, Vector2 v1, Vector2 v2);
        void DrawLine(Color color, Vector2 v1, Vector2 v2);

        void DrawString(IFont font, string text, Vector2 position, Color color,
                        float rotation = 0,
                        Vector2 origin = default(Vector2),
                        float scale = 1);

        void DrawImage(ITexture2D texture, RectangleI destinationRectangle, Color color);
        void DrawImages(params DrawImagesEntry[] entries);


        void Clear(Color color);
        void DrawCircleXZ(Vector3 color, Matrix? world = null, Matrix? view = null, Matrix? projection = null);

        void Reset3D();

        void DrawLines(Vector3 color, Matrix view, Matrix projection, IEnumerable<Vector3> points);
        void DrawLine(Vector3 color, Matrix view, Matrix projection, Vector3 p1, Vector3 p2);

        Viewport Viewport { get; set; }
    }

    public struct DrawImagesEntry
    {
        public DrawImagesEntry(ITexture2D texture, RectangleI dest)
            : this(texture, dest, Color.White)
        {
        }
        public DrawImagesEntry(ITexture2D texture, RectangleI dest, Color color)
        {
            Texture = texture;
            DestinationRectangle = dest;
            Color = color;
        }

        public ITexture2D Texture;
        public RectangleI DestinationRectangle;
        public Color Color;
    }

}

