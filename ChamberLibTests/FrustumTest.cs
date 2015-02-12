using System;
using NUnit.Framework;
using ChamberLib;

namespace ChamberLibTests
{
    [TestFixture]
    public class FrustumTest
    {
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
        [TestCase(5, 5, ContainmentType.Contains)]
        [TestCase(0, 0, ContainmentType.Contains)]
        [TestCase(2, 0, ContainmentType.Contains)]
        public void TestFrustumSphereIntersect(float x, float y, ContainmentType expected)
        {
            var view = Matrix.CreateLookAt(new Vector3(-5, 5, 0), Vector3.Zero, Vector3.UnitY);
            var proj = Matrix.CreatePerspective(6, 6, 0.1f, 10);

            var viewproj = view * proj;

            var f = new Frustum(viewproj);
            var s = new Sphere(new Vector3(x, y, 0), 1);
            var actual = f.Contains(s);

            Assert.AreEqual(expected, actual);
        }
    }
}

