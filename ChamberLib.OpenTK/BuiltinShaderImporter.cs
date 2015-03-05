using System;
using ChamberLib.Content;

namespace ChamberLib.OpenTK
{
    public class BuiltinShaderImporter
    {
        public BuiltinShaderImporter(ShaderImporter next)
        {
            if (next == null) throw new ArgumentNullException("next");

            this.next = next;
        }

        readonly ShaderImporter next;

        public ShaderContent ImportShader(string filename, IContentImporter importer)
        {
            if (filename == "$basic")
            {
                return BuiltinShaders.BasicShaderContent;
            }

            if (filename == "$skinned")
            {
                return BuiltinShaders.SkinnedShaderContent;
            }

            return next(filename, importer);
        }
    }
}

