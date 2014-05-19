using System;

namespace ChamberLib
{
    public static class VectorHelper
    {
        public static Microsoft.Xna.Framework.Vector2 ToXna(this Vector2 v)
        {
            return new Microsoft.Xna.Framework.Vector2(v.X, v.Y);
        }

        public static ChamberLib.Vector2 ToChamber(this Microsoft.Xna.Framework.Vector2 v)
        {
            return new Vector2(v.X, v.Y);
        }
    }
}

