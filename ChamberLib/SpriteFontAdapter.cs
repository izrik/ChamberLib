using System;
using SpriteFont = Microsoft.Xna.Framework.Graphics.SpriteFont;
using System.Collections.Generic;

namespace ChamberLib
{
    public class SpriteFontAdapter : IFont
    {
        protected static readonly Dictionary<SpriteFont, SpriteFontAdapter> _cache = new Dictionary<SpriteFont, SpriteFontAdapter>();

        public static IFont GetAdapter(SpriteFont font)
        {
            if (_cache.ContainsKey(font))
            {
                return _cache[font];
            }

            var adapter = new SpriteFontAdapter(font);
            _cache[font] = adapter;
            return adapter;
        }

        protected SpriteFontAdapter(SpriteFont font)
        {
            SpriteFont = font;
        }

        public SpriteFont SpriteFont;

        public Vector2 MeasureString(string text)
        {
            return SpriteFont.MeasureString(text).ToChamber();
        }
    }
}

