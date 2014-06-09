using System;
using System.Linq;

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

        public static Microsoft.Xna.Framework.Color[] ToXna(this ChamberLib.Color[] colors)
        {
            return colors.Select(c => c.ToXna()).ToArray();
        }

        public static ChamberLib.Color[] ToChamber(this Microsoft.Xna.Framework.Color[] colors)
        {
            return colors.Select(c => c.ToChamber()).ToArray();
        }
    }
}

