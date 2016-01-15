using System;

namespace ChamberLib
{
    public class Keyboard : IKeyboard
    {
        public Keyboard()
        {
        }

        #region IKeyboard implementation

        public KeyboardState GetState()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

