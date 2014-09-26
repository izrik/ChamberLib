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
            var normal = Direction.Normalized();

            if ((normal.X > 0 && Position.X > b.Max.X) ||
                (normal.X < 0 && Position.X < b.Min.X) ||
                (normal.Y > 0 && Position.Y > b.Max.Y) ||
                (normal.Y < 0 && Position.Y < b.Min.Y) ||
                (normal.Z > 0 && Position.Z > b.Max.Z) ||
                (normal.Z < 0 && Position.Z < b.Min.Z))
            {
                return null;
            }

            float? dist = null;

            float xint;
            float yint;
            float zint;


            yint = Position.Y + normal.Y * (b.Min.X - Position.X) / normal.X;
            zint = Position.Z + normal.Z * (b.Min.X - Position.X) / normal.X;
            if (yint >= b.Min.Y && yint <= b.Max.Y &&
                zint >= b.Min.Z && zint <= b.Max.Z)
            {
                //intersects at x = minx
                var v = new Vector3(b.Min.X, yint, zint) - Position;
                var s = v.Dot(normal);
                if (s > 0)
                {
                    dist = s;
                }
            }

            yint = Position.Y + normal.Y * (b.Max.X - Position.X) / normal.X;
            zint = Position.Z + normal.Z * (b.Max.X - Position.X) / normal.X;
            if (yint >= b.Min.Y && yint <= b.Max.Y &&
                zint >= b.Min.Z && zint <= b.Max.Z)
            {
                //intersects at x = maxx
                var v = new Vector3(b.Max.X, yint, zint) - Position;
                var s = v.Dot(normal);
                if (s > 0 && (!dist.HasValue || s < dist.Value))
                {
                    dist = s;
                }
            }

            xint = Position.X + normal.X * (b.Min.Y - Position.Y) / normal.Y;
            zint = Position.Z + normal.Z * (b.Min.Y - Position.Y) / normal.Y;
            if (xint >= b.Min.X && xint <= b.Max.X &&
                zint >= b.Min.Z && zint <= b.Max.Z)
            {
                //intersects at y = miny
                var v = new Vector3(xint, b.Min.Y, zint) - Position;
                var s = v.Dot(normal);
                if (s > 0 && (!dist.HasValue || s < dist.Value))
                {
                    dist = s;
                }
            }

            xint = Position.X + normal.X * (b.Max.Y - Position.Y) / normal.Y;
            zint = Position.Z + normal.Z * (b.Max.Y - Position.Y) / normal.Y;
            if (xint >= b.Min.X && xint <= b.Max.X &&
                zint >= b.Min.Z && zint <= b.Max.Z)
            {
                //intersects at y = maxy
                var v = new Vector3(xint, b.Max.Y, zint) - Position;
                var s = v.Dot(normal);
                if (s > 0 && (!dist.HasValue || s < dist.Value))
                {
                    dist = s;
                }
            }

            xint = Position.X + normal.X * (b.Min.Z - Position.Z) / normal.Z;
            yint = Position.Y + normal.Y * (b.Min.Z - Position.Z) / normal.Z;
            if (xint >= b.Min.X && xint <= b.Max.X &&
                yint >= b.Min.Y && yint <= b.Max.Y)
            {
                //intersects at z = minz
                var v = new Vector3(xint, yint, b.Min.Z) - Position;
                var s = v.Dot(normal);
                if (s > 0 && (!dist.HasValue || s < dist.Value))
                {
                    dist = s;
                }
            }

            xint = Position.X + normal.X * (b.Max.Z - Position.Z) / normal.Z;
            yint = Position.Y + normal.Y * (b.Max.Z - Position.Z) / normal.Z;
            if (xint >= b.Min.X && xint <= b.Max.X &&
                yint >= b.Min.Y && yint <= b.Max.Y)
            {
                //intersects at z = maxz
                var v = new Vector3(xint, yint, b.Max.Z) - Position;
                var s = v.Dot(normal);
                if (s > 0 && (!dist.HasValue || s < dist.Value))
                {
                    dist = s;
                }
            }

            if (dist != null && dist.HasValue)
            {
                dist = dist.Value / Direction.Length();
            }

            return dist;
        }
    }
}

