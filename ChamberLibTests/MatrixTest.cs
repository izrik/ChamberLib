using System;
using NUnit.Framework;
using ChamberLib;

namespace ChamberLibTests
{
    [TestFixture]
    public class MatrixTest
    {
        [Test]
        [TestCase(0, 0, 0,   1, 0, 0,   0, 1, 0,   0f,0f,-1f,0f, 0f,1f,0f,0f,  1f,0f,0f,0f,  0f,0f,0f,1f)]
        [TestCase(0, 0, 0,   0, 1, 0,   0, 0, 1,   1f,0f,0f,0f,  0f,0f,-1f,0f, 0f,1f,0f,0f,  0f,0f,0f,1f)]
        [TestCase(0, 0, 0,   0, 0, 1,   1, 0, 0,   0f,1f,0f,0f,  1f,0f,0f,0f,  0f,0f,-1f,0f, 0f,0f,0f,1f)]
        [TestCase(0, 0, 0,   -1, 0, 0,  0, 1, 0,   0f,0f,1f,0f,  0f,1f,0f,0f,  -1f,0f,0f,0f, 0f,0f,0f,1f)]
        [TestCase(0, 0, 0,   0, -1, 0,  0, 0, 1,   -1f,0f,0f,0f, 0f,0f,1f,0f,  0f,1f,0f,0f,  0f,0f,0f,1f)]
        [TestCase(0, 0, 0,   0, 0, -1,  1, 0, 0,   0f,1f,0f,0f,  -1f,0f,0f,0f, 0f,0f,1f,0f,  0f,0f,0f,1f)]
        [TestCase(1, 2, 3,   4, 5, 6,   7, 8, 9,   0.4082483f,-0.7071068f,-0.5773503f,0f, -0.8164966f,0f,-0.5773503f,0f, 0.4082483f,0.7071068f,-0.5773503f,0f, 0f,-1.414214f,3.464102f,1f)]
        [TestCase(0, 0, 0,   0, 0, 1,   0, 1, 0,   -1f,0f,0f,0f, 0f,1f,0f,0f,  0f,0f,-1f,0f, 0f,0f,0f,1f)]
        public void TestCreateLookAt(
            float px, float py, float pz,
            float fx, float fy, float fz,
            float ux, float uy, float uz,
            float e11, float e12, float e13, float e14,
            float e21, float e22, float e23, float e24,
            float e31, float e32, float e33, float e34,
            float e41, float e42, float e43, float e44)
        {
            var position = new Vector3(px, py, pz);
            var forward = new Vector3(fx, fy, fz);
            var up = new Vector3(ux, uy, uz);

            var m = Matrix.CreateLookAt(position, forward, up);

            Assert.AreEqual(e11, m.M11, 0.000001f);
            Assert.AreEqual(e12, m.M12, 0.000001f);
            Assert.AreEqual(e13, m.M13, 0.000001f);
            Assert.AreEqual(e14, m.M14, 0.000001f);
            Assert.AreEqual(e21, m.M21, 0.000001f);
            Assert.AreEqual(e22, m.M22, 0.000001f);
            Assert.AreEqual(e23, m.M23, 0.000001f);
            Assert.AreEqual(e24, m.M24, 0.000001f);
            Assert.AreEqual(e31, m.M31, 0.000001f);
            Assert.AreEqual(e32, m.M32, 0.000001f);
            Assert.AreEqual(e33, m.M33, 0.000001f);
            Assert.AreEqual(e34, m.M34, 0.000001f);
            Assert.AreEqual(e41, m.M41, 0.000001f);
            Assert.AreEqual(e42, m.M42, 0.000001f);
            Assert.AreEqual(e43, m.M43, 0.000001f);
            Assert.AreEqual(e44, m.M44, 0.000001f);
        }

