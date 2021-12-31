
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
using System.IO;
using System.Linq;

namespace ChamberLib.Content
{
    public class ResolvingContentImporter : IContentImporter
    {
        public ResolvingContentImporter(IContentImporter next, string basePath=null, bool normalizePathSeparators=true)
        {
            if (next == null) throw new ArgumentNullException("next");

            this.next = next;

            if (!string.IsNullOrWhiteSpace(basePath))
                BasePath = basePath;

            NormalizePathSeparators = normalizePathSeparators;
        }

        public string BasePath = "";
        public bool NormalizePathSeparators;
        readonly IContentImporter next;

        public string ResolveFilename(string filename)
        {
            if (Path.IsPathRooted(filename))
                return filename;

            string path = filename;
            if (!string.IsNullOrWhiteSpace(BasePath))
            {
                path = Path.Combine(BasePath, filename);
            }

            if (NormalizePathSeparators)
            {
                path = path.Replace('\\', Path.DirectorySeparatorChar);
                path = path.Replace('/', Path.DirectorySeparatorChar);
            }

            return Path.GetFullPath(path);
        }

        public ModelContent ImportModel(string name, IContentImporter importer = null)
        {
            name = ResolveFilename(name);

            return next.ImportModel(name, importer);
        }

        public TextureContent ImportTexture2D(string name, IContentImporter importer = null)
        {
            name = ResolveFilename(name);

            return next.ImportTexture2D(name, importer);
        }

        public FontContent ImportFont(string name, IContentImporter importer = null)
        {
            name = ResolveFilename(name);

            return next.ImportFont(name, importer);
        }

        public SongContent ImportSong(string name, IContentImporter importer = null)
        {
            name = ResolveFilename(name);

            return next.ImportSong(name, importer);
        }

        public SoundEffectContent ImportSoundEffect(string name, IContentImporter importer = null)
        {
            name = ResolveFilename(name);

            return next.ImportSoundEffect(name, importer);
        }

        public ShaderContent ImportShaderStage(string name, ShaderType type, IContentImporter importer = null)
        {
            var resolvedName = ResolveFilename(name);

            return next.ImportShaderStage(resolvedName, type, importer);
        }
    }
}

