using System;

namespace ChamberLib
{
    public interface IVertexBuffer : IAppliable
    {
        VertexAttribute[] VertexAttibutes { get; }
        int VertexSizeInBytes { get; }
    }
}

