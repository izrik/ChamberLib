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

        void DrawString(SpriteFont spriteFont, string text, Vector2 position, Color color);
        void DrawString(SpriteFont spriteFont, string text, Vector2 position, Color color,
                        float rotation, Vector2 origin, float scale, SpriteEffects effects,
                        float layerDepth);

        void Draw(Texture2D texture, Rectangle destinationRectangle, Color color);

        void Clear(Color color);

        void End();

        DepthStencilState DepthStencilState { get; set; }
        Viewport Viewport { get; set; }
        GraphicsDevice Device { get; }
    }
}

