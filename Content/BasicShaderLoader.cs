using System;
using System.IO;

namespace ChamberLib.Content
{
    public static class BasicShaderLoader
    {
        public static ShaderContent LoadShader(string vertexShaderFilename, string fragmentShaderFilename, string[] bindattrs)
        {
            var vertexShaderSource = File.ReadAllText(vertexShaderFilename);
            var fragmentShaderSource = File.ReadAllText(fragmentShaderFilename);

            var shaderContent =
                new ShaderContent(
                    vs: vertexShaderSource,
                    fs: fragmentShaderSource,
                    bindAttributes: bindattrs);

            return shaderContent;
        }
    }
}

