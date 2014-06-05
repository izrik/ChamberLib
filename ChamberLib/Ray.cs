using System;

namespace ChamberLib
{
    public struct Ray
    {
        public Ray(Vector3 position, Vector3 direction)
        {
            Position = position;
            Direction = direction;
        }

        public Vector3 Position;
        public Vector3 Direction;

        public float? Intersects(BoundingBox b)
        {
            throw new NotImplementedException();
        }
    }
}

