
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

namespace ChamberLib.Content
{
    public class CachingContentManager : IContentManager
    {
        public CachingContentManager(IContentManager next)
        {
            if (next == null) throw new ArgumentNullException("next");

            this.next = next;

            models = new Cache<string, IModel>(next.LoadModel);
            textures = new Cache<string, ITexture2D>(next.LoadTexture2D);
            shaderStages = new Cache2<string, ShaderType, IShaderStage>(next.LoadShaderStage);
            fonts = new Cache<string, IFont>(next.LoadFont);
            songs = new Cache<string, ISong>(next.LoadSong);
            soundEffects = new Cache<string, ISoundEffect>(next.LoadSoundEffect);
        }

        readonly IContentManager next;

        readonly Cache<string, IModel> models;
        readonly Cache<string, ITexture2D> textures;
        readonly Cache2<string, ShaderType, IShaderStage> shaderStages;
        readonly Cache<string, IFont> fonts;
        readonly Cache<string, ISong> songs;
        readonly Cache<string, ISoundEffect> soundEffects;

        public IContentImporter Importer { get { return next.Importer; } }
        public IContentProcessor Processor { get { return next.Processor; } }

        public IModel LoadModel(string name)
        {
            return models.Call(name);
        }

        public ITexture2D LoadTexture2D(string name)
        {
            return textures.Call(name);
        }

        public IShaderStage LoadShaderStage(string name, ShaderType type)
        {
            return shaderStages.Call(name, type);
        }

        public IFont LoadFont(string name)
        {
            return fonts.Call(name);
        }

        public ISong LoadSong(string name)
        {
            return songs.Call(name);
        }

        public ISoundEffect LoadSoundEffect(string name)
        {
            return soundEffects.Call(name);
        }

        public ITexture2D CreateTexture(int width, int height, Color[] data,
            PixelFormat pixelFormat=PixelFormat.Rgba)
        {
            return next.CreateTexture(width, height, data, pixelFormat);
        }
    }
}

