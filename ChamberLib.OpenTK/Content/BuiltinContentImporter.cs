
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
using ChamberLib.Content;

namespace ChamberLib.OpenTK.Content
{
    public class BuiltinContentImporter : IContentImporter
    {
        public BuiltinContentImporter(IContentImporter next)
        {
            if (next == null) throw new ArgumentNullException("next");

            this.next = next;
            this.shaderImporter = new BuiltinShaderImporter(next.ImportShaderStage);
            this.fontImporter = new BuiltinFontImporter(next.ImportFont);
        }

        readonly IContentImporter next;
        readonly BuiltinShaderImporter shaderImporter; 
        readonly BuiltinFontImporter fontImporter;

        public ModelContent ImportModel(string name, IContentImporter importer = null)
        {
            return next.ImportModel(name, importer);
        }
        public TextureContent ImportTexture2D(string name, IContentImporter importer = null)
        {
            return next.ImportTexture2D(name, importer);
        }
        public FontContent ImportFont(string name, IContentImporter importer = null)
        {
            return fontImporter.ImportFont(name, importer);
        }
        public SongContent ImportSong(string name, IContentImporter importer = null)
        {
            return next.ImportSong(name, importer);
        }
        public SoundEffectContent ImportSoundEffect(string name, IContentImporter importer = null)
        {
            return next.ImportSoundEffect(name, importer);
        }
        public ShaderContent ImportShaderStage(string name, ShaderType type, IContentImporter importer = null)
        {
            return shaderImporter.ImportShaderStage(name, type, importer);
        }
    }
}

