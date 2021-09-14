using System;
using System.Linq;

namespace ChamberLib
{
    public struct Sphere
    {
        public Sphere(Vector3 center, float radius)
        {
            Center = center;
            Radius = radius;
        }

        public readonly Vector3 Center;
        public readonly float Radius;

        public Sphere Transform(Matrix m)
        {
            Vector3 center = Vector3.Transform(this.Center, m);


            var xr = m.Row1.ToVectorXYZ().LengthSquared();
            var yr = m.Row2.ToVectorXYZ().LengthSquared();
            var zr = m.Row3.ToVectorXYZ().LengthSquared();

            var rr = Math.Max(xr, Math.Max(yr, zr));

            float radius = (float)(Math.Sqrt(rr) * this.Radius);

            return new Sphere(center, radius);
        }

        public ContainmentType Contains(Frustum f)
        {
            var x1 = Contains(f.NearTopLeft);
            var x2 = Contains(f.NearTopRight);
            var x3 = Contains(f.NearBottomLeft);
            var x4 = Contains(f.NearBottomRight);
            var x5 = Contains(f.FarTopLeft);
            var x6 = Contains(f.FarTopRight);
            var x7 = Contains(f.FarBottomLeft);
            var x8 = Contains(f.FarBottomRight);

            if (x1 == ContainmentType.Intersects ||
                x2 == ContainmentType.Intersects ||
                x3 == ContainmentType.Intersects ||
                x4 == ContainmentType.Intersects ||
                x5 == ContainmentType.Intersects ||
                x6 == ContainmentType.Intersects ||
                x7 == ContainmentType.Intersects ||
                x8 == ContainmentType.Intersects)
            {
                return ContainmentType.Intersects;
            }

            var contains =
                (x1 == ContainmentType.Contains) ||
                (x2 == ContainmentType.Contains) ||
                (x3 == ContainmentType.Contains) ||
                (x4 == ContainmentType.Contains) ||
                (x5 == ContainmentType.Contains) ||
                (x6 == ContainmentType.Contains) ||
                (x7 == ContainmentType.Contains) ||
                (x8 == ContainmentType.Contains);

            var disjoint =
                (x1 == ContainmentType.Disjoint) ||
                (x2 == ContainmentType.Disjoint) ||
                (x3 == ContainmentType.Disjoint) ||
                (x4 == ContainmentType.Disjoint) ||
                (x5 == ContainmentType.Disjoint) ||
                (x6 == ContainmentType.Disjoint) ||
                (x7 == ContainmentType.Disjoint) ||
                (x8 == ContainmentType.Disjoint);

            if (contains && disjoint)
            {
                return ContainmentType.Intersects;
            }

            return x1;
        }

        public ContainmentType Contains(Vector3 v)
        {
            var r2 = Radius * Radius;
            var dist2 = (v - Center).LengthSquared();

            if (dist2 > r2)
                return ContainmentType.Disjoint;
            if (dist2 <r2)
                return ContainmentType.Contains;
            return ContainmentType.Intersects;
        }
    }
}

