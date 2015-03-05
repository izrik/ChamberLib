using System;
using System.Collections.Generic;

using _OpenTK = global::OpenTK;

namespace ChamberLib.OpenTK
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

            var keysDown = new List<ChamberLib.Keys>(144);

            if (state.IsKeyDown(_OpenTK.Input.Key.ShiftLeft)) { keysDown.Add(ChamberLib.Keys.LeftShift); }
            if (state.IsKeyDown(_OpenTK.Input.Key.LShift)) { keysDown.Add(ChamberLib.Keys.LeftShift); }
            if (state.IsKeyDown(_OpenTK.Input.Key.ShiftRight)) { keysDown.Add(ChamberLib.Keys.RightShift); }
            if (state.IsKeyDown(_OpenTK.Input.Key.RShift)) { keysDown.Add(ChamberLib.Keys.RightShift); }
            if (state.IsKeyDown(_OpenTK.Input.Key.ControlLeft)) { keysDown.Add(ChamberLib.Keys.LeftControl); }
            if (state.IsKeyDown(_OpenTK.Input.Key.LControl)) { keysDown.Add(ChamberLib.Keys.LeftControl); }
            if (state.IsKeyDown(_OpenTK.Input.Key.ControlRight)) { keysDown.Add(ChamberLib.Keys.RightControl); }
            if (state.IsKeyDown(_OpenTK.Input.Key.RControl)) { keysDown.Add(ChamberLib.Keys.RightControl); }
            if (state.IsKeyDown(_OpenTK.Input.Key.AltLeft)) { keysDown.Add(ChamberLib.Keys.LeftAlt); }
            if (state.IsKeyDown(_OpenTK.Input.Key.LAlt)) { keysDown.Add(ChamberLib.Keys.LeftAlt); }
            if (state.IsKeyDown(_OpenTK.Input.Key.AltRight)) { keysDown.Add(ChamberLib.Keys.RightAlt); }
            if (state.IsKeyDown(_OpenTK.Input.Key.RAlt)) { keysDown.Add(ChamberLib.Keys.RightAlt); }
            if (state.IsKeyDown(_OpenTK.Input.Key.WinLeft)) { keysDown.Add(ChamberLib.Keys.LeftWindows); }
            if (state.IsKeyDown(_OpenTK.Input.Key.LWin)) { keysDown.Add(ChamberLib.Keys.LeftWindows); }
            if (state.IsKeyDown(_OpenTK.Input.Key.WinRight)) { keysDown.Add(ChamberLib.Keys.RightWindows); }
            if (state.IsKeyDown(_OpenTK.Input.Key.RWin)) { keysDown.Add(ChamberLib.Keys.RightWindows); }
            if (state.IsKeyDown(_OpenTK.Input.Key.Menu)) { keysDown.Add(ChamberLib.Keys.Apps); }
            if (state.IsKeyDown(_OpenTK.Input.Key.F1)) { keysDown.Add(ChamberLib.Keys.F1); }
            if (state.IsKeyDown(_OpenTK.Input.Key.F2)) { keysDown.Add(ChamberLib.Keys.F2); }
            if (state.IsKeyDown(_OpenTK.Input.Key.F3)) { keysDown.Add(ChamberLib.Keys.F3); }
            if (state.IsKeyDown(_OpenTK.Input.Key.F4)) { keysDown.Add(ChamberLib.Keys.F4); }
            if (state.IsKeyDown(_OpenTK.Input.Key.F5)) { keysDown.Add(ChamberLib.Keys.F5); }
            if (state.IsKeyDown(_OpenTK.Input.Key.F6)) { keysDown.Add(ChamberLib.Keys.F6); }
            if (state.IsKeyDown(_OpenTK.Input.Key.F7)) { keysDown.Add(ChamberLib.Keys.F7); }
            if (state.IsKeyDown(_OpenTK.Input.Key.F8)) { keysDown.Add(ChamberLib.Keys.F8); }
            if (state.IsKeyDown(_OpenTK.Input.Key.F9)) { keysDown.Add(ChamberLib.Keys.F9); }
            if (state.IsKeyDown(_OpenTK.Input.Key.F10)) { keysDown.Add(ChamberLib.Keys.F10); }
            if (state.IsKeyDown(_OpenTK.Input.Key.F11)) { keysDown.Add(ChamberLib.Keys.F11); }
            if (state.IsKeyDown(_OpenTK.Input.Key.F12)) { keysDown.Add(ChamberLib.Keys.F12); }
            if (state.IsKeyDown(_OpenTK.Input.Key.F13)) { keysDown.Add(ChamberLib.Keys.F13); }
            if (state.IsKeyDown(_OpenTK.Input.Key.F14)) { keysDown.Add(ChamberLib.Keys.F14); }
            if (state.IsKeyDown(_OpenTK.Input.Key.F15)) { keysDown.Add(ChamberLib.Keys.F15); }
            if (state.IsKeyDown(_OpenTK.Input.Key.F16)) { keysDown.Add(ChamberLib.Keys.F16); }
            if (state.IsKeyDown(_OpenTK.Input.Key.F17)) { keysDown.Add(ChamberLib.Keys.F17); }
            if (state.IsKeyDown(_OpenTK.Input.Key.F18)) { keysDown.Add(ChamberLib.Keys.F18); }
            if (state.IsKeyDown(_OpenTK.Input.Key.F19)) { keysDown.Add(ChamberLib.Keys.F19); }
            if (state.IsKeyDown(_OpenTK.Input.Key.F20)) { keysDown.Add(ChamberLib.Keys.F20); }
            if (state.IsKeyDown(_OpenTK.Input.Key.F21)) { keysDown.Add(ChamberLib.Keys.F21); }
            if (state.IsKeyDown(_OpenTK.Input.Key.F22)) { keysDown.Add(ChamberLib.Keys.F22); }
            if (state.IsKeyDown(_OpenTK.Input.Key.F23)) { keysDown.Add(ChamberLib.Keys.F23); }
            if (state.IsKeyDown(_OpenTK.Input.Key.F24)) { keysDown.Add(ChamberLib.Keys.F24); }
