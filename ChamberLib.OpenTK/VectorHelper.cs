using System;
using System.Linq;

namespace ChamberLib
{
    public static class VectorHelper
    {
        public static OpenTK.Vector2 ToOpenTK(this ChamberLib.Vector2 v)
        {
            return new OpenTK.Vector2(v.X, v.Y);
        }
        public static OpenTK.Vector2[] ToOpenTK(this ChamberLib.Vector2[] v)
        {
            return v.Select(mm => mm.ToOpenTK()).ToArray();
        }
        public static OpenTK.Vector3 ToOpenTK(this ChamberLib.Vector3 v)
        {
            return new OpenTK.Vector3(v.X, v.Y, v.Z);
        }
        public static OpenTK.Vector3[] ToOpenTK(this ChamberLib.Vector3[] v)
        {
            return v.Select(mm => mm.ToOpenTK()).ToArray();
        }
        public static OpenTK.Vector4 ToOpenTK(this ChamberLib.Vector4 v)
        {
            return new OpenTK.Vector4(v.X, v.Y, v.Z, v.W);
        }
        public static OpenTK.Vector4[] ToOpenTK(this ChamberLib.Vector4[] v)
        {
            return v.Select(mm => mm.ToOpenTK()).ToArray();
        }

        public static ChamberLib.Vector2 ToChamber(this OpenTK.Vector2 v)
        {
            return new ChamberLib.Vector2(v.X, v.Y);
        }
        public static ChamberLib.Vector2[] ToChamber(this OpenTK.Vector2[] v)
        {
            return v.Select(mm => mm.ToChamber()).ToArray();
        }
        public static ChamberLib.Vector3 ToChamber(this OpenTK.Vector3 v)
        {
            return new ChamberLib.Vector3(v.X, v.Y, v.Z);
        }
        public static ChamberLib.Vector3[] ToChamber(this OpenTK.Vector3[] v)
        {
            return v.Select(mm => mm.ToChamber()).ToArray();
        }
        public static ChamberLib.Vector4 ToChamber(this OpenTK.Vector4 v)
        {
            return new ChamberLib.Vector4(v.X, v.Y, v.Z, v.W);
        }
        public static ChamberLib.Vector4[] ToChamber(this OpenTK.Vector4[] v)
        {
            return v.Select(mm => mm.ToChamber()).ToArray();
        }


        public static ChamberLib.Vector2 ToChamber(this Assimp.Vector2D v)
        {
            return new ChamberLib.Vector2(v.X, v.Y);
        }
        public static ChamberLib.Vector2[] ToChamber(this Assimp.Vector2D[] v)
        {
            return v.Select(mm => mm.ToChamber()).ToArray();
        }
        public static ChamberLib.Vector3 ToChamber(this Assimp.Vector3D v)
        {
            return new ChamberLib.Vector3(v.X, v.Y, v.Z);
        }
        public static ChamberLib.Vector3[] ToChamber(this Assimp.Vector3D[] v)
        {
            return v.Select(mm => mm.ToChamber()).ToArray();
        }
    }
}

