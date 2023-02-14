
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
using System.Linq;

using _OpenTK = global::OpenTK;

namespace ChamberLib.OpenTK.Math
{
    public static class VectorHelper
    {
        public static _OpenTK.Vector2 ToOpenTK(this ChamberLib.Vector2 v)
        {
            return new _OpenTK.Vector2(v.X, v.Y);
        }
        public static _OpenTK.Vector2[] ToOpenTK(this ChamberLib.Vector2[] v)
        {
            return v.Select(mm => mm.ToOpenTK()).ToArray();
        }
        public static _OpenTK.Vector3 ToOpenTK(this ChamberLib.Vector3 v)
        {
            return new _OpenTK.Vector3(v.X, v.Y, v.Z);
        }
        public static _OpenTK.Vector3[] ToOpenTK(this ChamberLib.Vector3[] v)
        {
            return v.Select(mm => mm.ToOpenTK()).ToArray();
        }
        public static _OpenTK.Vector4 ToOpenTK(this ChamberLib.Vector4 v)
        {
            return new _OpenTK.Vector4(v.X, v.Y, v.Z, v.W);
        }
        public static _OpenTK.Vector4[] ToOpenTK(this ChamberLib.Vector4[] v)
        {
            return v.Select(mm => mm.ToOpenTK()).ToArray();
        }

        public static ChamberLib.Vector2 ToChamber(this _OpenTK.Vector2 v)
        {
            return new ChamberLib.Vector2(v.X, v.Y);
        }
        public static ChamberLib.Vector2[] ToChamber(this _OpenTK.Vector2[] v)
        {
            return v.Select(mm => mm.ToChamber()).ToArray();
        }
        public static ChamberLib.Vector3 ToChamber(this _OpenTK.Vector3 v)
        {
            return new ChamberLib.Vector3(v.X, v.Y, v.Z);
        }
        public static ChamberLib.Vector3[] ToChamber(this _OpenTK.Vector3[] v)
        {
            return v.Select(mm => mm.ToChamber()).ToArray();
        }
        public static ChamberLib.Vector4 ToChamber(this _OpenTK.Vector4 v)
        {
            return new ChamberLib.Vector4(v.X, v.Y, v.Z, v.W);
        }
        public static ChamberLib.Vector4[] ToChamber(this _OpenTK.Vector4[] v)
        {
            return v.Select(mm => mm.ToChamber()).ToArray();
        }

        public static void ToFloatArray(this ChamberLib.Vector3[] vectors, float[] floats)
        {
            int i;
            for (i=0;i<vectors.Length;i++)
            {
                int n = i * 3;
                floats[n + 0] = vectors[i].X;
                floats[n + 1] = vectors[i].Y;
                floats[n + 2] = vectors[i].Z;
            }
        }
    }
}

