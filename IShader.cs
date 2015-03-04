using System;

namespace ChamberLib
{
    public interface IShader
    {
        string Name { get; }

        void Apply();
        void UnApply();
    }
}

