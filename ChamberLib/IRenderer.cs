using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ChamberLib
{
    public interface IRenderer
    {
        void Begin();
        void Begin(SpriteSortMode sortMode, BlendState blendState);
        void Begin(SpriteSortMode sortMode, BlendState blendState, 
                   SamplerState samplerState, DepthStencilState depthStencilState,
                   RasterizerState rasterizerState, Effect effect);

        void DrawLine(float width, Color color, Vector2 v1, Vector2 v2);
        void DrawLine(Color color, Vector2 v1, Vector2 v2);

        void DrawString(SpriteFont spriteFont, string text, Vector2 position, Color color,
                        float rotation = 0,
                        Vector2 origin = default(Vector2),
                        float scale = 1);

        void DrawImage(Texture2D texture, RectangleI destinationRectangle, Color color);
        void DrawImages(params DrawImagesEntry[] entries);
        void DrawImages(Effect effect, params DrawImagesEntry[] entries);

        void Clear(Color color);

        void End();

        DepthStencilState DepthStencilState { get; set; }
        Viewport Viewport { get; set; }
        GraphicsDevice Device { get; }
    }

    public struct DrawImagesEntry
    {
        public DrawImagesEntry(Texture2D texture, RectangleI dest)
            : this(texture, dest, Color.White)
        {
        }
        public DrawImagesEntry(Texture2D texture, RectangleI dest, Color color)
        {
            Texture = texture;
            DestinationRectangle = dest;
            Color = color;
        }

        public Texture2D Texture;
        public RectangleI DestinationRectangle;
        public Color Color;
    }

}

