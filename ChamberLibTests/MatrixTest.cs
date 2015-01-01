using System;
using NUnit.Framework;
using ChamberLib;
using Xna = Microsoft.Xna.Framework;
using XMatrix = Microsoft.Xna.Framework.Matrix;

namespace ChamberLibTests
{
    [TestFixture]
    public class MatrixTest
    {
        [Test]
        [TestCase(0, 0, 0,   1, 0, 0,   0, 1, 0)]
        [TestCase(0, 0, 0,   0, 1, 0,   0, 0, 1)]
        [TestCase(0, 0, 0,   0, 0, 1,   1, 0, 0)]
        [TestCase(0, 0, 0,   -1, 0, 0,  0, 1, 0)]
        [TestCase(0, 0, 0,   0, -1, 0,  0, 0, 1)]
        [TestCase(0, 0, 0,   0, 0, -1,  1, 0, 0)]
        [TestCase(1, 2, 3,   4, 5, 6,   7, 8, 9)]
        [TestCase(0, 0, 0,   0, 0, 1,   0, 1, 0)]
        public void TestCreateLookAt(float px, float py, float pz, float fx, float fy, float fz, float ux, float uy, float uz)
        {
            var position = new Vector3(px, py, pz);
            var forward = new Vector3(fx, fy, fz);
            var up = new Vector3(ux, uy, uz);

            var reference = Microsoft.Xna.Framework.Matrix.CreateLookAt(position.ToXna(), forward.ToXna(), up.ToXna());

            var m = Matrix.CreateLookAt(position, forward, up);

            Assert.AreEqual(reference.M11, m.M11, 0.000001f);
            Assert.AreEqual(reference.M12, m.M12, 0.000001f);
            Assert.AreEqual(reference.M13, m.M13, 0.000001f);
            Assert.AreEqual(reference.M14, m.M14, 0.000001f);
            Assert.AreEqual(reference.M21, m.M21, 0.000001f);
            Assert.AreEqual(reference.M22, m.M22, 0.000001f);
            Assert.AreEqual(reference.M23, m.M23, 0.000001f);
            Assert.AreEqual(reference.M24, m.M24, 0.000001f);
            Assert.AreEqual(reference.M31, m.M31, 0.000001f);
            Assert.AreEqual(reference.M32, m.M32, 0.000001f);
            Assert.AreEqual(reference.M33, m.M33, 0.000001f);
            Assert.AreEqual(reference.M34, m.M34, 0.000001f);
            Assert.AreEqual(reference.M41, m.M41, 0.000001f);
            Assert.AreEqual(reference.M42, m.M42, 0.000001f);
            Assert.AreEqual(reference.M43, m.M43, 0.000001f);
            Assert.AreEqual(reference.M44, m.M44, 0.000001f);
        }

        [Test]
        [TestCase(3f, 2f, 0.01f, 10f)]
        [TestCase(10f, 6f, 5f, 50f)]
        [TestCase(5f, 3f, 1f, 100f)]
        [TestCase(3f, 7.5f, 1f, 10f)]
        public void TestOrthoPerspective(float width, float height, float znear, float zfar)
        {
            var reference = Microsoft.Xna.Framework.Matrix.CreatePerspective(width, height, znear, zfar);

            var m = Matrix.CreatePerspective(width, height, znear, zfar);

            Assert.AreEqual(reference.ToChamber(), m);
        }

        [Test]
        [TestCase(-3f, 3f, -2f, 2f, 0.05f, 10f)]
        [TestCase(-4f, 2f, -1f, 3f, 0.05f, 10f)]
        [TestCase(4f, 5f, 1f, 3f, 0.05f, 10f)]
        [TestCase(4f, 5f, 2f, 1f, 0.05f, 10f)]
        [TestCase(-3f, 3f, -2f, 2f, 0.2f, 1f)]
        public void TestMatrixCreatePerspectiveOffCenter(float left, float right, float bottom, float top, float near, float far)
        {
            var m = Matrix.CreatePerspectiveOffCenter(left, right, bottom, top, near, far);
            var xm = XMatrix.CreatePerspectiveOffCenter(left, right, bottom, top, near, far);

            Assert.AreEqual(xm.M11, m.M11, 0.000001f);
            Assert.AreEqual(xm.M12, m.M12, 0.000001f);
            Assert.AreEqual(xm.M13, m.M13, 0.000001f);
            Assert.AreEqual(xm.M14, m.M14, 0.000001f);
            Assert.AreEqual(xm.M21, m.M21, 0.000001f);
            Assert.AreEqual(xm.M22, m.M22, 0.000001f);
            Assert.AreEqual(xm.M23, m.M23, 0.000001f);
            Assert.AreEqual(xm.M24, m.M24, 0.000001f);
            Assert.AreEqual(xm.M31, m.M31, 0.000001f);
            Assert.AreEqual(xm.M32, m.M32, 0.000001f);
            Assert.AreEqual(xm.M33, m.M33, 0.000001f);
            Assert.AreEqual(xm.M34, m.M34, 0.000001f);
            Assert.AreEqual(xm.M41, m.M41, 0.000001f);
            Assert.AreEqual(xm.M42, m.M42, 0.000001f);
            Assert.AreEqual(xm.M43, m.M43, 0.000001f);
            Assert.AreEqual(xm.M44, m.M44, 0.000001f);
        }

        [Test]
        [TestCase(-3f, 3f, -2f, 2f, 0.05f, 10f)]
        [TestCase(-4f, 2f, -1f, 3f, 0.05f, 10f)]
        [TestCase(4f, 5f, 1f, 3f, 0.05f, 10f)]
        [TestCase(4f, 5f, 2f, 1f, 0.05f, 10f)]
        [TestCase(-3f, 3f, -2f, 2f, 0.2f, 1f)]
        public void TestMatrixCreateOrthographicOffCenter(float left, float right, float bottom, float top, float near, float far)
        {
            var m = Matrix.CreateOrthographicOffCenter(left, right, bottom, top, near, far);
            var xm = XMatrix.CreateOrthographicOffCenter(left, right, bottom, top, near, far);

            Assert.AreEqual(xm.M11, m.M11, 0.000001f);
            Assert.AreEqual(xm.M12, m.M12, 0.000001f);
            Assert.AreEqual(xm.M13, m.M13, 0.000001f);
            Assert.AreEqual(xm.M14, m.M14, 0.000001f);
            Assert.AreEqual(xm.M21, m.M21, 0.000001f);
            Assert.AreEqual(xm.M22, m.M22, 0.000001f);
            Assert.AreEqual(xm.M23, m.M23, 0.000001f);
            Assert.AreEqual(xm.M24, m.M24, 0.000001f);
            Assert.AreEqual(xm.M31, m.M31, 0.000001f);
            Assert.AreEqual(xm.M32, m.M32, 0.000001f);
            Assert.AreEqual(xm.M33, m.M33, 0.000001f);
            Assert.AreEqual(xm.M34, m.M34, 0.000001f);
            Assert.AreEqual(xm.M41, m.M41, 0.000001f);
            Assert.AreEqual(xm.M42, m.M42, 0.000001f);
            Assert.AreEqual(xm.M43, m.M43, 0.000001f);
            Assert.AreEqual(xm.M44, m.M44, 0.000001f);
        }

