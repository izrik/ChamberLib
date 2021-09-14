using System;
namespace ChamberLib
{
    public struct MouseState
    {
        public MouseState(float x, float y, bool leftButton, bool middleButton,
            bool rightButton, int wheel)
        {
            X = x;
            Y = y;
            LeftButton = leftButton;
            MiddleButton = middleButton;
            RightButton = rightButton;
            Wheel = wheel;
        }

        public readonly float X;
        public readonly float Y;
        public readonly bool LeftButton;
        public readonly bool MiddleButton;
        public readonly bool RightButton;
        public readonly int Wheel;

        public Vector2 Position { get { return new Vector2(X, Y); } }

        public bool IsButtonDown(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.Left: return LeftButton;
                case MouseButtons.Middle: return MiddleButton;
                case MouseButtons.Right: return RightButton;
            }
            return false;
        }

        public bool IsButtonUp(MouseButtons button)
        {
            return !(IsButtonDown(button));
        }
    }
}
