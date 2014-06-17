using System;

namespace ChamberLib
{
    public class FontAdapter : IFont
    {
        public FontAdapter()
        {
        }

        public Vector2 MeasureString(string text)
        {
            return new Vector2(100, 25);
        }
    }
}

