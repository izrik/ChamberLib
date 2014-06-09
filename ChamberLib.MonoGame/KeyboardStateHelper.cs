using System;

namespace ChamberLib
{
    public static class KeyboardStateHelper
    {
        public static Microsoft.Xna.Framework.Input.KeyboardState ToXna(this ChamberLib.KeyboardState kbs)
        {
            return new Microsoft.Xna.Framework.Input.KeyboardState(kbs.GetPressedKeys().ToXna());
        }

        public static ChamberLib.KeyboardState ToChamber(this Microsoft.Xna.Framework.Input.KeyboardState kbs)
        {
            return new KeyboardState(kbs.GetPressedKeys().ToChamber());
        }
    }
}

