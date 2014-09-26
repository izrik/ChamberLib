using System;

namespace ChamberLib
{
    public struct GamePadState
    {
        public GamePadState(GamePadThumbSticks thumbsticks, GamePadTriggers triggers, GamePadButtons buttons, GamePadDPad dpad)
        {
            ThumbSticks = thumbsticks;
            Triggers = triggers;
            Buttons = buttons;
            DPad = dpad;
        }

        public GamePadThumbSticks ThumbSticks;
        public GamePadTriggers Triggers;
        public GamePadButtons Buttons;
        public GamePadDPad DPad;

        public static float AnalogThreshold = 0.12f;

        public bool IsButtonDown(Buttons button)
        {
            switch (button)
            {
                case ChamberLib.Buttons.DPadUp: return (DPad.Up == ButtonState.Pressed);
                case ChamberLib.Buttons.DPadDown: return (DPad.Down == ButtonState.Pressed);
                case ChamberLib.Buttons.DPadLeft: return (DPad.Left == ButtonState.Pressed);
                case ChamberLib.Buttons.DPadRight: return (DPad.Right == ButtonState.Pressed);
                case ChamberLib.Buttons.Start: return (Buttons.Start == ButtonState.Pressed);
                case ChamberLib.Buttons.Back: return (Buttons.Back == ButtonState.Pressed);
                case ChamberLib.Buttons.LeftStick: return (Buttons.LeftStick == ButtonState.Pressed);
                case ChamberLib.Buttons.RightStick: return (Buttons.RightStick == ButtonState.Pressed);
                case ChamberLib.Buttons.LeftShoulder: return (Buttons.LeftShoulder == ButtonState.Pressed);
                case ChamberLib.Buttons.RightShoulder: return (Buttons.RightShoulder == ButtonState.Pressed);
                case ChamberLib.Buttons.BigButton: return (Buttons.BigButton == ButtonState.Pressed);
                case ChamberLib.Buttons.A: return (Buttons.A == ButtonState.Pressed);
                case ChamberLib.Buttons.B: return (Buttons.B == ButtonState.Pressed);
                case ChamberLib.Buttons.X: return (Buttons.X == ButtonState.Pressed);
                case ChamberLib.Buttons.Y: return (Buttons.Y == ButtonState.Pressed);
                case ChamberLib.Buttons.RightTrigger: return (Triggers.Right > AnalogThreshold);
                case ChamberLib.Buttons.LeftTrigger: return (Triggers.Left > AnalogThreshold);
                case ChamberLib.Buttons.RightThumbstickUp: return (ThumbSticks.Right.Y > AnalogThreshold);
                case ChamberLib.Buttons.RightThumbstickDown: return (ThumbSticks.Right.Y < -AnalogThreshold);
                case ChamberLib.Buttons.RightThumbstickLeft: return (ThumbSticks.Right.X < -AnalogThreshold);
                case ChamberLib.Buttons.RightThumbstickRight: return (ThumbSticks.Right.X > AnalogThreshold);
                case ChamberLib.Buttons.LeftThumbstickUp: return (ThumbSticks.Left.Y > AnalogThreshold);
                case ChamberLib.Buttons.LeftThumbstickDown: return (ThumbSticks.Left.Y < -AnalogThreshold);
                case ChamberLib.Buttons.LeftThumbstickLeft: return (ThumbSticks.Left.X < -AnalogThreshold);
                case ChamberLib.Buttons.LeftThumbstickRight: return (ThumbSticks.Left.X > AnalogThreshold);
            }

            return false;
        }

        public bool IsButtonUp(Buttons button)
        {
            return !IsButtonDown(button);
        }
    }
}

