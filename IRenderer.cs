using System;
using System.Collections.Generic;

namespace ChamberLib
{
    public interface IRenderer
    {
        void DrawLine(Color color, Vector2 v1, Vector2 v2);
        void DrawLines(Color color, IEnumerable<Vector2> vs);

        void DrawString(IFont font, string text, Vector2 position, Color color,
                        float rotation = 0,
                        Vector2 origin = default(Vector2),
                        float scale = 1);

        void DrawImage(ITexture2D texture, RectangleI destinationRectangle, Color color);
        void DrawImages(params DrawImagesEntry[] entries);

        void DrawLines(Vector3 color, Matrix world, Matrix view, Matrix projection, IEnumerable<Vector3> vs);

        Units Units { get; set; }

        void Clear(Color color);

        void Reset3D();


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
        public static void DrawLine(this IRenderer renderer, Vector3 color, Matrix world, Matrix view, Matrix projection, Vector3 p1, Vector3 p2)
        {
            renderer.DrawLines(color, world, view, projection, new [] { p1, p2 });
        }
    }

}

