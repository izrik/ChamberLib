﻿using System;

namespace ChamberLib
{
    public interface IShaderStage
    {
        string Source { get; }
        ShaderType ShaderType { get; }
    }
}