        [Test]
        [TestCase(3f, 2f, 0.01f, 10f, 0.006666666f,0f,0f,0f, 0f,0.01f,0f,0f, 0f,0f,-1.001001f,-1f, 0f,0f,-0.01001001f,0f)]
        [TestCase(10f, 6f, 5f, 50f,   1f,0f,0f,0f, 0f,1.666667f,0f,0f, 0f,0f,-1.111111f,-1f, 0f,0f,-5.555555f,0f)]
        [TestCase(5f, 3f, 1f, 100f,   0.4f,0f,0f,0f, 0f,0.6666667f,0f,0f, 0f,0f,-1.010101f,-1f, 0f,0f,-1.010101f,0f)]
        [TestCase(3f, 7.5f, 1f, 10f,  0.6666667f,0f,0f,0f, 0f,0.2666667f,0f,0f, 0f,0f,-1.111111f,-1f, 0f,0f,-1.111111f,0f)]
        public void TestOrthoPerspective(
            float width, float height, float znear, float zfar,
            float e11, float e12, float e13, float e14,
            float e21, float e22, float e23, float e24,
            float e31, float e32, float e33, float e34,
            float e41, float e42, float e43, float e44)
        {
            var m = Matrix.CreatePerspective(width, height, znear, zfar);

            Assert.AreEqual(e11, m.M11, 0.000001f);
            Assert.AreEqual(e12, m.M12, 0.000001f);
            Assert.AreEqual(e13, m.M13, 0.000001f);
            Assert.AreEqual(e14, m.M14, 0.000001f);
            Assert.AreEqual(e21, m.M21, 0.000001f);
            Assert.AreEqual(e22, m.M22, 0.000001f);
            Assert.AreEqual(e23, m.M23, 0.000001f);
            Assert.AreEqual(e24, m.M24, 0.000001f);
            Assert.AreEqual(e31, m.M31, 0.000001f);
            Assert.AreEqual(e32, m.M32, 0.000001f);
            Assert.AreEqual(e33, m.M33, 0.000001f);
            Assert.AreEqual(e34, m.M34, 0.000001f);
            Assert.AreEqual(e41, m.M41, 0.000001f);
            Assert.AreEqual(e42, m.M42, 0.000001f);
            Assert.AreEqual(e43, m.M43, 0.000001f);
            Assert.AreEqual(e44, m.M44, 0.000001f);
        }

        [Test]
        [TestCase(-3f, 3f, -2f, 2f, 0.05f, 10f,  0.01666667f,0f,0f,0f, 0f,0.025f,0f,0f, 0f,0f,-1.005025f,-1f, 0f,0f,-0.05025126f,0f)]
        [TestCase(-4f, 2f, -1f, 3f, 0.05f, 10f,  0.01666667f,0f,0f,0f, 0f,0.025f,0f,0f, -0.3333333f,0.5f,-1.005025f,-1f, 0f,0f,-0.05025126f,0f)]
        [TestCase(4f, 5f, 1f, 3f, 0.05f, 10f,    0.1f,0f,0f,0f, 0f,0.05f,0f,0f, 9f,2f,-1.005025f,-1f, 0f,0f,-0.05025126f,0f)]
        [TestCase(4f, 5f, 2f, 1f, 0.05f, 10f,    0.1f,0f,0f,0f, 0f,-0.1f,0f,0f, 9f,-3f,-1.005025f,-1f, 0f,0f,-0.05025126f,0f)]
        [TestCase(-3f, 3f, -2f, 2f, 0.2f, 1f,    0.06666667f,0f,0f,0f, 0f,0.1f,0f,0f, 0f,0f,-1.25f,-1f, 0f,0f,-0.25f,0f)]
        public void TestMatrixCreatePerspectiveOffCenter(
            float left, float right, float bottom, float top, float near, float far,
            float e11, float e12, float e13, float e14,
            float e21, float e22, float e23, float e24,
            float e31, float e32, float e33, float e34,
            float e41, float e42, float e43, float e44)
        {
            var m = Matrix.CreatePerspectiveOffCenter(left, right, bottom, top, near, far);

            Assert.AreEqual(e11, m.M11, 0.000001f);
            Assert.AreEqual(e12, m.M12, 0.000001f);
            Assert.AreEqual(e13, m.M13, 0.000001f);
            Assert.AreEqual(e14, m.M14, 0.000001f);
            Assert.AreEqual(e21, m.M21, 0.000001f);
            Assert.AreEqual(e22, m.M22, 0.000001f);
            Assert.AreEqual(e23, m.M23, 0.000001f);
            Assert.AreEqual(e24, m.M24, 0.000001f);
            Assert.AreEqual(e31, m.M31, 0.000001f);
            Assert.AreEqual(e32, m.M32, 0.000001f);
            Assert.AreEqual(e33, m.M33, 0.000001f);
            Assert.AreEqual(e34, m.M34, 0.000001f);
            Assert.AreEqual(e41, m.M41, 0.000001f);
            Assert.AreEqual(e42, m.M42, 0.000001f);
            Assert.AreEqual(e43, m.M43, 0.000001f);
            Assert.AreEqual(e44, m.M44, 0.000001f);
        }

