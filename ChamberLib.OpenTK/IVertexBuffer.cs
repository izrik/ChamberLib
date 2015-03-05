using System;

namespace ChamberLib.OpenTK
{
    public interface IVertexBuffer : IAppliable
    {
        VertexAttribute[] VertexAttibutes { get; }
        int VertexSizeInBytes { get; }
    }
}

