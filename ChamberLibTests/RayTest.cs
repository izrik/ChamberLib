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

            Assert.AreEqual(expected, idist);
        }
    }
}

