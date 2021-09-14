using System;

namespace ChamberLib
{
    public struct GamePadDPad
    {
        public GamePadDPad(ButtonState up, ButtonState down, ButtonState left, ButtonState right)
        {
            Up = up;
            Down = down;
            Left = left;
            Right = right;
        }

        public ButtonState Up;
        public ButtonState Down;
        public ButtonState Left;
        public ButtonState Right;
    }
}

