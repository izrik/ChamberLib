
//
// ChamberLib, a cross-platform game engine
// Copyright (C) 2021 izrik and Metaphysics Industries, Inc.
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301
// USA
//

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

        public readonly Vector3 Position;
        public readonly Vector3 Direction;

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

        public Vector3? Intersects(Plane plane, float epsilon=0)
        {
            var s = plane.Normal.Dot(Position);
            var costheta = plane.Normal.Dot(Direction);

            var delta = plane.Distance - s;
            if (Math.Abs(delta) <= epsilon) return Position;

            if (costheta == 0) return null;

            var n = delta / costheta;

            if (n < 0) return null;

            return Position + n * Direction;
        }

        public bool Intersects(Sphere s)
        {
            var direction = Direction.Normalized();
            var position = Position;
            var r2 = s.Radius * s.Radius;

            var v = s.Center - position;
            if (v.LengthSquared() <= r2) return true;

            var distAlongRay = v.Dot(direction);
            if (distAlongRay < 0) return false;

            var closestPointToCenter = position + direction * distAlongRay;
            var minDist2 = (closestPointToCenter - s.Center).LengthSquared();

            return (minDist2 <= r2);
        }

        public Ray TransformedBy(Matrix m)
        {
            // NOTE: This method performs no normalization. If the matrix
            // results in any scaling, the ray's direction will also be scaled,
            // possibly resulting in a non-unit vector direction, which could
            // affect other calculations.

            var pos = m.Transform(this.Position);
            var dir0 = this.Direction.ToVector4(w: 0);
            var dir1 = m.Transform(dir0);
            var dir = dir1.ToVectorXYZ();
            return new Ray(pos, dir);
        }

        public override string ToString()
        {
            return string.Format("{{Position:{0} Direction:{1}}}", Position, Direction);
        }
    }
}

