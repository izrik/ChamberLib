using System;

namespace ChamberLib
{
    public struct Quaternion
    {
        public Quaternion(float x, float y, float z , float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
        public Quaternion(Vector3 v, float w)
            : this(v.X, v.Y, v.Z, w)
        {
        }

        public float X;
        public float Y;
        public float Z;
        public float W;

        public static readonly Quaternion Identity = new Quaternion(0, 0, 0, 1);

        public static Quaternion CreateFromAxisAngle(Vector3 axis, float angle)
        {
            float c = (float)Math.Cos(angle);
            float s = (float)Math.Sin(angle);

            return new Quaternion(axis.Normalized() * s, c);
        }

        public static Quaternion Multiply(Quaternion a, Quaternion b)
        {
            float a1 = a.W;
            float a2 = b.W;
            float b1 = a.X;
            float b2 = b.X;
            float c1 = a.Y;
            float c2 = b.Y;
            float d1 = a.Z;
            float d2 = b.Z;

            return new Quaternion(
                a1 * b2 + a2 * b1 + c1 * d2 - c2 * d1,
                a1 * c2 - b1 * d2 + a2 * c1 + b2 * d1,
                a1 * d2 + b1 * c2 - b2 * c1 + a2 * d1,
                a1 * a2 - b1 * b2 - c1 * c2 - d1 * d2);
        }

        public static Quaternion Add(Quaternion a, Quaternion b)
        {
            return new Quaternion(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
        }

        public float LengthSquared
        {
            get { return X * X + Y * Y + Z * Z + W * W;}
        }

        public float Length
        {
            get { return (float)Math.Sqrt(LengthSquared); }
        }

        public static Quaternion operator *(Quaternion a, Quaternion b)
        {
            return Multiply(a, b);
        }

        public static Quaternion operator +(Quaternion a, Quaternion b)
        {
            return Add(a, b);
        }

        public Vector3 Transform(Vector3 v)
        {
            var q = (this * new Quaternion(v, 0)) * this.Conjugate();
            return new Vector3(q.X, q.Y, q.Z);
        }

        public Quaternion Conjugate()
        {
            return new Quaternion(-X, -Y, -Z, W);
        }

        public Quaternion Normalized()
        {
            var n = 1.0f / this.Length;

            return new Quaternion(X * n, Y * n, Z * n, W * n);
        }
    }
}

