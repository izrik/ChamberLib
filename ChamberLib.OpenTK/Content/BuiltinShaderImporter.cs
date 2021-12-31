using System;
using ChamberLib.Content;
using ChamberLib.OpenTK.Materials;

namespace ChamberLib.OpenTK.Content
{
    public class BuiltinShaderImporter
    {
        public BuiltinShaderImporter(ShaderStageImporter next)
        {
            if (next == null) throw new ArgumentNullException("next2");

            this.next = next;
        }

        readonly ShaderStageImporter next;

        public ShaderContent ImportShaderStage(string filename, ShaderType type, IContentImporter importer)
        {
            if (filename == "$basic")
            {
                if (type == ShaderType.Vertex)
                    return BuiltinShaders.BasicVertexShaderContent;
                else
                    return BuiltinShaders.BasicFragmentShaderContent;
            }

            if (filename == "$skinned")
            {
                if (type != ShaderType.Vertex)
                    throw new ArgumentOutOfRangeException(
                        "type",
                        "Wrong shader type for built-in shader \"$skinned\"");

                return BuiltinShaders.SkinnedVertexShaderContent;
            }

            return next(filename, type, importer);
        }
    }
}

