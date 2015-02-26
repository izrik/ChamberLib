using System;
using System.Collections.Generic;

namespace ChamberLib
{
    public class ShaderContent
    {
        public ShaderContent(string vs, string fs, string[] bindAttributes=null)
        {
            if (string.IsNullOrEmpty(vs)) throw new ArgumentNullException("vs");
            if (string.IsNullOrEmpty(fs)) throw new ArgumentNullException("fs");

            this.VertexShaderSource = vs;
            this.FragmentShaderSource = fs;
            if (bindAttributes != null)
            {
                this.BindAttributes.AddRange(bindAttributes);
            }
        }

        public string VertexShaderSource;
        public string FragmentShaderSource;
        public List<string> BindAttributes = new List<string>();

    }
}

