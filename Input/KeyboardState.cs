
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
using System.Linq;
using System.Collections.Generic;

namespace ChamberLib
{
    public struct KeyboardState
    {
        public KeyboardState(
                bool back=false,
                bool tab=false,
                bool enter=false,
                bool capsLock=false,
                bool escape=false,
                bool space=false,
                bool pageUp=false,
                bool pageDown=false,
                bool end=false,
                bool home=false,
                bool left=false,
                bool up=false,
                bool right=false,
                bool down=false,
                bool select=false,
                bool print=false,
                bool execute=false,
                bool printScreen=false,
                bool insert=false,
                bool delete=false,
                bool help=false,
                bool d0=false,
                bool d1=false,
                bool d2=false,
                bool d3=false,
                bool d4=false,
                bool d5=false,
                bool d6=false,
                bool d7=false,
                bool d8=false,
                bool d9=false,
                bool a=false,
                bool b=false,
                bool c=false,
                bool d=false,
                bool e=false,
                bool f=false,
                bool g=false,
                bool h=false,
                bool i=false,
                bool j=false,
                bool k=false,
                bool l=false,
                bool m=false,
                bool n=false,
                bool o=false,
                bool p=false,
                bool q=false,
                bool r=false,
                bool s=false,
                bool t=false,
                bool u=false,
                bool v=false,
                bool w=false,
                bool x=false,
                bool y=false,
                bool z=false,
                bool leftWindows=false,
                bool rightWindows=false,
                bool apps=false,
                bool number0=false,
                bool number1=false,
                bool number2=false,
                bool number3=false,
                bool number4=false,
                bool number5=false,
                bool number6=false,
                bool number7=false,
                bool number8=false,
                bool number9=false,
                bool sleep=false,
                bool numPad0=false,
                bool numPad1=false,
                bool numPad2=false,
                bool numPad3=false,
                bool numPad4=false,
                bool numPad5=false,
                bool numPad6=false,
                bool numPad7=false,
                bool numPad8=false,
                bool numPad9=false,
                bool multiply=false,
                bool add=false,
                bool separator=false,
                bool subtract=false,
                bool _decimal=false,
                bool divide=false,
                bool f1=false,
                bool f2=false,
                bool f3=false,
                bool f4=false,
                bool f5=false,
                bool f6=false,
                bool f7=false,
                bool f8=false,
                bool f9=false,
                bool f10=false,
                bool f11=false,
                bool f12=false,
                bool f13=false,
                bool f14=false,
                bool f15=false,
                bool f16=false,
                bool f17=false,
                bool f18=false,
                bool f19=false,
                bool f20=false,
                bool f21=false,
                bool f22=false,
                bool f23=false,
                bool f24=false,
                bool numLock=false,
                bool scrollLock=false,
                bool leftShift=false,
                bool rightShift=false,
                bool leftControl=false,
                bool rightControl=false,
                bool leftAlt=false,
                bool rightAlt=false,
                bool browserBack=false,
                bool browserForward=false,
                bool browserRefresh=false,
                bool browserStop=false,
                bool browserSearch=false,
                bool browserFavorites=false,
                bool browserHome=false,
                bool volumeMute=false,
                bool volumeDown=false,
                bool volumeUp=false,
                bool mediaNextTrack=false,
                bool mediaPreviousTrack=false,
                bool mediaStop=false,
                bool mediaPlayPause=false,
                bool launchMail=false,
                bool selectMedia=false,
                bool launchApplication1=false,
                bool launchApplication2=false,
                bool oemSemicolon=false,
                bool oemPlus=false,
                bool oemComma=false,
                bool oemMinus=false,
                bool oemPeriod=false,
                bool oemQuestion=false,
                bool oemTilde=false,
                bool oemOpenBrackets=false,
                bool oemPipe=false,
                bool oemCloseBrackets=false,
                bool oemQuotes=false,
                bool oem8=false,
                bool oemBackslash=false,
                bool processKey=false,
                bool attn=false,
                bool crsel=false,
                bool exsel=false,
                bool eraseEof=false,
                bool play=false,
                bool zoom=false,
                bool pa1=false,
                bool oemClear=false,
                bool chatPadGreen=false,
                bool chatPadOrange=false,
                bool pause=false,
                bool imeConvert=false,
                bool imeNoConvert=false,
                bool kana=false,
                bool kanji=false,
                bool oemAuto=false,
                bool oemCopy=false,
                bool oemEnlW=false,
                bool menu=false)
        {
            Back = back;
            Tab = tab;
            Enter = enter;
            CapsLock = capsLock;
            Escape = escape;
            Space = space;
            PageUp = pageUp;
            PageDown = pageDown;
            End = end;
            Home = home;
            Left = left;
            Up = up;
            Right = right;
            Down = down;
            Select = select;
            Print = print;
            Execute = execute;
            PrintScreen = printScreen;
            Insert = insert;
            Delete = delete;
            Help = help;
            D0 = d0;
            D1 = d1;
            D2 = d2;
            D3 = d3;
            D4 = d4;
            D5 = d5;
            D6 = d6;
            D7 = d7;
            D8 = d8;
            D9 = d9;
            A = a;
            B = b;
            C = c;
            D = d;
            E = e;
            F = f;
            G = g;
            H = h;
            I = i;
            J = j;
            K = k;
            L = l;
            M = m;
            N = n;
            O = o;
            P = p;
            Q = q;
            R = r;
            S = s;
            T = t;
            U = u;
            V = v;
            W = w;
            X = x;
            Y = y;
            Z = z;
            LeftWindows = leftWindows;
            RightWindows = rightWindows;
            Apps = apps;
            Number0 = number0;
            Number1 = number1;
            Number2 = number2;
            Number3 = number3;
            Number4 = number4;
            Number5 = number5;
            Number6 = number6;
            Number7 = number7;
            Number8 = number8;
            Number9 = number9;
            Sleep = sleep;
            NumPad0 = numPad0;
            NumPad1 = numPad1;
            NumPad2 = numPad2;
            NumPad3 = numPad3;
            NumPad4 = numPad4;
            NumPad5 = numPad5;
            NumPad6 = numPad6;
            NumPad7 = numPad7;
            NumPad8 = numPad8;
            NumPad9 = numPad9;
            Multiply = multiply;
            Add = add;
            Separator = separator;
            Subtract = subtract;
            Decimal = _decimal;
            Divide = divide;
            F1 = f1;
            F2 = f2;
            F3 = f3;
            F4 = f4;
            F5 = f5;
            F6 = f6;
            F7 = f7;
            F8 = f8;
            F9 = f9;
            F10 = f10;
            F11 = f11;
            F12 = f12;
            F13 = f13;
            F14 = f14;
            F15 = f15;
            F16 = f16;
            F17 = f17;
            F18 = f18;
            F19 = f19;
            F20 = f20;
            F21 = f21;
            F22 = f22;
            F23 = f23;
            F24 = f24;
            NumLock = numLock;
            ScrollLock = scrollLock;
            LeftShift = leftShift;
            RightShift = rightShift;
            LeftControl = leftControl;
            RightControl = rightControl;
            LeftAlt = leftAlt;
            RightAlt = rightAlt;
            BrowserBack = browserBack;
            BrowserForward = browserForward;
            BrowserRefresh = browserRefresh;
            BrowserStop = browserStop;
            BrowserSearch = browserSearch;
            BrowserFavorites = browserFavorites;
            BrowserHome = browserHome;
            VolumeMute = volumeMute;
            VolumeDown = volumeDown;
            VolumeUp = volumeUp;
            MediaNextTrack = mediaNextTrack;
            MediaPreviousTrack = mediaPreviousTrack;
            MediaStop = mediaStop;
            MediaPlayPause = mediaPlayPause;
            LaunchMail = launchMail;
            SelectMedia = selectMedia;
            LaunchApplication1 = launchApplication1;
            LaunchApplication2 = launchApplication2;
            OemSemicolon = oemSemicolon;
            OemPlus = oemPlus;
            OemComma = oemComma;
            OemMinus = oemMinus;
            OemPeriod = oemPeriod;
            OemQuestion = oemQuestion;
            OemTilde = oemTilde;
            OemOpenBrackets = oemOpenBrackets;
            OemPipe = oemPipe;
            OemCloseBrackets = oemCloseBrackets;
            OemQuotes = oemQuotes;
            Oem8 = oem8;
            OemBackslash = oemBackslash;
            ProcessKey = processKey;
            Attn = attn;
            Crsel = crsel;
            Exsel = exsel;
            EraseEof = eraseEof;
            Play = play;
            Zoom = zoom;
            Pa1 = pa1;
            OemClear = oemClear;
            ChatPadGreen = chatPadGreen;
            ChatPadOrange = chatPadOrange;
            Pause = pause;
            ImeConvert = imeConvert;
            ImeNoConvert = imeNoConvert;
            Kana = kana;
            Kanji = kanji;
            OemAuto = oemAuto;
            OemCopy = oemCopy;
            OemEnlW = oemEnlW;
            Menu = menu;
        }

