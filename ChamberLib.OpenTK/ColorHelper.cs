using System;

namespace ChamberLib.OpenTK
{
    public static class ColorHelper
    {
        public static System.Drawing.Color ToSystemDrawing(this ChamberLib.Color color)
        {
            return System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
        }
    }
}

