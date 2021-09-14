using System;

namespace ChamberLib
{
    public struct Vector3 : IFormattable, IEquatable<Vector3>
    {
        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public float X;
        public float Y;
        public float Z;

        public static readonly Vector3 Zero = new Vector3(0, 0, 0);
        public static readonly Vector3 One = new Vector3(1, 1, 1);
        public static readonly Vector3 UnitX = new Vector3(1, 0, 0);
        public static readonly Vector3 UnitY = new Vector3(0, 1, 0);
        public static readonly Vector3 UnitZ = new Vector3(0, 0, 1);

        public static Vector3 operator -(Vector3 v)
        {
            return new Vector3(-v.X, -v.Y, -v.Z);
        }
        public static Vector3 operator -(Vector3 x, Vector3 y)
        {
            return new Vector3(x.X - y.X, x.Y - y.Y, x.Z - y.Z);
        }
        public static Vector3 operator +(Vector3 x, Vector3 y)
        {
            return new Vector3(x.X + y.X, x.Y + y.Y, x.Z + y.Z);
        }
        public static Vector3 operator *(Vector3 v, float s)
        {
            return new Vector3(v.X * s, v.Y * s, v.Z * s);
        }
        public static Vector3 operator *(float s, Vector3 v)
        {
            return new Vector3(v.X * s, v.Y * s, v.Z * s);
        }
        public static Vector3 operator /(Vector3 v, float s)
        {
            return new Vector3(v.X / s, v.Y / s, v.Z / s);
        }
        public static bool operator ==(Vector3 u, Vector3 v)
        {
            return u.Equals(v);
        }
        public static bool operator !=(Vector3 u, Vector3 v)
        {
            return !u.Equals(v);
        }

        public bool Equals(Vector3 other)
        {
            return (this.X == other.X &&
                this.Y == other.Y &&
                this.Z == other.Z);
        }
        public override bool Equals(object obj)
        {
            if (obj is Vector3)
            {
                return Equals((Vector3)obj);
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            var x = 113 * X.GetHashCode();
            x = (x ^ 127) * Y.GetHashCode();
            return x ^ Z.GetHashCode();
        }

        public float Length()
        {
            return (float)Math.Sqrt(this.LengthSquared());
        }

        public float LengthSquared()
        {
            return Vector3.Dot(this, this);
        }

        public static float Dot(Vector3 a, Vector3 b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }
        public float Dot(Vector3 v)
        {
            return Dot(this, v);
        }

        public static Vector3 Cross(Vector3 a, Vector3 b)
        {
            return new Vector3(
                a.Y * b.Z - a.Z * b.Y,
                a.Z * b.X - a.X * b.Z,
                a.X * b.Y - a.Y * b.X);
        }

        public Vector3 Normalized()
        {
            return Normalize(this);
        }

        public static Vector3 Normalize(Vector3 v)
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

        public Vector3 Round()
        {
            return new Vector3(X.RoundToInt(), Y.RoundToInt(), Z.RoundToInt());
        }

        public static float Distance(Vector3 u, Vector3 v)
        {
            return (u - v).Length();
        }

        public static float DistanceSquared(Vector3 u, Vector3 v)
        {
            return (u - v).LengthSquared();
        }

        public static Vector3 Max(Vector3 u, Vector3 v)
        {
            return new Vector3(
                Math.Max(u.X, v.X),
                Math.Max(u.Y, v.Y),
                Math.Max(u.Z, v.Z));
        }

        public static Vector3 Min(Vector3 u, Vector3 v)
        {
            return new Vector3(
                Math.Min(u.X, v.X),
                Math.Min(u.Y, v.Y),
                Math.Min(u.Z, v.Z));
        }

        public static Vector3 Lerp(Vector3 u, Vector3 v, float s,
            bool clampToEnds=true)
        {
            if (clampToEnds)
                s = s.Clamp(0, 1);
            return u * (1 - s) + v * s;
        }

        public static Vector3 Transform(Vector3 v, Matrix m)
        {
            return new Vector3(
                v.X * m.M11 + v.Y * m.M21 + v.Z * m.M31 + m.M41,
                v.X * m.M12 + v.Y * m.M22 + v.Z * m.M32 + m.M42,
                v.X * m.M13 + v.Y * m.M23 + v.Z * m.M33 + m.M43);
        }

        public static Vector3 FromAngleAboutY(double angle)
        {
            return FromAngleAboutY((float)angle);
        }
        public static Vector3 FromAngleAboutY(float angle)
        {
            return new Vector3((float)Math.Cos(angle), 0, (float)Math.Sin(angle));
        }

        public Vector3 RotateAboutAxis(Vector3 axis, float angle)
        {
            var q = Quaternion.CreateFromAxisAngle(axis, angle);
            return q.Transform(this);
        }

        public override string ToString()
        {
            return string.Format("{{X:{0} Y:{1} Z:{2}}}", X, Y, Z);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Format(
                "{{X:{0} Y:{1} Z:{2}}}",
                X.ToString(format, formatProvider),
                Y.ToString(format, formatProvider),
                Z.ToString(format, formatProvider));
        }

        public Vector4 ToVector4(float w=0)
        {
            return new Vector4(X, Y, Z, w);
        }

        public Vector2 ToVector2()
        {
            return new Vector2(X, Y);
        }
    }
}

