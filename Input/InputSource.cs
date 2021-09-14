
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

namespace ChamberLib
{
    public class InputSource
    {
        public InputSource(IGamePad gamepad, IKeyboard keyboard=null,
            IMouse mouse=null)
        {
            GamePad = gamepad;
            Keyboard = keyboard;
            Mouse = mouse;
        }

        public readonly IGamePad GamePad;
        public readonly IKeyboard Keyboard;
        public readonly IMouse Mouse;

        public GameTime LastUpdateTime { get; protected set; }

        public void Update(GameTime gameTime)
        {
            var keyState = CurrentKeyboardState;
            if (Keyboard != null)
            {
                keyState = Keyboard.GetState();
            }

            var padState = CurrentGamepadState;
            if (GamePad != null)
            {
                padState = GamePad.GetState();
            }

            var mouseState = CurrentMouseState;
            if (Mouse !=null)
            {
                mouseState = Mouse.GetState();
            }

            SetCurrentStates(keyState, padState, mouseState);
        }

        protected void SetCurrentStates(KeyboardState keyState,
                                        GamePadState padState,
                                        MouseState mouseState)
        {
            PreviousKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = keyState;

            PreviousGamepadState = CurrentGamepadState;
            CurrentGamepadState = padState;

            PreviousMouseState = CurrentMouseState;
            CurrentMouseState = mouseState;
        }

        public KeyboardState CurrentKeyboardState { get; protected set; }
        public KeyboardState PreviousKeyboardState { get; protected set; }

        public GamePadState CurrentGamepadState { get; protected set; }
        public GamePadState PreviousGamepadState { get; protected set; }

        public MouseState CurrentMouseState { get; protected set; }
        public MouseState PreviousMouseState { get; protected set; }

        public bool IsKeyDown(Keys key)
        {
            return CurrentKeyboardState.IsKeyDown(key);
        }
        public bool IsKeyUp(Keys key)
        {
            return CurrentKeyboardState.IsKeyUp(key);
        }
        public bool IsKeyPressed(Keys key)
        {
            return (CurrentKeyboardState.IsKeyDown(key) && 
                    PreviousKeyboardState.IsKeyUp(key));
        }
        public bool IsKeyReleased(Keys key)
        {
            return (CurrentKeyboardState.IsKeyUp(key) && 
                    PreviousKeyboardState.IsKeyDown(key));
        }
        public bool IsKeyHeld(Keys key)
        {
            return (CurrentKeyboardState.IsKeyDown(key) && 
                    PreviousKeyboardState.IsKeyDown(key));
        }
        public bool IsKeyUnheld(Keys key)
        {
            return (CurrentKeyboardState.IsKeyUp(key) && 
                    PreviousKeyboardState.IsKeyUp(key));
        }

        public bool IsButtonDown(Buttons button)
        {
            return CurrentGamepadState.IsButtonDown(button);
        }
        public bool IsButtonUp(Buttons button)
        {
            return CurrentGamepadState.IsButtonUp(button);
        }
        public bool IsButtonPressed(Buttons button)
        {
            return (CurrentGamepadState.IsButtonDown(button) &&
                    PreviousGamepadState.IsButtonUp(button));
        }
        public bool IsButtonReleased(Buttons button)
        {
            return (CurrentGamepadState.IsButtonUp(button) &&
                    PreviousGamepadState.IsButtonDown(button));
        }
        public bool IsButtonHeld(Buttons button)
        {
            return (CurrentGamepadState.IsButtonDown(button) &&
                    PreviousGamepadState.IsButtonDown(button));
        }
        public bool IsButtonUnheld(Buttons button)
        {
            return (CurrentGamepadState.IsButtonUp(button) &&
                    PreviousGamepadState.IsButtonUp(button));
        }

        public bool IsButtonDown(MouseButtons button)
        {
            return CurrentMouseState.IsButtonDown(button);
        }
        public bool IsButtonUp(MouseButtons button)
        {
            return CurrentMouseState.IsButtonUp(button);
        }
        public bool IsButtonPressed(MouseButtons button)
        {
            return (CurrentMouseState.IsButtonDown(button) &&
                    PreviousMouseState.IsButtonUp(button));
        }
        public bool IsButtonReleased(MouseButtons button)
        {
            return (CurrentMouseState.IsButtonUp(button) &&
                    PreviousMouseState.IsButtonDown(button));
        }
        public bool IsButtonHeld(MouseButtons button)
        {
            return (CurrentMouseState.IsButtonDown(button) &&
                    PreviousMouseState.IsButtonDown(button));
        }
        public bool IsButtonUnheld(MouseButtons button)
        {
            return (CurrentMouseState.IsButtonUp(button) &&
                    PreviousMouseState.IsButtonUp(button));
        }

        public bool HasMouseMoved()
        {
            return CurrentMouseState.Position != PreviousMouseState.Position;
        }
    }
}
