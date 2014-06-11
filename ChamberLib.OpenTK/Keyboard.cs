using System;

namespace ChamberLib
{
    public class Keyboard : IKeyboard
    {
        static Keyboard _cache = new Keyboard();

        public static Keyboard GetKeyboard()
        {
            return _cache;
        }

        public KeyboardState GetState()
        {
            return new KeyboardState();
        }
    }
}

