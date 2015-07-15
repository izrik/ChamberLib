using NUnit.Framework;
using System;
using ChamberLib;


namespace ChamberLibTests
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


    }
}

