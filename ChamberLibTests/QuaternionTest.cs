using System;
using NUnit.Framework;
using ChamberLib;
using Xna = Microsoft.Xna.Framework;

namespace ChamberLibTests
{
    [TestFixture]
    public class QuaternionTest
    {
        [Test]
        [TestCase(1,0,0,0,1,0,0,0)]
        [TestCase(0,1,0,0,0,1,0,0)]
        [TestCase(0,0,1,0,0,0,1,0)]
        [TestCase(0,0,0,1,0,0,0,1)]
        [TestCase(0.5f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f)]
        [TestCase(2,3,4,1,5,6,7,1)]
        public void TestQuaternionMultiply(float x1, float y1, float z1, float w1, float x2, float y2, float z2, float w2)
        {
            var q1 = new Quaternion(x1, y1, z1, w1);
            var q2 = new Quaternion(x2, y2, z2, w2);
            var q3 = q1 * q2;

            var xq1 = new Microsoft.Xna.Framework.Quaternion(x1, y1, z1, w1);
            var xq2 = new Microsoft.Xna.Framework.Quaternion(x2, y2, z2, w2);
            var xq3 = xq1 * xq2;

            Assert.AreEqual(xq3.X, q3.X);
            Assert.AreEqual(xq3.Y, q3.Y);
            Assert.AreEqual(xq3.Z, q3.Z);
            Assert.AreEqual(xq3.W, q3.W);
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
        public void TestFromRotationMatrix(float qx, float qy, float qz, float qw)
        {
            var _q = new Quaternion(qx, qy, qz, qw);
            var q = _q;
            var qv = q.ToAxisAngle();
            var m = q.ToMatrix();

            var xm = m.ToXna();

            var xq2 = Xna.Quaternion.CreateFromRotationMatrix(xm);
            var q2 = Quaternion.FromRotationMatrix(m);

            var q2v = q2.ToAxisAngle();
            var xq2v = xq2.ToChamber().ToAxisAngle();
            Assert.That(
                q2,
                Is.EqualTo(xq2.ToChamber()).Using((Quaternion a, Quaternion b) => (a.IsEquivalentOrientationTo(b, 0.0001f) ? 0 : 1)),
                string.Format(
                    "{0} --> {1} --> {2} \n" +
                    "{3} --> {4} --> {5} \n" +
                    "{6} --> {7} --> {8}",
                    _q, q, qv,
                    q2, q2.ToAxisAngle(), q2v,
                    xq2.ToChamber(), xq2.ToChamber().ToAxisAngle(), xq2v));
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
    }
}

