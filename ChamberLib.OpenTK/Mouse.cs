using System;
namespace ChamberLib.OpenTK
{
    public class Mouse : IMouse
    {
        public Mouse()
        {
        }

        public MouseState GetState()
        {
            return new MouseState();
        }
    }
}
