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
            var intersections = f.GetCorners().Select(Contains).Distinct().ToList();

            if (intersections.Contains(ContainmentType.Intersects))
            {
                return ContainmentType.Intersects;
            }

            if (intersections.Contains(ContainmentType.Contains) &&
                intersections.Contains(ContainmentType.Disjoint))
            {
                return ContainmentType.Intersects;
            }

            return intersections[0];
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

