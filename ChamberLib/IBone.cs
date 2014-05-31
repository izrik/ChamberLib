using System;

namespace ChamberLib
{
    public interface IBone
    {
        Matrix Transform { get; set; }
        int Index { get; set; }
    }
}

