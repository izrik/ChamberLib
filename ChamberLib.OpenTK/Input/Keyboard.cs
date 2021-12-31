
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
using System.Collections.Generic;

using _OpenTK = global::OpenTK;

namespace ChamberLib.OpenTK.Input
{
    public class Keyboard : IKeyboard
    {
        static Keyboard _cache = new Keyboard();

        public static Keyboard GetKeyboard()
        {
            return _cache;
        }

        public KeyboardState GetState()
        {
            var state = _OpenTK.Input.Keyboard.GetState();

            return new KeyboardState(
                leftShift: state.IsKeyDown(_OpenTK.Input.Key.ShiftLeft),
                rightShift: state.IsKeyDown(_OpenTK.Input.Key.ShiftRight),
                leftControl: state.IsKeyDown(_OpenTK.Input.Key.ControlLeft),
                rightControl: state.IsKeyDown(_OpenTK.Input.Key.ControlRight),
                leftAlt: state.IsKeyDown(_OpenTK.Input.Key.AltLeft),
                rightAlt: state.IsKeyDown(_OpenTK.Input.Key.AltRight),
                leftWindows: state.IsKeyDown(_OpenTK.Input.Key.WinLeft),
                rightWindows: state.IsKeyDown(_OpenTK.Input.Key.WinRight),
                menu: state.IsKeyDown(_OpenTK.Input.Key.Menu),
                f1: state.IsKeyDown(_OpenTK.Input.Key.F1),
                f2: state.IsKeyDown(_OpenTK.Input.Key.F2),
                f3: state.IsKeyDown(_OpenTK.Input.Key.F3),
                f4: state.IsKeyDown(_OpenTK.Input.Key.F4),
                f5: state.IsKeyDown(_OpenTK.Input.Key.F5),
                f6: state.IsKeyDown(_OpenTK.Input.Key.F6),
                f7: state.IsKeyDown(_OpenTK.Input.Key.F7),
                f8: state.IsKeyDown(_OpenTK.Input.Key.F8),
                f9: state.IsKeyDown(_OpenTK.Input.Key.F9),
                f10: state.IsKeyDown(_OpenTK.Input.Key.F10),
                f11: state.IsKeyDown(_OpenTK.Input.Key.F11),
                f12: state.IsKeyDown(_OpenTK.Input.Key.F12),
                f13: state.IsKeyDown(_OpenTK.Input.Key.F13),
                f14: state.IsKeyDown(_OpenTK.Input.Key.F14),
                f15: state.IsKeyDown(_OpenTK.Input.Key.F15),
                f16: state.IsKeyDown(_OpenTK.Input.Key.F16),
                f17: state.IsKeyDown(_OpenTK.Input.Key.F17),
                f18: state.IsKeyDown(_OpenTK.Input.Key.F18),
                f19: state.IsKeyDown(_OpenTK.Input.Key.F19),
                f20: state.IsKeyDown(_OpenTK.Input.Key.F20),
                f21: state.IsKeyDown(_OpenTK.Input.Key.F21),
                f22: state.IsKeyDown(_OpenTK.Input.Key.F22),
                f23: state.IsKeyDown(_OpenTK.Input.Key.F23),
                f24: state.IsKeyDown(_OpenTK.Input.Key.F24),
                //f25: state.IsKeyDown(_OpenTK.Input.Key.F25),
                //f26: state.IsKeyDown(_OpenTK.Input.Key.F26),
                //f27: state.IsKeyDown(_OpenTK.Input.Key.F27),
                //f28: state.IsKeyDown(_OpenTK.Input.Key.F28),
                //f29: state.IsKeyDown(_OpenTK.Input.Key.F29),
                //f30: state.IsKeyDown(_OpenTK.Input.Key.F30),
                //f31: state.IsKeyDown(_OpenTK.Input.Key.F31),
                //f32: state.IsKeyDown(_OpenTK.Input.Key.F32),
                //f33: state.IsKeyDown(_OpenTK.Input.Key.F33),
                //f34: state.IsKeyDown(_OpenTK.Input.Key.F34),
                //f35: state.IsKeyDown(_OpenTK.Input.Key.F35),
                up: state.IsKeyDown(_OpenTK.Input.Key.Up),
                down: state.IsKeyDown(_OpenTK.Input.Key.Down),
                left: state.IsKeyDown(_OpenTK.Input.Key.Left),
                right: state.IsKeyDown(_OpenTK.Input.Key.Right),
                enter: state.IsKeyDown(_OpenTK.Input.Key.Enter),
                escape: state.IsKeyDown(_OpenTK.Input.Key.Escape),
                space: state.IsKeyDown(_OpenTK.Input.Key.Space),
                tab: state.IsKeyDown(_OpenTK.Input.Key.Tab),
                back: state.IsKeyDown(_OpenTK.Input.Key.BackSpace),
                insert: state.IsKeyDown(_OpenTK.Input.Key.Insert),
                delete: state.IsKeyDown(_OpenTK.Input.Key.Delete),
                pageUp: state.IsKeyDown(_OpenTK.Input.Key.PageUp),
                pageDown: state.IsKeyDown(_OpenTK.Input.Key.PageDown),
                home: state.IsKeyDown(_OpenTK.Input.Key.Home),
                end: state.IsKeyDown(_OpenTK.Input.Key.End),
                capsLock: state.IsKeyDown(_OpenTK.Input.Key.CapsLock),
                scrollLock: state.IsKeyDown(_OpenTK.Input.Key.ScrollLock),
                printScreen: state.IsKeyDown(_OpenTK.Input.Key.PrintScreen),
                pause: state.IsKeyDown(_OpenTK.Input.Key.Pause),
                numLock: state.IsKeyDown(_OpenTK.Input.Key.NumLock),
                //clear: state.IsKeyDown(_OpenTK.Input.Key.Clear),
                sleep: state.IsKeyDown(_OpenTK.Input.Key.Sleep),
                numPad0: state.IsKeyDown(_OpenTK.Input.Key.Keypad0),
                numPad1: state.IsKeyDown(_OpenTK.Input.Key.Keypad1),
                numPad2: state.IsKeyDown(_OpenTK.Input.Key.Keypad2),
                numPad3: state.IsKeyDown(_OpenTK.Input.Key.Keypad3),
                numPad4: state.IsKeyDown(_OpenTK.Input.Key.Keypad4),
                numPad5: state.IsKeyDown(_OpenTK.Input.Key.Keypad5),
                numPad6: state.IsKeyDown(_OpenTK.Input.Key.Keypad6),
                numPad7: state.IsKeyDown(_OpenTK.Input.Key.Keypad7),
                numPad8: state.IsKeyDown(_OpenTK.Input.Key.Keypad8),
                numPad9: state.IsKeyDown(_OpenTK.Input.Key.Keypad9),
                //keypadDivide: state.IsKeyDown(_OpenTK.Input.Key.KeypadDivide),
                //keypadMultiply: state.IsKeyDown(_OpenTK.Input.Key.KeypadMultiply),
                //keypadSubtract: state.IsKeyDown(_OpenTK.Input.Key.KeypadSubtract),
                //keypadMinus: state.IsKeyDown(_OpenTK.Input.Key.KeypadMinus),
                //keypadAdd: state.IsKeyDown(_OpenTK.Input.Key.KeypadAdd),
                //keypadPlus: state.IsKeyDown(_OpenTK.Input.Key.KeypadPlus),
                //keypadDecimal: state.IsKeyDown(_OpenTK.Input.Key.KeypadDecimal),
                //keypadPeriod: state.IsKeyDown(_OpenTK.Input.Key.KeypadPeriod),
                //keypadEnter: state.IsKeyDown(_OpenTK.Input.Key.KeypadEnter),
                a: state.IsKeyDown(_OpenTK.Input.Key.A),
                b: state.IsKeyDown(_OpenTK.Input.Key.B),
                c: state.IsKeyDown(_OpenTK.Input.Key.C),
                d: state.IsKeyDown(_OpenTK.Input.Key.D),
                e: state.IsKeyDown(_OpenTK.Input.Key.E),
                f: state.IsKeyDown(_OpenTK.Input.Key.F),
                g: state.IsKeyDown(_OpenTK.Input.Key.G),
                h: state.IsKeyDown(_OpenTK.Input.Key.H),
                i: state.IsKeyDown(_OpenTK.Input.Key.I),
                j: state.IsKeyDown(_OpenTK.Input.Key.J),
                k: state.IsKeyDown(_OpenTK.Input.Key.K),
                l: state.IsKeyDown(_OpenTK.Input.Key.L),
                m: state.IsKeyDown(_OpenTK.Input.Key.M),
                n: state.IsKeyDown(_OpenTK.Input.Key.N),
                o: state.IsKeyDown(_OpenTK.Input.Key.O),
                p: state.IsKeyDown(_OpenTK.Input.Key.P),
                q: state.IsKeyDown(_OpenTK.Input.Key.Q),
                r: state.IsKeyDown(_OpenTK.Input.Key.R),
                s: state.IsKeyDown(_OpenTK.Input.Key.S),
                t: state.IsKeyDown(_OpenTK.Input.Key.T),
                u: state.IsKeyDown(_OpenTK.Input.Key.U),
                v: state.IsKeyDown(_OpenTK.Input.Key.V),
                w: state.IsKeyDown(_OpenTK.Input.Key.W),
                x: state.IsKeyDown(_OpenTK.Input.Key.X),
                y: state.IsKeyDown(_OpenTK.Input.Key.Y),
                z: state.IsKeyDown(_OpenTK.Input.Key.Z),
                number0: state.IsKeyDown(_OpenTK.Input.Key.Number0),
                number1: state.IsKeyDown(_OpenTK.Input.Key.Number1),
                number2: state.IsKeyDown(_OpenTK.Input.Key.Number2),
                number3: state.IsKeyDown(_OpenTK.Input.Key.Number3),
                number4: state.IsKeyDown(_OpenTK.Input.Key.Number4),
                number5: state.IsKeyDown(_OpenTK.Input.Key.Number5),
                number6: state.IsKeyDown(_OpenTK.Input.Key.Number6),
                number7: state.IsKeyDown(_OpenTK.Input.Key.Number7),
                number8: state.IsKeyDown(_OpenTK.Input.Key.Number8),
                number9: state.IsKeyDown(_OpenTK.Input.Key.Number9),
                //tilde: state.IsKeyDown(_OpenTK.Input.Key.Tilde),
                //grave: state.IsKeyDown(_OpenTK.Input.Key.Grave),
                //minus: state.IsKeyDown(_OpenTK.Input.Key.Minus),
                //plus: state.IsKeyDown(_OpenTK.Input.Key.Plus),
                oemOpenBrackets: state.IsKeyDown(_OpenTK.Input.Key.BracketLeft),
                oemCloseBrackets: state.IsKeyDown(_OpenTK.Input.Key.BracketRight),
                oemSemicolon: state.IsKeyDown(_OpenTK.Input.Key.Semicolon),
                oemQuotes: state.IsKeyDown(_OpenTK.Input.Key.Quote),
                oemComma: state.IsKeyDown(_OpenTK.Input.Key.Comma),
                oemPeriod: state.IsKeyDown(_OpenTK.Input.Key.Period),
                //slash: state.IsKeyDown(_OpenTK.Input.Key.Slash),
                oemBackslash: state.IsKeyDown(_OpenTK.Input.Key.BackSlash)
                //nonUSBackSlash: state.IsKeyDown(_OpenTK.Input.Key.NonUSBackSlash),
            );
        }
    }
}

