using System;
using NUnit.Framework;
using ChamberLib;

namespace ChamberLibTests
{
    [TestFixture]
    public class Vector3Test
    {
        [Test]
        [TestCase(1, 0, 0,   0, 1, 0,  0, 0, 1 )]
        [TestCase(0, 1, 0,   0, 0, 1,  1, 0, 0 )]
        [TestCase(0, 0, 1,   1, 0, 0,  0, 1, 0 )]
        [TestCase(-1, 0, 0,   0, 1, 0,  0, 0, -1 )]
        [TestCase(0, -1, 0,   0, 0, 1,  -1, 0, 0 )]
        [TestCase(0, 0, -1,   1, 0, 0,  0, -1, 0 )]
        [TestCase(2, 0, 0,   0, 3, 0,  0, 0, 6 )]
        public void Vector3CrossTest(float ax, float ay, float az, float bx, float by, float bz, float ex, float ey, float ez)
        {
            var a = new Vector3(ax, ay, az);
            var b = new Vector3(bx, by, bz);

            var expected = new Vector3(ex, ey, ez);

            var actual = Vector3.Cross(a, b);

            Assert.AreEqual(expected, actual);
        }
    }
}


