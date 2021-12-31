using _System = global::System;

namespace ChamberLib.OpenTK.Math
{
    public static class RectangleHelper
    {
        public static RectangleI ToChamber(this _System.Drawing.Rectangle rect)
        {
            return new RectangleI(rect.Left, rect.Top, rect.Width, rect.Height);
        }
    }
}
