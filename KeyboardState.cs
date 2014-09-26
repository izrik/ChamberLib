using System;
using System.Linq;
using System.Collections.Generic;

namespace ChamberLib
{
    public struct KeyboardState
    {
        public KeyboardState(Keys[] pressedKeys)
        {
            _pressedKeys = (new HashSet<Keys>(pressedKeys)).ToArray();
        }

        readonly Keys[] _pressedKeys;

        public Keys[] GetPressedKeys()
        {
            return _pressedKeys.ToArray();
        }

        public bool IsKeyDown(Keys key)
        {
            if (_pressedKeys == null)
            {
                return false;
            }

            return _pressedKeys.Contains(key);
        }

        public bool IsKeyUp(Keys key)
        {
            return !IsKeyDown(key);
        }
    }
}

