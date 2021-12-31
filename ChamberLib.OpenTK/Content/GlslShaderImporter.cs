using System;
using ChamberLib.Content;
using System.IO;

namespace ChamberLib.OpenTK.Content
{
    public class GlslShaderImporter
    {
        public GlslShaderImporter(ShaderStageImporter next=null)
        {
            this.next = next;
        }

        readonly ShaderStageImporter next;

        public ShaderContent ImportShaderStage(string filename, ShaderType type, IContentImporter importer)
        {
            if (File.Exists(filename))
            {
            }
            else if (type == ShaderType.Vertex && File.Exists(filename + ".vert"))
            {
                filename += ".vert";
            }
            else if (type == ShaderType.Fragment && File.Exists(filename + ".frag"))
            {
                filename += ".frag";
            }
            else if (next != null)
            {
                return next(filename, type, importer);
            }
            else
            {
                throw new FileNotFoundException(
                    string.Format(
                        "The {0} shader file could not be found: {1}",
                        type,
                        filename),
                    filename);
            }

            var source = File.ReadAllText(filename);

            return new ShaderContent(
                source: source,
                name: filename,
                type: type);
        }
    }
}

