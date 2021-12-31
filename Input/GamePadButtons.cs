
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
    public struct GamePadButtons
    {
        public GamePadButtons(Buttons buttons)
        {
            _buttons = buttons;
        }

        public ButtonState A
        {
            get
            {
                return ((_buttons & Buttons.A) == Buttons.A ? ButtonState.Pressed : ButtonState.Released);
            }
        }

        public ButtonState B
        {
            get
            {
                return ((_buttons & Buttons.B) == Buttons.B ? ButtonState.Pressed : ButtonState.Released);
            }
        }

        public ButtonState Back
        {
            get
            {
                return ((_buttons & Buttons.Back) == Buttons.Back ? ButtonState.Pressed : ButtonState.Released);
            }
        }

        public ButtonState BigButton
        {
            get
            {
                return ((_buttons & Buttons.A) == Buttons.A ? ButtonState.Pressed : ButtonState.Released);
            }
        }

        public ButtonState LeftShoulder
        {
            get
            {
                return ((_buttons & Buttons.LeftShoulder) == Buttons.LeftShoulder ? ButtonState.Pressed : ButtonState.Released);
            }
        }

        public ButtonState LeftStick
        {
            get
            {
                return ((_buttons & Buttons.LeftStick) == Buttons.LeftStick ? ButtonState.Pressed : ButtonState.Released);
            }
        }

        public ButtonState RightShoulder
        {
            get
            {
                return ((_buttons & Buttons.RightShoulder) == Buttons.RightShoulder ? ButtonState.Pressed : ButtonState.Released);
            }
        }

        public ButtonState RightStick
        {
            get
            {
                return ((_buttons & Buttons.RightStick) == Buttons.RightStick ? ButtonState.Pressed : ButtonState.Released);
            }
        }

        public ButtonState Start
        {
            get
            {
                return ((_buttons & Buttons.Start) == Buttons.Start ? ButtonState.Pressed : ButtonState.Released);
            }
        }

        public ButtonState X
        {
            get
            {
                return ((_buttons & Buttons.X) == Buttons.X ? ButtonState.Pressed : ButtonState.Released);
            }
        }

        public ButtonState Y
        {
            get
            {
                return ((_buttons & Buttons.Y) == Buttons.Y ? ButtonState.Pressed : ButtonState.Released);
            }
        }

        readonly Buttons _buttons;
    }
}

