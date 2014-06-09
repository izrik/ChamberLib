using System;
using XButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using XButtons = Microsoft.Xna.Framework.Input.Buttons;

namespace ChamberLib
{
    public static class GamePadStateHelper
    {
        public static Microsoft.Xna.Framework.Input.GamePadState ToXna(this ChamberLib.GamePadState gps)
        {
            return new Microsoft.Xna.Framework.Input.GamePadState(
                gps.ThumbSticks.ToXna(),
                gps.Triggers.ToXna(),
                gps.Buttons.ToXna(),
                gps.DPad.ToXna());
        }

        public static ChamberLib.GamePadState ToChamber(this Microsoft.Xna.Framework.Input.GamePadState gps)
        {
            return new ChamberLib.GamePadState(
                gps.ThumbSticks.ToChamber(),
                gps.Triggers.ToChamber(),
                gps.Buttons.ToChamber(),
                gps.DPad.ToChamber());
        }

        public static Microsoft.Xna.Framework.Input.GamePadThumbSticks ToXna(this ChamberLib.GamePadThumbSticks gps)
        {
            return new Microsoft.Xna.Framework.Input.GamePadThumbSticks(
                gps.Left.ToXna(),
                gps.Right.ToXna());
        }

        public static ChamberLib.GamePadThumbSticks ToChamber(this Microsoft.Xna.Framework.Input.GamePadThumbSticks gpts)
        {
            return new ChamberLib.GamePadThumbSticks(gpts.Left.ToChamber(), gpts.Right.ToChamber());
        }

        public static Microsoft.Xna.Framework.Input.GamePadTriggers ToXna(this ChamberLib.GamePadTriggers gpts)
        {
            return new Microsoft.Xna.Framework.Input.GamePadTriggers(gpts.Left, gpts.Right);
        }

        public static ChamberLib.GamePadTriggers ToChamber(this Microsoft.Xna.Framework.Input.GamePadTriggers gpt)
        {
            return new ChamberLib.GamePadTriggers(gpt.Left, gpt.Right);
        }

        public static Microsoft.Xna.Framework.Input.GamePadButtons ToXna(this ChamberLib.GamePadButtons gpb)
        {
            XButtons buttons = default(XButtons);

            if (gpb.A == ButtonState.Pressed) buttons |= XButtons.A;
            if (gpb.B == ButtonState.Pressed) buttons |= XButtons.B;
            if (gpb.Back == ButtonState.Pressed) buttons |= XButtons.Back;
            if (gpb.BigButton == ButtonState.Pressed) buttons |= XButtons.BigButton;
            if (gpb.LeftShoulder == ButtonState.Pressed) buttons |= XButtons.LeftShoulder;
            if (gpb.LeftStick == ButtonState.Pressed) buttons |= XButtons.LeftStick;
            if (gpb.RightShoulder == ButtonState.Pressed) buttons |= XButtons.RightShoulder;
            if (gpb.RightStick == ButtonState.Pressed) buttons |= XButtons.RightStick;
            if (gpb.Start == ButtonState.Pressed) buttons |= XButtons.Start;
            if (gpb.X == ButtonState.Pressed) buttons |= XButtons.X;
            if (gpb.Y == ButtonState.Pressed) buttons |= XButtons.Y;

            return new Microsoft.Xna.Framework.Input.GamePadButtons(buttons);
        }

        public static ChamberLib.GamePadButtons ToChamber(this Microsoft.Xna.Framework.Input.GamePadButtons gpb)
        {
            ChamberLib.Buttons buttons = default(ChamberLib.Buttons);

            if (gpb.A == XButtonState.Pressed) buttons |= ChamberLib.Buttons.A;
            if (gpb.B == XButtonState.Pressed) buttons |= ChamberLib.Buttons.B;
            if (gpb.Back == XButtonState.Pressed) buttons |= ChamberLib.Buttons.Back;
            if (gpb.BigButton == XButtonState.Pressed) buttons |= ChamberLib.Buttons.BigButton;
            if (gpb.LeftShoulder == XButtonState.Pressed) buttons |= ChamberLib.Buttons.LeftShoulder;
            if (gpb.LeftStick == XButtonState.Pressed) buttons |= ChamberLib.Buttons.LeftStick;
            if (gpb.RightShoulder == XButtonState.Pressed) buttons |= ChamberLib.Buttons.RightShoulder;
            if (gpb.RightStick == XButtonState.Pressed) buttons |= ChamberLib.Buttons.RightStick;
            if (gpb.Start == XButtonState.Pressed) buttons |= ChamberLib.Buttons.Start;
            if (gpb.X == XButtonState.Pressed) buttons |= ChamberLib.Buttons.X;
            if (gpb.Y == XButtonState.Pressed) buttons |= ChamberLib.Buttons.Y;

            return new ChamberLib.GamePadButtons(buttons);
        }

        public static Microsoft.Xna.Framework.Input.GamePadDPad ToXna(this ChamberLib.GamePadDPad gpdp)
        {
            return new Microsoft.Xna.Framework.Input.GamePadDPad(gpdp.Up.ToXna(), gpdp.Down.ToXna(), gpdp.Left.ToXna(), gpdp.Right.ToXna());
        }

        public static ChamberLib.GamePadDPad ToChamber(this Microsoft.Xna.Framework.Input.GamePadDPad gpdp)
        {
            return new ChamberLib.GamePadDPad(gpdp.Up.ToChamber(), gpdp.Down.ToChamber(), gpdp.Left.ToChamber(), gpdp.Right.ToChamber());
        }

        public static XButtonState ToXna(this ChamberLib.ButtonState bs)
        {
            if (bs == ButtonState.Pressed)
            {
                return XButtonState.Pressed;
            }
            else
            {
                return XButtonState.Released;
            }
        }

        public static ChamberLib.ButtonState ToChamber(this XButtonState bs)
        {
            if (bs == XButtonState.Pressed)
            {
                return ButtonState.Pressed;
            }
            else
            {
                return ButtonState.Released;
            }
        }
    }
}

