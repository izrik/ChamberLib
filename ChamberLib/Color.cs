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
        public static readonly Color Black = new Color(0, 0, 0, 255);
        public static readonly Color Red = new Color(255, 0, 0, 255);
        public static readonly Color Green = new Color(0, 255, 0, 255);
        public static readonly Color Blue = new Color(0, 0, 255, 255);
        public static readonly Color Yellow = new Color(255, 255, 0, 255);
        public static readonly Color Cyan = new Color(0, 255, 255, 255);
        public static readonly Color Magenta = new Color(255, 0, 255, 255);
        public static readonly Color Gray = new Color(127, 127, 127, 255);

        public Vector3 ToVector3()
        {
            return new Vector3(R / 255.0f, G / 255.0f, B / 255.0f);
        }

        public Vector4 ToVector4()
        {
            return new Vector4(R / 255.0f, G / 255.0f, B / 255.0f, A / 255.0f);
        }
    }
}

