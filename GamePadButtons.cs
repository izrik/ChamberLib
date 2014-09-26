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

