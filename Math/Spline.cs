
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
    public static class Spline
    {
        public static float Calculate(float startTime, float startValue, float endTime, float endValue, float time)
        {
            // Modified from FbxAnimCurve.Evaluate in FbxSharp
            float t = time;
            float t1 = startTime;
            float t2 = endTime;
            float v1 = startValue;
            float v2 = endValue;

            if (t <= t1) return v1;
            if (t >= t2) return v2;

            float v0 = v1 - (v2 - v1);
            float t0 = t1 - (t2 - t1);
            float v3 = v2 + (v2 - v1);
            float t3 = t2 + (t2 - t1);

            float s0 = (t0 - t1) / (t2 - t1);
            float s1 = (t1 - t1) / (t2 - t1);
            float s2 = (t2 - t1) / (t2 - t1);
            float s3 = (t3 - t1) / (t2 - t1);
            double s = (t - t1) / (t2 - t1);

            double ss = s * s;
            double sss = ss * s;
            double m1 = (v2 - v0) / (s2 - s0);
            double m2 = (v3 - v1) / (s3 - s1);
            double x =
                (2 * sss - 3 * ss + 1) * v1 +
                (sss - 2 * ss + s) * m1 +
                (-2 * sss + 3 * ss) * v2 +
                (sss - ss) * m2;

            return (float)x;
        }
    }
}
