
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
    public class ViewportTest
    {
        [Test]
        [TestCase(4f, 0f, 5f, 400f, 240f, 0.1991992f)]
        [TestCase(5f, 0f, 5f, 400f, 240f, 0.1991992f)]
        [TestCase(4f, 0f, 4f, 400f, 240f, 0.1991992f)]
        [TestCase(5f, 0f, 4f, 400f, 240f, 0.1991992f)]
        public void TestViewportProject(float x, float y, float z, float ex, float ey, float ez)
        {
            var vp = new Viewport(0, 0, 800, 480);
            const float width = 9f;
            float aspectRatio = vp.Width / (float)vp.Height;
            var proj = Matrix.CreateOrthographic(width, width / aspectRatio, 0.05f, 50);

            var target = new Vector3(4, 0, 5);
            const float distance = 10f;
            const float cameraPositionTheta = (float)(5 * Math.PI / 4);
            const float cameraPositionPhi = (float)(Math.PI / 6);
            Vector3 cameraPosition =
                distance * new Vector3(
                    (float)Math.Cos(cameraPositionTheta) * (float)Math.Cos(cameraPositionPhi),
                    (float)Math.Sin(cameraPositionPhi),
                    (float)Math.Sin(cameraPositionTheta) * (float)Math.Cos(cameraPositionPhi));
            var view = Matrix.CreateLookAt((cameraPosition + target), target, Vector3.UnitY);

            var v = new Vector3(4, 0, 5);

            var u = vp.Project(v, proj, view, Matrix.Identity);



            Assert.AreEqual(ex, u.X, 0.0001f);
            Assert.AreEqual(ey, u.Y, 0.0001f);
            Assert.AreEqual(ez, u.Z, 0.0001f);
        }
    }
}


