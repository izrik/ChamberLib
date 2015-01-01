using System;
using NUnit.Framework;
using ChamberLib;
using Xna = Microsoft.Xna.Framework;

namespace ChamberLibTests
{
    [TestFixture]
    public class FrustumTest
    {
        [Test]
        public void TestCompareFrustumPlanes()
        {
            var view = Matrix.CreateLookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
            var proj = Matrix.CreatePerspective(1, 1, 0.5f, 1);
            var viewproj = view * proj;
            var f = new Frustum(viewproj);

            var xview = Xna.Matrix.CreateLookAt(Xna.Vector3.Zero, Xna.Vector3.UnitZ, Xna.Vector3.UnitY);
            var xproj = Xna.Matrix.CreatePerspective(1, 1, 0.5f, 1);
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
        public void TestCompareFrustumCorners()
        {
            var view = Matrix.CreateLookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
            var proj = Matrix.CreatePerspective(1, 1, 0.5f, 1);
            var viewproj = view * proj;
            var f = new Frustum(viewproj);
            var corners = f.Corners;

            var xview = Xna.Matrix.CreateLookAt(Xna.Vector3.Zero, Xna.Vector3.UnitZ, Xna.Vector3.UnitY);
            var xproj = Xna.Matrix.CreatePerspective(1, 1, 0.5f, 1);
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
        }
    }
}

