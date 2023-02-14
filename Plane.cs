using System;
using System.Linq;

namespace ChamberLib
{
    public struct Plane
    {
        public Plane(Vector3 normal, float distance)
        {
            if (normal.LengthSquared() != 1)
                normal = normal.Normalized();

            if (distance < 0)
            {
                normal = -normal;
                distance = -distance;
            }

            Normal = normal;
            Distance = distance;
        }

        // TODO: throw exceptions for invalid inputs (ie. NaN)?

        public readonly Vector3 Normal;
        public readonly float Distance;

        public static Plane FromPoints(Vector3 p1, Vector3 p2, Vector3 p3)
        {
            var n = Vector3.Cross(p2 - p1, p3 - p1).Normalized();
            if (n == Vector3.Zero)
                n = Vector3.UnitX;
            var d = p1.Dot(n);

            return new Plane(n, d);
        }

        public float D
        {
            get { return Distance; }
        }

        public PlaneIntersectionType Intersects(BoundingBox b)
        {
            var pointIntersections = b.GetCorners().Select(IntersectsPoint).Distinct().ToList();

            if (pointIntersections.Contains(PlaneIntersectionType.Intersecting))
            {
                return PlaneIntersectionType.Intersecting;
            }

            if (pointIntersections.Contains(PlaneIntersectionType.Front) &&
                pointIntersections.Contains(PlaneIntersectionType.Back))
            {
                return PlaneIntersectionType.Intersecting;
            }

            return pointIntersections.First();
        }

        public PlaneIntersectionType Intersects(Frustum f)
        {
            var x1 = IntersectsPoint(f.NearTopLeft);
            var x2 = IntersectsPoint(f.NearTopRight);
            var x3 = IntersectsPoint(f.NearBottomLeft);
            var x4 = IntersectsPoint(f.NearBottomRight);
            var x5 = IntersectsPoint(f.FarTopLeft);
            var x6 = IntersectsPoint(f.FarTopRight);
            var x7 = IntersectsPoint(f.FarBottomLeft);
            var x8 = IntersectsPoint(f.FarBottomRight);

            if (x1 == PlaneIntersectionType.Intersecting ||
                x2 == PlaneIntersectionType.Intersecting ||
                x3 == PlaneIntersectionType.Intersecting ||
                x4 == PlaneIntersectionType.Intersecting ||
                x5 == PlaneIntersectionType.Intersecting ||
                x6 == PlaneIntersectionType.Intersecting ||
                x7 == PlaneIntersectionType.Intersecting ||
                x8 == PlaneIntersectionType.Intersecting)
            {
                return PlaneIntersectionType.Intersecting;
            }

            var front =
                (x1 == PlaneIntersectionType.Front) ||
                (x2 == PlaneIntersectionType.Front) ||
                (x3 == PlaneIntersectionType.Front) ||
                (x4 == PlaneIntersectionType.Front) ||
                (x5 == PlaneIntersectionType.Front) ||
                (x6 == PlaneIntersectionType.Front) ||
                (x7 == PlaneIntersectionType.Front) ||
                (x8 == PlaneIntersectionType.Front);

            var back =
                (x1 == PlaneIntersectionType.Back) ||
                (x2 == PlaneIntersectionType.Back) ||
                (x3 == PlaneIntersectionType.Back) ||
                (x4 == PlaneIntersectionType.Back) ||
                (x5 == PlaneIntersectionType.Back) ||
                (x6 == PlaneIntersectionType.Back) ||
                (x7 == PlaneIntersectionType.Back) ||
                (x8 == PlaneIntersectionType.Back);

            if (front && back) return PlaneIntersectionType.Intersecting;

            return x1;
        }

        public PlaneIntersectionType Intersects(Sphere s)
        {
            float f = Normal.Dot(s.Center) - Distance;

            if (f > s.Radius)
            {
                return PlaneIntersectionType.Front;
            }

            if (f >= -s.Radius)
            {
                return PlaneIntersectionType.Intersecting;
            }

            return PlaneIntersectionType.Back;
        }

        public PlaneIntersectionType IntersectsPoint(Vector3 v)
        {
            var s = Normal.Dot(v);

            if (s == Distance)
            {
                return PlaneIntersectionType.Intersecting;
            }

            if (s > Distance)
            {
                return PlaneIntersectionType.Front;
            }

            return PlaneIntersectionType.Back;
        }

        public Vector3 Project(Vector3 v)
        {
            var s = Normal.Dot(v) - Distance;
            return v - s * Normal;
        }

        public Vector3? Intersects(Ray ray, float epsilon=0)
        {
            return ray.Intersects(this, epsilon);
        }
    }
}

