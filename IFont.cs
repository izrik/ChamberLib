using System;

namespace ChamberLib
{
    public interface IFont
    {
        Vector2 MeasureString(string text,
            float? wrapWordsToMaxLineWidth=null);
    }
}

