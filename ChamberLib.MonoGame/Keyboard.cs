using System;
using System.Collections.Generic;

namespace ChamberLib
{
    public class Keyboard : IKeyboard
    {
        protected static Keyboard _cache;

        public static Keyboard GetKeyboard()
        {
            if (_cache != null)
            {
                return _cache;
            }

            _cache = new Keyboard();

            return _cache;
        }

        protected Keyboard()
        {
        }

        public KeyboardState GetState()
        {
            return Microsoft.Xna.Framework.Input.Keyboard.GetState().ToChamber();
        }
    }
}

