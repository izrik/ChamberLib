using System;

namespace ChamberLib
{
    public struct ColorF
    {
        public ColorF(float r, float g, float b)
            : this(r, g, b, 1)
        {
        }
        public ColorF(float r, float g, float b, float a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public ColorF(Vector3 v)
            : this(v.X.Clamp(0, 1),v.Y.Clamp(0, 1), v.Z.Clamp(0, 1))
        {
        }

        public float R;
        public float G;
        public float B;
        public float A;

        public static readonly ColorF White = new ColorF(1, 1, 1, 1);
        public static readonly ColorF Black = new ColorF(0, 0, 0, 1);
        public static readonly ColorF Red = new ColorF(1, 0, 0, 1);
        public static readonly ColorF Green = new ColorF(0, 1, 0, 1);
        public static readonly ColorF Blue = new ColorF(0, 0, 1, 1);
        public static readonly ColorF Yellow = new ColorF(1, 1, 0, 1);
        public static readonly ColorF Cyan = new ColorF(0, 1, 1, 1);
        public static readonly ColorF Magenta = new ColorF(1, 0, 1, 1);
        public static readonly ColorF Gray = new ColorF(.5f, .5f, .5f, 1);
        public static readonly ColorF DarkGray = new ColorF(.25f, .25f, .25f, 1);

        public Vector3 ToVector3()
        {
            return new Vector3(R, G, B);
        }

        public Vector4 ToVector4()
        {
            return new Vector4(R, G, B, A);
        }

        public Color ToColor()
        {
            return new Color(
                (byte)(R * 255).Clamp(0, 255),
                (byte)(G * 255).Clamp(0, 255),
                (byte)(B * 255).Clamp(0, 255),
                (byte)(A * 255).Clamp(0, 255));
        }

        public static ColorF FromHsl(float h, float s, float l)
        {
            return new ColorF(Vector3Colors.FromHslVector(h,s,l));
        }
    }
}

