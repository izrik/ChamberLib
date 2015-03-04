using System;
using System.IO;
using System.Linq;

namespace ChamberLib.Content
{
    public class ResolvingContentImporter : IContentImporter
    {
        public ResolvingContentImporter(IContentImporter next, string basePath=null, bool normalizePathSeparators=true)
        {
            if (next == null) throw new ArgumentNullException("next");

            this.next = next;

            if (!string.IsNullOrWhiteSpace(basePath))
                BasePath = basePath;

            NormalizePathSeparators = normalizePathSeparators;
        }

        public string BasePath = "";
        public bool NormalizePathSeparators;
        readonly IContentImporter next;

        public string ResolveFilename(string filename)
        {
            if (Path.IsPathRooted(filename))
                return filename;

            string path = filename;
            if (!string.IsNullOrWhiteSpace(BasePath))
            {
                path = Path.Combine(BasePath, filename);
            }

            if (NormalizePathSeparators)
            {
                path = path.Replace('\\', Path.DirectorySeparatorChar);
                path = path.Replace('/', Path.DirectorySeparatorChar);
            }

            return Path.GetFullPath(path);
        }

        public ModelContent ImportModel(string name, IContentImporter importer = null)
        {
            name = ResolveFilename(name);

            return next.ImportModel(name, importer);
        }

        public TextureContent ImportTexture2D(string name, IContentImporter importer = null)
        {
            name = ResolveFilename(name);

            return next.ImportTexture2D(name, importer);
        }

        public FontContent ImportFont(string name, IContentImporter importer = null)
        {
            name = ResolveFilename(name);

            return next.ImportFont(name, importer);
        }

        public SongContent ImportSong(string name, IContentImporter importer = null)
        {
            name = ResolveFilename(name);

            return next.ImportSong(name, importer);
        }

        public SoundEffectContent ImportSoundEffect(string name, IContentImporter importer = null)
        {
            name = ResolveFilename(name);

            return next.ImportSoundEffect(name, importer);
        }

        public ShaderContent ImportShader(string name, IContentImporter importer = null)
        {
            name = string.Join(",",
                name.Split(',').Select(x => ResolveFilename(x)));

            return next.ImportShader(name, importer);
        }
    }
}

