using System;

namespace ChamberLib.OpenTK
{
    public partial class Renderer
    {
        public void DrawString(IFont font, string text, Vector2 position, Color color, float rotation = 0f, Vector2 origin = default(Vector2), float scale = 1f)
        {
            var font2 = (FontAdapter)font;
            font2.DrawString(this, text, position, color, rotation, origin, scale);
        }

    }
}

