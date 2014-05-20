using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ChamberLib
{
    public interface IRenderer
    {
        void Begin();
        void Begin(SpriteSortMode sortMode, BlendState blendState);

        void DrawLine(float width, Color color, Vector2 v1, Vector2 v2);
        void DrawLine(Color color, Vector2 v1, Vector2 v2);

        void DrawString(IFont font, string text, Vector2 position, Color color,
                        float rotation = 0,
                        Vector2 origin = default(Vector2),
                        float scale = 1);

        void DrawImage(ITexture2D texture, RectangleI destinationRectangle, Color color);
        void DrawImages(params DrawImagesEntry[] entries);

        void Clear(Color color);

        void End();

        DepthStencilState DepthStencilState { get; set; }
        Viewport Viewport { get; set; }
        GraphicsDevice Device { get; }
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

