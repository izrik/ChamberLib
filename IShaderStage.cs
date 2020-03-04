using System;
using System.Collections.Generic;

namespace ChamberLib
{
    public interface IShaderStage
    {
        string Name { get; }
        string Source { get; }
        ShaderType ShaderType { get; }

        IEnumerable<string> BindAttributes { get; }
        void SetBindAttributes(IEnumerable<string> bindattrs);
    }
}

