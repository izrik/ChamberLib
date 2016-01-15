using System;

namespace ChamberLib
{
    public class GamePad : IGamePad
    {
        public GamePad()
        {
        }

        #region IGamePad implementation

        public GamePadState GetState()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

