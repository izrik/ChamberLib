using NUnit.Framework;
using System;
using ChamberLib;
using XMatrix = Microsoft.Xna.Framework.Matrix;
using XVector3 = Microsoft.Xna.Framework.Vector3;
using XVector4 = Microsoft.Xna.Framework.Vector4;

namespace ChamberLibTests
{
    [TestFixture()]
    public class Test
    {
        static Random rand = new Random();

        [Test()]
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

//            Assert.AreEqual(reference.ToChamber(), m);

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
        [TestCase(new float[] { 1, 0, 0,   0, 1, 0, })]
        [TestCase(new float[] { 0, 1, 0,   0, 0, 1, })]
        [TestCase(new float[] { 0, 0, 1,   1, 0, 0, })]
        [TestCase(new float[] { -1, 0, 0,   0, 1, 0, })]
        [TestCase(new float[] { 0, -1, 0,   0, 0, 1, })]
        [TestCase(new float[] { 0, 0, -1,   1, 0, 0, })]
        public void Vector3CrossTest(float[] fs)
        {
            var a = new Vector3(fs[0], fs[1], fs[2]);
            var b = new Vector3(fs[3], fs[4], fs[5]);

            var expected = Microsoft.Xna.Framework.Vector3.Cross(a.ToXna(), b.ToXna()).ToChamber();

            var actual = Vector3.Cross(a, b);

            Assert.AreEqual(expected, actual);
        }

//        [Test]
//        public void Vector4Transform


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

        float NextFloat()
        {
            return (float)rand.NextDouble();
        }

        [Test]
        [TestCase(4f, 0f, 5f)]
        [TestCase(5f, 0f, 5f)]
        [TestCase(4f, 0f, 4f)]
        [TestCase(5f, 0f, 4f)]
        public void TestViewportProject(float x, float y, float z)
        {
            var vp = new Viewport(0, 0, 800, 480);
            const float width = 9f;
            float aspectRatio = vp.Width / (float)vp.Height;
            var proj = Matrix.CreateOrthographic(width, width / aspectRatio, 0.05f, 50);

            var target = new Vector3(4, 0, 5);
            const float distance = 10f;
            const float cameraPositionTheta = (float)(5 * Math.PI / 4);
            const float cameraPositionPhi = (float)(Math.PI / 6);
            Vector3 cameraPosition =
                distance * new Vector3(
                    (float)Math.Cos(cameraPositionTheta) * (float)Math.Cos(cameraPositionPhi),
                    (float)Math.Sin(cameraPositionPhi),
                    (float)Math.Sin(cameraPositionTheta) * (float)Math.Cos(cameraPositionPhi));
            var view = Matrix.CreateLookAt((cameraPosition + target), target, Vector3.UnitY);

            var v = new Vector3(4, 0, 5);

            var u = vp.Project(v, proj, view, Matrix.Identity);

            var vp2 = new Microsoft.Xna.Framework.Graphics.Viewport(0, 0, 800, 480);

            var u2 = vp2.Project(v.ToXna(), proj.ToXna(), view.ToXna(), Microsoft.Xna.Framework.Matrix.Identity);

            Assert.AreEqual(u2.X, u.X);
            Assert.AreEqual(u2.Y, u.Y);
            Assert.AreEqual(u2.Z, u.Z);
        }

        [Test]
        [TestCase(1,0,0,0,1,0,0,0)]
        [TestCase(0,1,0,0,0,1,0,0)]
        [TestCase(0,0,1,0,0,0,1,0)]
        [TestCase(0,0,0,1,0,0,0,1)]
        [TestCase(0.5f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f)]
        [TestCase(2,3,4,1,5,6,7,1)]
        public void TestQuaternionMultiply(float x1, float y1, float z1, float w1, float x2, float y2, float z2, float w2)
        {
            var q1 = new Quaternion(x1, y1, z1, w1);
            var q2 = new Quaternion(x2, y2, z2, w2);
            var q3 = q1 * q2;

            var xq1 = new Microsoft.Xna.Framework.Quaternion(x1, y1, z1, w1);
            var xq2 = new Microsoft.Xna.Framework.Quaternion(x2, y2, z2, w2);
            var xq3 = xq1 * xq2;

            Assert.AreEqual(xq3.X, q3.X);
            Assert.AreEqual(xq3.Y, q3.Y);
            Assert.AreEqual(xq3.Z, q3.Z);
            Assert.AreEqual(xq3.W, q3.W);
        }

