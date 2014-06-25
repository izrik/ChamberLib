using System;

namespace ChamberLib
{
    public class FontAdapter : IFont
    {
        public FontAdapter()
        {
        }

        public float CharacterWidth = 15;
        public float CharacterHeight = 25;

        public Vector2 MeasureString(string text)
        {
            return new Vector2(text.Length * CharacterWidth, CharacterHeight);
        }
    }
}

