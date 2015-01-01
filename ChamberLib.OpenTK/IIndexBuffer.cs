using System;

namespace ChamberLib
{
    public interface IIndexBuffer : IAppliable
    {
        int IndexSizeInBytes { get; }
    }
}

