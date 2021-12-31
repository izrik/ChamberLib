
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

