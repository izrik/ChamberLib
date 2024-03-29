﻿
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
    public class FrustumTest
    {
        [Test]
        public void TestFrustumTop()
        {
            var view = Matrix.CreateLookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
            var proj = Matrix.CreatePerspective(1, 1, 0.5f, 1);

            var f = new Frustum(view * proj);

            var n = new Vector3(0, 1, -1).Normalized();
            Assert.AreEqual(n.X, f.Top.Normal.X, 0.00001f);
            Assert.AreEqual(n.Y, f.Top.Normal.Y, 0.00001f);
            Assert.AreEqual(n.Z, f.Top.Normal.Z, 0.00001f);
            Assert.AreEqual(0, f.Top.Distance);
        }

        [Test]
        public void TestFrustumBottom()
        {
            var view = Matrix.CreateLookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
            var proj = Matrix.CreatePerspective(1, 1, 0.5f, 1);

            var f = new Frustum(view * proj);

            var n = new Vector3(0, -1, -1).Normalized();
            Assert.AreEqual(n.X, f.Bottom.Normal.X, 0.00001f);
            Assert.AreEqual(n.Y, f.Bottom.Normal.Y, 0.00001f);
            Assert.AreEqual(n.Z, f.Bottom.Normal.Z, 0.00001f);
            Assert.AreEqual(0, f.Bottom.Distance);
        }

        [Test]
        public void TestFrustumLeft()
        {
            var view = Matrix.CreateLookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
            var proj = Matrix.CreatePerspective(1, 1, 0.5f, 1);

            var f = new Frustum(view * proj);

            var n = new Vector3(1, 0, -1).Normalized();
            Assert.AreEqual(n.X, f.Left.Normal.X, 0.00001f);
            Assert.AreEqual(n.Y, f.Left.Normal.Y, 0.00001f);
            Assert.AreEqual(n.Z, f.Left.Normal.Z, 0.00001f);
            Assert.AreEqual(0, f.Left.Distance);
        }

        [Test]
        public void TestFrustumRight()
        {
            var view = Matrix.CreateLookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
            var proj = Matrix.CreatePerspective(1, 1, 0.5f, 1);

            var f = new Frustum(view * proj);

            var n = new Vector3(-1, 0, -1).Normalized();
            Assert.AreEqual(n.X, f.Right.Normal.X, 0.00001f);
            Assert.AreEqual(n.Y, f.Right.Normal.Y, 0.00001f);
            Assert.AreEqual(n.Z, f.Right.Normal.Z, 0.00001f);
            Assert.AreEqual(0, f.Right.Distance);
        }

        [Test]
        public void TestFrustumNear()
        {
            var view = Matrix.CreateLookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
            var proj = Matrix.CreatePerspective(1, 1, 0.5f, 1);

            var f = new Frustum(view * proj);

            Assert.AreEqual(Vector3.UnitZ, f.Near.Normal);
            Assert.AreEqual(0.5f, f.Near.Distance);
        }

        [Test]
        public void TestFrustumFar()
        {
            var view = Matrix.CreateLookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
            var proj = Matrix.CreatePerspective(1, 1, 0.5f, 1);

            var f = new Frustum(view * proj);

            Assert.AreEqual(Vector3.UnitZ, f.Far.Normal);
            Assert.AreEqual(1, f.Far.Distance);
        }

        [Test]
        [TestCase(5, 5, ContainmentType.Contains)]
        [TestCase(0, 0, ContainmentType.Contains)]
        [TestCase(2, 0, ContainmentType.Contains)]
        public void TestFrustumSphereIntersect(float x, float y, ContainmentType expected)
        {
            var view = Matrix.CreateLookAt(new Vector3(-5, 5, 0), Vector3.Zero, Vector3.UnitY);
            var proj = Matrix.CreatePerspective(6, 6, 0.1f, 10);

            var viewproj = view * proj;

            var f = new Frustum(viewproj);
            var s = new Sphere(new Vector3(x, y, 0), 1);
            var actual = f.Contains(s);

            Assert.AreEqual(expected, actual);
        }
    }
}

