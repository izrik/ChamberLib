using System;

namespace ChamberLib
{
    public static class MathHelper
    {
        public static int RoundToInt(this float x)
        {
            return (int)Math.Round(x);
        }
    }
}

