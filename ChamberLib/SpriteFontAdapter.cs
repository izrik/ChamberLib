using System;

namespace ChamberLib
{
    public class SpriteFontAdapter : IFont
    {
        public SpriteFontAdapter(Microsoft.Xna.Framework.Graphics.SpriteFont font)
        {
            SpriteFont = font;
        }

        public Microsoft.Xna.Framework.Graphics.SpriteFont SpriteFont;

        public Vector2 MeasureString(string text)
        {
            return SpriteFont.MeasureString(text).ToChamber();
        }
    }
}

