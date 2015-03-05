using System;

namespace ChamberLib.OpenTK
{
    public interface IIndexBuffer : IAppliable
    {
        int IndexSizeInBytes { get; }
    }
}

