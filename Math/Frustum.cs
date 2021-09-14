using System;
using System.Linq;

namespace ChamberLib
{
    public struct Frustum
    {
        public Frustum(Matrix m)
        {
            Matrix = m;

            var inv = m.Inverted();

            NearTopLeft = inv.TransformHomogeneous(new Vector4(-1, 1, 0, 1));
            NearTopRight = inv.TransformHomogeneous(new Vector4(1, 1, 0, 1));
            NearBottomLeft = inv.TransformHomogeneous(new Vector4(-1, -1, 0, 1));
            NearBottomRight = inv.TransformHomogeneous(new Vector4(1, -1, 0, 1));
            FarTopLeft = inv.TransformHomogeneous(new Vector4(-1, 1, 1, 1));
            FarTopRight = inv.TransformHomogeneous(new Vector4(1, 1, 1, 1));
            FarBottomLeft = inv.TransformHomogeneous(new Vector4(-1, -1, 1, 1));
            FarBottomRight = inv.TransformHomogeneous(new Vector4(1, -1, 1, 1));

            Top =       Plane.FromPoints(NearTopLeft     ,  NearTopRight    ,  FarTopLeft       );
            Bottom =    Plane.FromPoints(NearBottomLeft  ,  FarBottomLeft   ,  NearBottomRight  );
            Left =      Plane.FromPoints(NearTopLeft     ,  FarBottomLeft   ,  NearBottomLeft   );
            Right =     Plane.FromPoints(NearTopRight    ,  NearBottomRight ,  FarBottomRight   );
            Near =      Plane.FromPoints(NearTopLeft     ,  NearBottomLeft  ,  NearTopRight     );
            Far =       Plane.FromPoints(FarTopLeft      ,  FarTopRight     ,  FarBottomLeft    );
        }

        public readonly Matrix Matrix;

        public readonly Plane Top;
        public readonly Plane Bottom;
        public readonly Plane Left;
        public readonly Plane Right;
        public readonly Plane Near;
        public readonly Plane Far;

        public readonly Vector3 NearTopLeft;
        public readonly Vector3 NearTopRight;
        public readonly Vector3 NearBottomLeft;
        public readonly Vector3 NearBottomRight;
        public readonly Vector3 FarTopLeft;
        public readonly Vector3 FarTopRight;
        public readonly Vector3 FarBottomLeft;
        public readonly Vector3 FarBottomRight;

        public ContainmentType Contains(Sphere s)
        {
            var center = (NearTopLeft +
                            NearTopRight +
                            NearBottomLeft +
                            NearBottomRight +
                            FarTopLeft +
                            FarTopRight +
                            FarBottomLeft +
                            FarBottomRight) / 8;

            bool intersecting = false;

            Plane p;
            PlaneIntersectionType intersect;
            PlaneIntersectionType centerSide;

            p = Top;
            intersect = p.Intersects(s);
            if (intersect == PlaneIntersectionType.Intersecting)
            {
                intersecting = true;
            }
            centerSide = p.IntersectsPoint(center);
            if ((intersect == PlaneIntersectionType.Front && centerSide == PlaneIntersectionType.Back) ||
                (intersect == PlaneIntersectionType.Back && centerSide == PlaneIntersectionType.Front))
            {
                return ContainmentType.Disjoint;
            }

            p = Bottom;
            intersect = p.Intersects(s);
            if (intersect == PlaneIntersectionType.Intersecting)
            {
                intersecting = true;
            }
            centerSide = p.IntersectsPoint(center);
            if ((intersect == PlaneIntersectionType.Front && centerSide == PlaneIntersectionType.Back) ||
                (intersect == PlaneIntersectionType.Back && centerSide == PlaneIntersectionType.Front))
            {
                return ContainmentType.Disjoint;
            }

            p = Left;
            intersect = p.Intersects(s);
            if (intersect == PlaneIntersectionType.Intersecting)
            {
                intersecting = true;
            }
            centerSide = p.IntersectsPoint(center);
            if ((intersect == PlaneIntersectionType.Front && centerSide == PlaneIntersectionType.Back) ||
                (intersect == PlaneIntersectionType.Back && centerSide == PlaneIntersectionType.Front))
            {
                return ContainmentType.Disjoint;
            }

            p = Right;
            intersect = p.Intersects(s);
            if (intersect == PlaneIntersectionType.Intersecting)
            {
                intersecting = true;
            }
            centerSide = p.IntersectsPoint(center);
            if ((intersect == PlaneIntersectionType.Front && centerSide == PlaneIntersectionType.Back) ||
                (intersect == PlaneIntersectionType.Back && centerSide == PlaneIntersectionType.Front))
            {
                return ContainmentType.Disjoint;
            }

            p = Near;
            intersect = p.Intersects(s);
            if (intersect == PlaneIntersectionType.Intersecting)
            {
                intersecting = true;
            }
            centerSide = p.IntersectsPoint(center);
            if ((intersect == PlaneIntersectionType.Front && centerSide == PlaneIntersectionType.Back) ||
                (intersect == PlaneIntersectionType.Back && centerSide == PlaneIntersectionType.Front))
            {
                return ContainmentType.Disjoint;
            }

            p = Far;
            intersect = p.Intersects(s);
            if (intersect == PlaneIntersectionType.Intersecting)
            {
                intersecting = true;
            }
            centerSide = p.IntersectsPoint(center);
            if ((intersect == PlaneIntersectionType.Front && centerSide == PlaneIntersectionType.Back) ||
                (intersect == PlaneIntersectionType.Back && centerSide == PlaneIntersectionType.Front))
            {
                return ContainmentType.Disjoint;
            }

            return (intersecting ? ContainmentType.Intersects : ContainmentType.Contains);
        }
    }
}

