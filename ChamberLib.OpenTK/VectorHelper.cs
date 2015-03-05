using System;
using System.Linq;

using _OpenTK = global::OpenTK;

namespace ChamberLib.OpenTK
{
    public static class VectorHelper
    {
        public static _OpenTK.Vector2 ToOpenTK(this ChamberLib.Vector2 v)
        {
            return new _OpenTK.Vector2(v.X, v.Y);
        }
        public static _OpenTK.Vector2[] ToOpenTK(this ChamberLib.Vector2[] v)
        {
            return v.Select(mm => mm.ToOpenTK()).ToArray();
        }
        public static _OpenTK.Vector3 ToOpenTK(this ChamberLib.Vector3 v)
        {
            return new _OpenTK.Vector3(v.X, v.Y, v.Z);
        }
        public static _OpenTK.Vector3[] ToOpenTK(this ChamberLib.Vector3[] v)
        {
            return v.Select(mm => mm.ToOpenTK()).ToArray();
        }
        public static _OpenTK.Vector4 ToOpenTK(this ChamberLib.Vector4 v)
        {
            return new _OpenTK.Vector4(v.X, v.Y, v.Z, v.W);
        }
        public static _OpenTK.Vector4[] ToOpenTK(this ChamberLib.Vector4[] v)
        {
            return v.Select(mm => mm.ToOpenTK()).ToArray();
        }

        public static ChamberLib.Vector2 ToChamber(this _OpenTK.Vector2 v)
        {
            return new ChamberLib.Vector2(v.X, v.Y);
        }
        public static ChamberLib.Vector2[] ToChamber(this _OpenTK.Vector2[] v)
        {
            return v.Select(mm => mm.ToChamber()).ToArray();
        }
        public static ChamberLib.Vector3 ToChamber(this _OpenTK.Vector3 v)
        {
            return new ChamberLib.Vector3(v.X, v.Y, v.Z);
        }
        public static ChamberLib.Vector3[] ToChamber(this _OpenTK.Vector3[] v)
        {
            return v.Select(mm => mm.ToChamber()).ToArray();
        }
        public static ChamberLib.Vector4 ToChamber(this _OpenTK.Vector4 v)
        {
            return new ChamberLib.Vector4(v.X, v.Y, v.Z, v.W);
        }
        public static ChamberLib.Vector4[] ToChamber(this _OpenTK.Vector4[] v)
        {
            return v.Select(mm => mm.ToChamber()).ToArray();
        }
    }
}

