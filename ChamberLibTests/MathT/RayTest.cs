
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
using System;
using ChamberLib;

namespace ChamberLibTests.MathT
{
    [TestFixture]
    public class RayTest
    {
        [Test]
        [TestCase(0,0,0,1,1,0,2f)]
        [TestCase(0,1,0,1,0,0,1f)]
        [TestCase(0,-1,0,1,1,0,2f)]
        [TestCase(0,-0.5f,0,2,1,0,1.25f)]
        [TestCase(0,0,0,2,1,0,1.25f)]
        [TestCase(0,2,0,1,-1,0,2f)]
        [TestCase(0,3,0,1,-1,0,2f)]
        [TestCase(0,4,0,1,-1,0,8f)]
        [TestCase(1,3,0,1,-1,0,2f)]
        [TestCase(0,1,0,1,-1,0,2f)]
        [TestCase(0,0,0,1,-1,0,null)]
        [TestCase(0,1,0,1,1,0,2f)]
        [TestCase(0,2,0,1,1,0,null)]
        [TestCase(2,3,0,1,-1,0,2f)]
        [TestCase(3,3,0,1,-1,0,null)]
        [TestCase(1,3,0,-1,-1,0,null)]
        [TestCase(2,3,0,-1,-1,0,2f)]
        [TestCase(3,4,0,-1,-1,0,8f)]
        [TestCase(3,3,0,-1,-1,0,2f)]
        [TestCase(4,4,0,-1,-1,0,8f)]
        [TestCase(4,3,0,-1,-1,0,2f)]
        [TestCase(4,2,0,-1,-1,0,2f)]
        [TestCase(4,1,0,-1,-1,0,2f)]
        [TestCase(4,0,0,-1,-1,0,null)]
        [TestCase(5,2,0,-1,-1,0,8f)]
        [TestCase(5,1,0,-1,-1,0,null)]
        [TestCase(5,3,0,-1,-1,0,8f)]
        [TestCase(4,-1,0,-1,0,0,null)]
        [TestCase(4,0,0,-1,0,0,1f)]
        [TestCase(4,1,0,-1,0,0,1f)]
        [TestCase(4,2,0,-1,0,0,1f)]
        [TestCase(4,3,0,-1,0,0,null)]
        [TestCase(1,-1,0,-1,1,0,null)]
        [TestCase(2,-1,0,-1,1,0,2f  )]
        [TestCase(3,-2,0,-1,1,0,8f  )]
        [TestCase(3,-1,0,-1,1,0,2f  )]
        [TestCase(4,-2,0,-1,1,0,8f  )]
        [TestCase(4,-1,0,-1,1,0,2f  )]
        [TestCase(4, 0,0,-1,1,0,2f  )]
        [TestCase(5,-1,0,-1,1,0,8f  )]
        [TestCase(4, 1,0,-1,1,0,2f  )]
        [TestCase(5, 0,0,-1,1,0,8f  )]
        [TestCase(4, 2,0,-1,1,0,null)]
        [TestCase(5, 1,0,-1,1,0,null)]
        public void TestIntersectsBoundingBox(float x, float y, float z, float dx, float dy, float dz, float? expected2)
        {
            var b = new BoundingBox(new Vector3(1, 0, -1), new Vector3(3, 2, 1));
            var ray = new Ray(new Vector3(x, y, z), new Vector3(dx, dy, dz).Normalized());
            float? expected = null;
            if (expected2.HasValue)
            {
                expected = (float)Math.Sqrt(expected2.Value);
            }

            float? idist = ray.Intersects(b);

            Assert.AreEqual(expected.HasValue, idist.HasValue);
            if (expected.HasValue)
            {
                Assert.AreEqual(expected.Value, idist.Value, 0.000001);
            }
        }

        [Test]
        [TestCase(1, 0, 0, 1.25f, 1, 2, 3, 1, 0, 0, 1.25f, 2, 3)]
        [TestCase(1, 0, 0, 1.25f, 1, 2, 3, 1, 1, 1, 1.25f, 2.25f, 3.25f)]
        [TestCase(1, 0, 0, 1.25f, -1, 2, 3, 1, 0, 0, 1.25f, 2, 3)]
        [TestCase(1, 0, 0, 1.25f, 2, 2, 3, -1, 0, 0, 1.25f, 2, 3)]
        [TestCase(1, 0, 0, 1, 1, 2, 3, 0, 1, 0, 1, 2, 3)]
        [TestCase(1, 1, 0, 0.7071068f, 0, 0, 0, 1, 1, 0, 0.5f, 0.5f, 0)]
        [TestCase(1, 1, 0, 0.7071068f, 0, 0, 0, 2, 1, 0, 2/3.0f, 1/3.0f, 0)]
        public void TestRayIntersectsPlane(float px, float py, float pz, float pdist,
            float rpx, float rpy, float rpz, float rdx, float rdy, float rdz,
            float x, float y, float z)
        {
            // given
            var plane = new Plane(new Vector3(px, py, pz).Normalized(), pdist);
            var ray = new Ray(new Vector3(rpx, rpy, rpz), new Vector3(rdx, rdy, rdz).Normalized());

            // when
            var p = ray.Intersects(plane);

            // then
            Assert.True(p.HasValue);
            Assert.AreEqual(x, p.Value.X, 0.00001f);
            Assert.AreEqual(y, p.Value.Y, 0.00001f);
            Assert.AreEqual(z, p.Value.Z, 0.00001f);
        }

