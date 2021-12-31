
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
    public struct Color
    {
        public Color(byte r, byte g, byte b)
            : this(r, g, b, 255)
        {
        }
        public Color(byte r, byte g, byte b, byte a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public Color(Vector3 v)
            : this((byte)(v.X.Clamp(0, 1) * 255.0f), (byte)(v.Y.Clamp(0, 1) * 255.0f), (byte)(v.Z.Clamp(0, 1) * 255.0f))
        {
        }

        public byte R;
        public byte G;
        public byte B;
        public byte A;

        public static readonly Color White = new Color(255, 255, 255, 255);
        public static readonly Color LightGray = new Color(191, 191, 191, 255);
        public static readonly Color Gray = new Color(127, 127, 127, 255);
        public static readonly Color DarkGray = new Color(63, 63, 63, 255);
        public static readonly Color Black = new Color(0, 0, 0, 255);

        public static readonly Color Red = new Color(255, 0, 0, 255);
        public static readonly Color DarkRed = new Color(127, 0, 0, 255);
        public static readonly Color Green = new Color(0, 255, 0, 255);
        public static readonly Color DarkGreen = new Color(0, 127, 0, 255);
        public static readonly Color Blue = new Color(0, 0, 255, 255);
        public static readonly Color DarkBlue = new Color(0, 0, 127, 255);
        public static readonly Color Yellow = new Color(255, 255, 0, 255);
        public static readonly Color DarkYellow = new Color(127, 127, 0, 255);
        public static readonly Color Cyan = new Color(0, 255, 255, 255);
        public static readonly Color DarkCyan = new Color(0, 127, 127, 255);
        public static readonly Color Magenta = new Color(255, 0, 255, 255);
        public static readonly Color DarkMagenta = new Color(127, 0, 127, 255);

        public static readonly Color Brown = new Color(63, 63, 0, 255);
        public static readonly Color DarkBrown = new Color(31, 31, 0, 255);

        public Vector3 ToVector3()
        {
            return new Vector3(R / 255.0f, G / 255.0f, B / 255.0f);
        }

        public Vector4 ToVector4()
        {
            return new Vector4(R / 255.0f, G / 255.0f, B / 255.0f, A / 255.0f);
        }

        public ColorF ToColorF()
        {
            var v = this.ToVector4();
            return new ColorF(v.X, v.Y, v.Z, v.W);
        }

        public static Color FromHsl(float h, float s, float l)
        {
            return new Color(Vector3Colors.FromHslVector(h,s,l));
        }
    }
}