        [Test]
        [TestCase(-3f, 3f, -2f, 2f, 0.2f, 1f,    0.3333333f,0f,0f,0f, 0f,0.5f,0f,0f, 0f,0f,-1.25f,0f, 0f,0f,-0.25f,1f)]
        [TestCase(4f, 5f, 2f, 1f, 0.05f, 10f,    2f,0f,0f,0f, 0f,-2f,0f,0f, 0f,0f,-0.1005025f,0f, -9f,3f,-0.005025126f,1f)]
        [TestCase(4f, 5f, 1f, 3f, 0.05f, 10f,    2f,0f,0f,0f, 0f,1f,0f,0f, 0f,0f,-0.1005025f,0f, -9f,-2f,-0.005025126f,1f)]
        [TestCase(-4f, 2f, -1f, 3f, 0.05f, 10f,  0.3333333f,0f,0f,0f, 0f,0.5f,0f,0f, 0f,0f,-0.1005025f,0f, 0.3333333f,-0.5f,-0.005025126f,1f)]
        [TestCase(-3f, 3f, -2f, 2f, 0.05f, 10f,  0.3333333f,0f,0f,0f, 0f,0.5f,0f,0f, 0f,0f,-0.1005025f,0f, 0f,0f,-0.005025126f,1f)]
        public void TestMatrixCreateOrthographicOffCenter(
            float left, float right, float bottom, float top, float near, float far,
            float e11, float e12, float e13, float e14,
            float e21, float e22, float e23, float e24,
            float e31, float e32, float e33, float e34,
            float e41, float e42, float e43, float e44)
        {
            var m = Matrix.CreateOrthographicOffCenter(left, right, bottom, top, near, far);

            Assert.AreEqual(e11, m.M11, 0.000001f);
            Assert.AreEqual(e12, m.M12, 0.000001f);
            Assert.AreEqual(e13, m.M13, 0.000001f);
            Assert.AreEqual(e14, m.M14, 0.000001f);
            Assert.AreEqual(e21, m.M21, 0.000001f);
            Assert.AreEqual(e22, m.M22, 0.000001f);
            Assert.AreEqual(e23, m.M23, 0.000001f);
            Assert.AreEqual(e24, m.M24, 0.000001f);
            Assert.AreEqual(e31, m.M31, 0.000001f);
            Assert.AreEqual(e32, m.M32, 0.000001f);
            Assert.AreEqual(e33, m.M33, 0.000001f);
            Assert.AreEqual(e34, m.M34, 0.000001f);
            Assert.AreEqual(e41, m.M41, 0.000001f);
            Assert.AreEqual(e42, m.M42, 0.000001f);
            Assert.AreEqual(e43, m.M43, 0.000001f);
            Assert.AreEqual(e44, m.M44, 0.000001f);
        }

