
//
// ChamberLib, a cross-platform game engine
// Copyright (C) 2021 izrik and Metaphysics Industries, Inc.
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301
// USA
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ChamberLib;

namespace ChamberLibTests.MathT
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
