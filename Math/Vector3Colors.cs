
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

namespace ChamberLib
{
    public static class Vector3Colors
    {
        public static Vector3 FromHslVector(float h, float s, float l)
        {
            h -= (int)Math.Floor(h);
            float h6 = h * 6;
            float t = h6 - (int)Math.Floor(h6);
            float c = (1 - Math.Abs(2 * l - 1)) * s;
            float x = c * (1 - Math.Abs(h6 % 2 - 1));

            float r;
            float g;
            float b;

            if (h6 < 1) { r = c; g = x; b = 0;}
            else if (h6 < 2) { r = x; g = c; b = 0;}
            else if (h6 < 3) { r = 0; g = c; b = x;}
            else if (h6 < 4) { r = 0; g = x; b = c;}
            else if (h6 < 5) { r = x; g = 0; b = c;}
            else { r = c; g = 0; b = x; }

            return new Vector3(r, g, b);
        }
    }
}