//            if (state.IsKeyDown(_OpenTK.Input.Key.F25)) { keysDown.Add(ChamberLib.Keys.F25); }
//            if (state.IsKeyDown(_OpenTK.Input.Key.F26)) { keysDown.Add(ChamberLib.Keys.F26); }
//            if (state.IsKeyDown(_OpenTK.Input.Key.F27)) { keysDown.Add(ChamberLib.Keys.F27); }
//            if (state.IsKeyDown(_OpenTK.Input.Key.F28)) { keysDown.Add(ChamberLib.Keys.F28); }
//            if (state.IsKeyDown(_OpenTK.Input.Key.F29)) { keysDown.Add(ChamberLib.Keys.F29); }
//            if (state.IsKeyDown(_OpenTK.Input.Key.F30)) { keysDown.Add(ChamberLib.Keys.F30); }
//            if (state.IsKeyDown(_OpenTK.Input.Key.F31)) { keysDown.Add(ChamberLib.Keys.F31); }
//            if (state.IsKeyDown(_OpenTK.Input.Key.F32)) { keysDown.Add(ChamberLib.Keys.F32); }
//            if (state.IsKeyDown(_OpenTK.Input.Key.F33)) { keysDown.Add(ChamberLib.Keys.F33); }
//            if (state.IsKeyDown(_OpenTK.Input.Key.F34)) { keysDown.Add(ChamberLib.Keys.F34); }
//            if (state.IsKeyDown(_OpenTK.Input.Key.F35)) { keysDown.Add(ChamberLib.Keys.F35); }
            if (state.IsKeyDown(_OpenTK.Input.Key.Up)) { keysDown.Add(ChamberLib.Keys.Up); }
            if (state.IsKeyDown(_OpenTK.Input.Key.Down)) { keysDown.Add(ChamberLib.Keys.Down); }
            if (state.IsKeyDown(_OpenTK.Input.Key.Left)) { keysDown.Add(ChamberLib.Keys.Left); }
            if (state.IsKeyDown(_OpenTK.Input.Key.Right)) { keysDown.Add(ChamberLib.Keys.Right); }
            if (state.IsKeyDown(_OpenTK.Input.Key.Enter)) { keysDown.Add(ChamberLib.Keys.Enter); }
            if (state.IsKeyDown(_OpenTK.Input.Key.Escape)) { keysDown.Add(ChamberLib.Keys.Escape); }
            if (state.IsKeyDown(_OpenTK.Input.Key.Space)) { keysDown.Add(ChamberLib.Keys.Space); }
            if (state.IsKeyDown(_OpenTK.Input.Key.Tab)) { keysDown.Add(ChamberLib.Keys.Tab); }
            if (state.IsKeyDown(_OpenTK.Input.Key.BackSpace)) { keysDown.Add(ChamberLib.Keys.Back); }
            if (state.IsKeyDown(_OpenTK.Input.Key.Back)) { keysDown.Add(ChamberLib.Keys.Back); }
            if (state.IsKeyDown(_OpenTK.Input.Key.Insert)) { keysDown.Add(ChamberLib.Keys.Insert); }
            if (state.IsKeyDown(_OpenTK.Input.Key.Delete)) { keysDown.Add(ChamberLib.Keys.Delete); }
            if (state.IsKeyDown(_OpenTK.Input.Key.PageUp)) { keysDown.Add(ChamberLib.Keys.PageUp); }
            if (state.IsKeyDown(_OpenTK.Input.Key.PageDown)) { keysDown.Add(ChamberLib.Keys.PageDown); }
            if (state.IsKeyDown(_OpenTK.Input.Key.Home)) { keysDown.Add(ChamberLib.Keys.Home); }
            if (state.IsKeyDown(_OpenTK.Input.Key.End)) { keysDown.Add(ChamberLib.Keys.End); }
            if (state.IsKeyDown(_OpenTK.Input.Key.CapsLock)) { keysDown.Add(ChamberLib.Keys.CapsLock); }
            if (state.IsKeyDown(_OpenTK.Input.Key.ScrollLock)) { keysDown.Add(ChamberLib.Keys.Scroll); }
            if (state.IsKeyDown(_OpenTK.Input.Key.PrintScreen)) { keysDown.Add(ChamberLib.Keys.PrintScreen); }
            if (state.IsKeyDown(_OpenTK.Input.Key.Pause)) { keysDown.Add(ChamberLib.Keys.Pause); }
            if (state.IsKeyDown(_OpenTK.Input.Key.NumLock)) { keysDown.Add(ChamberLib.Keys.NumLock); }
            if (state.IsKeyDown(_OpenTK.Input.Key.Clear)) { keysDown.Add(ChamberLib.Keys.OemClear); }
            if (state.IsKeyDown(_OpenTK.Input.Key.Sleep)) { keysDown.Add(ChamberLib.Keys.Sleep); }
            if (state.IsKeyDown(_OpenTK.Input.Key.Keypad0)) { keysDown.Add(ChamberLib.Keys.NumPad0); }
            if (state.IsKeyDown(_OpenTK.Input.Key.Keypad1)) { keysDown.Add(ChamberLib.Keys.NumPad1); }
            if (state.IsKeyDown(_OpenTK.Input.Key.Keypad2)) { keysDown.Add(ChamberLib.Keys.NumPad2); }
            if (state.IsKeyDown(_OpenTK.Input.Key.Keypad3)) { keysDown.Add(ChamberLib.Keys.NumPad3); }
            if (state.IsKeyDown(_OpenTK.Input.Key.Keypad4)) { keysDown.Add(ChamberLib.Keys.NumPad4); }
            if (state.IsKeyDown(_OpenTK.Input.Key.Keypad5)) { keysDown.Add(ChamberLib.Keys.NumPad5); }
            if (state.IsKeyDown(_OpenTK.Input.Key.Keypad6)) { keysDown.Add(ChamberLib.Keys.NumPad6); }
            if (state.IsKeyDown(_OpenTK.Input.Key.Keypad7)) { keysDown.Add(ChamberLib.Keys.NumPad7); }
            if (state.IsKeyDown(_OpenTK.Input.Key.Keypad8)) { keysDown.Add(ChamberLib.Keys.NumPad8); }
            if (state.IsKeyDown(_OpenTK.Input.Key.Keypad9)) { keysDown.Add(ChamberLib.Keys.NumPad9); }
