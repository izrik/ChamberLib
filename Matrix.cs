using System;
using System.Collections.Generic;
using System.Linq;

namespace ChamberLib
{
    public struct Matrix
    {
        public static readonly Matrix Identity = new Matrix(1,0,0,0,
                                                            0,1,0,0,
                                                            0,0,1,0,
                                                            0,0,0,1);

        public static readonly Matrix Zero = new Matrix(0,0,0,0,
                                                        0,0,0,0,
                                                        0,0,0,0,
                                                        0,0,0,0);

        public Matrix(
            float m11, float m12, float m13, float m14,
            float m21, float m22, float m23, float m24,
            float m31, float m32, float m33, float m34,
            float m41, float m42, float m43, float m44)
        {
            M11 = m11;
            M12 = m12;
            M13 = m13;
            M14 = m14;
            M21 = m21;
            M22 = m22;
            M23 = m23;
            M24 = m24;
            M31 = m31;
            M32 = m32;
            M33 = m33;
            M34 = m34;
            M41 = m41;
            M42 = m42;
            M43 = m43;
            M44 = m44;
        }

        public Matrix Transposed()
        {
            return new Matrix(
                M11, M21, M31, M41,
                M12, M22, M32, M42,
                M13, M23, M33, M43,
                M14, M24, M34, M44);
        }

        public static Matrix CreateLookAt(Vector3 cameraPosition, Vector3 cameraTarget, Vector3 cameraUpVector)
        {
            var zaxis = (cameraPosition - cameraTarget).Normalized();
            var xaxis = (Vector3.Cross(cameraUpVector, zaxis)).Normalized();
            var yaxis = Vector3.Cross(zaxis, xaxis).Normalized();

            return new Matrix(
                xaxis.X, yaxis.X, zaxis.X, 0,
                xaxis.Y, yaxis.Y, zaxis.Y, 0,
                xaxis.Z, yaxis.Z, zaxis.Z, 0,
                -Vector3.Dot(xaxis, cameraPosition), -Vector3.Dot(yaxis, cameraPosition), -Vector3.Dot(zaxis, cameraPosition), 1);
        }

        public static Matrix CreatePerspective(float width, float height, float zNearPlane, float zFarPlane)
        {
            float diff = zNearPlane - zFarPlane;
            return new Matrix(
                2*zNearPlane/width, 0, 0, 0,
                0, 2*zNearPlane/height, 0, 0,
                0, 0, zFarPlane / diff, -1,
                0, 0, zFarPlane * zNearPlane / diff, 0);
        }

        public static Matrix CreatePerspectiveOffCenter(float left, float right, float bottom, float top, float near, float far)
        {
            float width = right - left;
            float cx2 = (right + left);
            float height = top - bottom;
            float cy2 = (top + bottom);
            float diff = near - far;
            return new Matrix(
                2*near/width, 0, 0, 0,
                0, 2*near/height, 0, 0,
                cx2/width, cy2/height, far / diff, -1,
                0, 0, far * near / diff, 0);
        }

        public static Matrix CreateOrthographic(float width, float height, float zNearPlane, float zFarPlane)
        {
            float diff = zFarPlane - zNearPlane;
            return new Matrix(
                2 / width, 0, 0, 0,
                0, 2 / height, 0, 0,
                0, 0, -1 / diff, 0,
                0, 0, -zNearPlane / diff, 1);
        }

        public static Matrix CreateOrthographicOffCenter(float left, float right, float bottom, float top, float near, float far)
        {
            float width = right - left;
            float cx2 = (right + left);
            float height = top - bottom;
            float cy2 = (top + bottom);
            float diff = far - near;
            return new Matrix(
                2 / width, 0, 0, 0,
                0, 2 / height, 0, 0,
                0, 0, -1 / diff, 0,
                -cx2/width, -cy2/height, -near/ diff, 1);
        }

