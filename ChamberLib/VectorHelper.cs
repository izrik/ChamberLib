using System;
using System.Linq;

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

        public static ChamberLib.Vector2? ToChamber(this Microsoft.Xna.Framework.Vector2? v)
        {
            if (v.HasValue)
            {
                return new Vector2(v.Value.X, v.Value.Y);
            }
            else
            {
                return null;
            }
        }

        public static Microsoft.Xna.Framework.Vector3 ToXna(this Vector3 v)
        {
            return new Microsoft.Xna.Framework.Vector3(v.X, v.Y, v.Z);
        }

        public static ChamberLib.Vector3 ToChamber(this Microsoft.Xna.Framework.Vector3 v)
        {
            return new Vector3(v.X, v.Y, v.Z);
        }

        public static Microsoft.Xna.Framework.Vector3[] ToXna(this ChamberLib.Vector3[] v)
        {
            return v.Select(mm => mm.ToXna()).ToArray();
        }

        public static ChamberLib.Vector3[] ToChamber(this Microsoft.Xna.Framework.Vector3[] v)
        {
            return v.Select(mm => mm.ToChamber()).ToArray();
        }

        public static ChamberLib.Vector3? ToChamber(this Microsoft.Xna.Framework.Vector3? v)
        {
            if (v.HasValue)
            {
                return new Vector3(v.Value.X, v.Value.Y, v.Value.Z);
            }
            else
            {
                return null;
            }
        }

        public static Microsoft.Xna.Framework.Vector4 ToXna(this Vector4 v)
        {
            return new Microsoft.Xna.Framework.Vector4(v.X, v.Y, v.Z, v.W);
        }

        public static ChamberLib.Vector4 ToChamber(this Microsoft.Xna.Framework.Vector4 v)
        {
            return new Vector4(v.X, v.Y, v.Z, v.W);
        }

        public static Microsoft.Xna.Framework.Vector4[] ToXna(this ChamberLib.Vector4[] v)
        {
            return v.Select(mm => mm.ToXna()).ToArray();
        }

        public static ChamberLib.Vector4[] ToChamber(this Microsoft.Xna.Framework.Vector4[] v)
        {
            return v.Select(mm => mm.ToChamber()).ToArray();
        }
    }
}

