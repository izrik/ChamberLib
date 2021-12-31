using _System = global::System;

namespace ChamberLib.OpenTK.Math
{
    public static class ColorHelper
    {
        public static _System.Drawing.Color ToSystemDrawing(this ChamberLib.Color color)
        {
            return _System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
        }
    }
}

