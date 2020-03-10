using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ChamberLib;

namespace ChamberLibTests
{
    [TestFixture]
    public class PlaneTest
    {
        [Test]
        public void TestProject()
        {
            var plane = new Plane(Vector3.UnitX, 1.25f);
            var v = new Vector3(1, 2, 3);
            var p = plane.Project(v);

            Assert.AreEqual(new Vector3(1.25f, 2, 3), p);
        }

        [Test]
        [TestCase(1, 0, 0)]
        [TestCase(1, 2, 3)]
        [TestCase(1, -2, 3)]
        public void PlaneIntersectsPoint(float x, float y, float z)
        {
            // given
            var plane = new Plane(Vector3.UnitX, 1);
            var v = new Vector3(x, y, z);

            // when
            var p = plane.IntersectsPoint(v);

            // then
            Assert.AreEqual(p, PlaneIntersectionType.Intersecting);
        }

        [Test]
        [TestCase(2, 0, 0)]
        [TestCase(1.0001f, 2, 3)]
        public void PlaneIntersectsPointFront(float x, float y, float z)
        {
            // given
            var plane = new Plane(Vector3.UnitX, 1);
            var v = new Vector3(x, y, z);

            // when
            var p = plane.IntersectsPoint(v);

            // then
            Assert.AreEqual(p, PlaneIntersectionType.Front);
        }

        [Test]
        [TestCase(0, 0, 0)]
        [TestCase(-1, 0, 0)]
        [TestCase(0.9999f, 2, 3)]
        public void PlaneIntersectsPointBack(float x, float y, float z)
        {
            // given
            var plane = new Plane(Vector3.UnitX, 1);
            var v = new Vector3(x,y,z);

            // when
            var p = plane.IntersectsPoint(v);

            // then
            Assert.AreEqual(p, PlaneIntersectionType.Back);
        }

        [Test]
        public void Plane_FromPoints()
        {
            // given
            var v1 = Vector3.Zero;
            var v2 = Vector3.UnitX;
            var v3 = Vector3.UnitY;

            // when
            var p = Plane.FromPoints(v1, v2, v3);

            // then
            Assert.AreEqual(0, p.Distance);
            Assert.AreEqual(Vector3.UnitZ, p.Normal);
        }

        [Test]
        public void Plane_FromPoints_DegenerateDefaultsToNormalOfUnitX()
        {
            // given
            var v1 = Vector3.Zero;
            var v2 = Vector3.UnitX;
            var v3 = 2 * Vector3.UnitX;

            // when
            var p = Plane.FromPoints(v1, v2, v3);

            // then
            Assert.AreEqual(0, p.Distance);
            Assert.AreEqual(Vector3.UnitX, p.Normal);
        }
    }
}
