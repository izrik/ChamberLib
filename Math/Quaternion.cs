using System;
using System.Diagnostics;

namespace ChamberLib
{
    public struct Quaternion : IFormattable
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

        // q = Xi + Yj +Zk + W
        // ii = jj = kk = ijk = -1
        // ij = k, ji = -k
        // jk = i, kj = -i
        // ki = j, ik = -j

        public readonly float X;
        public readonly float Y;
        public readonly float Z;
        public readonly float W;

        public static readonly Quaternion Identity = new Quaternion(0, 0, 0, 1);

        public static Quaternion CreateFromAxisAngle(Vector3 axis, float angle)
        {
            float c = (float)Math.Cos(angle/2);
            float s = (float)Math.Sin(angle/2);

            return new Quaternion(axis.Normalized() * s, c);
        }

        public void ToAxisAngle(out Vector3 axis, out float angle)
        {
            var v = ToAxisAngle();
            axis = v.ToVectorXYZ();
            angle = v.W;
        }

        public Vector4 ToAxisAngle()
        {
            if (this.X == 0.0f &&
                this.Y == 0.0f &&
                this.Z == 0.0f &&
                this.W == 0.0f)
            {
                return Vector4.UnitX;
            }

            var q = this.Normalized();

            if (q == Quaternion.Identity)
                return Vector4.UnitX;

            float angle = (float)(2*Math.Acos(q.W));
            var v = (angle != 0 ? new Vector3(q.X, q.Y, q.Z) : Vector3.UnitX);
            Vector3 axis = v / (float)Math.Sin(angle/2);

            return NormalizeAxisAngle(axis, angle);
        }

        public Vector4 AxisAngle
        {
            get
            {
                return ToAxisAngle();
            }
        }

        public static Vector4 NormalizeAxisAngle(Vector4 axisAngle)
        {
            var _axis = axisAngle.ToVectorXYZ();

            if (_axis == Vector3.Zero)
            {
                return Vector4.UnitX;
            }
            var angle = axisAngle.W;
            if (angle == 0)
            {
                return Vector4.UnitX;
            }

            var axis = _axis.Normalized();
            var axisAngle2 = axis.ToVector4(angle);

            if (axisAngle2.X != 0)
            {
                if (axisAngle2.X < 0)
                    axisAngle2 = -axisAngle2;
            }
            else if (axisAngle2.Y != 0)
            {
                if (axisAngle2.Y < 0)
                    axisAngle2 = -axisAngle2;
            }
            else if (axisAngle2.Z < 0)
            {
                axisAngle2 = -axisAngle2;
            }

            if (axis.LengthSquared() >= 1.0001f)
            {
                Debug.Assert(false, "Axis vector is not normalized");
            }

            return axisAngle2;
        }
        public static void NormalizeAxisAngle(ref Vector3 axis, ref float angle)
        {
            var axisAngle = NormalizeAxisAngle(axis.ToVector4(angle));
            axis = axisAngle.ToVectorXYZ();
            angle = axisAngle.W;
        }
        public static Vector4 NormalizeAxisAngle(Vector3 axis, float angle)
        {
            return NormalizeAxisAngle(axis.ToVector4(angle));
        }

        public static Vector4 NormalizeAxisAngleOrientation(Vector4 axisAngle, float delta=0)
        {
            // TODO: define and document "orientation"

            var _axis = axisAngle.ToVectorXYZ();

            if (_axis == Vector3.Zero)
            {
                return Vector4.UnitX;
            }
            var angle = axisAngle.W;
            if (angle == 0)
            {
                return Vector4.UnitX;
            }

            var axis = _axis.Normalized();

            if (Math.Abs(axis.X) > delta)
            {
                if (axis.X < 0)
                {
                    axis = -axis;
                    angle = -angle;
                }
            }
            else if (Math.Abs(axis.Y) > delta)
            {
                if (axis.Y < 0)
                {
                    axis = -axis;
                    angle = -angle;
                }
            }
            else if (axis.Z < 0)
            {
                axis = -axis;
                angle = -angle;
            }

            angle = (float)(angle % (2 * Math.PI) + (angle < 0 ? 2 * Math.PI : 0));

            var axisAngle2 = axis.ToVector4(angle);

            if (axis.LengthSquared() >= 1.0001f)
            {
                Debug.Assert(false, "Axis vector is not normalized");
            }

            return axisAngle2;
        }

