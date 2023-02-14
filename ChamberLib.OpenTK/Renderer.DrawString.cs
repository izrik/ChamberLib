using System;

namespace ChamberLib.OpenTK
{
    public partial class Renderer
    {
        public void DrawString(IFont font, string text, Vector2 position,
            Color color,
            float rotation=0,
            float scaleX=1,
            float scaleY=1)
        {
            var font2 = (FontAdapter)font;
            font2.DrawString(this, text, position, color, rotation,
                scaleX, scaleY);
        }

    }
}

