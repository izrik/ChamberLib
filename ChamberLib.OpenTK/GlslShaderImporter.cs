using System;
using ChamberLib.Content;
using System.IO;

namespace ChamberLib.OpenTK
{
    public class GlslShaderImporter
    {
        public GlslShaderImporter(ShaderImporter next=null)
        {
            this.next = next;
        }

        readonly ShaderImporter next;

        public ShaderContent ImportShader(string filename, IContentImporter importer)
        {
            var parts = filename.Split(',');
            string vert;
            string frag;

            if (parts.Length > 1)
            {
                vert = parts[0];
                frag = parts[1];
            }
            else
            {
                vert = parts[0];
                frag = parts[0];
            }

            if (File.Exists(vert))
            {
            }
            else if (File.Exists(vert + ".vert"))
            {
                vert = vert + ".vert";
            }
            else if (next != null)
            {
                next(filename, importer);
            }
            else
            {
                throw new FileNotFoundException("The vertex shader file could not be found", vert);
            }

            if (File.Exists(frag))
            {
            }
            else if (File.Exists(frag + ".frag"))
            {
                frag = frag + ".frag";
            }
            else if (next != null)
            {
                next(filename, importer);
            }
            else
            {
                throw new FileNotFoundException("The fragment shader file could not be found", frag);
            }

            var vertexShaderSource = File.ReadAllText(vert);
            var fragmentShaderSource = File.ReadAllText(frag);

            return new ShaderContent(
                    vs: vertexShaderSource,
                    fs: fragmentShaderSource,
                    name: filename,
                    type: ShaderType.Vertex);
        }

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
                vs: (type == ShaderType.Vertex ? source : null),
                fs: (type == ShaderType.Fragment ? source : null),
                name: filename,
                type: ShaderType.Vertex);
        }
    }
}

