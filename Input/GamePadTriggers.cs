using System;

namespace ChamberLib
{
    public struct GamePadTriggers
    {
        public GamePadTriggers(float left, float right)
        {
            Left = left;
            Right = right;
        }

        public float Left;
        public float Right;
    }
}