        public Vector3 ToModulatedAxis()
        {
            var v = ToAxisAngle();
            return v.ToVectorXYZ() * v.W;
        }

        public static Quaternion FromRotationX(float angle)
        {
            return CreateFromAxisAngle(Vector3.UnitX, angle);
        }

        public static Quaternion FromRotationY(float angle)
        {
            return CreateFromAxisAngle(Vector3.UnitY, angle);
        }

        public static Quaternion FromRotationZ(float angle)
        {
            return CreateFromAxisAngle(Vector3.UnitZ, angle);
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

        public static Quaternion Multiply(Quaternion q, float s)
        {
            return new Quaternion(q.X * s, q.Y * s, q.Z * s, q.W * s);
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

        public override bool Equals(object obj)
        {
              if (!(obj is Quaternion))
                return false;

            var q = (Quaternion)obj;

            return
                this.X.Equals(q.X) &&
                this.Y.Equals(q.Y) &&
                this.Z.Equals(q.Z) &&
                this.W.Equals(q.W);
        }

        public bool IsEquivalent(Quaternion other, float delta=0)
        {
            var thisV = this.ToAxisAngle();

            var otherV = other.ToAxisAngle();

            return
                Math.Abs(thisV.X - otherV.X) < delta &&
                Math.Abs(thisV.Y - otherV.Y) < delta &&
                Math.Abs(thisV.Z - otherV.Z) < delta &&
                Math.Abs(thisV.W - otherV.W) < delta;
        }

        public bool IsEquivalentOrientationTo(Quaternion other, float delta=0)
        {
            // TODO: define and document "orientation"

            var v1 = this.ToAxisAngle();
            var thisV = NormalizeAxisAngleOrientation(v1, delta);

            var v2 = other.ToAxisAngle();
            var otherV = NormalizeAxisAngleOrientation(v2, delta);

            return
                Math.Abs(thisV.X - otherV.X) <= delta &&
                Math.Abs(thisV.Y - otherV.Y) <= delta &&
                Math.Abs(thisV.Z - otherV.Z) <= delta &&
                Math.Abs(thisV.W - otherV.W) <= delta;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^
                    Y.GetHashCode() ^
                    Z.GetHashCode() ^
                    W.GetHashCode();
        }

        public static bool operator ==(Quaternion a, Quaternion b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Quaternion a, Quaternion b)
        {
            return !a.Equals(b);
        }

        public static Quaternion operator *(Quaternion a, Quaternion b)
        {
            return Multiply(a, b);
        }

        public static Quaternion operator *(Quaternion q, float s)
        {
            return Multiply(q, s);
        }

        public static Quaternion operator *(float s, Quaternion q)
        {
            return Multiply(q, s);
        }

        public static Quaternion operator +(Quaternion a, Quaternion b)
        {
            return Add(a, b);
        }

        public static Quaternion operator-(Quaternion q)
        {
            return new Quaternion(-q.X, -q.Y, -q.Z, -q.W);
        }

        public Vector3 Transform(Vector3 v)
        {
            var m = this.ToMatrix();
            return m.Transform(v);
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

        public Matrix ToMatrix()
        {
            return Matrix.CreateFromQuaternion(this);
        }

        public Vector4 ToVector4()
        {
            return new Vector4(X, Y, Z, W);
        }

        public static Quaternion FromVector4(Vector4 v)
        {
            return new Quaternion(v.X, v.Y, v.Z, v.W);
        }

        public static Quaternion Lerp(Quaternion a, Quaternion b, float s)
        {
            a = a.Normalized();
            b = b.Normalized();

            return Quaternion.FromVector4(
                a.ToVector4() * s + 
                b.ToVector4() * (1 - s)).Normalized();
        }

        public static Quaternion Slerp(Quaternion a, Quaternion b, float s)
        {
            a = a.Normalized();
            b = b.Normalized();

            float dot =
                a.X * b.X +
                a.Y * b.Y +
                a.Z * b.Z +
                a.W * b.W;
            float omega = (float)Math.Acos(dot);
            float sinomega = (float)Math.Sin(omega);

            return 
                (a * (float)(Math.Sin((1 - s) * omega) / sinomega) +
                 b * (float)(Math.Sin(s * omega) / sinomega)).Normalized();
        }

        public static Quaternion FromRotationMatrix(Matrix m)
        {
            var qxx = m.M11;
            var qxy = m.M12;
            var qxz = m.M13;
            var qyx = m.M21;
            var qyy = m.M22;
            var qyz = m.M23;
            var qzx = m.M31;
            var qzy = m.M32;
            var qzz = m.M33;

            var trace = qxx + qyy + qzz;
            if (trace > 0)
            {
                var r = (float)Math.Sqrt(1 + trace);
                var s = 0.5f / r;
                var w = 0.5f * r;
                var x = (qyz - qzy) * s;
                var y = (qzx - qxz) * s;
                var z = (qxy - qyx) * s;

                return new Quaternion(x, y, z, w);
            }
            if (qxx > qyy && qxx > qzz)
            {
                var r = (float)Math.Sqrt(1 + qxx - qyy - qzz);
                var s = 0.5f / r;
                var w = (qyz - qzy) * s;
                var x = 0.5f * r;
                var y = (qxy + qyx) * s;
                var z = (qzx + qxz) * s;
                return new Quaternion(x, y, z, w);
            }
            if (qyy > qzz)
            {
                var r = (float)Math.Sqrt(1 - qxx + qyy - qzz);
                var s = 0.5f / r;
                var w = (qzx - qxz) * s;
                var x = (qxy + qyx) * s;
                var y = 0.5f * r;
                var z = (qzy + qyz) * s;
                return new Quaternion(x, y, z, w);
            }
            else
            {
                var r = (float)Math.Sqrt(1 - qxx - qyy + qzz);
                var s = 0.5f / r;
                var w = (qxy - qyx) * s;
                var x = (qxz + qzx) * s;
                var y = (qzy + qyz) * s;
                var z = 0.5f * r;
                return new Quaternion(x, y, z, w);
            }
        }

        public static Quaternion CreateFromRotationMatrix2(Matrix matrix)
        {
            float trace = matrix.M11 + matrix.M22 + matrix.M33;
            Quaternion result;
            if (trace > 0)
            {
                float r = (float)Math.Sqrt((double)(trace + 1));
                float W = r * 0.5f;
                float s = 0.5f / r;
                float X = (matrix.M23 - matrix.M32) * s;
                float Y = (matrix.M31 - matrix.M13) * s;
                float Z = (matrix.M12 - matrix.M21) * s;
                result = new Quaternion(X,Y,Z,W);
            }
            else
            {
                if (matrix.M11 >= matrix.M22 && matrix.M11 >= matrix.M33)
                {
                    float r = (float)Math.Sqrt((double)(1 + matrix.M11 - matrix.M22 - matrix.M33));
                    float s = 0.5f / r;
                    float X = 0.5f * r;
                    float Y = (matrix.M12 + matrix.M21) * s;
                    float Z = (matrix.M13 + matrix.M31) * s;
                    float W = (matrix.M23 - matrix.M32) * s;
                    result = new Quaternion(X,Y,Z,W);
                }
                else
                {
                    if (matrix.M22 > matrix.M33)
                    {
                        float r = (float)Math.Sqrt((double)(1 + matrix.M22 - matrix.M11 - matrix.M33));
                        float s = 0.5f / r;
                        float X = (matrix.M21 + matrix.M12) * s;
                        float Y = 0.5f * r;
                        float Z = (matrix.M32 + matrix.M23) * s;
                        float W = (matrix.M31 - matrix.M13) * s;
                        result = new Quaternion(X,Y,Z,W);
                    }
                    else
                    {
                        float r = (float)Math.Sqrt((double)(1 + matrix.M33 - matrix.M11 - matrix.M22));
                        float s = 0.5f / r;
                        float X = (matrix.M31 + matrix.M13) * s;
                        float Y = (matrix.M32 + matrix.M23) * s;
                        float Z = 0.5f * r;
                        float W = (matrix.M12 - matrix.M21) * s;
                        result = new Quaternion(X,Y,Z,W);
                    }
                }
            }
            return result;
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

        public static Quaternion FromAngleBetweenVectors(Vector3 from,
            Vector3 to)
        {
            from = from.Normalized();
            to = to.Normalized();
            Vector3 c = Vector3.Cross(from, to);
            var axis = c.Normalized();
            var angle = (float)Math.Asin(c.Length());
            return Quaternion.CreateFromAxisAngle(axis, angle);
        }
    }
}

