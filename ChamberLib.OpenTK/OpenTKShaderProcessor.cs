using System;
using ChamberLib.Content;

namespace ChamberLib
{
    public class OpenTKShaderProcessor
    {
        public ShaderAdapter ProcessShader(ShaderContent shaderContent, IContentProcessor processor, string[] bindattrs=null)
        {
            var shader = new ShaderAdapter(shaderContent, bindattrs);
            return shader;
        }
    }
}

