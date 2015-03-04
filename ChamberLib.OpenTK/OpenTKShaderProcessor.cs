using System;
using ChamberLib.Content;
using System.Collections.Generic;
using System.Linq;

namespace ChamberLib
{
    public class OpenTKShaderProcessor
    {
        public ShaderAdapter ProcessShader(ShaderContent shaderContent, IContentProcessor processor, object bindattrs=null)
        {
            string[] bindattrs2=null;
            if (bindattrs == null)
            {
            }
            else if (bindattrs is IEnumerable<string>)
            {
                bindattrs2 = (bindattrs as IEnumerable<string>).ToArray();
            }
            else
            {
                throw new InvalidOperationException();
            }

            var shader = new ShaderAdapter(shaderContent, (String[])bindattrs2);
            return shader;
        }
    }
}