        [Test]
        [TestCase(1,0,0,0)]
        [TestCase(0,1,0,0)]
        [TestCase(0,0,1,0)]
        [TestCase(0,0,0,1)]
        [TestCase(0.5f,0.5f,0.5f,0.5f)]
        [TestCase(2,3,4,1)]
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
        [TestCase(1,0,0,0,1,0,0)]
        [TestCase(0,1,0,0,1,0,0)]
        [TestCase(0,0,1,0,1,0,0)]
        [TestCase(0,0,0,1,1,0,0)]
        [TestCase(0.5f,0.5f,0.5f,0.5f,1,0,0)]
        [TestCase(2,3,4,1,1,0,0)]
        [TestCase(1,0,0,0,0,1,0)]
        [TestCase(0,1,0,0,0,1,0)]
        [TestCase(0,0,1,0,0,1,0)]
        [TestCase(0,0,0,1,0,1,0)]
        [TestCase(0.5f,0.5f,0.5f,0.5f,0,1,0)]
        [TestCase(2,3,4,1,0,1,0)]
        [TestCase(1,0,0,0,0,0,1)]
        [TestCase(0,1,0,0,0,0,1)]
        [TestCase(0,0,1,0,0,0,1)]
        [TestCase(0,0,0,1,0,0,1)]
        [TestCase(0.5f,0.5f,0.5f,0.5f,0,0,1)]
        [TestCase(2,3,4,1,0,0,1)]
        [TestCase(1,0,0,0,5,6,7)]
        [TestCase(0,1,0,0,5,6,7)]
        [TestCase(0,0,1,0,5,6,7)]
        [TestCase(0,0,0,1,5,6,7)]
        [TestCase(0.5f,0.5f,0.5f,0.5f,5,6,7)]
        [TestCase(2,3,4,1,5,6,7)]
        public void TestQuaternionTransform(float x, float y, float z, float w, float vx, float vy, float vz)
        {
            var q = new Quaternion(x, y, z, w);
            var v = new Vector3(vx, vy, vz);

            var v2 = q.Normalized().Transform(v);

            var m = Matrix.CreateFromQuaternion(q);
            var v3 = Vector3.Transform(v, m);

            Assert.AreEqual(v3.X, v2.X, 0.000001f);
            Assert.AreEqual(v3.Y, v2.Y, 0.000001f);
            Assert.AreEqual(v3.Z, v2.Z, 0.000001f);
        }

        [Test]
        public void TestCompareFrustumPlanes()
        {
            var view = Matrix.CreateLookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
            var proj = Matrix.CreatePerspective(1, 1, 0.5f, 1);
            var viewproj = view * proj;
            var f = new Frustum(viewproj);

            var xview = XMatrix.CreateLookAt(XVector3.Zero, XVector3.UnitZ, XVector3.UnitY);
            var xproj = XMatrix.CreatePerspective(1, 1, 0.5f, 1);
            var xviewproj = xview * xproj;
            var xf = new Microsoft.Xna.Framework.BoundingFrustum(xviewproj);

            Assert.AreEqual(xf.Top.Normal.ToChamber(), f.Top.Normal);
            Assert.AreEqual(xf.Top.D, f.Top.Distance);
            Assert.AreEqual(xf.Bottom.Normal.ToChamber(), f.Bottom.Normal);
            Assert.AreEqual(xf.Bottom.D, f.Bottom.Distance);
            Assert.AreEqual(xf.Left.Normal.ToChamber(), f.Left.Normal);
            Assert.AreEqual(xf.Left.D, f.Left.Distance);
            Assert.AreEqual(xf.Right.Normal.ToChamber(), f.Right.Normal);
            Assert.AreEqual(xf.Right.D, f.Right.Distance);
            Assert.AreEqual(xf.Near.Normal.ToChamber(), f.Near.Normal);
            Assert.AreEqual(xf.Near.D, f.Near.Distance);
            Assert.AreEqual(xf.Far.Normal.ToChamber(), f.Far.Normal);
            Assert.AreEqual(xf.Far.D, f.Far.Distance);
        }


        [Test]
        public void TestFrustumTop()
        {
            var view = Matrix.CreateLookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
            var proj = Matrix.CreatePerspective(1, 1, 0.5f, 1);

            var f = new Frustum(view * proj);

            Assert.AreEqual(new Vector3(0, 1, -1).Normalized(), f.Top.Normal);
            Assert.AreEqual(0, f.Top.Distance);
        }

        [Test]
        public void TestFrustumBottom()
        {
            var view = Matrix.CreateLookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
            var proj = Matrix.CreatePerspective(1, 1, 0.5f, 1);

            var f = new Frustum(view * proj);

            Assert.AreEqual(new Vector3(0, -1, -1).Normalized(), f.Bottom.Normal);
            Assert.AreEqual(0, f.Bottom.Distance);
        }

