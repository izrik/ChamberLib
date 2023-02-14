
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

namespace ChamberLib
{
    public struct Parabola
    {
        public float A;
        public float B;
        public float C;

        public Parabola(float a, float b, float c)
        {
            A = a;
            B = b;
            C = c;
        }

        public static Parabola FromParameters(float a, float b, float c)
        {
            return new Parabola(a, b, c);
        }
        public static Parabola FromParametersAndPoints(float a, Vector2 v1, Vector2 v2)
        {
            if (v1.X == v2.X) throw new ArgumentException("X coordinates must not be equal");

            float x0 = v1.X;
            float y0 = v1.Y;
            float x1 = v2.X;
            float y1 = v2.Y;

            float c = (x0 * (y1 - a * x1 * x1) - x1 * (y0 - a * x0 * x0)) / (x0 - x1);

            if (x0 == 0)
            {
                return FromParametersAndPoints(a, v2, c);
            }
            else
            {
                return FromParametersAndPoints(a, v1, c);
            }
        }
        public static Parabola FromParametersAndPoints(Vector2 v1, float b, Vector2 v2)
        {
            if (v1.X == v2.X) throw new ArgumentException("X coordinates must not be equal");

            throw new NotImplementedException();
        }
        public static Parabola FromParametersAndPoints(Vector2 v1, Vector2 v2, float c)
        {
            if (v1.X == v2.X) throw new ArgumentException("X coordinates must not be equal");

            throw new NotImplementedException();
        }
        public static Parabola FromParametersAndPoints(float a, float b, Vector2 v)
        {
            throw new NotImplementedException();
        }
        public static Parabola FromParametersAndPoints(float a, Vector2 v, float c)
        {
            float x0 = v.X;
            float y0 = v.Y;

            float b = (y0 - a * x0 * x0 - c) / x0;

            return new Parabola(a, b, c);
        }
        public static Parabola FromParametersAndPoints(Vector2 v, float b, float c)
        {
            throw new NotImplementedException();
        }
        public static Parabola FromPoints(Vector2 v1, Vector2 v2, Vector2 v3)
        {
            throw new NotImplementedException();
        }

        public float Calculate(float x)
        {
            return (A * x + B) * x + C;
        }

        public Vector2[] Iterate(float xStart, float xEnd, float xStep)
        {
            //TODO: re-use instead of allocating
            List<Vector2> values = new List<Vector2>();
            float x;
            for (x = xStart; x <= xEnd; x += xStep)
            {
                float y = Calculate(x);
                values.Add(new Vector2(x, y));
            }
            return values.ToArray();
        }
    }
}
