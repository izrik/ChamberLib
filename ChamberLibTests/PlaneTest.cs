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
    }
}
