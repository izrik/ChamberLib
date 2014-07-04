using System;

namespace ChamberLib
{
    public static class VectorHelper
    {
        public static OpenTK.Vector2 ToOpenTK(this ChamberLib.Vector2 v)
        {
            return new OpenTK.Vector2(v.X, v.Y);
        }
        public static OpenTK.Vector3 ToOpenTK(this ChamberLib.Vector3 v)
        {
            return new OpenTK.Vector3(v.X, v.Y, v.Z);
        }
        public static OpenTK.Vector4 ToOpenTK(this ChamberLib.Vector4 v)
        {
            return new OpenTK.Vector4(v.X, v.Y, v.Z, v.W);
        }
    }
}

