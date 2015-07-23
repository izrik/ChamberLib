using System;

namespace ChamberLib.Content
{
    public class ContentProcessor : IContentProcessor
    {
        public ContentProcessor(
            ModelProcessor modelProcessor,
            TextureProcessor textureProcessor,
            ShaderStageProcessor shaderStageProcessor,
            FontProcessor fontProcessor,
            SongProcessor songProcessor,
            SoundEffectProcessor soundEffectProcessor)
        {
            ModelProcessor = modelProcessor;
            TextureProcessor = textureProcessor;
            ShaderStageProcessor = shaderStageProcessor;
            FontProcessor = fontProcessor;
            SongProcessor = songProcessor;
            SoundEffectProcessor = soundEffectProcessor;
        }

        public readonly ModelProcessor ModelProcessor;
        public readonly TextureProcessor TextureProcessor;
        public readonly ShaderStageProcessor ShaderStageProcessor;
        public readonly FontProcessor FontProcessor;
        public readonly SongProcessor SongProcessor;
        public readonly SoundEffectProcessor SoundEffectProcessor;

        public IModel ProcessModel(ModelContent asset, IContentProcessor importer = null)
        {
            return ModelProcessor(asset, importer);
        }
        public ITexture2D ProcessTexture2D(TextureContent asset, IContentProcessor importer = null)
        {
            return TextureProcessor(asset, importer);
        }
        public IFont ProcessFont(FontContent asset, IContentProcessor importer = null)
        {
            return FontProcessor(asset, importer);
        }
        public ISong ProcessSong(SongContent asset, IContentProcessor importer = null)
        {
            return SongProcessor(asset, importer);
        }
        public ISoundEffect ProcessSoundEffect(SoundEffectContent asset, IContentProcessor importer = null)
        {
            return SoundEffectProcessor(asset, importer);
        }
        public IShaderStage ProcessShaderStage(ShaderContent asset, IContentProcessor processor = null)
        {
            return ShaderStageProcessor(asset, processor);
        }

        public IShaderProgram MakeShaderProgram(IShaderStage vertexShader,
            IShaderStage fragmentShader)
        {
            throw new NotImplementedException();
        }
    }
}

