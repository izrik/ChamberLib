using System;
using System.Collections.Generic;
using XButtons = Microsoft.Xna.Framework.Input.Buttons;

namespace ChamberLib
{
    public static class ButtonsHelper
    {
        public static XButtons ToXna(ChamberLib.Buttons b)
        {
            switch (b)
            {
                case Buttons.DPadUp: return XButtons.DPadUp;
                case Buttons.DPadDown: return XButtons.DPadDown;
                case Buttons.DPadLeft: return XButtons.DPadLeft;
                case Buttons.DPadRight: return XButtons.DPadRight;
                case Buttons.Start: return XButtons.Start;
                case Buttons.Back: return XButtons.Back;
                case Buttons.LeftStick: return XButtons.LeftStick;
                case Buttons.RightStick: return XButtons.RightStick;
                case Buttons.LeftShoulder: return XButtons.LeftShoulder;
                case Buttons.RightShoulder: return XButtons.RightShoulder;
                case Buttons.BigButton: return XButtons.BigButton;
                case Buttons.A: return XButtons.A;
                case Buttons.B: return XButtons.B;
                case Buttons.X: return XButtons.X;
                case Buttons.Y: return XButtons.Y;
                case Buttons.LeftThumbstickLeft: return XButtons.LeftThumbstickLeft;
                case Buttons.RightTrigger: return XButtons.RightTrigger;
                case Buttons.LeftTrigger: return XButtons.LeftTrigger;
                case Buttons.RightThumbstickUp: return XButtons.RightThumbstickUp;
                case Buttons.RightThumbstickDown: return XButtons.RightThumbstickDown;
                case Buttons.RightThumbstickRight: return XButtons.RightThumbstickRight;
                case Buttons.RightThumbstickLeft: return XButtons.RightThumbstickLeft;
                case Buttons.LeftThumbstickUp: return XButtons.LeftThumbstickUp;
                case Buttons.LeftThumbstickDown: return XButtons.LeftThumbstickDown;
                case Buttons.LeftThumbstickRight: return XButtons.LeftThumbstickRight;
            }

            throw new ArgumentException();
        }

        public static ChamberLib.Buttons[] ToChamber(this XButtons b)
        {
            var all = new List<Buttons>();

            if ((b & XButtons.DPadUp) == XButtons.DPadUp) all.Add(Buttons.DPadUp);
            if ((b & XButtons.DPadDown) == XButtons.DPadDown) all.Add(Buttons.DPadDown);
            if ((b & XButtons.DPadLeft) == XButtons.DPadLeft) all.Add(Buttons.DPadLeft);
            if ((b & XButtons.DPadRight) == XButtons.DPadRight) all.Add(Buttons.DPadRight);
            if ((b & XButtons.Start) == XButtons.Start) all.Add(Buttons.Start);
            if ((b & XButtons.Back) == XButtons.Back) all.Add(Buttons.Back);
            if ((b & XButtons.LeftStick) == XButtons.LeftStick) all.Add(Buttons.LeftStick);
            if ((b & XButtons.RightStick) == XButtons.RightStick) all.Add(Buttons.RightStick);
            if ((b & XButtons.LeftShoulder) == XButtons.LeftShoulder) all.Add(Buttons.LeftShoulder);
            if ((b & XButtons.RightShoulder) == XButtons.RightShoulder) all.Add(Buttons.RightShoulder);
            if ((b & XButtons.BigButton) == XButtons.BigButton) all.Add(Buttons.BigButton);
            if ((b & XButtons.A) == XButtons.A) all.Add(Buttons.A);
            if ((b & XButtons.B) == XButtons.B) all.Add(Buttons.B);
            if ((b & XButtons.X) == XButtons.X) all.Add(Buttons.X);
            if ((b & XButtons.Y) == XButtons.Y) all.Add(Buttons.Y);
            if ((b & XButtons.LeftThumbstickLeft) == XButtons.LeftThumbstickLeft) all.Add(Buttons.LeftThumbstickLeft);
            if ((b & XButtons.RightTrigger) == XButtons.RightTrigger) all.Add(Buttons.RightTrigger);
            if ((b & XButtons.LeftTrigger) == XButtons.LeftTrigger) all.Add(Buttons.LeftTrigger);
            if ((b & XButtons.RightThumbstickUp) == XButtons.RightThumbstickUp) all.Add(Buttons.RightThumbstickUp);
            if ((b & XButtons.RightThumbstickDown) == XButtons.RightThumbstickDown) all.Add(Buttons.RightThumbstickDown);
            if ((b & XButtons.RightThumbstickRight) == XButtons.RightThumbstickRight) all.Add(Buttons.RightThumbstickRight);
            if ((b & XButtons.RightThumbstickLeft) == XButtons.RightThumbstickLeft) all.Add(Buttons.RightThumbstickLeft);
            if ((b & XButtons.LeftThumbstickUp) == XButtons.LeftThumbstickUp) all.Add(Buttons.LeftThumbstickUp);
            if ((b & XButtons.LeftThumbstickDown) == XButtons.LeftThumbstickDown) all.Add(Buttons.LeftThumbstickDown);
            if ((b & XButtons.LeftThumbstickRight) == XButtons.LeftThumbstickRight) all.Add(Buttons.LeftThumbstickRight);

            return all.ToArray();
        }
    }
}

