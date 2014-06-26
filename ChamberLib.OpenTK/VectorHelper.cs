using System;

namespace ChamberLib
{
    public static class VectorHelper
    {
        public static OpenTK.Vector3 ToOpenTK(this ChamberLib.Vector3 v)
        {
            return new OpenTK.Vector3(v.X, v.Y, v.Z);
        }
    }
}

