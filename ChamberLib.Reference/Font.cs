using System;

namespace ChamberLib
{
    public class Font : IFont
    {
        public Font()
        {
        }

        #region IFont implementation

        public Vector2 MeasureString(string text)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

