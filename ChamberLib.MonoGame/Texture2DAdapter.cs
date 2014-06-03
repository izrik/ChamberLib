using System;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using System.Collections.Generic;

namespace ChamberLib
{
    public class Texture2DAdapter : ITexture2D
    {
        protected static readonly Dictionary<Texture2D, Texture2DAdapter> _cache = new Dictionary<Texture2D, Texture2DAdapter>();

        public static ITexture2D GetAdapter(Texture2D font)
        {
            if (_cache.ContainsKey(font))
            {
                return _cache[font];
            }

            var adapter = new Texture2DAdapter(font);
            _cache[font] = adapter;
            return adapter;
        }

        protected Texture2DAdapter(Texture2D texture)
        {
            Texture = texture;
        }

        public Texture2D Texture;

        public Vector2 GetSize()
        {
            return new Vector2(Texture.Width, Texture.Height);
        }
    }
}

