
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

namespace ChamberLib
{
    public struct Parabola3
    {
        public Parabola3(Vector3 a, Vector3 b, Vector3 c)
        {
            A = a;
            B = b;
            C = c;
        }

        public readonly Vector3 A;
        public readonly Vector3 B;
        public readonly Vector3 C;

        public Vector3 Calculate(float t)
        {
            return (A * t + B) * t + C;
        }

        public static Parabola3 FromParameterAndPointAndTangentAtTime(
            Vector3 a, Vector3 p1, Vector3 dp1, float t1)
        {
            var b = dp1 - 2 * a * t1;
            var c = p1 - (a * t1 + b) * t1;
            return new Parabola3(a, b, c);
        }
    }
}
