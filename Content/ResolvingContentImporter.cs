using System;
using System.IO;
using System.Linq;

namespace ChamberLib.Content
{
    public class ResolvingContentImporter : IContentImporter
    {
        public ResolvingContentImporter(IContentImporter next, string basePath=null)
        {
            if (next == null) throw new ArgumentNullException("next");

            this.next = next;

            if (!string.IsNullOrWhiteSpace(basePath))
                BasePath = basePath;
        }

        public string BasePath = "";
        readonly IContentImporter next;

        public ModelContent ImportModel(string name, IContentImporter importer = null)
        {
            if (!string.IsNullOrEmpty(BasePath))
                name = Path.Combine(BasePath, name);

            return next.ImportModel(name, importer);
        }

        public TextureContent ImportTexture2D(string name, IContentImporter importer = null)
        {
            if (!string.IsNullOrEmpty(BasePath))
                name = Path.Combine(BasePath, name);

            return next.ImportTexture2D(name, importer);
        }

        public FontContent ImportFont(string name, IContentImporter importer = null)
        {
            if (!string.IsNullOrEmpty(BasePath))
                name = Path.Combine(BasePath, name);

            return next.ImportFont(name, importer);
        }

        public SongContent ImportSong(string name, IContentImporter importer = null)
        {
            if (!string.IsNullOrEmpty(BasePath))
                name = Path.Combine(BasePath, name);

            return next.ImportSong(name, importer);
        }

        public SoundEffectContent ImportSoundEffect(string name, IContentImporter importer = null)
        {
            if (!string.IsNullOrEmpty(BasePath))
                name = Path.Combine(BasePath, name);

            return next.ImportSoundEffect(name, importer);
        }

        public ShaderContent ImportShader(string name, IContentImporter importer = null)
        {
            if (!string.IsNullOrEmpty(BasePath))
            {
                name = string.Join(",",
                    name.Split(',').Select(x => Path.Combine(BasePath, x)));
            }

            return next.ImportShader(name, importer);
        }
    }
}

