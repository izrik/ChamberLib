using System;

namespace ChamberLib
{
    public struct Vector4 : IFormattable
    {
        public Vector4(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public float X;
        public float Y;
        public float Z;
        public float W;

        public static readonly Vector4 Zero = new Vector4(0, 0, 0, 0);
        public static readonly Vector4 One = new Vector4(1, 1, 1, 0);
        public static readonly Vector4 UnitX = new Vector4(1, 0, 0, 0);
        public static readonly Vector4 UnitY = new Vector4(0, 1, 0, 0);
        public static readonly Vector4 UnitZ = new Vector4(0, 0, 1, 0);
        public static readonly Vector4 UnitW = new Vector4(0, 0, 0, 1);

        public static Vector4 operator -(Vector4 v)
        {
            return new Vector4(-v.X, -v.Y, -v.Z, -v.W);
        }
        public static Vector4 operator -(Vector4 x, Vector4 y)
        {
            return new Vector4(x.X - y.X, x.Y - y.Y, x.Z - y.Z, x.W - y.W);
        }
        public static Vector4 operator +(Vector4 x, Vector4 y)
        {
            return new Vector4(x.X + y.X, x.Y + y.Y, x.Z + y.Z, x.W + y.W);
        }
        public static Vector4 operator *(Vector4 v, float s)
        {
            return new Vector4(v.X * s, v.Y * s, v.Z * s, v.W * s);
        }
        public static Vector4 operator *(float s, Vector4 v)
        {
            return new Vector4(v.X * s, v.Y * s, v.Z * s, v.W * s);
        }
        public static Vector4 operator /(Vector4 v, float s)
        {
            return new Vector4(v.X / s, v.Y / s, v.Z / s, v.W / s);
        }
        public static bool operator ==(Vector4 u, Vector4 v)
        {
            return u.Equals(v);
        }
        public static bool operator !=(Vector4 u, Vector4 v)
        {
            return !u.Equals(v);
        }

        public bool Equals(Vector4 other)
        {
            return (this.X == other.X &&
                this.Y == other.Y &&
                this.Z == other.Z &&
                this.W == other.W);
        }
        public override bool Equals(object obj)
        {
            if (obj is Vector4)
            {
                return Equals((Vector4)obj);
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode() ^ W.GetHashCode();
        }

        public float Length()
        {
            return (float)Math.Sqrt(this.LengthSquared());
        }

        public float LengthSquared()
        {
            return Vector4.Dot(this, this);
        }

        public static float Dot(Vector4 a, Vector4 b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z + a.W * b.W;
        }

        public Vector4 Normalized()
        {
            return Normalize(this);
        }

        public static Vector4 Normalize(Vector4 v)
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

        public Vector4 Round()
        {
            return new Vector4(X.RoundToInt(), Y.RoundToInt(), Z.RoundToInt(), W.RoundToInt());
        }

        public static float Distance(Vector4 u, Vector4 v)
        {
            return (u - v).Length();
        }

        public static float DistanceSquared(Vector4 u, Vector4 v)
        {
            return (u - v).LengthSquared();
        }

        public static Vector4 Max(Vector4 u, Vector4 v)
        {
            return new Vector4(
                Math.Max(u.X, v.X),
                Math.Max(u.Y, v.Y),
                Math.Max(u.Z, v.Z),
                Math.Max(u.W, v.W));
        }

        public static Vector4 Transform(Vector4 v, Matrix m)
        {
            return new Vector4(
                v.X * m.M11 + v.Y * m.M21 + v.Z * m.M31 + v.W * m.M41,
                v.X * m.M12 + v.Y * m.M22 + v.Z * m.M32 + v.W * m.M42,
                v.X * m.M13 + v.Y * m.M23 + v.Z * m.M33 + v.W * m.M43,
                v.X * m.M14 + v.Y * m.M24 + v.Z * m.M34 + v.W * m.M44);
        }

        public Vector3 ToVectorXYZ()
        {
            return new Vector3(X, Y, Z);
        }

        public override string ToString()
        {
            return string.Format("{{X:{0} Y:{1} Z:{2} W:{3}}}", X, Y, Z, W);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Format(
                "{{X:{0} Y:{1} Z:{2} W:{3}}}",
                X.ToString(format, formatProvider),
                Y.ToString(format, formatProvider),
                Z.ToString(format, formatProvider),
                W.ToString(format, formatProvider));
        }
    }
}