        [Test]
        [TestCase(1f,0f,0f,0f,           1f,0f,0f,0f, 0f,-1f,0f,0f, 0f,0f,-1f,0f, 0f,0f,0f,1f)]
        [TestCase(0f,1f,0f,0f,           -1f,0f,0f,0f, 0f,1f,0f,0f, 0f,0f,-1f,0f, 0f,0f,0f,1f)]
        [TestCase(0f,0f,1f,0f,           -1f,0f,0f,0f, 0f,-1f,0f,0f, 0f,0f,1f,0f, 0f,0f,0f,1f)]
        [TestCase(0f,0f,0f,1f,           1f,0f,0f,0f, 0f,1f,0f,0f, 0f,0f,1f,0f, 0f,0f,0f,1f)]
        [TestCase(0.5f,0.5f,0.5f,0.5f,   0f,1f,0f,0f, 0f,0f,1f,0f, 1f,0f,0f,0f, 0f,0f,0f,1f)]
        [TestCase(2f,3f,4f,1f,           -49f,20f,10f,0f, 4f,-39f,28f,0f, 22f,20f,-25f,0f, 0f,0f,0f,1f)]
        [TestCase(-0.1f,-0.1f,-1f,-5f,   -1.02f,10.02f,-0.8f,0f, -9.98f,-1.02f,1.2f,0f, 1.2f,-0.8f,0.96f,0f, 0f,0f,0f,1f)]
        [TestCase(-0.1f,-0.1f,0.5f,-1f,  0.48f,-0.98f,-0.3f,0f, 1.02f,0.48f,0.1f,0f, 0.1f,-0.3f,0.96f,0f, 0f,0f,0f,1f)]
        [TestCase(-0.1f,-0.5f,-0.5f,1f,  0f,-0.9f,1.1f,0f, 1.1f,0.48f,0.3f,0f, -0.9f,0.7f,0.48f,0f, 0f,0f,0f,1f)]
        [TestCase(-0.1f,-0.5f,-1f,-0.1f, -1.5f,0.3f,0.1f,0f, -0.1f,-1.02f,1.02f,0f, 0.3f,0.98f,0.48f,0f, 0f,0f,0f,1f)]
        [TestCase(-0.1f,-0.5f,0.5f,-0.1f, 0f,0f,-0.2f,0f, 0.2f,0.48f,-0.48f,0f, 0f,-0.52f,0.48f,0f, 0f,0f,0f,1f)]
        [TestCase(-0.1f,-1f,-0.1f,0f,    -1.02f,0.2f,0.02f,0f, 0.2f,0.96f,0.2f,0f, 0.02f,0.2f,-1.02f,0f, 0f,0f,0f,1f)]
        [TestCase(-0.1f,-1f,0f,-1f,      -1f,0.2f,-2f,0f, 0.2f,0.98f,0.2f,0f, 2f,-0.2f,-1.02f,0f, 0f,0f,0f,1f)]
        [TestCase(-0.1f,0.5f,1f,-0.5f,   -1.5f,-1.1f,0.3f,0f, 0.9f,-1.02f,1.1f,0f, -0.7f,0.9f,0.48f,0f, 0f,0f,0f,1f)]
        [TestCase(-0.1f,0f,-0.5f,0f,     0.5f,0f,0.1f,0f, 0f,0.48f,0f,0f, 0.1f,0f,0.98f,0f, 0f,0f,0f,1f)]
        [TestCase(-0.1f,0f,-1f,-5f,      -1f,10f,0.2f,0f, -10f,-1.02f,1f,0f, 0.2f,-1f,0.98f,0f, 0f,0f,0f,1f)]
        [TestCase(-0.1f,1f,0.5f,0f,      -1.5f,-0.2f,-0.1f,0f, -0.2f,0.48f,1f,0f, -0.1f,1f,-1.02f,0f, 0f,0f,0f,1f)]
        [TestCase(-0.1f,1f,0f,-0.1f,     -1f,-0.2f,0.2f,0f, -0.2f,0.98f,0.02f,0f, -0.2f,-0.02f,-1.02f,0f, 0f,0f,0f,1f)]
        [TestCase(-0.5f,-0.1f,-0.5f,1f,  0.48f,-0.9f,0.7f,0f, 1.1f,0f,-0.9f,0f, 0.3f,1.1f,0.48f,0f, 0f,0f,0f,1f)]
        [TestCase(-0.5f,-0.5f,-1f,-0.5f, -1.5f,1.5f,0.5f,0f, -0.5f,-1.5f,1.5f,0f, 1.5f,0.5f,0f,0f, 0f,0f,0f,1f)]
        [TestCase(-0.5f,-1f,0.5f,-1f,    -1.5f,0f,-2.5f,0f, 2f,0f,0f,0f, 1.5f,-2f,-1.5f,0f, 0f,0f,0f,1f)]
        [TestCase(-0.5f,-1f,0.5f,-5f,    -1.5f,-4f,-10.5f,0f, 6f,0f,4f,0f, 9.5f,-6f,-1.5f,0f, 0f,0f,0f,1f)]
        [TestCase(-0.5f,-1f,0f,-5f,      -1f,1f,-10f,0f, 1f,0.5f,5f,0f, 10f,-5f,-1.5f,0f, 0f,0f,0f,1f)]
        [TestCase(-0.5f,0.5f,-0.5f,-0.1f, 0f,-0.4f,0.6f,0f, -0.6f,0f,-0.4f,0f, 0.4f,-0.6f,0f,0f, 0f,0f,0f,1f)]
        [TestCase(-0.5f,0.5f,-1f,-0.5f,  -1.5f,0.5f,1.5f,0f, -1.5f,-1.5f,-0.5f,0f, 0.5f,-1.5f,0f,0f, 0f,0f,0f,1f)]
        [TestCase(-0.5f,0.5f,0.5f,0f,    0f,-0.5f,-0.5f,0f, -0.5f,0f,0.5f,0f, -0.5f,0.5f,0f,0f, 0f,0f,0f,1f)]
        [TestCase(-0.5f,0.5f,0f,0f,      0.5f,-0.5f,0f,0f, -0.5f,0.5f,0f,0f, 0f,0f,0f,0f, 0f,0f,0f,1f)]
        [TestCase(-0.5f,0f,-0.1f,0f,     0.98f,0f,0.1f,0f, 0f,0.48f,0f,0f, 0.1f,0f,0.5f,0f, 0f,0f,0f,1f)]
        [TestCase(-0.5f,1f,0.5f,-0.5f,   -1.5f,-1.5f,0.5f,0f, -0.5f,0f,1.5f,0f, -1.5f,0.5f,-1.5f,0f, 0f,0f,0f,1f)]
        [TestCase(-0.5f,1f,1f,-1f,       -3f,-3f,1f,0f, 1f,-1.5f,3f,0f, -3f,1f,-1.5f,0f, 0f,0f,0f,1f)]
        [TestCase(-1f,-0.1f,0f,-1f,      0.98f,0.2f,-0.2f,0f, 0.2f,-1f,2f,0f, 0.2f,-2f,-1.02f,0f, 0f,0f,0f,1f)]
        [TestCase(-1f,-0.1f,0f,1f,       0.98f,0.2f,0.2f,0f, 0.2f,-1f,-2f,0f, -0.2f,2f,-1.02f,0f, 0f,0f,0f,1f)]
        [TestCase(-1f,-0.5f,0.5f,0f,     0f,1f,-1f,0f, 1f,-1.5f,-0.5f,0f, -1f,-0.5f,-1.5f,0f, 0f,0f,0f,1f)]
        [TestCase(-1f,-1f,-0.1f,-0.1f,   -1.02f,2.02f,0f,0f, 1.98f,-1.02f,0.4f,0f, 0.4f,0f,-3f,0f, 0f,0f,0f,1f)]
        [TestCase(-1f,-1f,-0.5f,-0.5f,   -1.5f,2.5f,0f,0f, 1.5f,-1.5f,2f,0f, 2f,0f,-3f,0f, 0f,0f,0f,1f)]
        [TestCase(-1f,-1f,1f,-5f,        -3f,-8f,-12f,0f, 12f,-3f,8f,0f, 8f,-12f,-3f,0f, 0f,0f,0f,1f)]
        [TestCase(-1f,0.5f,-0.1f,-5f,    0.48f,0f,5.2f,0f, -2f,-1.02f,9.9f,0f, -4.8f,-10.1f,-1.5f,0f, 0f,0f,0f,1f)]
        [TestCase(-1f,0.5f,-0.5f,1f,     0f,-2f,0f,0f, 0f,-1.5f,-2.5f,0f, 2f,1.5f,-1.5f,0f, 0f,0f,0f,1f)]
        [TestCase(-1f,0.5f,0f,1f,        0.5f,-1f,-1f,0f, -1f,-1f,-2f,0f, 1f,2f,-1.5f,0f, 0f,0f,0f,1f)]
        [TestCase(-1f,0f,-1f,1f,         -1f,-2f,2f,0f, 2f,-3f,-2f,0f, 2f,2f,-1f,0f, 0f,0f,0f,1f)]
        [TestCase(-1f,0f,1f,-0.1f,       -1f,-0.2f,-2f,0f, 0.2f,-3f,0.2f,0f, -2f,-0.2f,-1f,0f, 0f,0f,0f,1f)]
        [TestCase(-1f,1f,-0.1f,-0.1f,    -1.02f,-1.98f,0.4f,0f, -2.02f,-1.02f,0f,0f, 0f,-0.4f,-3f,0f, 0f,0f,0f,1f)]
        [TestCase(0.5f,-0.1f,-0.1f,-0.5f, 0.96f,0f,-0.2f,0f, -0.2f,0.48f,-0.48f,0f, 0f,0.52f,0.48f,0f, 0f,0f,0f,1f)]
        [TestCase(0.5f,-0.1f,-0.5f,-1f,  0.48f,0.9f,-0.7f,0f, -1.1f,0f,-0.9f,0f, -0.3f,1.1f,0.48f,0f, 0f,0f,0f,1f)]
        [TestCase(0.5f,-0.5f,-1f,-0.1f,  -1.5f,-0.3f,-1.1f,0f, -0.7f,-1.5f,0.9f,0f, -0.9f,1.1f,0f,0f, 0f,0f,0f,1f)]
        [TestCase(0.5f,-0.5f,1f,-0.1f,   -1.5f,-0.7f,0.9f,0f, -0.3f,-1.5f,-1.1f,0f, 1.1f,-0.9f,0f,0f, 0f,0f,0f,1f)]
        [TestCase(0.5f,-1f,-1f,-1f,      -3f,1f,-3f,0f, -3f,-1.5f,1f,0f, 1f,3f,-1.5f,0f, 0f,0f,0f,1f)]
        [TestCase(0.5f,-1f,0f,-1f,       -1f,-1f,-2f,0f, -1f,0.5f,-1f,0f, 2f,1f,-1.5f,0f, 0f,0f,0f,1f)]
        [TestCase(0.5f,0.5f,-1f,-1f,     -1.5f,2.5f,0f,0f, -1.5f,-1.5f,-2f,0f, -2f,0f,0f,0f, 0f,0f,0f,1f)]
        [TestCase(0.5f,0f,0.5f,1f,       0.5f,1f,0.5f,0f, -1f,0f,1f,0f, 0.5f,-1f,0.5f,0f, 0f,0f,0f,1f)]
        [TestCase(0.5f,0f,1f,-5f,        -1f,-10f,1f,0f, 10f,-1.5f,-5f,0f, 1f,5f,0.5f,0f, 0f,0f,0f,1f)]
        [TestCase(0.5f,1f,-0.5f,0f,      -1.5f,1f,-0.5f,0f, 1f,0f,-1f,0f, -0.5f,-1f,-1.5f,0f, 0f,0f,0f,1f)]
        [TestCase(0.5f,1f,-1f,-1f,       -3f,3f,1f,0f, -1f,-1.5f,-3f,0f, -3f,-1f,-1.5f,0f, 0f,0f,0f,1f)]
        [TestCase(0f,-0.1f,0.5f,-0.1f,   0.48f,-0.1f,-0.02f,0f, 0.1f,0.5f,-0.1f,0f, 0.02f,-0.1f,0.98f,0f, 0f,0f,0f,1f)]
        [TestCase(0f,-0.1f,1f,-0.5f,     -1.02f,-1f,-0.1f,0f, 1f,-1f,-0.2f,0f, 0.1f,-0.2f,0.98f,0f, 0f,0f,0f,1f)]
        [TestCase(0f,-0.5f,-0.1f,-0.5f,  0.48f,0.1f,-0.5f,0f, -0.1f,0.98f,0.1f,0f, 0.5f,0.1f,0.5f,0f, 0f,0f,0f,1f)]
        [TestCase(0f,-0.5f,-0.1f,-1f,    0.48f,0.2f,-1f,0f, -0.2f,0.98f,0.1f,0f, 1f,0.1f,0.5f,0f, 0f,0f,0f,1f)]
        [TestCase(0f,-1f,1f,1f,          -3f,2f,2f,0f, -2f,-1f,-2f,0f, -2f,-2f,-1f,0f, 0f,0f,0f,1f)]
        [TestCase(0f,0.5f,1f,0f,         -1.5f,0f,0f,0f, 0f,-1f,1f,0f, 0f,1f,0.5f,0f, 0f,0f,0f,1f)]
        [TestCase(0f,0f,-0.5f,-5f,       0.5f,5f,0f,0f, -5f,0.5f,0f,0f, 0f,0f,1f,0f, 0f,0f,0f,1f)]
        [TestCase(0f,0f,0f,-0.5f,        1f,0f,0f,0f, 0f,1f,0f,0f, 0f,0f,1f,0f, 0f,0f,0f,1f)]
        [TestCase(0f,1f,-0.5f,1f,        -1.5f,-1f,-2f,0f, 1f,0.5f,-1f,0f, 2f,-1f,-1f,0f, 0f,0f,0f,1f)]
        [TestCase(0f,1f,-1f,-5f,         -3f,10f,10f,0f, -10f,-1f,-2f,0f, -10f,-2f,-1f,0f, 0f,0f,0f,1f)]
        [TestCase(0f,1f,0f,1f,           -1f,0f,-2f,0f, 0f,1f,0f,0f, 2f,0f,-1f,0f, 0f,0f,0f,1f)]
        [TestCase(1f,-0.1f,-0.5f,-5f,    0.48f,4.8f,-2f,0f, -5.2f,-1.5f,-9.9f,0f, 0f,10.1f,-1.02f,0f, 0f,0f,0f,1f)]
        [TestCase(1f,-0.1f,-1f,-0.5f,    -1.02f,0.8f,-2.1f,0f, -1.2f,-3f,-0.8f,0f, -1.9f,1.2f,-1.02f,0f, 0f,0f,0f,1f)]
        [TestCase(1f,-0.1f,1f,0f,        -1.02f,-0.2f,2f,0f, -0.2f,-3f,-0.2f,0f, 2f,-0.2f,-1.02f,0f, 0f,0f,0f,1f)]
        [TestCase(1f,-0.5f,-0.1f,0f,     0.48f,-1f,-0.2f,0f, -1f,-1.02f,0.1f,0f, -0.2f,0.1f,-1.5f,0f, 0f,0f,0f,1f)]
        [TestCase(1f,-0.5f,-1f,0f,       -1.5f,-1f,-2f,0f, -1f,-3f,1f,0f, -2f,1f,-1.5f,0f, 0f,0f,0f,1f)]
        [TestCase(1f,-0.5f,0f,-5f,       0.5f,-1f,-5f,0f, -1f,-1f,-10f,0f, 5f,10f,-1.5f,0f, 0f,0f,0f,1f)]
        [TestCase(1f,-0.5f,1f,-0.5f,     -1.5f,-2f,1.5f,0f, 0f,-3f,-2f,0f, 2.5f,0f,-1.5f,0f, 0f,0f,0f,1f)]
        [TestCase(1f,-1f,-1f,-0.1f,      -3f,-1.8f,-2.2f,0f, -2.2f,-3f,1.8f,0f, -1.8f,2.2f,-3f,0f, 0f,0f,0f,1f)]
        [TestCase(1f,0.5f,0.5f,-0.1f,    0f,0.9f,1.1f,0f, 1.1f,-1.5f,0.3f,0f, 0.9f,0.7f,-1.5f,0f, 0f,0f,0f,1f)]
        [TestCase(1f,0f,-0.1f,-5f,       0.98f,1f,-0.2f,0f, -1f,-1.02f,-10f,0f, -0.2f,10f,-1f,0f, 0f,0f,0f,1f)]
        [TestCase(1f,0f,-0.5f,-1f,       0.5f,1f,-1f,0f, -1f,-1.5f,-2f,0f, -1f,2f,-1f,0f, 0f,0f,0f,1f)]
        [TestCase(1f,1f,-0.1f,1f,        -1.02f,1.8f,-2.2f,0f, 2.2f,-1.02f,1.8f,0f, 1.8f,-2.2f,-3f,0f, 0f,0f,0f,1f)]
        public void TestMatrixCreateFromQuaternion(
            float x, float y, float z, float w,
            float e11, float e12, float e13, float e14,
            float e21, float e22, float e23, float e24,
            float e31, float e32, float e33, float e34,
            float e41, float e42, float e43, float e44)
        {
            var q = new Quaternion(x, y, z, w);
            var m = Matrix.CreateFromQuaternion(q);

            Assert.AreEqual(e11, m.M11, 0.000001f);
            Assert.AreEqual(e12, m.M12, 0.000001f);
            Assert.AreEqual(e13, m.M13, 0.000001f);
            Assert.AreEqual(e14, m.M14, 0.000001f);
            Assert.AreEqual(e21, m.M21, 0.000001f);
            Assert.AreEqual(e22, m.M22, 0.000001f);
            Assert.AreEqual(e23, m.M23, 0.000001f);
            Assert.AreEqual(e24, m.M24, 0.000001f);
            Assert.AreEqual(e31, m.M31, 0.000001f);
            Assert.AreEqual(e32, m.M32, 0.000001f);
            Assert.AreEqual(e33, m.M33, 0.000001f);
            Assert.AreEqual(e34, m.M34, 0.000001f);
            Assert.AreEqual(e41, m.M41, 0.000001f);
            Assert.AreEqual(e42, m.M42, 0.000001f);
            Assert.AreEqual(e43, m.M43, 0.000001f);
            Assert.AreEqual(e44, m.M44, 0.000001f);
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

            float delta = 0.0001f;

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