        [Test]
        [TestCase(1,0,0,0)]
        [TestCase(0,1,0,0)]
        [TestCase(0,0,1,0)]
        [TestCase(0,0,0,1)]
        [TestCase(0.5f,0.5f,0.5f,0.5f)]
        [TestCase(2,3,4,1)]
        [TestCase(-0.1f,-0.1f,-1f,-5f)]
        [TestCase(-0.1f,-0.1f,0.5f,-1f)]
        [TestCase(-0.1f,-0.5f,-0.5f,1f)]
        [TestCase(-0.1f,-0.5f,-1f,-0.1f)]
        [TestCase(-0.1f,-0.5f,0.5f,-0.1f)]
        [TestCase(-0.1f,-1f,-0.1f,0f)]
        [TestCase(-0.1f,-1f,0f,-1f)]
        [TestCase(-0.1f,0.5f,1f,-0.5f)]
        [TestCase(-0.1f,0f,-0.5f,0f)]
        [TestCase(-0.1f,0f,-1f,-5f)]
        [TestCase(-0.1f,1f,0.5f,0f)]
        [TestCase(-0.1f,1f,0f,-0.1f)]
        [TestCase(-0.5f,-0.1f,-0.5f,1f)]
        [TestCase(-0.5f,-0.5f,-1f,-0.5f)]
        [TestCase(-0.5f,-1f,0.5f,-1f)]
        [TestCase(-0.5f,-1f,0.5f,-5f)]
        [TestCase(-0.5f,-1f,0f,-5f)]
        [TestCase(-0.5f,0.5f,-0.5f,-0.1f)]
        [TestCase(-0.5f,0.5f,-1f,-0.5f)]
        [TestCase(-0.5f,0.5f,0.5f,0f)]
        [TestCase(-0.5f,0.5f,0f,0f)]
        [TestCase(-0.5f,0f,-0.1f,0f)]
        [TestCase(-0.5f,1f,0.5f,-0.5f)]
        [TestCase(-0.5f,1f,1f,-1f)]
        [TestCase(-1f,-0.1f,0f,-1f)]
        [TestCase(-1f,-0.1f,0f,1f)]
        [TestCase(-1f,-0.5f,0.5f,0f)]
        [TestCase(-1f,-1f,-0.1f,-0.1f)]
        [TestCase(-1f,-1f,-0.5f,-0.5f)]
        [TestCase(-1f,-1f,1f,-5f)]
        [TestCase(-1f,0.5f,-0.1f,-5f)]
        [TestCase(-1f,0.5f,-0.5f,1f)]
        [TestCase(-1f,0.5f,0f,1f)]
        [TestCase(-1f,0f,-1f,1f)]
        [TestCase(-1f,0f,1f,-0.1f)]
        [TestCase(-1f,1f,-0.1f,-0.1f)]
        [TestCase(0.5f,-0.1f,-0.1f,-0.5f)]
        [TestCase(0.5f,-0.1f,-0.5f,-1f)]
        [TestCase(0.5f,-0.5f,-1f,-0.1f)]
        [TestCase(0.5f,-0.5f,1f,-0.1f)]
        [TestCase(0.5f,-1f,-1f,-1f)]
        [TestCase(0.5f,-1f,0f,-1f)]
        [TestCase(0.5f,0.5f,-1f,-1f)]
        [TestCase(0.5f,0f,0.5f,1f)]
        [TestCase(0.5f,0f,1f,-5f)]
        [TestCase(0.5f,1f,-0.5f,0f)]
        [TestCase(0.5f,1f,-1f,-1f)]
        [TestCase(0f,-0.1f,0.5f,-0.1f)]
        [TestCase(0f,-0.1f,1f,-0.5f)]
        [TestCase(0f,-0.5f,-0.1f,-0.5f)]
        [TestCase(0f,-0.5f,-0.1f,-1f)]
        [TestCase(0f,-1f,1f,1f)]
        [TestCase(0f,0.5f,1f,0f)]
        [TestCase(0f,0f,-0.5f,-5f)]
        [TestCase(0f,0f,0f,-0.5f)]
        [TestCase(0f,1f,-0.5f,1f)]
        [TestCase(0f,1f,-1f,-5f)]
        [TestCase(0f,1f,0f,1f)]
        [TestCase(1f,-0.1f,-0.5f,-5f)]
        [TestCase(1f,-0.1f,-1f,-0.5f)]
        [TestCase(1f,-0.1f,1f,0f)]
        [TestCase(1f,-0.5f,-0.1f,0f)]
        [TestCase(1f,-0.5f,-1f,0f)]
        [TestCase(1f,-0.5f,0f,-5f)]
        [TestCase(1f,-0.5f,1f,-0.5f)]
        [TestCase(1f,-1f,-1f,-0.1f)]
        [TestCase(1f,0.5f,0.5f,-0.1f)]
        [TestCase(1f,0f,-0.1f,-5f)]
        [TestCase(1f,0f,-0.5f,-1f)]
        [TestCase(1f,1f,-0.1f,1f)]
        public void TestMatrixCreateFromQuaternion(float x, float y, float z, float w)
        {
            var q = new Quaternion(x, y, z, w);
            var m = Matrix.CreateFromQuaternion(q);

            var xq = new Microsoft.Xna.Framework.Quaternion(x, y, z, w);
            var xm = Microsoft.Xna.Framework.Matrix.CreateFromQuaternion(xq);

            Assert.AreEqual(xm.M11, m.M11, 0.000001f);
            Assert.AreEqual(xm.M12, m.M12, 0.000001f);
            Assert.AreEqual(xm.M13, m.M13, 0.000001f);
            Assert.AreEqual(xm.M14, m.M14, 0.000001f);
            Assert.AreEqual(xm.M21, m.M21, 0.000001f);
            Assert.AreEqual(xm.M22, m.M22, 0.000001f);
            Assert.AreEqual(xm.M23, m.M23, 0.000001f);
            Assert.AreEqual(xm.M24, m.M24, 0.000001f);
            Assert.AreEqual(xm.M31, m.M31, 0.000001f);
            Assert.AreEqual(xm.M32, m.M32, 0.000001f);
            Assert.AreEqual(xm.M33, m.M33, 0.000001f);
            Assert.AreEqual(xm.M34, m.M34, 0.000001f);
            Assert.AreEqual(xm.M41, m.M41, 0.000001f);
            Assert.AreEqual(xm.M42, m.M42, 0.000001f);
            Assert.AreEqual(xm.M43, m.M43, 0.000001f);
            Assert.AreEqual(xm.M44, m.M44, 0.000001f);
        }

