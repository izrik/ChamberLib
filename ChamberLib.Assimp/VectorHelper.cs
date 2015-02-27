using System;
using System.Linq;

namespace ChamberLib
{
    public static class VectorHelper
    {
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

