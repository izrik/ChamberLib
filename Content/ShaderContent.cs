using System;
using System.Collections.Generic;

namespace ChamberLib.Content
{
    public class ShaderContent
    {
        public ShaderContent(string source, string name, ShaderType type)
        {
            this.Source = source;
            this.Name = name ?? "";
            this.Type = type;
        }

        public readonly string Source;
        public readonly string Name;
        public readonly ShaderType Type;
    }
}

