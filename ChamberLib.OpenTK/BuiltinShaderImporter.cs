using System;
using ChamberLib.Content;

namespace ChamberLib.OpenTK
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
                if (type != ShaderType.Vertex)
                    throw new ArgumentOutOfRangeException(
                        "type",
                        "Wrong shader type for built-in shader \"$basic\"");

                return BuiltinShaders.BasicVertexShaderContent;
            }

            if (filename == "$skinned")
            {
                if (type != ShaderType.Vertex)
                    throw new ArgumentOutOfRangeException(
                        "type",
                        "Wrong shader type for built-in shader \"$skinned\"");

                return BuiltinShaders.SkinnedVertexShaderContent;
            }

            if (filename == "$fragment")
            {
                if (type != ShaderType.Fragment)
                    throw new ArgumentOutOfRangeException(
                        "type",
                        "Wrong shader type for built-in shader \"$fragment\"");

                return BuiltinShaders.BuiltinFragmentShaderContent;
            }

            return next(filename, type, importer);
        }
    }
}

