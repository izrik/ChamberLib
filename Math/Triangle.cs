
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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChamberLib
{
    public struct Triangle
    {
        public Triangle(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            V1 = v1;
            V2 = v2;
            V3 = v3;
        }

        public readonly Vector3 V1;
        public readonly Vector3 V2;
        public readonly Vector3 V3;

        public Plane ToPlane()
        {
            return Plane.FromPoints(V1, V2, V3);
        }

        public float Area
        {
            get
            {
                var ab = V2 - V1;
                var ac = V3 - V1;
                return Vector3.Cross(ab, ac).Length() / 2;
            }
        }

        public bool IsDegenerate(float epsilon=0)
        {
            var ab = V2 - V1;
            var ac = V3 - V1;
            return Vector3.Cross(ab, ac).LengthSquared() <= epsilon;
        }

        public Vector3? Intersects(Ray ray, float epsilon=0)
        {
            var plane = this.ToPlane();
            var p = plane.Intersects(ray, epsilon);
            if (!p.HasValue) return null;

            var pv = p.Value;

            var b = V3 - V1;
            var a = V2 - V1;
            var q = pv - V1;

            var aa = a.Dot(a);
            var ab = a.Dot(b);
            var bb = b.Dot(b);
            var qa = q.Dot(a);
            var qb = q.Dot(b);

            var invDenom = 1 / (aa * bb - ab * ab);
            var u = (qa * bb - qb * ab) * invDenom;
            var v = (qb * aa - qa * ab) * invDenom;

            if (u < -epsilon) return null;
            if (v < -epsilon) return null;
            if (u + v > 1+epsilon) return null;

            return pv;
        }
    }
}
