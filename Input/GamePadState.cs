
//
// ChamberLib, a cross-platform game engine
// Copyright (C) 2021 izrik and Metaphysics Industries, Inc.
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301
// USA
//

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

