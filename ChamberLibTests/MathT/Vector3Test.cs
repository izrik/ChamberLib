
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

using NUnit.Framework;
using ChamberLib;

namespace ChamberLibTests.MathT
{
    [TestFixture]
    public class Vector3Test
    {
        [Test]
        [TestCase(1, 0, 0, 0, 1, 0, 0, 0, 1)]
        [TestCase(0, 1, 0, 0, 0, 1, 1, 0, 0)]
        [TestCase(0, 0, 1, 1, 0, 0, 0, 1, 0)]
        [TestCase(-1, 0, 0, 0, 1, 0, 0, 0, -1)]
        [TestCase(0, -1, 0, 0, 0, 1, -1, 0, 0)]
        [TestCase(0, 0, -1, 1, 0, 0, 0, -1, 0)]
        [TestCase(2, 0, 0, 0, 3, 0, 0, 0, 6)]
        public void Vector3CrossTest(float ax, float ay, float az, float bx, float by, float bz, float ex, float ey, float ez)
        {
            var a = new Vector3(ax, ay, az);
            var b = new Vector3(bx, by, bz);

            var expected = new Vector3(ex, ey, ez);

            var actual = Vector3.Cross(a, b);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase(1, 0, 0, 0, 1, 0, 90, 0, 0, -1)]
        [TestCase(1, 0, 0, 0, 1, 0, -90, 0, 0, 1)]
        [TestCase(1, 0, 0, 0, -1, 0, 90, 0, 0, 1)]
        [TestCase(1, 0, 0, 0, -1, 0, -90, 0, 0, -1)]
        [TestCase(1, 0, 0, 0, 0, 1, 90, 0, 1, 0)]
        [TestCase(1, 0, 0, 0, 0, 1, -90, 0, -1, 0)]
        [TestCase(1, 0, 0, 0, 0, -1, 90, 0, -1, 0)]
        [TestCase(1, 0, 0, 0, 0, -1, -90, 0, 1, 0)]
        [TestCase(1, 0, 0, 1, 1, 1, 120, 0, 1, 0)]
        [TestCase(1, 0, 0, 1, 1, 1, 240, 0, 0, 1)]
        [TestCase(1, 0, 0, 1, 1, 1, -120, 0, 0, 1)]
        [TestCase(1, 2, 3, 4, 5, 6, 78.9f, 1.8703f, 1.3919f, 2.9265f)]
        public void RotateAboutAxis(float vx, float vy, float vz, float ax, float ay, float az, float degrees, float ex, float ey, float ez)
        {
            // given
            Vector3 v = new Vector3(vx, vy, vz);
            Vector3 axis = new Vector3(ax, ay, az);
            float angle = degrees.ToRadians();

            // when
            Vector3 result = v.RotateAboutAxis(axis, angle);

            // then
            Assert.AreEqual(ex, result.X, 0.0001f);
            Assert.AreEqual(ey, result.Y, 0.0001f);
            Assert.AreEqual(ez, result.Z, 0.0001f);
        }
    }
}