        public readonly bool Back;
        public readonly bool Tab;
        public readonly bool Enter;
        public readonly bool CapsLock;
        public readonly bool Escape;
        public readonly bool Space;
        public readonly bool PageUp;
        public readonly bool PageDown;
        public readonly bool End;
        public readonly bool Home;
        public readonly bool Left;
        public readonly bool Up;
        public readonly bool Right;
        public readonly bool Down;
        public readonly bool Select;
        public readonly bool Print;
        public readonly bool Execute;
        public readonly bool PrintScreen;
        public readonly bool Insert;
        public readonly bool Delete;
        public readonly bool Help;
        public readonly bool D0;
        public readonly bool D1;
        public readonly bool D2;
        public readonly bool D3;
        public readonly bool D4;
        public readonly bool D5;
        public readonly bool D6;
        public readonly bool D7;
        public readonly bool D8;
        public readonly bool D9;
        public readonly bool A;
        public readonly bool B;
        public readonly bool C;
        public readonly bool D;
        public readonly bool E;
        public readonly bool F;
        public readonly bool G;
        public readonly bool H;
        public readonly bool I;
        public readonly bool J;
        public readonly bool K;
        public readonly bool L;
        public readonly bool M;
        public readonly bool N;
        public readonly bool O;
        public readonly bool P;
        public readonly bool Q;
        public readonly bool R;
        public readonly bool S;
        public readonly bool T;
        public readonly bool U;
        public readonly bool V;
        public readonly bool W;
        public readonly bool X;
        public readonly bool Y;
        public readonly bool Z;
        public readonly bool LeftWindows;
        public readonly bool RightWindows;
        public readonly bool Apps;
        public readonly bool Number0;
        public readonly bool Number1;
        public readonly bool Number2;
        public readonly bool Number3;
        public readonly bool Number4;
        public readonly bool Number5;
        public readonly bool Number6;
        public readonly bool Number7;
        public readonly bool Number8;
        public readonly bool Number9;
        public readonly bool Sleep;
        public readonly bool NumPad0;
        public readonly bool NumPad1;
        public readonly bool NumPad2;
        public readonly bool NumPad3;
        public readonly bool NumPad4;
        public readonly bool NumPad5;
        public readonly bool NumPad6;
        public readonly bool NumPad7;
        public readonly bool NumPad8;
        public readonly bool NumPad9;
        public readonly bool Multiply;
        public readonly bool Add;
        public readonly bool Separator;
        public readonly bool Subtract;
        public readonly bool Decimal;
        public readonly bool Divide;
        public readonly bool F1;
        public readonly bool F2;
        public readonly bool F3;
        public readonly bool F4;
        public readonly bool F5;
        public readonly bool F6;
        public readonly bool F7;
        public readonly bool F8;
        public readonly bool F9;
        public readonly bool F10;
        public readonly bool F11;
        public readonly bool F12;
        public readonly bool F13;
        public readonly bool F14;
        public readonly bool F15;
        public readonly bool F16;
        public readonly bool F17;
        public readonly bool F18;
        public readonly bool F19;
        public readonly bool F20;
        public readonly bool F21;
        public readonly bool F22;
        public readonly bool F23;
        public readonly bool F24;
        public readonly bool NumLock;
        public readonly bool ScrollLock;
        public readonly bool LeftShift;
        public readonly bool RightShift;
        public readonly bool LeftControl;
        public readonly bool RightControl;
        public readonly bool LeftAlt;
        public readonly bool RightAlt;
        public readonly bool BrowserBack;
        public readonly bool BrowserForward;
        public readonly bool BrowserRefresh;
        public readonly bool BrowserStop;
        public readonly bool BrowserSearch;
        public readonly bool BrowserFavorites;
        public readonly bool BrowserHome;
        public readonly bool VolumeMute;
        public readonly bool VolumeDown;
        public readonly bool VolumeUp;
        public readonly bool MediaNextTrack;
        public readonly bool MediaPreviousTrack;
        public readonly bool MediaStop;
        public readonly bool MediaPlayPause;
        public readonly bool LaunchMail;
        public readonly bool SelectMedia;
        public readonly bool LaunchApplication1;
        public readonly bool LaunchApplication2;
        public readonly bool OemSemicolon;
        public readonly bool OemPlus;
        public readonly bool OemComma;
        public readonly bool OemMinus;
        public readonly bool OemPeriod;
        public readonly bool OemQuestion;
        public readonly bool OemTilde;
        public readonly bool OemOpenBrackets;
        public readonly bool OemPipe;
        public readonly bool OemCloseBrackets;
        public readonly bool OemQuotes;
        public readonly bool Oem8;
        public readonly bool OemBackslash;
        public readonly bool ProcessKey;
        public readonly bool Attn;
        public readonly bool Crsel;
        public readonly bool Exsel;
        public readonly bool EraseEof;
        public readonly bool Play;
        public readonly bool Zoom;
        public readonly bool Pa1;
        public readonly bool OemClear;
        public readonly bool ChatPadGreen;
        public readonly bool ChatPadOrange;
        public readonly bool Pause;
        public readonly bool ImeConvert;
        public readonly bool ImeNoConvert;
        public readonly bool Kana;
        public readonly bool Kanji;
        public readonly bool OemAuto;
        public readonly bool OemCopy;
        public readonly bool OemEnlW;
        public readonly bool Menu;

