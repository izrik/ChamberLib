using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ChamberLib
{
    public static class RectangleHelper
    {
        public static Vector2 GetSize(this Rectangle rect)
        {
            return new Vector2(rect.Width, rect.Height);
        }

        public static RectangleF ToRectangleF(this Rectangle rect)
        {
            return new RectangleF(
                rect.Left,
                rect.Top,
                rect.Width,
                rect.Height);
        }

        public static Rectangle ToXna(this RectangleF r)
        {
            return
                new Rectangle(
                    r.Left.RoundToInt(),
                    r.Top.RoundToInt(),
                    r.Width.RoundToInt(),
                    r.Height.RoundToInt());
        }
    }
}
