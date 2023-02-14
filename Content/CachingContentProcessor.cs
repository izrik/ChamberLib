
//
// ChamberLib, a cross-platform game engine
// Copyright (C) 2021 izrik and Metaphysics Industries, Inc.
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301
// USA
//

using System;
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

            Next = next;
        }

        readonly Cache<ModelContent, IContentProcessor, IModel> models;
        readonly Cache<TextureContent, IContentProcessor, ITexture2D> textures;
        readonly Cache<ShaderContent, IContentProcessor, IShaderStage> shaderStages;
        readonly Cache<FontContent, IContentProcessor, IFont> fonts;
        readonly Cache<SongContent, IContentProcessor, ISong> songs;
        readonly Cache<SoundEffectContent, IContentProcessor, ISoundEffect> soundEffects;

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

        #endregion
    }
}

