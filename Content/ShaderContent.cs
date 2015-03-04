using System;
using System.Collections.Generic;

namespace ChamberLib.Content
{
    public class ShaderContent
    {
        public ShaderContent(string vs, string fs, string name)
        {
            if (string.IsNullOrEmpty(vs)) throw new ArgumentNullException("vs");
            if (string.IsNullOrEmpty(fs)) throw new ArgumentNullException("fs");

            this.VertexShaderSource = vs;
            this.FragmentShaderSource = fs;
            this.Name = name ?? "";
        }

        public string VertexShaderSource;
        public string FragmentShaderSource;
        public string Name;
    }
}

