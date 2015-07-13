using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ChamberLib;

namespace ChamberLibTests
{
    [TestFixture]
    public class TriangleTest
    {
        [Test]
        public void TriangleIntersectsRay1()
        {
            // when
            var triangle = new Triangle(Vector3.Zero, Vector3.UnitX, Vector3.UnitY);
            var ray = new Ray(new Vector3(0.1f, 0.1f, 0.1f), new Vector3(0, 0, -1));

            // expect
            Assert.True(triangle.Intersects(ray));
        }

        [Test]
        public void TriangleIntersectsRay2()
        {
            // when
            var triangle = new Triangle(Vector3.Zero, Vector3.UnitX, Vector3.UnitY);
            var ray = new Ray(new Vector3(0.5f, 0.5f, 0.1f), new Vector3(0, 0, -1));

            // expect
            Assert.True(triangle.Intersects(ray));
        }

        [Test]
        public void TriangleIntersectsRay3()
        {
            // when
            var triangle = new Triangle(Vector3.Zero, Vector3.UnitX, Vector3.UnitY);
            var ray = new Ray(new Vector3(0.501f, 0.501f, 0.1f), new Vector3(0, 0, -1));

            // expect
            Assert.False(triangle.Intersects(ray));
        }

        [Test]
        public void TriangleIntersectsRay4()
        {
            // when
            var triangle = new Triangle(Vector3.UnitX, Vector3.UnitY, Vector3.UnitZ);
            var ray = new Ray(Vector3.Zero, new Vector3(1, 1, 1).Normalized());

            // expect
            Assert.True(triangle.Intersects(ray));
        }

        [Test]
        public void TriangleIntersectsRay5()
        {
            // given
            var triangle = new Triangle(Vector3.UnitX, Vector3.UnitY, Vector3.UnitZ);
            var pos = new Vector3(1, 1, 1) / 3.0f;
            var dir = new Vector3(1, 1, 1).Normalized();
            var ray = new Ray(pos, dir);

            // expect
            Assert.True(triangle.Intersects(ray, 0.00001f));
        }

        [Test]
        public void TriangleIntersectsRay6()
        {
            // given
            var triangle = new Triangle(Vector3.UnitX, Vector3.UnitY, Vector3.UnitZ);
            var n111 = new Vector3(1, 1, 1).Normalized();
            var ray = new Ray(new Vector3(1, 1, 1), n111);

            // expect
            Assert.False(triangle.Intersects(ray));
        }

        [Test]
        public void TriangleIntersectsRay7()
        {
            // given
            var triangle = new Triangle(Vector3.UnitX, Vector3.UnitY, Vector3.UnitZ);
            var ray = new Ray(Vector3.Zero, Vector3.UnitX);

            // expect
            Assert.IsTrue(triangle.Intersects(ray));
        }

        [Test]
        public void TriangleIntersectsRay8()
        {
            // given
            var triangle = new Triangle(Vector3.UnitX, Vector3.UnitY, Vector3.UnitZ);
            var ray = new Ray(Vector3.Zero, Vector3.UnitY);

            // expect
            Assert.IsTrue(triangle.Intersects(ray));
        }

        [Test]
        public void TriangleIntersectsRay9()
        {
            // given
            var triangle = new Triangle(Vector3.UnitX, Vector3.UnitY, Vector3.UnitZ);
            var ray = new Ray(Vector3.Zero, Vector3.UnitZ);

            // expect
            Assert.IsTrue(triangle.Intersects(ray));
        }
    }
}
