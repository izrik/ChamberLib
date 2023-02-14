using System;
using _OpenTK = global::OpenTK;
using _OpenTKI = global::OpenTK.Input;

namespace ChamberLib.OpenTK
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
                new System.Drawing.Point(cstate.X, cstate.Y));
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
