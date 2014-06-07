using System;

namespace ChamberLib
{
    public static class PlayerIndexHelper
    {
        public static Microsoft.Xna.Framework.PlayerIndex ToXna(this ChamberLib.PlayerIndex pi)
        {
            switch (pi)
            {
                case ChamberLib.PlayerIndex.One:
                    return Microsoft.Xna.Framework.PlayerIndex.One;
                case ChamberLib.PlayerIndex.Two:
                    return Microsoft.Xna.Framework.PlayerIndex.Two;
                case ChamberLib.PlayerIndex.Three:
                    return Microsoft.Xna.Framework.PlayerIndex.Three;
                case ChamberLib.PlayerIndex.Four:
                    return Microsoft.Xna.Framework.PlayerIndex.Four;
            }

            throw new ArgumentException();
        }

        public static ChamberLib.PlayerIndex ToChamber(this Microsoft.Xna.Framework.PlayerIndex pi)
        {
            switch (pi)
            {
                case Microsoft.Xna.Framework.PlayerIndex.One:
                    return ChamberLib.PlayerIndex.One;
                case Microsoft.Xna.Framework.PlayerIndex.Two:
                    return ChamberLib.PlayerIndex.Two;
                case Microsoft.Xna.Framework.PlayerIndex.Three:
                    return ChamberLib.PlayerIndex.Three;
                case Microsoft.Xna.Framework.PlayerIndex.Four:
                    return ChamberLib.PlayerIndex.Four;
            }

            throw new ArgumentException();
        }
    }
}