        [Test]
        public void TestFrustumLeft()
        {
            var view = Matrix.CreateLookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
            var proj = Matrix.CreatePerspective(1, 1, 0.5f, 1);

            var f = new Frustum(view * proj);

            Assert.AreEqual(new Vector3(1, 0, -1).Normalized(), f.Left.Normal);
            Assert.AreEqual(0, f.Left.Distance);
        }

        [Test]
        public void TestFrustumRight()
        {
            var view = Matrix.CreateLookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
            var proj = Matrix.CreatePerspective(1, 1, 0.5f, 1);

            var f = new Frustum(view * proj);

            Assert.AreEqual(new Vector3(-1, 0, -1).Normalized(), f.Right.Normal);
            Assert.AreEqual(0, f.Right.Distance);
        }

        [Test]
        public void TestFrustumNear()
        {
            var view = Matrix.CreateLookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
            var proj = Matrix.CreatePerspective(1, 1, 0.5f, 1);

            var f = new Frustum(view * proj);

            Assert.AreEqual(-Vector3.UnitZ, f.Near.Normal);
            Assert.AreEqual(0.5f, f.Near.Distance);
        }

        [Test]
        public void TestFrustumFar()
        {
            var view = Matrix.CreateLookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
            var proj = Matrix.CreatePerspective(1, 1, 0.5f, 1);

            var f = new Frustum(view * proj);

            Assert.AreEqual(Vector3.UnitZ, f.Far.Normal);
            Assert.AreEqual(-1f, f.Far.Distance);
        }

        [Test]
        [TestCase(5, 5)]
        [TestCase(0, 0)]
        [TestCase(2, 0)]
        public void TestFrustumSphereIntersect(float x, float y)
        {
            var view = Matrix.CreateLookAt(new Vector3(-5, 5, 0), Vector3.Zero, Vector3.UnitY);
            var proj = Matrix.CreatePerspective(6, 6, 0.1f, 10);

            var viewproj = view * proj;

            var f = new Frustum(viewproj);
            var s = new Sphere(new Vector3(x, y, 0), 1);
            var actual = f.Contains(s);

            var xf = new Microsoft.Xna.Framework.BoundingFrustum(viewproj.ToXna());
            var xs = new Microsoft.Xna.Framework.BoundingSphere(new Microsoft.Xna.Framework.Vector3(x, y, 0), 1);
            var expected = xf.Contains(xs).ToChamber();

            Assert.AreEqual(expected, actual);
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

            var xview = XMatrix.CreateLookAt(XVector3.Zero, XVector3.UnitZ, XVector3.UnitY);
            var xproj = XMatrix.CreatePerspective(1, 1, 0.5f, 1);
            var xviewproj = xview * xproj;
            var expected = XVector4.Transform(new XVector4(x, y, z, 1), xviewproj).ToChamber();

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

            var f = new Frustum(viewproj);

            var xview = XMatrix.CreateLookAt(XVector3.Zero, XVector3.UnitZ, XVector3.UnitY);
            var xproj = XMatrix.CreatePerspective(1, 1, 0.5f, 1);
            var xviewproj = xview * xproj;
            var xv = XVector4.Transform(new XVector4(x, y, z, 1), xviewproj).ToChamber();
            var xexpected = xv.ToVectorXYZ() / xv.W;

            var xf = new Microsoft.Xna.Framework.BoundingFrustum(xviewproj);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCompareFrustumCorners()
        {
            var view = Matrix.CreateLookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
            var proj = Matrix.CreatePerspective(1, 1, 0.5f, 1);
            var viewproj = view * proj;
            var f = new Frustum(viewproj);
            var corners = f.Corners;

            var xview = XMatrix.CreateLookAt(XVector3.Zero, XVector3.UnitZ, XVector3.UnitY);
            var xproj = XMatrix.CreatePerspective(1, 1, 0.5f, 1);
            var xviewproj = xview * xproj;
            var xf = new Microsoft.Xna.Framework.BoundingFrustum(xviewproj);
            var xcorners = xf.GetCorners();

            Assert.AreEqual(xcorners.Length, corners.Length);
            Assert.Contains(xcorners[0].ToChamber(), corners);
            Assert.Contains(xcorners[1].ToChamber(), corners);
            Assert.Contains(xcorners[2].ToChamber(), corners);
            Assert.Contains(xcorners[3].ToChamber(), corners);
            Assert.Contains(xcorners[4].ToChamber(), corners);
            Assert.Contains(xcorners[5].ToChamber(), corners);
            Assert.Contains(xcorners[6].ToChamber(), corners);
            Assert.Contains(xcorners[7].ToChamber(), corners);
//            Assert.AreEqual(xcorners.ToChamber(), corners);
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
    }
}

