using System;
using ChamberLib.Content;

namespace ChamberLib.OpenTK
{
    public class BuiltinShaderImporter
    {
        public BuiltinShaderImporter(ShaderImporter next, ShaderStageImporter next2)
        {
            if (next == null) throw new ArgumentNullException("next");
            if (next2 == null) throw new ArgumentNullException("next2");

            this.next = next;
            this.next2 = next2;
        }

        readonly ShaderImporter next;
        readonly ShaderStageImporter next2;

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

        public ShaderContent ImportShaderStage(string filename, ShaderType type, IContentImporter importer)
        {

            if (filename == "$basic")
            {
                throw new NotImplementedException("ImportShaderStage $basic");
            }

            if (filename == "$skinned")
            {
                throw new NotImplementedException("ImportShaderStage $skinned");
            }

            return next2(filename, type, importer);
        }
    }
}

