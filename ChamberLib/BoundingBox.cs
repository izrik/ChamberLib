using System;

namespace ChamberLib
{
    public struct BoundingBox
    {
        public BoundingBox(Vector3 p1, Vector3 p2)
        {
            Max = Vector3.Max(p1, p2);
            Min = Vector3.Min(p1, p2);
        }

        public Vector3 Max;
        public Vector3 Min;

        public Vector3[] GetCorners()
        {
            return new Vector3[] {
                new Vector3(Max.X, Max.Y, Max.Z),
                new Vector3(Min.X, Max.Y, Max.Z),
                new Vector3(Max.X, Min.Y, Max.Z),
                new Vector3(Min.X, Min.Y, Max.Z),
                new Vector3(Max.X, Max.Y, Min.Z),
                new Vector3(Min.X, Max.Y, Min.Z),
                new Vector3(Max.X, Min.Y, Min.Z),
                new Vector3(Min.X, Min.Y, Min.Z),
            };
        }
    }
}