//            if (state.IsKeyDown(_OpenTK.Input.Key.KeypadDivide)) { keysDown.Add(ChamberLib.Keys.KeypadDivide); }
//            if (state.IsKeyDown(_OpenTK.Input.Key.KeypadMultiply)) { keysDown.Add(ChamberLib.Keys.KeypadMultiply); }
//            if (state.IsKeyDown(_OpenTK.Input.Key.KeypadSubtract)) { keysDown.Add(ChamberLib.Keys.KeypadSubtract); }
//            if (state.IsKeyDown(_OpenTK.Input.Key.KeypadMinus)) { keysDown.Add(ChamberLib.Keys.KeypadMinus); }
//            if (state.IsKeyDown(_OpenTK.Input.Key.KeypadAdd)) { keysDown.Add(ChamberLib.Keys.KeypadAdd); }
//            if (state.IsKeyDown(_OpenTK.Input.Key.KeypadPlus)) { keysDown.Add(ChamberLib.Keys.KeypadPlus); }
//            if (state.IsKeyDown(_OpenTK.Input.Key.KeypadDecimal)) { keysDown.Add(ChamberLib.Keys.KeypadDecimal); }
//            if (state.IsKeyDown(_OpenTK.Input.Key.KeypadPeriod)) { keysDown.Add(ChamberLib.Keys.KeypadPeriod); }
//            if (state.IsKeyDown(_OpenTK.Input.Key.KeypadEnter)) { keysDown.Add(ChamberLib.Keys.KeypadEnter); }
            if (state.IsKeyDown(_OpenTK.Input.Key.A)) { keysDown.Add(ChamberLib.Keys.A); }
            if (state.IsKeyDown(_OpenTK.Input.Key.B)) { keysDown.Add(ChamberLib.Keys.B); }
            if (state.IsKeyDown(_OpenTK.Input.Key.C)) { keysDown.Add(ChamberLib.Keys.C); }
            if (state.IsKeyDown(_OpenTK.Input.Key.D)) { keysDown.Add(ChamberLib.Keys.D); }
            if (state.IsKeyDown(_OpenTK.Input.Key.E)) { keysDown.Add(ChamberLib.Keys.E); }
            if (state.IsKeyDown(_OpenTK.Input.Key.F)) { keysDown.Add(ChamberLib.Keys.F); }
            if (state.IsKeyDown(_OpenTK.Input.Key.G)) { keysDown.Add(ChamberLib.Keys.G); }
            if (state.IsKeyDown(_OpenTK.Input.Key.H)) { keysDown.Add(ChamberLib.Keys.H); }
            if (state.IsKeyDown(_OpenTK.Input.Key.I)) { keysDown.Add(ChamberLib.Keys.I); }
            if (state.IsKeyDown(_OpenTK.Input.Key.J)) { keysDown.Add(ChamberLib.Keys.J); }
            if (state.IsKeyDown(_OpenTK.Input.Key.K)) { keysDown.Add(ChamberLib.Keys.K); }
            if (state.IsKeyDown(_OpenTK.Input.Key.L)) { keysDown.Add(ChamberLib.Keys.L); }
            if (state.IsKeyDown(_OpenTK.Input.Key.M)) { keysDown.Add(ChamberLib.Keys.M); }
            if (state.IsKeyDown(_OpenTK.Input.Key.N)) { keysDown.Add(ChamberLib.Keys.N); }
            if (state.IsKeyDown(_OpenTK.Input.Key.O)) { keysDown.Add(ChamberLib.Keys.O); }
            if (state.IsKeyDown(_OpenTK.Input.Key.P)) { keysDown.Add(ChamberLib.Keys.P); }
            if (state.IsKeyDown(_OpenTK.Input.Key.Q)) { keysDown.Add(ChamberLib.Keys.Q); }
            if (state.IsKeyDown(_OpenTK.Input.Key.R)) { keysDown.Add(ChamberLib.Keys.R); }
            if (state.IsKeyDown(_OpenTK.Input.Key.S)) { keysDown.Add(ChamberLib.Keys.S); }
            if (state.IsKeyDown(_OpenTK.Input.Key.T)) { keysDown.Add(ChamberLib.Keys.T); }
            if (state.IsKeyDown(_OpenTK.Input.Key.U)) { keysDown.Add(ChamberLib.Keys.U); }
            if (state.IsKeyDown(_OpenTK.Input.Key.V)) { keysDown.Add(ChamberLib.Keys.V); }
            if (state.IsKeyDown(_OpenTK.Input.Key.W)) { keysDown.Add(ChamberLib.Keys.W); }
            if (state.IsKeyDown(_OpenTK.Input.Key.X)) { keysDown.Add(ChamberLib.Keys.X); }
            if (state.IsKeyDown(_OpenTK.Input.Key.Y)) { keysDown.Add(ChamberLib.Keys.Y); }
            if (state.IsKeyDown(_OpenTK.Input.Key.Z)) { keysDown.Add(ChamberLib.Keys.Z); }
