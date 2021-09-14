using System;

namespace ChamberLib
{
    public struct GamePadThumbSticks
    {
        public GamePadThumbSticks(Vector2 leftPosition, Vector2 rightPosition)
        {
            this.Left = leftPosition;
            this.Right = rightPosition;
        }

        public Vector2 Left;
        public Vector2 Right;
    }
}

