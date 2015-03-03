using System;
using System.Collections.Generic;

namespace ChamberLib.Content
{
    public class CachingContentProcessor : IContentProcessor
    {
        public CachingContentProcessor(IContentProcessor next)
        {
            if (next == null) throw new ArgumentNullException("next");

            this.next = next;

            models = new Cache<ModelContent, IContentProcessor, IModel>(next.ProcessModel);
            textures = new Cache<TextureContent, IContentProcessor, ITexture2D>(next.ProcessTexture2D);
            //shaders = new Cache<ShaderContent, IContentProcessor, IShader>(next.ProcessShader);
            fonts = new Cache<FontContent, IContentProcessor, IFont>(next.ProcessFont);
            songs = new Cache<SongContent, IContentProcessor, ISong>(next.ProcessSong);
            soundEffects = new Cache<SoundEffectContent, IContentProcessor, ISoundEffect>(next.ProcessSoundEffect);
        }

        readonly IContentProcessor next;

        readonly Cache<ModelContent, IContentProcessor, IModel> models;
        readonly Cache<TextureContent, IContentProcessor, ITexture2D> textures;
        //readonly Cache<ShaderContent, IContentProcessor, IShader> shaders;
        readonly Cache<FontContent, IContentProcessor, IFont> fonts;
        readonly Cache<SongContent, IContentProcessor, ISong> songs;
        readonly Cache<SoundEffectContent, IContentProcessor, ISoundEffect> soundEffects;

        readonly Dictionary<ShaderContent, IShader> shaderCache = new Dictionary<ShaderContent, IShader>();

        #region IContentProcessor implementation

        public IModel ProcessModel(ModelContent asset, IContentProcessor processor = null)
        {
            return models.Call(asset, processor);
        }

        public ITexture2D ProcessTexture2D(TextureContent asset, IContentProcessor processor = null)
        {
            return textures.Call(asset, processor);
        }

        public IShader ProcessShader(ShaderContent asset, IContentProcessor processor = null, object bindattrs = null)
        {
            //return shaders.Call(asset, processor);

            if (shaderCache.ContainsKey(asset))
                return shaderCache[asset];

            var output = next.ProcessShader(asset, processor, bindattrs);
            shaderCache[asset] = output;
            return output;
        }

        public IFont ProcessFont(FontContent asset, IContentProcessor processor = null)
        {
            return fonts.Call(asset, processor);
        }

        public ISong ProcessSong(SongContent asset, IContentProcessor processor = null)
        {
            return songs.Call(asset, processor);
        }

        public ISoundEffect ProcessSoundEffect(SoundEffectContent asset, IContentProcessor processor = null)
        {
            return soundEffects.Call(asset, processor);
        }

        #endregion
    }
}