        public static Matrix CreateScale(float s)
        {
            return CreateScale(s, s, s);
        }

        public static Matrix CreateScale(Vector3 s)
        {
            return CreateScale(s.X, s.Y, s.Z);
        }

        public static Matrix CreateScale(float x, float y, float z)
        {
            return new Matrix(
                x, 0, 0, 0,
                0, y, 0, 0,
                0, 0, z, 0,
                0, 0, 0, 1);
        }

        public static Matrix CreateTranslation(float x, float y, float z)
        {
            return new Matrix(
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                x, y, z, 1);
        }

        public static Matrix CreateTranslation(Vector3 v)
        {
            return new Matrix(
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                v.X, v.Y, v.Z, 1);

        }

        public static Matrix CreateWorld(Vector3 position, Vector3 forward, Vector3 up)
        {
            var forward2 = -forward.Normalized();
            var right = Vector3.Cross(forward, up.Normalized()).Normalized();
            var up2 = Vector3.Cross(right, forward).Normalized();

            return new Matrix(
                right.X, right.Y, right.Z, 0,
                up2.X, up2.Y, up2.Z, 0,
                forward2.X, forward2.Y, forward2.Z, 0,
                position.X, position.Y, position.Z, 1);
        }

        public static Matrix CreateRotationX(float radians)
        {
            float c = (float)Math.Cos(radians);
            float s = (float)Math.Sin(radians);
            return new Matrix(
                1, 0, 0, 0,
                0, c, s, 0,
                0, -s, c, 0,
                0, 0, 0, 1);
        }

        public static Matrix CreateRotationY(float radians)
        {
            float c = (float)Math.Cos(radians);
            float s = (float)Math.Sin(radians);
            return new Matrix(
                c, 0, -s, 0,
                0, 1, 0, 0,
                s, 0, c, 0,
                0, 0, 0, 1);
        }

        public static Matrix CreateRotationZ(float radians)
        {
            float c = (float)Math.Cos(radians);
            float s = (float)Math.Sin(radians);
            return new Matrix(
                c, s, 0, 0,
                -s, c, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1);
        }

        public static Matrix CreateFromQuaternion(Quaternion rotation)
        {
            var q2 = rotation;

            var x = q2.X;
            var y = q2.Y;
            var z = q2.Z;
            var w = q2.W;

            var wx = 2 * w * x;
            var wy = 2 * w * y;
            var wz = 2 * w * z;
            var xx = 2 * x * x;
            var xy = 2 * x * y;
            var xz = 2 * x * z;
            var yy = 2 * y * y;
            var yz = 2 * y * z;
            var zz = 2 * z * z;

            return new Matrix(
                1 - (yy + zz), xy + wz, xz - wy, 0,
                xy - wz, 1 - (xx + zz), yz + wx, 0,
                xz + wy, yz - wx, 1 - (xx + yy), 0,
                0, 0, 0, 1);
        }

        public static Matrix Compose(Vector3 scale, Quaternion rotation, Vector3 translation)
        {
            var sm = Matrix.CreateScale(scale);
            var rm = Matrix.CreateFromQuaternion(rotation);
            var tm = Matrix.CreateTranslation(translation);
            return sm * rm * tm;
        }

