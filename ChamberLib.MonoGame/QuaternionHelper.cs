using System;

namespace ChamberLib
{
    public static class QuaternionHelper
    {
        public static Microsoft.Xna.Framework.Quaternion ToXna(this ChamberLib.Quaternion q)
        {
            return new Microsoft.Xna.Framework.Quaternion(q.X, q.Y, q.Z, q.W);
        }

        public static ChamberLib.Quaternion ToChamber(this Microsoft.Xna.Framework.Quaternion q)
        {
            return new ChamberLib.Quaternion(q.X, q.Y, q.Z, q.W);
        }
    }
}

