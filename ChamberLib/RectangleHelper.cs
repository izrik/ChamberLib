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

        public static RectangleI ToChamber(this Rectangle rect)
        {
            return new RectangleI(
                rect.Left,
                rect.Top,
                rect.Width,
                rect.Height);
        }

        public static Rectangle ToXna(this RectangleI r)
        {
            return
                new Rectangle(
                    r.Left,
                    r.Top,
                    r.Width,
                    r.Height);
        }

    }
}