        [Test]
        [TestCase(1f,1f,1f)]
        [TestCase(0.5f,0.5f,0.5f)]
        [TestCase(-1f,-1f,1f)]
        [TestCase(-0.5f,-0.5f,0.5f)]
        [TestCase(0f,0f,1f)]
        [TestCase(0f,0f,0.5f)]
        public void TestCompareViewProjection(float x, float y, float z)
        {
            var view = Matrix.CreateLookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
            var proj = Matrix.CreatePerspective(1, 1, 0.5f, 1);
            var viewproj = view * proj;
            var actual = Vector4.Transform(new Vector4(x, y, z, 1), viewproj);

            var xview = XMatrix.CreateLookAt(Xna.Vector3.Zero, Xna.Vector3.UnitZ, Xna.Vector3.UnitY);
            var xproj = XMatrix.CreatePerspective(1, 1, 0.5f, 1);
            var xviewproj = xview * xproj;
            var expected = Xna.Vector4.Transform(new Xna.Vector4(x, y, z, 1), xviewproj).ToChamber();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase(1,1,1,-1,1,1)]
        [TestCase(-1,1,1,1,1,1)]
        [TestCase(1,-1,1,-1,-1,1)]
        [TestCase(-1,-1,1,1,-1,1)]
        [TestCase(0.5f,0.5f,0.5f,-1,1,0)]
        [TestCase(-0.5f,0.5f,0.5f,1,1,0)]
        [TestCase(0.5f,-0.5f,0.5f,-1,-1,0)]
        [TestCase(-0.5f,-0.5f,0.5f,1,-1,0)]
        public void TestViewProjection(float x, float y, float z, float tx, float ty, float tz)
        {
            var view = Matrix.CreateLookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
            var proj = Matrix.CreatePerspective(1, 1, 0.5f, 1);
            var viewproj = view * proj;
            var v = Vector4.Transform(new Vector4(x, y, z, 1), viewproj);
            var actual = v.ToVectorXYZ() / v.W;
            var expected = new Vector3(tx, ty, tz);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase(1,1,1,-1,1,1)]
        [TestCase(-1,1,1,1,1,1)]
        [TestCase(1,-1,1,-1,-1,1)]
        [TestCase(-1,-1,1,1,-1,1)]
        [TestCase(0.5f,0.5f,0.5f,-1,1,0)]
        [TestCase(-0.5f,0.5f,0.5f,1,1,0)]
        [TestCase(0.5f,-0.5f,0.5f,-1,-1,0)]
        [TestCase(-0.5f,-0.5f,0.5f,1,-1,0)]
        public void TestViewProjectionHomogeneous(float x, float y, float z, float tx, float ty, float tz)
        {
            var view = Matrix.CreateLookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
            var proj = Matrix.CreatePerspective(1, 1, 0.5f, 1);
            var viewproj = view * proj;
            var actual = viewproj.TransformHomogeneous(new Vector4(x, y, z, 1));
            var expected = new Vector3(tx, ty, tz);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase(1,1,1,-1,1,1)]
        [TestCase(-1,1,1,1,1,1)]
        [TestCase(1,-1,1,-1,-1,1)]
        [TestCase(-1,-1,1,1,-1,1)]
        [TestCase(0.5f,0.5f,0.5f,-1,1,0)]
        [TestCase(-0.5f,0.5f,0.5f,1,1,0)]
        [TestCase(0.5f,-0.5f,0.5f,-1,-1,0)]
        [TestCase(-0.5f,-0.5f,0.5f,1,-1,0)]
        public void TestViewProjectionInverse(float x, float y, float z, float tx, float ty, float tz)
        {
            var view = Matrix.CreateLookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
            var proj = Matrix.CreatePerspective(1, 1, 0.5f, 1);
            var viewproj = view * proj;
            var viewprojinv = viewproj.Inverted();
            var actual = viewprojinv.TransformHomogeneous(new Vector4(tx, ty, tz, 1));
            var expected = new Vector3(x, y, z);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestViewProjectionInverse2()
        {
            var view = Matrix.CreateLookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
            var proj = Matrix.CreatePerspective(1, 1, 0.5f, 1);
            var viewproj = view * proj;
            var viewprojinv = viewproj.Inverted();

            Assert.AreEqual(new Vector3(    1,     1,   1), viewprojinv.TransformHomogeneous(new Vector4(-1,  1, 1, 1)));
            Assert.AreEqual(new Vector3(   -1,     1,   1), viewprojinv.TransformHomogeneous(new Vector4( 1,  1, 1, 1)));
            Assert.AreEqual(new Vector3(    1,    -1,   1), viewprojinv.TransformHomogeneous(new Vector4(-1, -1, 1, 1)));
            Assert.AreEqual(new Vector3(   -1,    -1,   1), viewprojinv.TransformHomogeneous(new Vector4( 1, -1, 1, 1)));
            Assert.AreEqual(new Vector3( 0.5f,  0.5f,0.5f), viewprojinv.TransformHomogeneous(new Vector4(-1,  1, 0, 1)));
            Assert.AreEqual(new Vector3(-0.5f,  0.5f,0.5f), viewprojinv.TransformHomogeneous(new Vector4( 1,  1, 0, 1)));
            Assert.AreEqual(new Vector3( 0.5f, -0.5f,0.5f), viewprojinv.TransformHomogeneous(new Vector4(-1, -1, 0, 1)));
            Assert.AreEqual(new Vector3(-0.5f, -0.5f,0.5f), viewprojinv.TransformHomogeneous(new Vector4( 1, -1, 0, 1)));
        }

        [Test]
        [TestCase(-10f,-10f,-10f,0.5f,0.5f,-1f,-1f,1f,0.1f,10f)]
        [TestCase(-10f,0f,0f,-0.1f,1f,0f,-0.1f,100f,10f,0.1f)]
        [TestCase(-10f,0.5f,1f,-1f,-0.5f,0.5f,0f,0.5f,0.01f,0.01f)]
        [TestCase(-1f,1f,-1f,0f,1f,-1f,-5f,0.5f,100f,100f)]
        [TestCase(-10f,-1f,0.5f,1f,-0.1f,-0.5f,-5f,10f,0.5f,1f)]
        [TestCase(-1f,-10f,0.5f,-1f,0f,1f,-0.1f,0.01f,0.5f,10f)]
        [TestCase(0f,-0.5f,-0.5f,0.5f,1f,-0.5f,0f,0.01f,0.01f,0.5f)]
        [TestCase(-10f,1f,-0.5f,-0.5f,0f,-0.1f,0f,0.1f,10f,100f)]
        [TestCase(-0.5f,1f,-10f,1f,-0.1f,1f,0f,100f,1f,0.01f)]
        [TestCase(-1f,-1f,-10f,-0.1f,-0.5f,-0.5f,1f,0.1f,0.1f,1f)]
        [TestCase(0f,0.5f,-10f,0f,0f,0f,-0.5f,10f,100f,10f)]
        [TestCase(1f,1f,0f,-1f,-1f,-0.5f,-0.5f,1f,1f,0.01f)]
        [TestCase(-1f,0f,1f,0.5f,-0.1f,-0.1f,-0.5f,10f,0.01f,100f)]
        [TestCase(0f,-1f,1f,1f,-1f,-1f,-0.1f,0.1f,100f,0.5f)]
        [TestCase(1f,-1f,-1f,0.5f,0f,0.5f,1f,100f,10f,0.1f)]
        [TestCase(-0.5f,-1f,0f,0f,-0.5f,-0.1f,-1f,0.01f,10f,1f)]
        [TestCase(0.5f,-0.5f,-1f,-1f,-0.1f,0f,-1f,0.1f,0.01f,1f)]
        [TestCase(0.5f,0f,-10f,-0.5f,-1f,0.5f,-5f,0.01f,10f,10f)]
        [TestCase(0.5f,0.5f,0.5f,1f,1f,-0.1f,1f,1f,100f,0.1f)]
        [TestCase(-0.5f,0.5f,-1f,-0.5f,0.5f,-0.5f,-0.1f,10f,0.5f,0.1f)]
        [TestCase(0.5f,-1f,-0.5f,-0.1f,0.5f,1f,-0.5f,0.5f,0.01f,0.01f)]
        [TestCase(1f,0f,0.5f,0f,0.5f,1f,0f,0.1f,0.1f,100f)]
        [TestCase(0f,-10f,0f,-0.5f,-0.1f,-0.5f,1f,0.5f,1f,100f)]
        [TestCase(-0.5f,-0.5f,1f,-0.1f,0f,-1f,-5f,1f,100f,1f)]
        [TestCase(1f,-10f,-0.5f,1f,-0.5f,0f,-5f,1f,0.01f,10f)]
        [TestCase(0f,1f,0.5f,-0.1f,-0.1f,0.5f,-1f,0.01f,0.1f,0.1f)]
        [TestCase(-1f,0.5f,-0.5f,-0.1f,-1f,0f,-1f,100f,10f,0.5f)]
        [TestCase(0.5f,-0.5f,0.5f,-0.5f,-0.5f,-1f,-0.5f,100f,0.1f,10f)]
        [TestCase(-10f,-0.5f,1f,0f,-1f,1f,1f,0.1f,1f,10f)]
        [TestCase(-0.5f,-10f,-10f,-0.5f,1f,0.5f,-0.5f,0.1f,0.01f,0.5f)]
        [TestCase(-0.5f,0.5f,0.5f,0.5f,-1f,0f,-1f,0.5f,0.5f,100f)]
        [TestCase(-1f,-0.5f,0f,1f,0.5f,0.5f,-0.1f,10f,0.01f,0.01f)]
        [TestCase(0f,-1f,-10f,-1f,0.5f,-0.1f,-5f,100f,0.5f,0.01f)]
        [TestCase(0f,0f,-1f,0.5f,-0.5f,1f,-0.1f,1f,10f,1f)]
        [TestCase(-0.5f,0f,-0.5f,-1f,0f,-1f,1f,10f,0.1f,10f)]
        [TestCase(0.5f,-10f,-1f,-0.1f,-1f,-0.1f,0f,10f,0.5f,0.01f)]
        [TestCase(1f,0.5f,1f,-0.5f,1f,1f,-1f,10f,0.1f,1f)]
        [TestCase(-1f,-1f,-0.5f,-0.5f,0.5f,0f,0f,1f,1f,0.1f)]
        [TestCase(0.5f,-10f,1f,0f,0f,-0.5f,-5f,100f,1f,0.5f)]
        [TestCase(0.5f,1f,1f,-1f,0.5f,0f,1f,0.01f,100f,100f)]
        [TestCase(-10f,-10f,-1f,1f,-0.5f,1f,-0.5f,0.01f,100f,0.5f)]
        [TestCase(1f,0.5f,-10f,-0.1f,-0.1f,-1f,-5f,0.01f,0.5f,0.5f)]
        [TestCase(-10f,0f,0.5f,0.5f,1f,-1f,-1f,0.01f,1f,0.01f)]
        [TestCase(1f,-0.5f,-10f,-1f,-1f,-0.1f,-0.1f,0.5f,100f,0.01f)]
        [TestCase(0.5f,1f,0f,0.5f,-0.5f,-1f,-0.1f,10f,0.1f,0.01f)]
        [TestCase(-1f,-1f,-0.5f,0f,-0.1f,0.5f,-0.1f,1f,0.1f,0.5f)]
        [TestCase(-0.5f,0f,0.5f,1f,0f,-0.5f,-1f,0.5f,0.01f,0.1f)]
        [TestCase(0f,0.5f,0f,0.5f,0f,1f,-5f,0.1f,10f,0.01f)]
        [TestCase(0.5f,-10f,-0.5f,-0.5f,0.5f,0.5f,0f,100f,100f,1f)]
        [TestCase(1f,-0.5f,0.5f,-1f,0.5f,-0.5f,1f,0.5f,10f,0.5f)]
        [TestCase(-10f,-0.5f,-1f,-0.1f,-0.5f,0.5f,-0.1f,0.01f,1f,100f)]
        [TestCase(-1f,-10f,1f,-1f,-1f,1f,-5f,0.5f,0.1f,0.1f)]
        [TestCase(-0.5f,-1f,-0.5f,0f,1f,0f,1f,0.1f,0.5f,0.01f)]
        [TestCase(0f,0f,0f,0.5f,-0.1f,-0.5f,-1f,0.1f,100f,10f)]
        [TestCase(0.5f,0f,0f,1f,-0.5f,-1f,0f,1f,0.5f,100f)]
        [TestCase(1f,1f,1f,1f,-0.1f,-1f,-0.5f,10f,10f,1f)]
        [TestCase(-10f,-1f,-1f,1f,0f,-0.1f,-5f,0.5f,0.1f,10f)]
        [TestCase(-1f,-0.5f,-10f,-0.5f,0.5f,-1f,-0.5f,0.1f,0.5f,0.1f)]
        [TestCase(-0.5f,1f,1f,-0.1f,1f,0.5f,0f,0.5f,0.5f,10f)]
        [TestCase(0f,1f,0f,0f,1f,-0.5f,1f,10f,0.01f,0.5f)]
        [TestCase(0.5f,-1f,-10f,0.5f,-1f,-1f,-1f,100f,0.01f,100f)]
        [TestCase(1f,0.5f,-10f,0f,-0.5f,-0.1f,-0.5f,10f,1f,0.1f)]
        [TestCase(-10f,-0.5f,-1f,-1f,1f,-0.1f,-0.1f,0.5f,1f,1f)]
        [TestCase(-1f,0f,-0.5f,1f,-0.5f,-0.1f,0f,1f,0.01f,0.5f)]
        [TestCase(-0.5f,-10f,0f,-1f,-0.1f,0f,1f,10f,10f,0.01f)]
        [TestCase(0f,0f,0.5f,-0.5f,-1f,0f,-5f,100f,0.1f,1f)]
        [TestCase(0.5f,0.5f,1f,-0.5f,-1f,0.5f,-1f,0.01f,0.01f,0.01f)]
        [TestCase(1f,1f,-10f,0f,-0.1f,1f,-0.5f,0.1f,0.01f,0.1f)]
        [TestCase(-10f,-10f,-1f,-0.1f,-0.5f,-1f,-0.1f,0.5f,0.5f,0.5f)]
        [TestCase(-1f,-1f,-0.5f,-0.1f,0f,-0.5f,0f,1f,0.1f,10f)]
        public void TestScaleRotationTranslation(float tx, float ty, float tz, float qx, float qy, float qz, float qw, float sx, float sy, float sz)
        {
            var t = new Vector3(tx, ty, tz);
            var r = new Quaternion(qx, qy, qz, qw);
            var s = new Vector3(sx, sy, sz);

            var tm = Matrix.CreateTranslation(t);
            var rm = Matrix.CreateFromQuaternion(r);
            var sm = Matrix.CreateScale(s);
            var m = sm * rm * tm;

            var xrr = new Xna.Quaternion(qx, qy, qz, qw);
            Assert.AreEqual(xrr.ToChamber(), r);

            var xtm = Xna.Matrix.CreateTranslation(t.ToXna());
            var xrm = Xna.Matrix.CreateFromQuaternion(r.ToXna());
            var xsm = Xna.Matrix.CreateScale(s.ToXna());
            var xm = xsm * xrm * xtm;

            Assert.AreEqual(xtm.ToChamber(), tm);
            Assert.AreEqual(xrm.ToChamber(), rm);
            Assert.AreEqual(xsm.ToChamber(), sm);

            Assert.AreEqual(xm.ToChamber(), m);
        }

        [Test]
        [TestCase(-10f,-10f,-10f,0.5f,0.5f,-1f,-1f,1f,0.1f,10f)]
        [TestCase(-10f,0f,0f,-0.1f,1f,0f,-0.1f,100f,10f,0.1f)]
        [TestCase(-10f,0.5f,1f,-1f,-0.5f,0.5f,0f,0.5f,0.01f,0.01f)]
        [TestCase(-1f,1f,-1f,0f,1f,-1f,-5f,0.5f,100f,100f)]
        [TestCase(-10f,-1f,0.5f,1f,-0.1f,-0.5f,-5f,10f,0.5f,1f)]
        [TestCase(-1f,-10f,0.5f,-1f,0f,1f,-0.1f,0.01f,0.5f,10f)]
        [TestCase(0f,-0.5f,-0.5f,0.5f,1f,-0.5f,0f,0.01f,0.01f,0.5f)]
        [TestCase(-10f,1f,-0.5f,-0.5f,0f,-0.1f,0f,0.1f,10f,100f)]
        [TestCase(-0.5f,1f,-10f,1f,-0.1f,1f,0f,100f,1f,0.01f)]
        [TestCase(-1f,-1f,-10f,-0.1f,-0.5f,-0.5f,1f,0.1f,0.1f,1f)]
        [TestCase(0f,0.5f,-10f,0f,0f,0f,-0.5f,10f,100f,10f)]
        [TestCase(1f,1f,0f,-1f,-1f,-0.5f,-0.5f,1f,1f,0.01f)]
        [TestCase(-1f,0f,1f,0.5f,-0.1f,-0.1f,-0.5f,10f,0.01f,100f)]
        [TestCase(0f,-1f,1f,1f,-1f,-1f,-0.1f,0.1f,100f,0.5f)]
        [TestCase(1f,-1f,-1f,0.5f,0f,0.5f,1f,100f,10f,0.1f)]
        [TestCase(-0.5f,-1f,0f,0f,-0.5f,-0.1f,-1f,0.01f,10f,1f)]
        [TestCase(0.5f,-0.5f,-1f,-1f,-0.1f,0f,-1f,0.1f,0.01f,1f)]
        [TestCase(0.5f,0f,-10f,-0.5f,-1f,0.5f,-5f,0.01f,10f,10f)]
        [TestCase(0.5f,0.5f,0.5f,1f,1f,-0.1f,1f,1f,100f,0.1f)]
        [TestCase(-0.5f,0.5f,-1f,-0.5f,0.5f,-0.5f,-0.1f,10f,0.5f,0.1f)]
        [TestCase(0.5f,-1f,-0.5f,-0.1f,0.5f,1f,-0.5f,0.5f,0.01f,0.01f)]
        [TestCase(1f,0f,0.5f,0f,0.5f,1f,0f,0.1f,0.1f,100f)]
        [TestCase(0f,-10f,0f,-0.5f,-0.1f,-0.5f,1f,0.5f,1f,100f)]
        [TestCase(-0.5f,-0.5f,1f,-0.1f,0f,-1f,-5f,1f,100f,1f)]
        [TestCase(1f,-10f,-0.5f,1f,-0.5f,0f,-5f,1f,0.01f,10f)]
        [TestCase(0f,1f,0.5f,-0.1f,-0.1f,0.5f,-1f,0.01f,0.1f,0.1f)]
        [TestCase(-1f,0.5f,-0.5f,-0.1f,-1f,0f,-1f,100f,10f,0.5f)]
        [TestCase(0.5f,-0.5f,0.5f,-0.5f,-0.5f,-1f,-0.5f,100f,0.1f,10f)]
        [TestCase(-10f,-0.5f,1f,0f,-1f,1f,1f,0.1f,1f,10f)]
        [TestCase(-0.5f,-10f,-10f,-0.5f,1f,0.5f,-0.5f,0.1f,0.01f,0.5f)]
        [TestCase(-0.5f,0.5f,0.5f,0.5f,-1f,0f,-1f,0.5f,0.5f,100f)]
        [TestCase(-1f,-0.5f,0f,1f,0.5f,0.5f,-0.1f,10f,0.01f,0.01f)]
        [TestCase(0f,-1f,-10f,-1f,0.5f,-0.1f,-5f,100f,0.5f,0.01f)]
        [TestCase(0f,0f,-1f,0.5f,-0.5f,1f,-0.1f,1f,10f,1f)]
        [TestCase(-0.5f,0f,-0.5f,-1f,0f,-1f,1f,10f,0.1f,10f)]
        [TestCase(0.5f,-10f,-1f,-0.1f,-1f,-0.1f,0f,10f,0.5f,0.01f)]
        [TestCase(1f,0.5f,1f,-0.5f,1f,1f,-1f,10f,0.1f,1f)]
        [TestCase(-1f,-1f,-0.5f,-0.5f,0.5f,0f,0f,1f,1f,0.1f)]
        [TestCase(0.5f,-10f,1f,0f,0f,-0.5f,-5f,100f,1f,0.5f)]
        [TestCase(0.5f,1f,1f,-1f,0.5f,0f,1f,0.01f,100f,100f)]
        [TestCase(-10f,-10f,-1f,1f,-0.5f,1f,-0.5f,0.01f,100f,0.5f)]
        [TestCase(1f,0.5f,-10f,-0.1f,-0.1f,-1f,-5f,0.01f,0.5f,0.5f)]
        [TestCase(-10f,0f,0.5f,0.5f,1f,-1f,-1f,0.01f,1f,0.01f)]
        [TestCase(1f,-0.5f,-10f,-1f,-1f,-0.1f,-0.1f,0.5f,100f,0.01f)]
        [TestCase(0.5f,1f,0f,0.5f,-0.5f,-1f,-0.1f,10f,0.1f,0.01f)]
        [TestCase(-1f,-1f,-0.5f,0f,-0.1f,0.5f,-0.1f,1f,0.1f,0.5f)]
        [TestCase(-0.5f,0f,0.5f,1f,0f,-0.5f,-1f,0.5f,0.01f,0.1f)]
        [TestCase(0f,0.5f,0f,0.5f,0f,1f,-5f,0.1f,10f,0.01f)]
        [TestCase(0.5f,-10f,-0.5f,-0.5f,0.5f,0.5f,0f,100f,100f,1f)]
        [TestCase(1f,-0.5f,0.5f,-1f,0.5f,-0.5f,1f,0.5f,10f,0.5f)]
        [TestCase(-10f,-0.5f,-1f,-0.1f,-0.5f,0.5f,-0.1f,0.01f,1f,100f)]
        [TestCase(-1f,-10f,1f,-1f,-1f,1f,-5f,0.5f,0.1f,0.1f)]
        [TestCase(-0.5f,-1f,-0.5f,0f,1f,0f,1f,0.1f,0.5f,0.01f)]
        [TestCase(0f,0f,0f,0.5f,-0.1f,-0.5f,-1f,0.1f,100f,10f)]
        [TestCase(0.5f,0f,0f,1f,-0.5f,-1f,0f,1f,0.5f,100f)]
        [TestCase(1f,1f,1f,1f,-0.1f,-1f,-0.5f,10f,10f,1f)]
        [TestCase(-10f,-1f,-1f,1f,0f,-0.1f,-5f,0.5f,0.1f,10f)]
        [TestCase(-1f,-0.5f,-10f,-0.5f,0.5f,-1f,-0.5f,0.1f,0.5f,0.1f)]
        [TestCase(-0.5f,1f,1f,-0.1f,1f,0.5f,0f,0.5f,0.5f,10f)]
        [TestCase(0f,1f,0f,0f,1f,-0.5f,1f,10f,0.01f,0.5f)]
        [TestCase(0.5f,-1f,-10f,0.5f,-1f,-1f,-1f,100f,0.01f,100f)]
        [TestCase(1f,0.5f,-10f,0f,-0.5f,-0.1f,-0.5f,10f,1f,0.1f)]
        [TestCase(-10f,-0.5f,-1f,-1f,1f,-0.1f,-0.1f,0.5f,1f,1f)]
        [TestCase(-1f,0f,-0.5f,1f,-0.5f,-0.1f,0f,1f,0.01f,0.5f)]
        [TestCase(-0.5f,-10f,0f,-1f,-0.1f,0f,1f,10f,10f,0.01f)]
        [TestCase(0f,0f,0.5f,-0.5f,-1f,0f,-5f,100f,0.1f,1f)]
        [TestCase(0.5f,0.5f,1f,-0.5f,-1f,0.5f,-1f,0.01f,0.01f,0.01f)]
        [TestCase(1f,1f,-10f,0f,-0.1f,1f,-0.5f,0.1f,0.01f,0.1f)]
        [TestCase(-10f,-10f,-1f,-0.1f,-0.5f,-1f,-0.1f,0.5f,0.5f,0.5f)]
        [TestCase(-1f,-1f,-0.5f,-0.1f,0f,-0.5f,0f,1f,0.1f,10f)]
        public void TestXnaCompose(float tx, float ty, float tz, float qx, float qy, float qz, float qw, float sx, float sy, float sz)
        {
            var xt = new Xna.Vector3(tx, ty, tz);
            var xrv = new Xna.Vector3(qx, qy, qz);
            xrv.Normalize();
            var xr = Xna.Quaternion.CreateFromAxisAngle(xrv, qw);
            var xr1 = xr;
            xr1.Normalize();
            var xs = new Xna.Vector3(sx, sy, sz);

            var xtm = Xna.Matrix.CreateTranslation(xt);
            var xrm = Xna.Matrix.CreateFromQuaternion(xr);
            var xsm = Xna.Matrix.CreateScale(xs);
            var xm = xsm * xrm * xtm;



            Xna.Vector3 xs2;
            Xna.Quaternion xr2;
            Xna.Vector3 xt2;
            xm.Decompose(out xs2, out xr2, out xt2);

            var xr3 = xr2;
            xr3.Normalize();

            Vector3 xr3axis;
            float xr3angle;
            xr3.ToChamber().ToAxisAngle(out xr3axis, out xr3angle);


            Assert.AreEqual(xs2.X, xs.X, 0.001f);
            Assert.AreEqual(xs2.Y, xs.Y, 0.001f);
            Assert.AreEqual(xs2.Z, xs.Z, 0.001f);

            if (Math.Abs(xr3.W - -xr1.W) < 0.001f)
            {
                xr3 = -xr3;
            }

            if (xrv != Xna.Vector3.Zero)
            {
                Assert.AreEqual(xr3.X, xr1.X, 0.001f);
                Assert.AreEqual(xr3.Y, xr1.Y, 0.001f);
                Assert.AreEqual(xr3.Z, xr1.Z, 0.001f);
                Assert.AreEqual(xr3.W, xr1.W, 0.001f);
            }
            Assert.AreEqual(xt2.X, xt.X, 0.001f);
            Assert.AreEqual(xt2.Y, xt.Y, 0.001f);
            Assert.AreEqual(xt2.Z, xt.Z, 0.001f);
        }

        [Test]
        [TestCase(-0.5f,-0.5f,1f,0,0,0,0,1,1,1)]
        [TestCase(-0.5f,-10f,-10f,0,0,0,0,1,1,1)]
        [TestCase(-0.5f,-10f,0f,0,0,0,0,1,1,1)]
        [TestCase(-0.5f,-1f,-0.5f,0,0,0,0,1,1,1)]
        [TestCase(-0.5f,-1f,0f,0,0,0,0,1,1,1)]
        [TestCase(-0.5f,0.5f,-1f,0,0,0,0,1,1,1)]
        [TestCase(-0.5f,0.5f,0.5f,0,0,0,0,1,1,1)]
        [TestCase(-0.5f,0f,-0.5f,0,0,0,0,1,1,1)]
        [TestCase(-0.5f,0f,0.5f,0,0,0,0,1,1,1)]
        [TestCase(-0.5f,1f,-10f,0,0,0,0,1,1,1)]
        [TestCase(-0.5f,1f,1f,0,0,0,0,1,1,1)]
        [TestCase(-10f,-0.5f,-1f,0,0,0,0,1,1,1)]
        [TestCase(-10f,-0.5f,-1f,0,0,0,0,1,1,1)]
        [TestCase(-10f,-0.5f,1f,0,0,0,0,1,1,1)]
        [TestCase(-10f,-10f,-10f,0,0,0,0,1,1,1)]
        [TestCase(-10f,-10f,-1f,0,0,0,0,1,1,1)]
        [TestCase(-10f,-10f,-1f,0,0,0,0,1,1,1)]
        [TestCase(-10f,-1f,-1f,0,0,0,0,1,1,1)]
        [TestCase(-10f,-1f,0.5f,0,0,0,0,1,1,1)]
        [TestCase(-10f,0.5f,1f,0,0,0,0,1,1,1)]
        [TestCase(-10f,0f,0.5f,0,0,0,0,1,1,1)]
        [TestCase(-10f,0f,0f,0,0,0,0,1,1,1)]
        [TestCase(-10f,1f,-0.5f,0,0,0,0,1,1,1)]
        [TestCase(-1f,-0.5f,-10f,0,0,0,0,1,1,1)]
        [TestCase(-1f,-0.5f,0f,0,0,0,0,1,1,1)]
        [TestCase(-1f,-10f,0.5f,0,0,0,0,1,1,1)]
        [TestCase(-1f,-10f,1f,0,0,0,0,1,1,1)]
        [TestCase(-1f,-1f,-0.5f,0,0,0,0,1,1,1)]
        [TestCase(-1f,-1f,-0.5f,0,0,0,0,1,1,1)]
        [TestCase(-1f,-1f,-0.5f,0,0,0,0,1,1,1)]
        [TestCase(-1f,-1f,-10f,0,0,0,0,1,1,1)]
        [TestCase(-1f,0.5f,-0.5f,0,0,0,0,1,1,1)]
        [TestCase(-1f,0f,-0.5f,0,0,0,0,1,1,1)]
        [TestCase(-1f,0f,1f,0,0,0,0,1,1,1)]
        [TestCase(-1f,1f,-1f,0,0,0,0,1,1,1)]
        [TestCase(0.5f,-0.5f,-1f,0,0,0,0,1,1,1)]
        [TestCase(0.5f,-0.5f,0.5f,0,0,0,0,1,1,1)]
        [TestCase(0.5f,-10f,-0.5f,0,0,0,0,1,1,1)]
        [TestCase(0.5f,-10f,-1f,0,0,0,0,1,1,1)]
        [TestCase(0.5f,-10f,1f,0,0,0,0,1,1,1)]
        [TestCase(0.5f,-1f,-0.5f,0,0,0,0,1,1,1)]
        [TestCase(0.5f,-1f,-10f,0,0,0,0,1,1,1)]
        [TestCase(0.5f,0.5f,0.5f,0,0,0,0,1,1,1)]
        [TestCase(0.5f,0.5f,1f,0,0,0,0,1,1,1)]
        [TestCase(0.5f,0f,-10f,0,0,0,0,1,1,1)]
        [TestCase(0.5f,0f,0f,0,0,0,0,1,1,1)]
        [TestCase(0.5f,1f,0f,0,0,0,0,1,1,1)]
        [TestCase(0.5f,1f,1f,0,0,0,0,1,1,1)]
        [TestCase(0f,-0.5f,-0.5f,0,0,0,0,1,1,1)]
        [TestCase(0f,-10f,0f,0,0,0,0,1,1,1)]
        [TestCase(0f,-1f,-10f,0,0,0,0,1,1,1)]
        [TestCase(0f,-1f,1f,0,0,0,0,1,1,1)]
        [TestCase(0f,0.5f,-10f,0,0,0,0,1,1,1)]
        [TestCase(0f,0.5f,0f,0,0,0,0,1,1,1)]
        [TestCase(0f,0f,-1f,0,0,0,0,1,1,1)]
        [TestCase(0f,0f,0.5f,0,0,0,0,1,1,1)]
        [TestCase(0f,0f,0f,0,0,0,0,1,1,1)]
        [TestCase(0f,1f,0.5f,0,0,0,0,1,1,1)]
        [TestCase(0f,1f,0f,0,0,0,0,1,1,1)]
        [TestCase(1f,-0.5f,-10f,0,0,0,0,1,1,1)]
        [TestCase(1f,-0.5f,0.5f,0,0,0,0,1,1,1)]
        [TestCase(1f,-10f,-0.5f,0,0,0,0,1,1,1)]
        [TestCase(1f,-1f,-1f,0,0,0,0,1,1,1)]
        [TestCase(1f,0.5f,-10f,0,0,0,0,1,1,1)]
        [TestCase(1f,0.5f,-10f,0,0,0,0,1,1,1)]
        [TestCase(1f,0.5f,1f,0,0,0,0,1,1,1)]
        [TestCase(1f,0f,0.5f,0,0,0,0,1,1,1)]
        [TestCase(1f,1f,-10f,0,0,0,0,1,1,1)]
        [TestCase(1f,1f,0f,0,0,0,0,1,1,1)]
        [TestCase(1f,1f,1f,0,0,0,0,1,1,1)]

        [TestCase(0,0,0,-0.1f,-0.1f,-1f,-5f,1,1,1)]
        [TestCase(0,0,0,-0.1f,-0.1f,0.5f,-1f,1,1,1)]
        [TestCase(0,0,0,-0.1f,-0.5f,-0.5f,1f,1,1,1)]
        [TestCase(0,0,0,-0.1f,-0.5f,-1f,-0.1f,1,1,1)]
        [TestCase(0,0,0,-0.1f,-0.5f,0.5f,-0.1f,1,1,1)]
        [TestCase(0,0,0,-0.1f,-1f,-0.1f,0f,1,1,1)]
        [TestCase(0,0,0,-0.1f,-1f,0f,-1f,1,1,1)]
        [TestCase(0,0,0,-0.1f,0.5f,1f,-0.5f,1,1,1)]
        [TestCase(0,0,0,-0.1f,0f,-0.5f,0f,1,1,1)]
        [TestCase(0,0,0,-0.1f,0f,-1f,-5f,1,1,1)]
        [TestCase(0,0,0,-0.1f,1f,0.5f,0f,1,1,1)]
        [TestCase(0,0,0,-0.1f,1f,0f,-0.1f,1,1,1)]
        [TestCase(0,0,0,-0.5f,-0.1f,-0.5f,1f,1,1,1)]
        [TestCase(0,0,0,-0.5f,-0.5f,-1f,-0.5f,1,1,1)]
        [TestCase(0,0,0,-0.5f,-1f,0.5f,-1f,1,1,1)]
        [TestCase(0,0,0,-0.5f,-1f,0.5f,-5f,1,1,1)]
        [TestCase(0,0,0,-0.5f,-1f,0f,-5f,1,1,1)]
        [TestCase(0,0,0,-0.5f,0.5f,-0.5f,-0.1f,1,1,1)]
        [TestCase(0,0,0,-0.5f,0.5f,-1f,-0.5f,1,1,1)]
        [TestCase(0,0,0,-0.5f,0.5f,0.5f,0f,1,1,1)]
        [TestCase(0,0,0,-0.5f,0.5f,0f,0f,1,1,1)]
        [TestCase(0,0,0,-0.5f,0f,-0.1f,0f,1,1,1)]
        [TestCase(0,0,0,-0.5f,1f,0.5f,-0.5f,1,1,1)]
        [TestCase(0,0,0,-0.5f,1f,1f,-1f,1,1,1)]
        [TestCase(0,0,0,-1f,-0.1f,0f,-1f,1,1,1)]
        [TestCase(0,0,0,-1f,-0.1f,0f,1f,1,1,1)]
        [TestCase(0,0,0,-1f,-0.5f,0.5f,0f,1,1,1)]
        [TestCase(0,0,0,-1f,-1f,-0.1f,-0.1f,1,1,1)]
        [TestCase(0,0,0,-1f,-1f,-0.5f,-0.5f,1,1,1)]
        [TestCase(0,0,0,-1f,-1f,1f,-5f,1,1,1)]
        [TestCase(0,0,0,-1f,0.5f,-0.1f,-5f,1,1,1)]
        [TestCase(0,0,0,-1f,0.5f,-0.5f,1f,1,1,1)]
        [TestCase(0,0,0,-1f,0.5f,0f,1f,1,1,1)]
        [TestCase(0,0,0,-1f,0f,-1f,1f,1,1,1)]
        [TestCase(0,0,0,-1f,0f,1f,-0.1f,1,1,1)]
        [TestCase(0,0,0,-1f,1f,-0.1f,-0.1f,1,1,1)]
        [TestCase(0,0,0,0.5f,-0.1f,-0.1f,-0.5f,1,1,1)]
        [TestCase(0,0,0,0.5f,-0.1f,-0.5f,-1f,1,1,1)]
        [TestCase(0,0,0,0.5f,-0.5f,-1f,-0.1f,1,1,1)]
        [TestCase(0,0,0,0.5f,-0.5f,1f,-0.1f,1,1,1)]
        [TestCase(0,0,0,0.5f,-1f,-1f,-1f,1,1,1)]
        [TestCase(0,0,0,0.5f,-1f,0f,-1f,1,1,1)]
        [TestCase(0,0,0,0.5f,0.5f,-1f,-1f,1,1,1)]
        [TestCase(0,0,0,0.5f,0f,0.5f,1f,1,1,1)]
        [TestCase(0,0,0,0.5f,0f,1f,-5f,1,1,1)]
        [TestCase(0,0,0,0.5f,1f,-0.5f,0f,1,1,1)]
        [TestCase(0,0,0,0.5f,1f,-1f,-1f,1,1,1)]
        [TestCase(0,0,0,0f,-0.1f,0.5f,-0.1f,1,1,1)]
        [TestCase(0,0,0,0f,-0.1f,1f,-0.5f,1,1,1)]
        [TestCase(0,0,0,0f,-0.5f,-0.1f,-0.5f,1,1,1)]
        [TestCase(0,0,0,0f,-0.5f,-0.1f,-1f,1,1,1)]
        [TestCase(0,0,0,0f,-1f,1f,1f,1,1,1)]
        [TestCase(0,0,0,0f,0.5f,1f,0f,1,1,1)]
        [TestCase(0,0,0,0f,0f,-0.5f,-5f,1,1,1)]
        [TestCase(0,0,0,0f,0f,0f,-0.5f,1,1,1)]
        [TestCase(0,0,0,0f,1f,-0.5f,1f,1,1,1)]
        [TestCase(0,0,0,0f,1f,-1f,-5f,1,1,1)]
        [TestCase(0,0,0,0f,1f,0f,1f,1,1,1)]
        [TestCase(0,0,0,1f,-0.1f,-0.5f,-5f,1,1,1)]
        [TestCase(0,0,0,1f,-0.1f,-1f,-0.5f,1,1,1)]
        [TestCase(0,0,0,1f,-0.1f,1f,0f,1,1,1)]
        [TestCase(0,0,0,1f,-0.5f,-0.1f,0f,1,1,1)]
        [TestCase(0,0,0,1f,-0.5f,-1f,0f,1,1,1)]
        [TestCase(0,0,0,1f,-0.5f,0f,-5f,1,1,1)]
        [TestCase(0,0,0,1f,-0.5f,1f,-0.5f,1,1,1)]
        [TestCase(0,0,0,1f,-1f,-1f,-0.1f,1,1,1)]
        [TestCase(0,0,0,1f,0.5f,0.5f,-0.1f,1,1,1)]
        [TestCase(0,0,0,1f,0f,-0.1f,-5f,1,1,1)]
        [TestCase(0,0,0,1f,0f,-0.5f,-1f,1,1,1)]
        [TestCase(0,0,0,1f,1f,-0.1f,1f,1,1,1)]

        [TestCase(0,0,0,0,0,0,0,0.01f,0.01f,0.01f)]
        [TestCase(0,0,0,0,0,0,0,0.01f,0.01f,0.5f)]
        [TestCase(0,0,0,0,0,0,0,0.01f,0.1f,0.1f)]
        [TestCase(0,0,0,0,0,0,0,0.01f,0.5f,0.5f)]
        [TestCase(0,0,0,0,0,0,0,0.01f,0.5f,10f)]
        [TestCase(0,0,0,0,0,0,0,0.01f,100f,0.5f)]
        [TestCase(0,0,0,0,0,0,0,0.01f,100f,100f)]
        [TestCase(0,0,0,0,0,0,0,0.01f,10f,10f)]
        [TestCase(0,0,0,0,0,0,0,0.01f,10f,1f)]
        [TestCase(0,0,0,0,0,0,0,0.01f,1f,0.01f)]
        [TestCase(0,0,0,0,0,0,0,0.01f,1f,100f)]
        [TestCase(0,0,0,0,0,0,0,0.1f,0.01f,0.1f)]
        [TestCase(0,0,0,0,0,0,0,0.1f,0.01f,0.5f)]
        [TestCase(0,0,0,0,0,0,0,0.1f,0.01f,1f)]
        [TestCase(0,0,0,0,0,0,0,0.1f,0.1f,100f)]
        [TestCase(0,0,0,0,0,0,0,0.1f,0.1f,1f)]
        [TestCase(0,0,0,0,0,0,0,0.1f,0.5f,0.01f)]
        [TestCase(0,0,0,0,0,0,0,0.1f,0.5f,0.1f)]
        [TestCase(0,0,0,0,0,0,0,0.1f,100f,0.5f)]
        [TestCase(0,0,0,0,0,0,0,0.1f,100f,10f)]
        [TestCase(0,0,0,0,0,0,0,0.1f,10f,0.01f)]
        [TestCase(0,0,0,0,0,0,0,0.1f,10f,100f)]
        [TestCase(0,0,0,0,0,0,0,0.1f,1f,10f)]
        [TestCase(0,0,0,0,0,0,0,0.5f,0.01f,0.01f)]
        [TestCase(0,0,0,0,0,0,0,0.5f,0.01f,0.01f)]
        [TestCase(0,0,0,0,0,0,0,0.5f,0.01f,0.1f)]
        [TestCase(0,0,0,0,0,0,0,0.5f,0.1f,0.1f)]
        [TestCase(0,0,0,0,0,0,0,0.5f,0.1f,10f)]
        [TestCase(0,0,0,0,0,0,0,0.5f,0.5f,0.5f)]
        [TestCase(0,0,0,0,0,0,0,0.5f,0.5f,100f)]
        [TestCase(0,0,0,0,0,0,0,0.5f,0.5f,10f)]
        [TestCase(0,0,0,0,0,0,0,0.5f,100f,0.01f)]
        [TestCase(0,0,0,0,0,0,0,0.5f,100f,100f)]
        [TestCase(0,0,0,0,0,0,0,0.5f,10f,0.5f)]
        [TestCase(0,0,0,0,0,0,0,0.5f,1f,100f)]
        [TestCase(0,0,0,0,0,0,0,0.5f,1f,1f)]
        [TestCase(0,0,0,0,0,0,0,100f,0.01f,100f)]
        [TestCase(0,0,0,0,0,0,0,100f,0.1f,10f)]
        [TestCase(0,0,0,0,0,0,0,100f,0.1f,1f)]
        [TestCase(0,0,0,0,0,0,0,100f,0.5f,0.01f)]
        [TestCase(0,0,0,0,0,0,0,100f,100f,1f)]
        [TestCase(0,0,0,0,0,0,0,100f,10f,0.1f)]
        [TestCase(0,0,0,0,0,0,0,100f,10f,0.1f)]
        [TestCase(0,0,0,0,0,0,0,100f,10f,0.5f)]
        [TestCase(0,0,0,0,0,0,0,100f,1f,0.01f)]
        [TestCase(0,0,0,0,0,0,0,100f,1f,0.5f)]
        [TestCase(0,0,0,0,0,0,0,10f,0.01f,0.01f)]
        [TestCase(0,0,0,0,0,0,0,10f,0.01f,0.5f)]
        [TestCase(0,0,0,0,0,0,0,10f,0.01f,100f)]
        [TestCase(0,0,0,0,0,0,0,10f,0.1f,0.01f)]
        [TestCase(0,0,0,0,0,0,0,10f,0.1f,10f)]
        [TestCase(0,0,0,0,0,0,0,10f,0.1f,1f)]
        [TestCase(0,0,0,0,0,0,0,10f,0.5f,0.01f)]
        [TestCase(0,0,0,0,0,0,0,10f,0.5f,0.1f)]
        [TestCase(0,0,0,0,0,0,0,10f,0.5f,1f)]
        [TestCase(0,0,0,0,0,0,0,10f,100f,10f)]
        [TestCase(0,0,0,0,0,0,0,10f,10f,0.01f)]
        [TestCase(0,0,0,0,0,0,0,10f,10f,1f)]
        [TestCase(0,0,0,0,0,0,0,10f,1f,0.1f)]
        [TestCase(0,0,0,0,0,0,0,1f,0.01f,0.5f)]
        [TestCase(0,0,0,0,0,0,0,1f,0.01f,10f)]
        [TestCase(0,0,0,0,0,0,0,1f,0.1f,0.5f)]
        [TestCase(0,0,0,0,0,0,0,1f,0.1f,10f)]
        [TestCase(0,0,0,0,0,0,0,1f,0.1f,10f)]
        [TestCase(0,0,0,0,0,0,0,1f,0.5f,100f)]
        [TestCase(0,0,0,0,0,0,0,1f,100f,0.1f)]
        [TestCase(0,0,0,0,0,0,0,1f,100f,1f)]
        [TestCase(0,0,0,0,0,0,0,1f,10f,1f)]
        [TestCase(0,0,0,0,0,0,0,1f,1f,0.01f)]
        [TestCase(0,0,0,0,0,0,0,1f,1f,0.1f)]
        public void TestDecompose(float tx, float ty, float tz, float qx, float qy, float qz, float qw, float sx, float sy, float sz)
        {
            var t = new Vector3(tx, ty, tz);
            var _r = new Quaternion(qx, qy, qz, qw);
            if (qx == 0 && qy == 0 & qz == 0 & qw == 0)
            {
                _r = Quaternion.Identity;
            }
            var r = _r.Normalized();
            var s = new Vector3(sx, sy, sz);

            var m = Matrix.Compose(s, r, t);

            var xm = m.ToXna();



            Xna.Vector3 xs;
            Xna.Quaternion xr;
            Xna.Vector3 xt;
            xm.Decompose(out xs, out xr, out xt);

            Assert.AreEqual(xs.X, s.X, 0.0001f);
            Assert.AreEqual(xs.Y, s.Y, 0.0001f);
            Assert.AreEqual(xs.Z, s.Z, 0.0001f);

            float delta = 0.0001f;
            Assert.That(xr.ToChamber(), Is.EqualTo(r).Using(
                (Quaternion a, Quaternion b) =>
                a.IsEquivalentOrientationTo(b, delta) ? 0 : 1));

            Assert.AreEqual(xt.ToChamber(), t);

            Vector3 s2;
            Quaternion r2;
            Vector3 t2;
            m.Decompose(out s2, out r2, out t2);

            Assert.AreEqual(s2.X, s.X, 0.0001f);
            Assert.AreEqual(s2.Y, s.Y, 0.0001f);
            Assert.AreEqual(s2.Z, s.Z, 0.0001f);

            if (Math.Abs(r2.W - -r.W) < 0.0001f)
            {
                r2 = -r2;
            }
            Assert.That(r2, Is.EqualTo(r).Using(
                (Quaternion a, Quaternion b) =>
                a.IsEquivalentOrientationTo(b, delta) ? 0 : 1));

            Assert.AreEqual(t2, t);
        }
    }
}

