
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
using NUnit.Framework;
using ChamberLib;

namespace ChamberLibTests.MathT
{
    [TestFixture]
    public class QuaternionTest
    {
        [Test]
        [TestCase(1f,0f,0f,0f, 1f,0f,0f,0f, 0f,0f,0f,-1f)]
        [TestCase(0f,1f,0f,0f, 0f,1f,0f,0f, 0f,0f,0f,-1f)]
        [TestCase(0f,0f,1f,0f, 0f,0f,1f,0f, 0f,0f,0f,-1f)]
        [TestCase(0f,0f,0f,1f, 0f,0f,0f,1f, 0f,0f,0f,1f)]
        [TestCase(0.5f,0.5f,0.5f,0.5f, 0.5f,0.5f,0.5f,0.5f, 0.5f,0.5f,0.5f,-0.5f)]
        [TestCase(2f,3f,4f,1f, 5f,6f,7f,1f, 4f,15f,8f,-55f)]
        public void TestQuaternionMultiply(
            float x1, float y1, float z1, float w1,
            float x2, float y2, float z2, float w2,
            float x3, float y3, float z3, float w3)
        {
            var q1 = new Quaternion(x1, y1, z1, w1);
            var q2 = new Quaternion(x2, y2, z2, w2);
            var q3 = q1 * q2;

            Assert.AreEqual(x3, q3.X, 0.000001f);
            Assert.AreEqual(y3, q3.Y, 0.000001f);
            Assert.AreEqual(z3, q3.Z, 0.000001f);
            Assert.AreEqual(w3, q3.W, 0.000001f);
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

            var v2 = q.Transform(v);

            var m = Matrix.CreateFromQuaternion(q);
            var v3 = Vector3.Transform(v, m);

            Assert.AreEqual(v3.X, v2.X, 0.000001f);
            Assert.AreEqual(v3.Y, v2.Y, 0.000001f);
            Assert.AreEqual(v3.Z, v2.Z, 0.000001f);
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
        public void TestToAxisAngle(float ax, float ay, float az, float angle)
        {
            var axis = new Vector3(ax, ay, az).Normalized();
            if (axis == Vector3.Zero ||
                angle == 0)
            {
                axis = Vector3.UnitX;
                angle = 0;
            }
            if (angle < 0)
            {
                angle = (float)(angle + 2 * Math.PI);
            }
            var q = Quaternion.CreateFromAxisAngle(axis, angle);

            Vector3 axis2;
            float angle2;
            q.ToAxisAngle(out axis2, out angle2);

            float delta = 0.0001f;
            Assert.That(
                axis.ToVector4(angle),
                Is.EqualTo(axis2.ToVector4(angle2))
                .Using((Vector4 expected, Vector4 actual) => {
                    var e2 = Quaternion.NormalizeAxisAngle(expected);
                    var a2 = Quaternion.NormalizeAxisAngle(actual);

                    return (
                        Math.Abs(e2.X - a2.X) <= delta &&
                        Math.Abs(e2.Y - a2.Y) <= delta &&
                        Math.Abs(e2.Z - a2.Z) <= delta &&
                        Math.Abs(e2.W - a2.W) <= delta) ? 0 : 1;
                }));
        }

        [Test]
        public void TestFromAngleBetweenVectors()
        {
            // given
            var from = Vector3.UnitY;
            var to = new Vector3(1, 1, 0).Normalized();
            var expected = Quaternion.FromRotationZ(-45f.ToRadians());

            // when
            var result = Quaternion.FromAngleBetweenVectors(from, to);

            // then
            float delta = 0.0001f;
            Assert.That(
                expected,
                Is.EqualTo(result)
                .Using((Quaternion ex, Quaternion ac) => (
                        Math.Abs(ex.X - ac.X) <= delta &&
                        Math.Abs(ex.Y - ac.Y) <= delta &&
                        Math.Abs(ex.Z - ac.Z) <= delta &&
                        Math.Abs(ex.W - ac.W) <= delta) ? 0 : 1));
        }
    }
}