        [Test]
        [TestCase(1, 0, 0, 1, 2, 0, 0, 1, 0, 0)]
        [TestCase(1, 0, 0, 1, 2, 0, 0, 0, 1, 0)]
        [TestCase(1, 0, 0, 1, 2, 0, 0, 0, 0, 1)]
        [TestCase(1, 0, 0, 1, 0, 0, 0, -1, 0, 0)]
        [TestCase(1, 0, 0, 1, 0, 0, 0, 0, 1, 0)]
        public void TestRayDoesNotIntersectPlane(float px, float py, float pz, float pdist,
            float rpx, float rpy, float rpz, float rdx, float rdy, float rdz)
        {
            // given
            var plane = new Plane(new Vector3(px, py, pz).Normalized(), pdist);
            var ray = new Ray(new Vector3(rpx, rpy, rpz), new Vector3(rdx, rdy, rdz).Normalized());

            // when
            var p = ray.Intersects(plane);

            // then
            Assert.AreEqual(null, p);
        }

        [Test]
        [TestCase(0, 0, 0, 1, 0, 0, 0, 0, 0, 1)]
        [TestCase(0, 0, 0, 1, 0, 0, 1, 0, 0, 1)]
        [TestCase(0, 0, 0, 1, 0, 0, 2, 0, 0, 1)]
        [TestCase(0, 0, 0, 1, 0, 0, 2, 1, 0, 1)]
        [TestCase(0, 0, 0, 1, 0, 0, -1, 0, 0, 1)]
        [TestCase(0, 0, 0, 0.6f, 0.8f, 0, -3, 0, 0, 3)]
        [TestCase(0, 0, 0, 0.6f, 0.8f, 0, -3, 0, 0, 3.001f)]
        public void TestRayIntersectsSphere(
            float rpx, float rpy,float rpz,
            float rdx, float rdy, float rdz,
            float sx, float sy, float sz, float sr)
        {
            // given
            var ray = new Ray(
                new Vector3(rpx, rpy, rpz),
                new Vector3(rdx, rdy, rdz));
            var sphere = new Sphere(new Vector3(sx, sy, sz), sr);

            // expect
            Assert.True(ray.Intersects(sphere));
        }

        [Test]
        [TestCase(0, 0, 0, 0.6f, 0.8f, 0, -3, 0, 0, 2.999f)]
        public void TestRayDoesNotIntersectSphere(
            float rpx, float rpy, float rpz,
            float rdx, float rdy, float rdz,
            float sx, float sy, float sz, float sr)
        {
            // given
            var ray = new Ray(
                new Vector3(rpx, rpy, rpz),
                new Vector3(rdx, rdy, rdz));
            var sphere = new Sphere(new Vector3(sx, sy, sz), sr);

            // expect
            Assert.False(ray.Intersects(sphere));
        }

        [Test]
        public void TransformedBy_TransformsRays_Translation()
        {
            // given
            var r = new Ray(Vector3.Zero, Vector3.UnitX);
            var m = Matrix.CreateTranslation(2, 0, 0);

            // when
            var r2 = r.TransformedBy(m);

            // then
            Assert.AreEqual(new Vector3(2, 0, 0), r2.Position);
            Assert.AreEqual(Vector3.UnitX, r2.Direction);
        }

        [Test]
        public void TransformedBy_TransformsRays_Rotation()
        {
            // given
            var r = new Ray(Vector3.Zero, Vector3.UnitX);
            var m = Matrix.CreateRotationY((90f).ToRadians());

            // when
            var r2 = r.TransformedBy(m);

            // then
            Assert.AreEqual(Vector3.Zero, r2.Position);
            Assert.AreEqual(0, r2.Direction.X, 0.0001f);
            Assert.AreEqual(0, r2.Direction.Y, 0.0001f);
            Assert.AreEqual(-1, r2.Direction.Z, 0.0001f);
        }

        [Test]
        public void TransformedBy_TransformsRays_TranslationAndScale()
        {
            // given
            var r = new Ray(Vector3.One, Vector3.One);
            var m = Matrix.CreateScale(1, 1, 2) *
                Matrix.CreateTranslation(2, 0, 0);

            // when
            var r2 = r.TransformedBy(m);

            // then
            Assert.AreEqual(new Vector3(3, 1, 2), r2.Position);
            Assert.AreEqual(new Vector3(1, 1, 2), r2.Direction);
        }

        [Test]
        public void TransformedBy_TransformsAndInverseRoundtrip_YieldsOriginalValue()
        {
            // given
            var r = new Ray(Vector3.One, Vector3.One);
            var m = Matrix.CreateScale(1, 1, 2) *
                Matrix.CreateTranslation(2, 0, 0);
            var m2 = m.Inverted();

            // when
            var r2 = r.TransformedBy(m);
            var r3 = r2.TransformedBy(m2);

            // then
            Assert.AreEqual(Vector3.One, r3.Position);
            Assert.AreEqual(Vector3.One, r3.Direction);
        }
    }
}

