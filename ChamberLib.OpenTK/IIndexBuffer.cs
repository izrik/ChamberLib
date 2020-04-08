using System;
using OpenTK.Graphics.OpenGL;

namespace ChamberLib.OpenTK
{
    public interface IIndexBuffer : IAppliable
    {
        int IndexSizeInBytes { get; }
        DrawElementsType DrawElementsType { get; }
    }
}

