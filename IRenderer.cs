using System;
using System.Collections.Generic;

namespace ChamberLib
{
    public interface IRenderer
    {
        void DrawLine(Color color, Vector2 v1, Vector2 v2);
        void DrawLines(Color color, Vector2[] vs);
        void DrawLines(Color color, Vector2[] vs, int count);

        void DrawString(IFont font, string text, Vector2 position, Color color,
                        float rotation = 0,
                        Vector2 origin = default(Vector2),
                        float scale = 1);
        void DrawString(IFont font, string text, Vector2 position, Color color,
                        float rotation,
                        Vector2 origin,
                        float scaleX, float scaleY);

        void DrawImages(params DrawImagesEntry[] entries);

        void Clear(Color color);

        void Reset3D();

        void DrawLines(Vector3 color, Matrix world, Matrix view, Matrix projection, Vector3[] vs);
        void DrawLines(Vector3 color, Matrix world, Matrix view, Matrix projection, Vector3[] vs, int numPoints);

        Viewport Viewport { get; set; }
        void SetViewport(Viewport value, bool windowed=true);
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

    public static class RendererHelper
    {
        static readonly Vector3[] __DrawLine_points = new Vector3[2];
        public static void DrawLine(this IRenderer renderer, Vector3 color, Matrix world, Matrix view, Matrix projection, Vector3 p1, Vector3 p2)
        {
            __DrawLine_points[0] = p1;
            __DrawLine_points[1] = p2;
            renderer.DrawLines(color, world, view, projection, __DrawLine_points);
        }
    }

}

