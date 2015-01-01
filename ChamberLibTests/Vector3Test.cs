using System;
using NUnit.Framework;
using ChamberLib;

namespace ChamberLibTests
{
    [TestFixture]
    public class Vector3Test
    {
        [Test]
        [TestCase(new float[] { 1, 0, 0,   0, 1, 0, })]
        [TestCase(new float[] { 0, 1, 0,   0, 0, 1, })]
        [TestCase(new float[] { 0, 0, 1,   1, 0, 0, })]
        [TestCase(new float[] { -1, 0, 0,   0, 1, 0, })]
        [TestCase(new float[] { 0, -1, 0,   0, 0, 1, })]
        [TestCase(new float[] { 0, 0, -1,   1, 0, 0, })]
        public void Vector3CrossTest(float[] fs)
        {
            var a = new Vector3(fs[0], fs[1], fs[2]);
            var b = new Vector3(fs[3], fs[4], fs[5]);

            var expected = Microsoft.Xna.Framework.Vector3.Cross(a.ToXna(), b.ToXna()).ToChamber();

            var actual = Vector3.Cross(a, b);

            Assert.AreEqual(expected, actual);
        }
    }
}

