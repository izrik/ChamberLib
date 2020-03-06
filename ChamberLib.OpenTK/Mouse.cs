using System;
using _OpenTK = global::OpenTK;
using _OpenTKI = global::OpenTK.Input;

namespace ChamberLib.OpenTK
{
    public class Mouse : IMouse
    {
        public MouseState GetState()
        {
            var cstate = _OpenTKI.Mouse.GetCursorState();

            return new MouseState(x: cstate.X, y: cstate.Y,
                leftButton: cstate.IsButtonDown(_OpenTKI.MouseButton.Left),
                rightButton: cstate.IsButtonDown(_OpenTKI.MouseButton.Right),
                middleButton: cstate.IsButtonDown(_OpenTKI.MouseButton.Middle),
                wheel: cstate.Wheel);
        }
    }
}
