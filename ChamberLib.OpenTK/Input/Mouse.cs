
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

using _System = global::System;
using _OpenTK = global::OpenTK;
using _OpenTKI = global::OpenTK.Input;

namespace ChamberLib.OpenTK.Input
{
    public class Mouse : IMouse
    {
        static Mouse _cache;

        public static IMouse GetMouse(
            _OpenTK.GameWindow window,
            IRenderer renderer)
        {
            if (_cache == null)
                _cache = new Mouse(window, renderer);
            return _cache;
        }

        public Mouse(_OpenTK.GameWindow window, IRenderer renderer)
        {
            Window = window;
            Renderer = renderer;
        }

        public readonly _OpenTK.GameWindow Window;
        public readonly IRenderer Renderer;

        public MouseState GetState()
        {
            var cstate = _OpenTKI.Mouse.GetCursorState();

            var clientCoords = Window.PointToClient(
                new _System.Drawing.Point(cstate.X, cstate.Y));
            var relativeCoords = Renderer.Viewport.PointToRelativeCoordinates(
                clientCoords.X, clientCoords.Y);

            return new MouseState(
                x: relativeCoords.X, y: relativeCoords.Y,
                leftButton: cstate.IsButtonDown(_OpenTKI.MouseButton.Left),
                rightButton: cstate.IsButtonDown(_OpenTKI.MouseButton.Right),
                middleButton: cstate.IsButtonDown(_OpenTKI.MouseButton.Middle),
                wheel: cstate.Wheel);
        }
    }
}
