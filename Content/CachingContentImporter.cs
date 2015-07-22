using System;

namespace ChamberLib.Content
{
    public class CachingContentImporter : IContentImporter
    {
        public CachingContentImporter(IContentImporter next)
        {
            if (next == null) throw new ArgumentNullException("next");

            models = new Cache<string, IContentImporter, ModelContent>(next.ImportModel);
            textures = new Cache<string, IContentImporter, TextureContent>(next.ImportTexture2D);
            shaders = new Cache<string, IContentImporter, ShaderContent>(next.ImportShader);
            shaderStages = new Cache2<string, ShaderType, IContentImporter, ShaderContent>(next.ImportShaderStage);
            fonts = new Cache<string, IContentImporter, FontContent>(next.ImportFont);
            songs = new Cache<string, IContentImporter, SongContent>(next.ImportSong);
            soundEffects = new Cache<string, IContentImporter, SoundEffectContent>(next.ImportSoundEffect);
        }

        readonly Cache<string, IContentImporter, ModelContent> models;
        readonly Cache<string, IContentImporter, TextureContent> textures;
        readonly Cache<string, IContentImporter, ShaderContent> shaders;
        readonly Cache2<string, ShaderType, IContentImporter, ShaderContent> shaderStages;
        readonly Cache<string, IContentImporter, FontContent> fonts;
        readonly Cache<string, IContentImporter, SongContent> songs;
        readonly Cache<string, IContentImporter, SoundEffectContent> soundEffects;

        public ModelContent ImportModel(string name, IContentImporter importer = null)
        {
            return models.Call(name, importer);
        }

        public TextureContent ImportTexture2D(string name, IContentImporter importer = null)
        {
            return textures.Call(name, importer);
        }

        public ShaderContent ImportShader(string name, IContentImporter importer = null)
        {
            return shaders.Call(name, importer);
        }

        public ShaderContent ImportShaderStage(string name, ShaderType type, IContentImporter importer = null)
        {
            return shaderStages.Call(name, type, importer);
        }

        public FontContent ImportFont(string name, IContentImporter importer = null)
        {
            return fonts.Call(name, importer);
        }

        public SongContent ImportSong(string name, IContentImporter importer = null)
        {
            return songs.Call(name, importer);
        }

        public SoundEffectContent ImportSoundEffect(string name, IContentImporter importer = null)
        {
            return soundEffects.Call(name, importer);
        }
    }
}

