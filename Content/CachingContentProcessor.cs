﻿using System;
using System.Collections.Generic;

namespace ChamberLib.Content
{
    public class CachingContentProcessor : IContentProcessor
    {
        public CachingContentProcessor(IContentProcessor next)
        {
            if (next == null) throw new ArgumentNullException("next");

            models = new Cache<ModelContent, IContentProcessor, IModel>(next.ProcessModel);
            textures = new Cache<TextureContent, IContentProcessor, ITexture2D>(next.ProcessTexture2D);
            shaderStages = new Cache<ShaderContent, IContentProcessor, IShaderStage>(next.ProcessShaderStage);
            fonts = new Cache<FontContent, IContentProcessor, IFont>(next.ProcessFont);
            songs = new Cache<SongContent, IContentProcessor, ISong>(next.ProcessSong);
            soundEffects = new Cache<SoundEffectContent, IContentProcessor, ISoundEffect>(next.ProcessSoundEffect);
            shaderPrograms = new Cache2<IShaderStage, IShaderStage, IShaderProgram>(next.MakeShaderProgram);

            Next = next;
        }

        readonly Cache<ModelContent, IContentProcessor, IModel> models;
        readonly Cache<TextureContent, IContentProcessor, ITexture2D> textures;
        readonly Cache<ShaderContent, IContentProcessor, IShaderStage> shaderStages;
        readonly Cache<FontContent, IContentProcessor, IFont> fonts;
        readonly Cache<SongContent, IContentProcessor, ISong> songs;
        readonly Cache<SoundEffectContent, IContentProcessor, ISoundEffect> soundEffects;
        readonly Cache2<IShaderStage, IShaderStage, IShaderProgram> shaderPrograms;

        readonly IContentProcessor Next;

        #region IContentProcessor implementation

        public IModel ProcessModel(ModelContent asset, IContentProcessor processor = null)
        {
            return models.Call(asset, processor);
        }

        public ITexture2D ProcessTexture2D(TextureContent asset, IContentProcessor processor = null)
        {
            return textures.Call(asset, processor);
        }

        public IShaderStage ProcessShaderStage(ShaderContent asset, IContentProcessor processor = null)
        {
            return shaderStages.Call(asset, processor);
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

        public IShaderProgram MakeShaderProgram(IShaderStage vertexShader,
            IShaderStage fragmentShader)
        {
            return shaderPrograms.Call(vertexShader, fragmentShader);
        }

        #endregion
    }
}