        public bool IsKeyDown(Keys key)
        {
            switch (key)
            {
                case Keys.Back: return Back;
                case Keys.Tab: return Tab;
                case Keys.Enter: return Enter;
                case Keys.CapsLock: return CapsLock;
                case Keys.Escape: return Escape;
                case Keys.Space: return Space;
                case Keys.PageUp: return PageUp;
                case Keys.PageDown: return PageDown;
                case Keys.End: return End;
                case Keys.Home: return Home;
                case Keys.Left: return Left;
                case Keys.Up: return Up;
                case Keys.Right: return Right;
                case Keys.Down: return Down;
                case Keys.Select: return Select;
                case Keys.Print: return Print;
                case Keys.Execute: return Execute;
                case Keys.PrintScreen: return PrintScreen;
                case Keys.Insert: return Insert;
                case Keys.Delete: return Delete;
                case Keys.Help: return Help;
                case Keys.D0: return D0;
                case Keys.D1: return D1;
                case Keys.D2: return D2;
                case Keys.D3: return D3;
                case Keys.D4: return D4;
                case Keys.D5: return D5;
                case Keys.D6: return D6;
                case Keys.D7: return D7;
                case Keys.D8: return D8;
                case Keys.D9: return D9;
                case Keys.A: return A;
                case Keys.B: return B;
                case Keys.C: return C;
                case Keys.D: return D;
                case Keys.E: return E;
                case Keys.F: return F;
                case Keys.G: return G;
                case Keys.H: return H;
                case Keys.I: return I;
                case Keys.J: return J;
                case Keys.K: return K;
                case Keys.L: return L;
                case Keys.M: return M;
                case Keys.N: return N;
                case Keys.O: return O;
                case Keys.P: return P;
                case Keys.Q: return Q;
                case Keys.R: return R;
                case Keys.S: return S;
                case Keys.T: return T;
                case Keys.U: return U;
                case Keys.V: return V;
                case Keys.W: return W;
                case Keys.X: return X;
                case Keys.Y: return Y;
                case Keys.Z: return Z;
                case Keys.LeftWindows: return LeftWindows;
                case Keys.RightWindows: return RightWindows;
                case Keys.Apps: return Apps;
                case Keys.Number0: return Number0;
                case Keys.Number1: return Number1;
                case Keys.Number2: return Number2;
                case Keys.Number3: return Number3;
                case Keys.Number4: return Number4;
                case Keys.Number5: return Number5;
                case Keys.Number6: return Number6;
                case Keys.Number7: return Number7;
                case Keys.Number8: return Number8;
                case Keys.Number9: return Number9;
                case Keys.Sleep: return Sleep;
                case Keys.NumPad0: return NumPad0;
                case Keys.NumPad1: return NumPad1;
                case Keys.NumPad2: return NumPad2;
                case Keys.NumPad3: return NumPad3;
                case Keys.NumPad4: return NumPad4;
                case Keys.NumPad5: return NumPad5;
                case Keys.NumPad6: return NumPad6;
                case Keys.NumPad7: return NumPad7;
                case Keys.NumPad8: return NumPad8;
                case Keys.NumPad9: return NumPad9;
                case Keys.Multiply: return Multiply;
                case Keys.Add: return Add;
                case Keys.Separator: return Separator;
                case Keys.Subtract: return Subtract;
                case Keys.Decimal: return Decimal;
                case Keys.Divide: return Divide;
                case Keys.F1: return F1;
                case Keys.F2: return F2;
                case Keys.F3: return F3;
                case Keys.F4: return F4;
                case Keys.F5: return F5;
                case Keys.F6: return F6;
                case Keys.F7: return F7;
                case Keys.F8: return F8;
                case Keys.F9: return F9;
                case Keys.F10: return F10;
                case Keys.F11: return F11;
                case Keys.F12: return F12;
                case Keys.F13: return F13;
                case Keys.F14: return F14;
                case Keys.F15: return F15;
                case Keys.F16: return F16;
                case Keys.F17: return F17;
                case Keys.F18: return F18;
                case Keys.F19: return F19;
                case Keys.F20: return F20;
                case Keys.F21: return F21;
                case Keys.F22: return F22;
                case Keys.F23: return F23;
                case Keys.F24: return F24;
                case Keys.NumLock: return NumLock;
                case Keys.ScrollLock: return ScrollLock;
                case Keys.LeftShift: return LeftShift;
                case Keys.RightShift: return RightShift;
                case Keys.LeftControl: return LeftControl;
                case Keys.RightControl: return RightControl;
                case Keys.LeftAlt: return LeftAlt;
                case Keys.RightAlt: return RightAlt;
                case Keys.BrowserBack: return BrowserBack;
                case Keys.BrowserForward: return BrowserForward;
                case Keys.BrowserRefresh: return BrowserRefresh;
                case Keys.BrowserStop: return BrowserStop;
                case Keys.BrowserSearch: return BrowserSearch;
                case Keys.BrowserFavorites: return BrowserFavorites;
                case Keys.BrowserHome: return BrowserHome;
                case Keys.VolumeMute: return VolumeMute;
                case Keys.VolumeDown: return VolumeDown;
                case Keys.VolumeUp: return VolumeUp;
                case Keys.MediaNextTrack: return MediaNextTrack;
                case Keys.MediaPreviousTrack: return MediaPreviousTrack;
                case Keys.MediaStop: return MediaStop;
                case Keys.MediaPlayPause: return MediaPlayPause;
                case Keys.LaunchMail: return LaunchMail;
                case Keys.SelectMedia: return SelectMedia;
                case Keys.LaunchApplication1: return LaunchApplication1;
                case Keys.LaunchApplication2: return LaunchApplication2;
                case Keys.OemSemicolon: return OemSemicolon;
                case Keys.OemPlus: return OemPlus;
                case Keys.OemComma: return OemComma;
                case Keys.OemMinus: return OemMinus;
                case Keys.OemPeriod: return OemPeriod;
                case Keys.OemQuestion: return OemQuestion;
                case Keys.OemTilde: return OemTilde;
                case Keys.OemOpenBrackets: return OemOpenBrackets;
                case Keys.OemPipe: return OemPipe;
                case Keys.OemCloseBrackets: return OemCloseBrackets;
                case Keys.OemQuotes: return OemQuotes;
                case Keys.Oem8: return Oem8;
                case Keys.OemBackslash: return OemBackslash;
                case Keys.ProcessKey: return ProcessKey;
                case Keys.Attn: return Attn;
                case Keys.Crsel: return Crsel;
                case Keys.Exsel: return Exsel;
                case Keys.EraseEof: return EraseEof;
                case Keys.Play: return Play;
                case Keys.Zoom: return Zoom;
                case Keys.Pa1: return Pa1;
                case Keys.OemClear: return OemClear;
                case Keys.ChatPadGreen: return ChatPadGreen;
                case Keys.ChatPadOrange: return ChatPadOrange;
                case Keys.Pause: return Pause;
                case Keys.ImeConvert: return ImeConvert;
                case Keys.ImeNoConvert: return ImeNoConvert;
                case Keys.Kana: return Kana;
                case Keys.Kanji: return Kanji;
                case Keys.OemAuto: return OemAuto;
                case Keys.OemCopy: return OemCopy;
                case Keys.OemEnlW: return OemEnlW;
                case Keys.Menu: return Menu;
            }

            return false;
        }

        public bool IsKeyUp(Keys key)
        {
            return !IsKeyDown(key);
        }
    }
}

