
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
    }
}

