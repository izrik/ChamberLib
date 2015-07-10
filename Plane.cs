using System;
using System.Linq;

namespace ChamberLib
{
    public struct Plane
    {
        public Plane(Vector3 normal, float distance)
        {
            Normal = normal;
            Distance = distance;
        }

        // TODO: Should Normal be normalized in the constructor?
        // TODO: readonly
        // TODO: is Distance always positive?

        public Vector3 Normal;
        public float Distance;

        public static Plane FromPoints(Vector3 p1, Vector3 p2, Vector3 p3)
        {
            var n = Vector3.Cross(p2 - p1, p3 - p1).Normalized();
            var d = p1.Dot(n);

            return new Plane(n, -d);
        }

        public float D
        {
            get { return Distance; }
            set { Distance = value; }
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
            var pointIntersections = f.GetCorners().Select(IntersectsPoint).Distinct().ToList();

            if (pointIntersections.Contains(PlaneIntersectionType.Intersecting))
            {
                return PlaneIntersectionType.Intersecting;
            }

            if (pointIntersections.Contains(PlaneIntersectionType.Front) &&
                pointIntersections.Contains(PlaneIntersectionType.Back))
            {
                return PlaneIntersectionType.Intersecting;
            }

            return pointIntersections[0];
        }

        public PlaneIntersectionType Intersects(Sphere s)
        {
            float f = Normal.Dot(s.Center) + Distance;

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
            if (s > 0)
            {
                return PlaneIntersectionType.Front;
            }

            if (s == 0)
            {
                return PlaneIntersectionType.Intersecting;
            }

            return PlaneIntersectionType.Back;
        }

        public Vector3 Project(Vector3 v)
        {
            var s = Normal.Dot(v) - Distance;
            return v - s * Normal;
        }
    }
}

