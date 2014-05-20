﻿using System;

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
    }
}

