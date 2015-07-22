using System;

namespace ChamberLib
{
    public interface IShaderStage
    {
        string Name { get; }
        string Source { get; }
        ShaderType ShaderType { get; }
    }
}

