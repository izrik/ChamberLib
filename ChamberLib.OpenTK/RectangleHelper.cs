using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChamberLib.OpenTK
{
    public static class RectangleHelper
    {
        public static RectangleI ToChamber(this System.Drawing.Rectangle rect)
        {
            return new RectangleI(rect.Left, rect.Top, rect.Width, rect.Height);
        }
    }
}
