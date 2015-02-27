using System;

namespace ChamberLib
{
    public static class QuaternionHelper
    {
        public static ChamberLib.Quaternion ToChamber(this Assimp.Quaternion q)
        {
            return new ChamberLib.Quaternion(q.X, q.Y, q.Z, q.W);
        }
    }
}

