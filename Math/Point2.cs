
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
    public struct Point2
    {
        public Point2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X;
        public int Y;

        public static readonly Point2 Zero = new Point2(0, 0);
        public static readonly Point2 One = new Point2(1, 1);
        public static readonly Point2 UnitX = new Point2(1, 0);
        public static readonly Point2 UnitY = new Point2(0, 1);

        public static Point2 operator -(Point2 v)
        {
            return new Point2(-v.X, -v.Y);
        }
        public static Point2 operator -(Point2 x, Point2 y)
        {
            return new Point2(x.X - y.X, x.Y - y.Y);
        }
        public static Point2 operator +(Point2 x, Point2 y)
        {
            return new Point2(x.X + y.X, x.Y + y.Y);
        }
        public static bool operator ==(Point2 u, Point2 v)
        {
            return u.Equals(v);
        }
        public static bool operator !=(Point2 u, Point2 v)
        {
            return !u.Equals(v);
        }

        public bool Equals(Point2 other)
        {
            return (this.X == other.X &&
                this.Y == other.Y);
        }
        public override bool Equals(object other)
        {
            if (other is Point2)
            {
                return Equals((Point2)other);
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        public static Point2 Max(Point2 u, Point2 v)
        {
            return new Point2(
                Math.Max(u.X, v.X),
                Math.Max(u.Y, v.Y));
        }

        public override string ToString()
        {
            return string.Format("{{X:{0} Y:{1}}}", X, Y);
        }

        public Vector2 ToVector2()
        {
            return new Vector2(X, Y);
        }
    }
}