        public void Decompose(out Vector3 scale, out Quaternion rotation, out Vector3 translation)
        {
            var m = this;

            if (m.EnumerateValuesRowMajor().Any(f => float.IsNaN(f)))
            {
                translation = new Vector3(float.NaN, float.NaN, float.NaN);
                rotation = new Quaternion(float.NaN, float.NaN, float.NaN, float.NaN);
                scale = new Vector3(float.NaN, float.NaN, float.NaN);
                return;
            }

            translation = new Vector3(m.M41, m.M42, m.M43);
            m = new Matrix(
                m.M11, m.M12, m.M13, m.M14,
                m.M21, m.M22, m.M23, m.M24,
                m.M31, m.M32, m.M33, m.M34,
                0, 0, 0, m.M44);

            scale = new Vector3(
                m.Transform(Vector3.UnitX).Length(),
                m.Transform(Vector3.UnitY).Length(),
                m.Transform(Vector3.UnitZ).Length());

            var s = Matrix.CreateScale(scale).Inverted();

            m = m * s;

            if (m.EnumerateValuesRowMajor().Any(f => float.IsNaN(f)))
            {
                rotation = new Quaternion(float.NaN, float.NaN, float.NaN, float.NaN);
                scale = new Vector3(float.NaN, float.NaN, float.NaN);
            }

            rotation = Quaternion.FromRotationMatrix(m);
        }
        public Tuple<Vector3, Quaternion, Vector3> Decompose()
        {
            Vector3 translation;
            Quaternion rotation;
            Vector3 scale;

            this.Decompose(out scale, out rotation, out translation);

            return new Tuple<Vector3, Quaternion, Vector3>(scale, rotation, translation);
        }

        public Vector3 DecomposedScale
        {
            get
            {
                return this.Decompose().Item1;
            }
        }
        public Quaternion DecomposedRotation
        {
            get
            {
                return this.Decompose().Item2;
            }
        }
        public Vector3 DecomposedTranslation
        {
            get
            {
                return this.Decompose().Item3;
            }
        }

        public readonly float M11;
        public readonly float M12;
        public readonly float M13;
        public readonly float M14;
        public readonly float M21;
        public readonly float M22;
        public readonly float M23;
        public readonly float M24;
        public readonly float M31;
        public readonly float M32;
        public readonly float M33;
        public readonly float M34;
        public readonly float M41;
        public readonly float M42;
        public readonly float M43;
        public readonly float M44;

        public IEnumerable<float> EnumerateValuesRowMajor()
        {
            yield return M11;
            yield return M12;
            yield return M13;
            yield return M14;
            yield return M21;
            yield return M22;
            yield return M23;
            yield return M24;
            yield return M31;
            yield return M32;
            yield return M33;
            yield return M34;
            yield return M41;
            yield return M42;
            yield return M43;
            yield return M44;
        }

        public IEnumerable<float> EnumerateValuesColumnMajor()
        {
            yield return M11;
            yield return M21;
            yield return M31;
            yield return M41;
            yield return M12;
            yield return M22;
            yield return M32;
            yield return M42;
            yield return M13;
            yield return M23;
            yield return M33;
            yield return M43;
            yield return M14;
            yield return M24;
            yield return M34;
            yield return M44;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Matrix))
                return false;

            var b = (Matrix)obj;

