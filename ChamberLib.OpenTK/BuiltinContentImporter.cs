using System;
using ChamberLib.Content;

namespace ChamberLib.OpenTK
{
    public class BuiltinContentImporter : IContentImporter
    {
        public BuiltinContentImporter(IContentImporter next)
        {
            if (next == null) throw new ArgumentNullException("next");

            this.next = next;
            this.shaderImporter = new BuiltinShaderImporter(next.ImportShader,
                                                            next.ImportShaderStage);
            this.fontImporter = new BuiltinFontImporter(next.ImportFont);
        }

        readonly IContentImporter next;
        readonly BuiltinShaderImporter shaderImporter; 
        readonly BuiltinFontImporter fontImporter;

        public ModelContent ImportModel(string name, IContentImporter importer = null)
        {
            return next.ImportModel(name, importer);
        }
        public TextureContent ImportTexture2D(string name, IContentImporter importer = null)
        {
            return next.ImportTexture2D(name, importer);
        }
        public FontContent ImportFont(string name, IContentImporter importer = null)
        {
            return fontImporter.ImportFont(name, importer);
        }
        public SongContent ImportSong(string name, IContentImporter importer = null)
        {
            return next.ImportSong(name, importer);
        }
        public SoundEffectContent ImportSoundEffect(string name, IContentImporter importer = null)
        {
            return next.ImportSoundEffect(name, importer);
        }
        public ShaderContent ImportShader(string name, IContentImporter importer = null)
        {
            return shaderImporter.ImportShader(name, importer);
        }
        public ShaderContent ImportShaderStage(string name, ShaderType type, IContentImporter importer = null)
        {
            return shaderImporter.ImportShaderStage(name, type, importer);
        }
    }
}

