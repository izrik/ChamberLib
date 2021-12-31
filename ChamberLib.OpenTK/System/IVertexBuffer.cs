using System;

namespace ChamberLib.OpenTK.System
{
    public interface IVertexBuffer : IAppliable
    {
        VertexAttribute[] VertexAttibutes { get; }
        int VertexSizeInBytes { get; }
    }
}

