using System;
using System.Collections.Generic;

namespace ChamberLib
{
    public interface IBone
    {
        string Name { get; set; }
        Matrix Transform { get; set; }
        int Index { get; set; }

        IBone Parent { get; set; }
        List<IBone> Children { get; }
    }
}

