using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChamberLib
{
    public struct Triangle
    {
        public Triangle(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            V1 = v1;
            V2 = v2;
            V3 = v3;
        }

        public readonly Vector3 V1;
        public readonly Vector3 V2;
        public readonly Vector3 V3;

        public Plane ToPlane()
        {
            return Plane.FromPoints(V1, V2, V3);
        }

        public float Area
        {
            get
            {
                var ab = V2 - V1;
                var ac = V3 - V1;
                return Vector3.Cross(ab, ac).Length() / 2;
            }
        }

        public bool Intersects(Ray ray)
        {
            var p = this.ToPlane().Intersects(ray);
            if (!p.HasValue) return false;

            var n21 = V2 - V1;
            var n31 = V3 - V1;

            var pv = p.Value;
            var np = pv - V1;
            var s21 = np.Dot(n21) / n21.LengthSquared();
            if (s21 < 0 || s21 > 1) return false;
            var s31 = np.Dot(n31) / n31.LengthSquared();
            if (s31 < 0 || s31 > 1) return false;

            if (s21 + s31 > 1) return false;

            return true;
        }
    }
}