//            if (state.IsKeyDown(_OpenTK.Input.Key.Number0)) { keysDown.Add(ChamberLib.Keys.Number0); }
//            if (state.IsKeyDown(_OpenTK.Input.Key.Number1)) { keysDown.Add(ChamberLib.Keys.Number1); }
//            if (state.IsKeyDown(_OpenTK.Input.Key.Number2)) { keysDown.Add(ChamberLib.Keys.Number2); }
//            if (state.IsKeyDown(_OpenTK.Input.Key.Number3)) { keysDown.Add(ChamberLib.Keys.Number3); }
//            if (state.IsKeyDown(_OpenTK.Input.Key.Number4)) { keysDown.Add(ChamberLib.Keys.Number4); }
//            if (state.IsKeyDown(_OpenTK.Input.Key.Number5)) { keysDown.Add(ChamberLib.Keys.Number5); }
//            if (state.IsKeyDown(_OpenTK.Input.Key.Number6)) { keysDown.Add(ChamberLib.Keys.Number6); }
//            if (state.IsKeyDown(_OpenTK.Input.Key.Number7)) { keysDown.Add(ChamberLib.Keys.Number7); }
//            if (state.IsKeyDown(_OpenTK.Input.Key.Number8)) { keysDown.Add(ChamberLib.Keys.Number8); }
//            if (state.IsKeyDown(_OpenTK.Input.Key.Number9)) { keysDown.Add(ChamberLib.Keys.Number9); }
//            if (state.IsKeyDown(_OpenTK.Input.Key.Tilde)) { keysDown.Add(ChamberLib.Keys.Tilde); }
//            if (state.IsKeyDown(_OpenTK.Input.Key.Grave)) { keysDown.Add(ChamberLib.Keys.Grave); }
//            if (state.IsKeyDown(_OpenTK.Input.Key.Minus)) { keysDown.Add(ChamberLib.Keys.Minus); }
//            if (state.IsKeyDown(_OpenTK.Input.Key.Plus)) { keysDown.Add(ChamberLib.Keys.Plus); }
            if (state.IsKeyDown(_OpenTK.Input.Key.BracketLeft)) { keysDown.Add(ChamberLib.Keys.OemOpenBrackets); }
            if (state.IsKeyDown(_OpenTK.Input.Key.LBracket)) { keysDown.Add(ChamberLib.Keys.OemOpenBrackets); }
            if (state.IsKeyDown(_OpenTK.Input.Key.BracketRight)) { keysDown.Add(ChamberLib.Keys.OemCloseBrackets); }
            if (state.IsKeyDown(_OpenTK.Input.Key.RBracket)) { keysDown.Add(ChamberLib.Keys.OemCloseBrackets); }
            if (state.IsKeyDown(_OpenTK.Input.Key.Semicolon)) { keysDown.Add(ChamberLib.Keys.OemSemicolon); }
            if (state.IsKeyDown(_OpenTK.Input.Key.Quote)) { keysDown.Add(ChamberLib.Keys.OemQuotes); }
            if (state.IsKeyDown(_OpenTK.Input.Key.Comma)) { keysDown.Add(ChamberLib.Keys.OemComma); }
            if (state.IsKeyDown(_OpenTK.Input.Key.Period)) { keysDown.Add(ChamberLib.Keys.OemPeriod); }
//            if (state.IsKeyDown(_OpenTK.Input.Key.Slash)) { keysDown.Add(ChamberLib.Keys.Slash); }
            if (state.IsKeyDown(_OpenTK.Input.Key.BackSlash)) { keysDown.Add(ChamberLib.Keys.OemBackslash); }
//            if (state.IsKeyDown(_OpenTK.Input.Key.NonUSBackSlash)) { keysDown.Add(ChamberLib.Keys.NonUSBackSlash); }

            return new KeyboardState(keysDown.ToArray());
        }
    }
}

