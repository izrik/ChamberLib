using System;
using System.Collections.Generic;

namespace ChamberLib.Content
{
    public class ShaderContent
    {
        public ShaderContent(string vs, string fs, string name)
        {
            this.VertexShaderSource = vs;
            this.FragmentShaderSource = fs;
            this.Name = name ?? "";
        }

        public string VertexShaderSource;
        public string FragmentShaderSource;
        public string Name;
    }
}

