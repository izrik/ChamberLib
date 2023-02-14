
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
    public delegate ModelContent ModelImporter(string filename, IContentImporter importer);
    public delegate TextureContent TextureImporter(string filename, IContentImporter importer);
    public delegate ShaderContent ShaderStageImporter(string filename, ShaderType type, IContentImporter importer);
    public delegate FontContent FontImporter(string filename, IContentImporter importer);
    public delegate SongContent SongImporter(string filename, IContentImporter importer);
    public delegate SoundEffectContent SoundEffectImporter(string filename, IContentImporter importer);
}

