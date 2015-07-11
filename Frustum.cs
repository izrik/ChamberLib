using System;

namespace ChamberLib
{
    public struct Frustum
    {
        public Frustum(Matrix m)
        {
            Matrix = m;

            var inv = m.Inverted();

            var nearTopLeft = inv.TransformHomogeneous(new Vector4(-1, 1, 0, 1));
            var nearTopRight = inv.TransformHomogeneous(new Vector4(1, 1, 0, 1));
            var nearBottomLeft = inv.TransformHomogeneous(new Vector4(-1, -1, 0, 1));
            var nearBottomRight = inv.TransformHomogeneous(new Vector4(1, -1, 0, 1));
            var farTopLeft = inv.TransformHomogeneous(new Vector4(-1, 1, 1, 1));
            var farTopRight = inv.TransformHomogeneous(new Vector4(1, 1, 1, 1));
            var farBottomLeft = inv.TransformHomogeneous(new Vector4(-1, -1, 1, 1));
            var farBottomRight = inv.TransformHomogeneous(new Vector4(1, -1, 1, 1));

            Corners = new [] {
                nearTopLeft,
                nearTopRight,
                nearBottomLeft,
                nearBottomRight,
                farTopLeft,
                farTopRight,
                farBottomLeft,
                farBottomRight,
            };

            Top =       Plane.FromPoints(nearTopLeft     ,  nearTopRight    ,  farTopLeft       );
            Bottom =    Plane.FromPoints(nearBottomLeft  ,  farBottomLeft   ,  nearBottomRight  );
            Left =      Plane.FromPoints(nearTopLeft     ,  farBottomLeft   ,  nearBottomLeft   );
            Right =     Plane.FromPoints(nearTopRight    ,  nearBottomRight ,  farBottomRight   );
            Near =      Plane.FromPoints(nearTopLeft     ,  nearBottomLeft  ,  nearTopRight     );
            Far =       Plane.FromPoints(farTopLeft      ,  farTopRight     ,  farBottomLeft    );
        }

        public readonly Matrix Matrix;

        public readonly Plane Top;
        public readonly Plane Bottom;
        public readonly Plane Left;
        public readonly Plane Right;
        public readonly Plane Near;
        public readonly Plane Far;

        public readonly Vector3[] Corners;

        public ContainmentType Contains(Sphere s)
        {
            foreach (var p in new [] { Top, Bottom, Left, Right, Near, Far })
            {
                var intersect = p.Intersects(s);
                if (intersect == PlaneIntersectionType.Front)
                {
                    return ContainmentType.Disjoint;
                }
                if (intersect == PlaneIntersectionType.Intersecting)
                {
                    return ContainmentType.Intersects;
                }
            }

            return ContainmentType.Contains;
        }

        public Vector3[] GetCorners()
        {
            return Corners;
        }
    }
}

