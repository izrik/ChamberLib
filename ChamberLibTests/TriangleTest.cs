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
        public void TriangleIntersectsRay()
        {
            // given
            Triangle triangle;
            Ray ray;

            // when
            triangle = new Triangle(Vector3.UnitX, Vector3.UnitY, Vector3.UnitZ);
            ray = new Ray(Vector3.Zero, new Vector3(1, 1, 1).Normalized());

            // then
            Assert.True(triangle.Intersects(ray));

            // when
            var n111 = new Vector3(1,1,1).Normalized();
            ray = new Ray(n111, n111);

            // then
            Assert.True(triangle.Intersects(ray));

            // when
            ray = new Ray(new Vector3(1, 1, 1), n111);

            // then
            Assert.False(triangle.Intersects(ray));

            // when
            ray = new Ray(Vector3.Zero, Vector3.UnitX);

            // then
            Assert.IsTrue(triangle.Intersects(ray));

            // when
            ray = new Ray(Vector3.Zero, Vector3.UnitY);

            // then
            Assert.IsTrue(triangle.Intersects(ray));

            // when
            ray = new Ray(Vector3.Zero, Vector3.UnitZ);

            // then
            Assert.IsTrue(triangle.Intersects(ray));
        }
    }
}
