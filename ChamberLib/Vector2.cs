using System;

namespace ChamberLib
{
    public struct Vector2
    {
        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float X;
        public float Y;


        public static readonly Vector2 Zero = new Vector2(0, 0);
        public static readonly Vector2 One = new Vector2(1, 1);
        public static readonly Vector2 UnitX = new Vector2(1, 0);
        public static readonly Vector2 UnitY = new Vector2(0, 1);

        public static Vector2 operator -(Vector2 v)
        {
            return new Vector2(-v.X, -v.Y);
        }
        public static Vector2 operator -(Vector2 x, Vector2 y)
        {
            return new Vector2(x.X - y.X, x.Y - y.Y);
        }
        public static Vector2 operator +(Vector2 x, Vector2 y)
        {
            return new Vector2(x.X + y.X, x.Y + y.Y);
        }
        public static Vector2 operator *(Vector2 v, float s)
        {
            return new Vector2(v.X * s, v.Y * s);
        }
        public static Vector2 operator *(float s, Vector2 v)
        {
            return new Vector2(v.X * s, v.Y * s);
        }
        public static Vector2 operator /(Vector2 v, float s)
        {
            return new Vector2(v.X / s, v.Y / s);
        }
        public static bool operator ==(Vector2 u, Vector2 v)
        {
            return u.Equals(v);
        }
        public static bool operator !=(Vector2 u, Vector2 v)
        {
            return !u.Equals(v);
        }

        public bool Equals(Vector2 other)
        {
            return (this.X == other.X &&
                this.Y == other.Y);
        }
        public override bool Equals(object other)
        {
            if (other is Vector2)
            {
                return Equals((Vector2)other);
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

        public float Length()
        {
            return (float)Math.Sqrt(this.LengthSquared());
        }

        public float LengthSquared()
        {
            return Vector2.Dot(this, this);
        }

        public static float Dot(Vector2 a, Vector2 b)
        {
            return a.X * b.X + a.Y * b.Y;
        }

        public float ToAngle()
        {
            if (this.LengthSquared() > 0)
            {
                return (float)Math.Atan2(this.Y, this.X);
            }
            else
            {
                return 0;
            }
        }
        public static Vector2 FromAngle(float angle)
        {
            return FromAngle((double)angle);
        }
        public static Vector2 FromAngle(double angle)
        {
            return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }

        public Vector2 Normalized()
        {
            return Normalize(this);
        }

        public static Vector2 Normalize(Vector2 v)
        {
            if (v.LengthSquared() > 0)
            {
                return v / v.Length();
            }
            else
            {
                return Zero;
            }
        }

        public Vector2 Round()
        {
            return new Vector2(X.RoundToInt(), Y.RoundToInt());
        }



//        public static Vector3 FromAngleAboutY(double angle)
//        {
//            return FromAngleAboutY((float)angle);
//        }
//        public static Vector3 FromAngleAboutY(float angle)
//        {
//            return new Vector3((float)Math.Cos(angle), 0, (float)Math.Sin(angle));
//        }

//        public static Vector2 ToVectorXZ(this Vector3 value)
//        {
//            return new Vector2(value.X, value.Z);
//        }

        public static float Distance(Vector2 u, Vector2 v)
        {
            return (u - v).Length();
        }

        public static float DistanceSquared(Vector2 u, Vector2 v)
        {
            return (u - v).LengthSquared();
        }

        public static Vector2 Max(Vector2 u, Vector2 v)
        {
            return new Vector2(
                Math.Max(u.X, v.X),
                Math.Max(u.Y, v.Y));
        }

        public override string ToString()
        {
            return string.Format("{{X:{0} Y:{1}}}", X, Y);
        }
    }
}

