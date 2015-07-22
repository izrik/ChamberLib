using System;

namespace ChamberLib.Content
{
    public class ContentImporter : IContentImporter
    {
        public ContentImporter(
            ModelImporter modelImporter,
            TextureImporter textureImporter,
            ShaderStageImporter shaderStageImporter,
            FontImporter fontImporter,
            SongImporter songImporter,
            SoundEffectImporter soundEffectImporter)
        {
            ModelImporter = modelImporter;
            TextureImporter = textureImporter;
            ShaderStageImporter = shaderStageImporter;
            FontImporter = fontImporter;
            SongImporter = songImporter;
            SoundEffectImporter = soundEffectImporter;
        }

        public readonly ModelImporter ModelImporter;
        public readonly TextureImporter TextureImporter;
        public readonly ShaderStageImporter ShaderStageImporter;
        public readonly FontImporter FontImporter;
        public readonly SongImporter SongImporter;
        public readonly SoundEffectImporter SoundEffectImporter;

        public ModelContent ImportModel(string name, IContentImporter importer = null)
        {
            return ModelImporter(name, importer);
        }
        public TextureContent ImportTexture2D(string name, IContentImporter importer = null)
        {
            return TextureImporter(name, importer);
        }
        public FontContent ImportFont(string name, IContentImporter importer = null)
        {
            return FontImporter(name, importer);
        }
        public SongContent ImportSong(string name, IContentImporter importer = null)
        {
            return SongImporter(name, importer);
        }
        public SoundEffectContent ImportSoundEffect(string name, IContentImporter importer = null)
        {
            return SoundEffectImporter(name, importer);
        }
        public ShaderContent ImportShaderStage(string name, ShaderType type, IContentImporter importer = null)
        {
            return ShaderStageImporter(name, type, importer);
        }
    }
}