            return
                this.Column1 == b.Column1 &&
                this.Column2 == b.Column2 &&
                this.Column3 == b.Column3 &&
                this.Column4 == b.Column4;
        }

        public override int GetHashCode()
        {
            return
                M11.GetHashCode() ^
                M12.GetHashCode() ^
                M13.GetHashCode() ^
                M14.GetHashCode() ^
                M21.GetHashCode() ^
                M22.GetHashCode() ^
                M23.GetHashCode() ^
                M24.GetHashCode() ^
                M31.GetHashCode() ^
                M32.GetHashCode() ^
                M33.GetHashCode() ^
                M34.GetHashCode() ^
                M41.GetHashCode() ^
                M42.GetHashCode() ^
                M43.GetHashCode() ^
                M44.GetHashCode();
        }

        public static bool operator == (Matrix a, Matrix b)
        {
            return a.Equals(b);
        }

        public static bool operator != (Matrix a, Matrix b)
        {
            return !a.Equals(b);
        }

        public static Matrix Invert(Matrix m)
        {
            var det = m.Determinant();
            return new Matrix(
                (m.M23 * m.M34 * m.M42 - m.M24 * m.M33 * m.M42 + m.M24 * m.M32 * m.M43 - m.M22 * m.M34 * m.M43 - m.M23 * m.M32 * m.M44 + m.M22 * m.M33 * m.M44) / det,
                (m.M14 * m.M33 * m.M42 - m.M13 * m.M34 * m.M42 - m.M14 * m.M32 * m.M43 + m.M12 * m.M34 * m.M43 + m.M13 * m.M32 * m.M44 - m.M12 * m.M33 * m.M44) / det,
                (m.M13 * m.M24 * m.M42 - m.M14 * m.M23 * m.M42 + m.M14 * m.M22 * m.M43 - m.M12 * m.M24 * m.M43 - m.M13 * m.M22 * m.M44 + m.M12 * m.M23 * m.M44) / det,
                (m.M14 * m.M23 * m.M32 - m.M13 * m.M24 * m.M32 - m.M14 * m.M22 * m.M33 + m.M12 * m.M24 * m.M33 + m.M13 * m.M22 * m.M34 - m.M12 * m.M23 * m.M34) / det,
                (m.M24 * m.M33 * m.M41 - m.M23 * m.M34 * m.M41 - m.M24 * m.M31 * m.M43 + m.M21 * m.M34 * m.M43 + m.M23 * m.M31 * m.M44 - m.M21 * m.M33 * m.M44) / det,
                (m.M13 * m.M34 * m.M41 - m.M14 * m.M33 * m.M41 + m.M14 * m.M31 * m.M43 - m.M11 * m.M34 * m.M43 - m.M13 * m.M31 * m.M44 + m.M11 * m.M33 * m.M44) / det,
                (m.M14 * m.M23 * m.M41 - m.M13 * m.M24 * m.M41 - m.M14 * m.M21 * m.M43 + m.M11 * m.M24 * m.M43 + m.M13 * m.M21 * m.M44 - m.M11 * m.M23 * m.M44) / det,
                (m.M13 * m.M24 * m.M31 - m.M14 * m.M23 * m.M31 + m.M14 * m.M21 * m.M33 - m.M11 * m.M24 * m.M33 - m.M13 * m.M21 * m.M34 + m.M11 * m.M23 * m.M34) / det,
                (m.M22 * m.M34 * m.M41 - m.M24 * m.M32 * m.M41 + m.M24 * m.M31 * m.M42 - m.M21 * m.M34 * m.M42 - m.M22 * m.M31 * m.M44 + m.M21 * m.M32 * m.M44) / det,
                (m.M14 * m.M32 * m.M41 - m.M12 * m.M34 * m.M41 - m.M14 * m.M31 * m.M42 + m.M11 * m.M34 * m.M42 + m.M12 * m.M31 * m.M44 - m.M11 * m.M32 * m.M44) / det,
                (m.M12 * m.M24 * m.M41 - m.M14 * m.M22 * m.M41 + m.M14 * m.M21 * m.M42 - m.M11 * m.M24 * m.M42 - m.M12 * m.M21 * m.M44 + m.M11 * m.M22 * m.M44) / det,
                (m.M14 * m.M22 * m.M31 - m.M12 * m.M24 * m.M31 - m.M14 * m.M21 * m.M32 + m.M11 * m.M24 * m.M32 + m.M12 * m.M21 * m.M34 - m.M11 * m.M22 * m.M34) / det,
                (m.M23 * m.M32 * m.M41 - m.M22 * m.M33 * m.M41 - m.M23 * m.M31 * m.M42 + m.M21 * m.M33 * m.M42 + m.M22 * m.M31 * m.M43 - m.M21 * m.M32 * m.M43) / det,
                (m.M12 * m.M33 * m.M41 - m.M13 * m.M32 * m.M41 + m.M13 * m.M31 * m.M42 - m.M11 * m.M33 * m.M42 - m.M12 * m.M31 * m.M43 + m.M11 * m.M32 * m.M43) / det,
                (m.M13 * m.M22 * m.M41 - m.M12 * m.M23 * m.M41 - m.M13 * m.M21 * m.M42 + m.M11 * m.M23 * m.M42 + m.M12 * m.M21 * m.M43 - m.M11 * m.M22 * m.M43) / det,
                (m.M12 * m.M23 * m.M31 - m.M13 * m.M22 * m.M31 + m.M13 * m.M21 * m.M32 - m.M11 * m.M23 * m.M32 - m.M12 * m.M21 * m.M33 + m.M11 * m.M22 * m.M33) / det);
        }

        public Matrix Inverted()
        {
            return Matrix.Invert(this);
        }

        public float Determinant()
        {
            return 
                    M14*M23*M32*M41 - M13*M24*M32*M41 - M14*M22*M33*M41 + M12*M24*M33*M41+
                    M13*M22*M34*M41 - M12*M23*M34*M41 - M14*M23*M31*M42 + M13*M24*M31*M42+
                    M14*M21*M33*M42 - M11*M24*M33*M42 - M13*M21*M34*M42 + M11*M23*M34*M42+
                    M14*M22*M31*M43 - M12*M24*M31*M43 - M14*M21*M32*M43 + M11*M24*M32*M43+
                    M12*M21*M34*M43 - M11*M22*M34*M43 - M13*M22*M31*M44 + M12*M23*M31*M44+
                    M13*M21*M32*M44 - M11*M23*M32*M44 - M12*M21*M33*M44 + M11*M22*M33*M44;
        }

        public static Matrix operator * (Matrix m, float s)
        {
            return Multiply(m, s);
        }
        public static Matrix operator * (float s, Matrix m)
        {
            return Multiply(m, s);
        }

        public static Matrix Multiply(Matrix m, float s)
        {
            return new Matrix(
                m.M11 * s, m.M12 * s, m.M13 * s, m.M14 * s, 
                m.M21 * s, m.M22 * s, m.M23 * s, m.M24 * s, 
                m.M31 * s, m.M32 * s, m.M33 * s, m.M34 * s, 
                m.M41 * s, m.M42 * s, m.M43 * s, m.M44 * s);
        }

        // semantics for multiplication and transformation:
        //
        // if m1 and m2 are two Matrix'es, and v is a Vector3 or Vector4,
        //  then (m1 * m2).Transform(v) is equivalent to
        //  m2.Transform(m1.Transform(v)), which is to say, the vector
        //  is transformed by m1 first, and then m2.

        public static Matrix operator * (Matrix a, Matrix b)
        {
            return Multiply(a, b);
        }

        public static Matrix Multiply(Matrix a, Matrix b)
        {
            return new Matrix(
                a.M11 * b.M11 + a.M12 * b.M21 + a.M13 * b.M31 + a.M14 * b.M41,
                a.M11 * b.M12 + a.M12 * b.M22 + a.M13 * b.M32 + a.M14 * b.M42,
                a.M11 * b.M13 + a.M12 * b.M23 + a.M13 * b.M33 + a.M14 * b.M43,
                a.M11 * b.M14 + a.M12 * b.M24 + a.M13 * b.M34 + a.M14 * b.M44,
                a.M21 * b.M11 + a.M22 * b.M21 + a.M23 * b.M31 + a.M24 * b.M41,
                a.M21 * b.M12 + a.M22 * b.M22 + a.M23 * b.M32 + a.M24 * b.M42,
                a.M21 * b.M13 + a.M22 * b.M23 + a.M23 * b.M33 + a.M24 * b.M43,
                a.M21 * b.M14 + a.M22 * b.M24 + a.M23 * b.M34 + a.M24 * b.M44,
                a.M31 * b.M11 + a.M32 * b.M21 + a.M33 * b.M31 + a.M34 * b.M41,
                a.M31 * b.M12 + a.M32 * b.M22 + a.M33 * b.M32 + a.M34 * b.M42,
                a.M31 * b.M13 + a.M32 * b.M23 + a.M33 * b.M33 + a.M34 * b.M43,
                a.M31 * b.M14 + a.M32 * b.M24 + a.M33 * b.M34 + a.M34 * b.M44,
                a.M41 * b.M11 + a.M42 * b.M21 + a.M43 * b.M31 + a.M44 * b.M41,
                a.M41 * b.M12 + a.M42 * b.M22 + a.M43 * b.M32 + a.M44 * b.M42,
                a.M41 * b.M13 + a.M42 * b.M23 + a.M43 * b.M33 + a.M44 * b.M43,
                a.M41 * b.M14 + a.M42 * b.M24 + a.M43 * b.M34 + a.M44 * b.M44);
        }

        public static Matrix operator + (Matrix a, Matrix b)
        {
            return Add(a, b);
        }

        public static Matrix operator - (Matrix a, Matrix b)
        {
            return Add(a, -b);
        }

        public static Matrix Add(Matrix a, Matrix b)
        {
            return new Matrix(
                a.M11 + b.M11, a.M12 + b.M12, a.M13 + b.M13, a.M14 + b.M14, 
                a.M21 + b.M21, a.M22 + b.M22, a.M23 + b.M23, a.M24 + b.M24, 
                a.M31 + b.M31, a.M32 + b.M32, a.M33 + b.M33, a.M34 + b.M34, 
                a.M41 + b.M41, a.M42 + b.M42, a.M43 + b.M43, a.M44 + b.M44);
        }

        public static Matrix operator - (Matrix m)
        {
            return new Matrix(
                -m.M11, -m.M12, -m.M13, -m.M14,
                -m.M21, -m.M22, -m.M23, -m.M24,
                -m.M31, -m.M32, -m.M33, -m.M34,
                -m.M41, -m.M42, -m.M43, -m.M44);
        }

        public Vector3 Translation
        {
            get { return new Vector3(M41, M42, M43); }
        }

        public Vector3 Scale
        {
            get { return new Vector3(M11, M22, M33); }
        }

        public override string ToString()
        {
            return string.Format("[" +
                "{{M11 = {0}, M12 = {1}, M13 = {2}, M14 = {3}}} " +
                "{{M21 = {4}, M22 = {5}, M23 = {6}, M24 = {7}}} " +
                "{{M31 = {8}, M32 = {9}, M33 = {10}, M34 = {11}}} " +
                "{{M41 = {12}, M42 = {13}, M43 = {14}, M44 = {15}}}]", 
                M11, M12, M13, M14,
                M21, M22, M23, M24,
                M31, M32, M33, M34,
                M41, M42, M43, M44);
        }

        public Vector4 Column1
        {
            get { return new Vector4(M11, M21, M31, M41); }
        }
        public Vector4 Column2
        {
            get { return new Vector4(M12, M22, M32, M42); }
        }
        public Vector4 Column3
        {
            get { return new Vector4(M13, M23, M33, M43); }
        }
        public Vector4 Column4
        {
            get { return new Vector4(M14, M24, M34, M44); }
        }

        public Vector4 Row1
        {
            get { return new Vector4(M11, M12, M13, M14); }
        }
        public Vector4 Row2
        {
            get { return new Vector4(M21, M22, M23, M24); }
        }
        public Vector4 Row3
        {
            get { return new Vector4(M31, M32, M33, M34); }
        }
        public Vector4 Row4
        {
            get { return new Vector4(M41, M42, M43, M44); }
        }

        public Vector3 Transform(Vector3 v)
        {
            return Vector3.Transform(v, this);
        }

        public Vector4 Transform(Vector4 v)
        {
            return Vector4.Transform(v, this);
        }

        public Vector3 TransformHomogeneous(Vector4 v)
        {
            var v2 = this.Transform(v);
            return v2.ToVectorXYZ() / v2.W;
        }
    }
}

