using System;

namespace ChamberLib
{
    public static class MathHelper
    {
        public static int RoundToInt(this float x)
        {
            return (int)Math.Round(x);
        }

        public static float ToRadians(this float degrees)
        {
            return degrees * 0.01745329251994f; // pi / 180
        }

        public static float ToDegrees( this float radians)
        {
            return radians * 57.2957795130823f; // 180 / pi
        }

        public static float Clamp(this float value, float min, float max)
        {
            if (value > max)
                return max;

            if (value < min)
                return min;

            return value;
        }

        public static int Clamp(this int value, int min, int max)
        {
            if (value > max)
                return max;

            if (value < min)
                return min;

            return value;
        }

        public static byte Clamp(this byte value, byte min, byte max)
        {
            if (value > max)
                return max;

            if (value < min)
                return min;

            return value;
        }

        public static int ManhattanDistance(Vector2 a, Vector2 b)
        {
            return
                Math.Abs(a.X.RoundToInt() - b.X.RoundToInt()) +
                Math.Abs(a.Y.RoundToInt() - b.Y.RoundToInt());
        }

        public static Matrix Transform(Matrix mat, Quaternion rotation)
        {
            return mat * Matrix.CreateFromQuaternion(rotation);
        }

        public static float Lerp(float a, float b, float s)
        {
            return a * (1 - s) + b * s;
        }
    }
}

