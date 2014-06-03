using System;

namespace ChamberLib
{
    public static class SphereHelper
    {
        public static Microsoft.Xna.Framework.BoundingSphere ToXna(this ChamberLib.Sphere b)
        {
            return new Microsoft.Xna.Framework.BoundingSphere(b.Center.ToXna(), b.Radius);
        }

        public static ChamberLib.Sphere ToChamber(this Microsoft.Xna.Framework.BoundingSphere b)
        {
            return new ChamberLib.Sphere(b.Center.ToChamber(), b.Radius);
        }
    }
}

