using System;

namespace ChamberLib.Content
{
    public class ContentProcessor : IContentProcessor
    {
        public ContentProcessor(
            ModelProcessor modelProcessor,
            TextureProcessor textureProcessor,
            ShaderProcessor shaderProcessor,
            FontProcessor fontProcessor,
            SongProcessor songProcessor,
            SoundEffectProcessor soundEffectProcessor)
        {
            ModelProcessor = modelProcessor;
            TextureProcessor = textureProcessor;
            ShaderProcessor = shaderProcessor;
            FontProcessor = fontProcessor;
            SongProcessor = songProcessor;
            SoundEffectProcessor = soundEffectProcessor;
        }

        public readonly ModelProcessor ModelProcessor;
        public readonly TextureProcessor TextureProcessor;
        public readonly ShaderProcessor ShaderProcessor;
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
        public IShader ProcessShader(ShaderContent asset, IContentProcessor importer = null, object bindattrs = null)
        {
            return ShaderProcessor(asset, importer);
        }
    }
}

