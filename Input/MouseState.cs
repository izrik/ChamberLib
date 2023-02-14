
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
    public struct MouseState
    {
        public MouseState(float x, float y, bool leftButton, bool middleButton,
            bool rightButton, int wheel)
        {
            X = x;
            Y = y;
            LeftButton = leftButton;
            MiddleButton = middleButton;
            RightButton = rightButton;
            Wheel = wheel;
        }

        public readonly float X;
        public readonly float Y;
        public readonly bool LeftButton;
        public readonly bool MiddleButton;
        public readonly bool RightButton;
        public readonly int Wheel;

        public Vector2 Position { get { return new Vector2(X, Y); } }

        public bool IsButtonDown(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.Left: return LeftButton;
                case MouseButtons.Middle: return MiddleButton;
                case MouseButtons.Right: return RightButton;
            }
            return false;
        }

        public bool IsButtonUp(MouseButtons button)
        {
            return !(IsButtonDown(button));
        }
    }
}
