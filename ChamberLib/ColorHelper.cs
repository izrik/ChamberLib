using System;

namespace ChamberLib
{
    public static class ColorHelper
    {
        public static Microsoft.Xna.Framework.Color ToXna(this ChamberLib.Color color)
        {
            return new Microsoft.Xna.Framework.Color(color.R, color.G, color.B, color.A);
        }

        public static ChamberLib.Color ToChamber(this Microsoft.Xna.Framework.Color color)
        {
            return new ChamberLib.Color(color.R, color.G, color.B, color.A);
        }

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

        public static Color FromHsl(float h, float s, float l)
        {
            return new Color(FromHslVector(h,s,l));
        }
    }
}

