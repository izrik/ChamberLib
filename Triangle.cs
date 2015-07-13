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

        public bool Intersects(Ray ray, float epsilon=0)
        {
            var plane = this.ToPlane();
            var p = plane.Intersects(ray, epsilon);
            if (!p.HasValue) return false;

            var a = Vector3.Cross(plane.Normal, V2 - V1);
            var b = Vector3.Cross(plane.Normal, V3 - V2);
            var c = Vector3.Cross(plane.Normal, V1 - V3);

            var pv = p.Value;
            var pa = pv - V1;
            var pb = pv - V2;
            var pc = pv - V3;

            if (a.Dot(pa) < epsilon) return false;
            if (b.Dot(pb) < epsilon) return false;
            if (c.Dot(pc) < epsilon) return false;

            return true;
        }
    }
}
