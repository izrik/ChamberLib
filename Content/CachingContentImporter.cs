
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
    public class CachingContentImporter : IContentImporter
    {
        public CachingContentImporter(IContentImporter next)
        {
            if (next == null) throw new ArgumentNullException("next");

            models = new Cache<string, IContentImporter, ModelContent>(next.ImportModel);
            textures = new Cache<string, IContentImporter, TextureContent>(next.ImportTexture2D);
            shaderStages = new Cache2<string, ShaderType, IContentImporter, ShaderContent>(next.ImportShaderStage);
            fonts = new Cache<string, IContentImporter, FontContent>(next.ImportFont);
            songs = new Cache<string, IContentImporter, SongContent>(next.ImportSong);
            soundEffects = new Cache<string, IContentImporter, SoundEffectContent>(next.ImportSoundEffect);
        }

        readonly Cache<string, IContentImporter, ModelContent> models;
        readonly Cache<string, IContentImporter, TextureContent> textures;
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

