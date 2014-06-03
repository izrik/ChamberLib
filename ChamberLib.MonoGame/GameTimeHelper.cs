using System;

namespace ChamberLib
{
    public static class GameTimeHelper
    {
        public static Microsoft.Xna.Framework.GameTime ToXna(this ChamberLib.GameTime time)
        {
            return new Microsoft.Xna.Framework.GameTime(time.TotalGameTime, time.ElapsedGameTime);
        }

        public static ChamberLib.GameTime ToChamber(this Microsoft.Xna.Framework.GameTime time)
        {
            return new ChamberLib.GameTime(time.TotalGameTime, time.ElapsedGameTime);
        }
    }
}

