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

            // when
            var p = triangle.Intersects(ray);

            // then
            Assert.True(p.HasValue);
            Assert.AreEqual(0.1f, p.Value.X, 0.00001f);
            Assert.AreEqual(0.1f, p.Value.Y, 0.00001f);
            Assert.AreEqual(0.0f, p.Value.Z, 0.00001f);
        }

        [Test]
        public void TriangleIntersectsRay2()
        {
            // when
            var triangle = new Triangle(Vector3.Zero, Vector3.UnitX, Vector3.UnitY);
            var ray = new Ray(new Vector3(0.5f, 0.5f, 0.1f), new Vector3(0, 0, -1));

            // when
            var p = triangle.Intersects(ray);

            // then
            Assert.True(p.HasValue);
            Assert.AreEqual(0.5f, p.Value.X, 0.00001f);
            Assert.AreEqual(0.5f, p.Value.Y, 0.00001f);
            Assert.AreEqual(0.0f, p.Value.Z, 0.00001f);
        }

        [Test]
        public void TriangleIntersectsRay3()
        {
            // when
            var triangle = new Triangle(Vector3.Zero, Vector3.UnitX, Vector3.UnitY);
            var ray = new Ray(new Vector3(0.501f, 0.501f, 0.1f), new Vector3(0, 0, -1));

            // when
            var p = triangle.Intersects(ray);

            // then
            Assert.False(p.HasValue);
        }

        [Test]
        public void TriangleIntersectsRay4()
        {
            // when
            var triangle = new Triangle(Vector3.UnitX, Vector3.UnitY, Vector3.UnitZ);
            var ray = new Ray(Vector3.Zero, new Vector3(1, 1, 1).Normalized());

            // when
            var p = triangle.Intersects(ray);

            // then
            Assert.True(p.HasValue);
            Assert.AreEqual(1/3.0f, p.Value.X, 0.00001f);
            Assert.AreEqual(1/3.0f, p.Value.Y, 0.00001f);
            Assert.AreEqual(1/3.0f, p.Value.Z, 0.00001f);
        }

        [Test]
        public void TriangleIntersectsRay5()
        {
            // given
            var triangle = new Triangle(Vector3.UnitX, Vector3.UnitY, Vector3.UnitZ);
            var pos = new Vector3(1, 1, 1) / 3.0f;
            var dir = new Vector3(1, 1, 1).Normalized();
            var ray = new Ray(pos, dir);

            // when
            var p = triangle.Intersects(ray, 0.00001f);

            // then
            Assert.True(p.HasValue);
            Assert.AreEqual(pos.X, p.Value.X, 0.00001f);
            Assert.AreEqual(pos.Y, p.Value.Y, 0.00001f);
            Assert.AreEqual(pos.Z, p.Value.Z, 0.00001f);
        }

        [Test]
        public void TriangleIntersectsRay6()
        {
            // given
            var triangle = new Triangle(Vector3.UnitX, Vector3.UnitY, Vector3.UnitZ);
            var n111 = new Vector3(1, 1, 1).Normalized();
            var ray = new Ray(new Vector3(1, 1, 1), n111);

            // when
            var p = triangle.Intersects(ray);

            // then
            Assert.False(p.HasValue);
        }

        [Test]
        public void TriangleIntersectsRay7()
        {
            // given
            var triangle = new Triangle(Vector3.UnitX, Vector3.UnitY, Vector3.UnitZ);
            var ray = new Ray(Vector3.Zero, Vector3.UnitX);

            // when
            var p = triangle.Intersects(ray);

            // then
            Assert.True(p.HasValue);
            Assert.AreEqual(1, p.Value.X, 0.00001f);
            Assert.AreEqual(0, p.Value.Y, 0.00001f);
            Assert.AreEqual(0, p.Value.Z, 0.00001f);
        }

        [Test]
        public void TriangleIntersectsRay8()
        {
            // given
            var triangle = new Triangle(Vector3.UnitX, Vector3.UnitY, Vector3.UnitZ);
            var ray = new Ray(Vector3.Zero, Vector3.UnitY);

            // when
            var p = triangle.Intersects(ray);

            // then
            Assert.True(p.HasValue);
            Assert.AreEqual(0, p.Value.X, 0.00001f);
            Assert.AreEqual(1, p.Value.Y, 0.00001f);
            Assert.AreEqual(0, p.Value.Z, 0.00001f);
        }

        [Test]
        public void TriangleIntersectsRay9()
        {
            // given
            var triangle = new Triangle(Vector3.UnitX, Vector3.UnitY, Vector3.UnitZ);
            var ray = new Ray(Vector3.Zero, Vector3.UnitZ);

            // when
            var p = triangle.Intersects(ray);

            // then
            Assert.True(p.HasValue);
            Assert.AreEqual(0, p.Value.X, 0.00001f);
            Assert.AreEqual(0, p.Value.Y, 0.00001f);
            Assert.AreEqual(1, p.Value.Z, 0.00001f);
        }

        [Test]
        public void IsDegenerate_ThreeEqualVertices_YieldsTrue()
        {
            // given
            var t = new Triangle(Vector3.Zero, Vector3.Zero, Vector3.Zero);

            // expect
            Assert.True(t.IsDegenerate());
        }

        [Test]
        public void IsDegenerate_TwoEqualVertices_YieldsTrue()
        {
            // given
            var t = new Triangle(Vector3.UnitX, Vector3.Zero, Vector3.Zero);

            // expect
            Assert.True(t.IsDegenerate());

            // given
            t = new Triangle(Vector3.Zero, Vector3.UnitX, Vector3.Zero);

            // expect
            Assert.True(t.IsDegenerate());

            // given
            t = new Triangle(Vector3.Zero, Vector3.Zero, Vector3.UnitX);

            // expect
            Assert.True(t.IsDegenerate());
        }

        [Test]
        public void IsDegenerate_ColinearVertices_YieldsTrue()
        {
            // given
            var t = new Triangle(Vector3.Zero, Vector3.UnitX, 2 * Vector3.UnitX);

            // expect
            Assert.True(t.IsDegenerate());
        }

        [Test]
        public void IsDegenerate_NormalTriangle_YieldsFalse()
        {
            // given
            var t = new Triangle(Vector3.UnitX, Vector3.UnitY, Vector3.UnitZ);

            // expect
            Assert.False(t.IsDegenerate());
        }
    }
}
