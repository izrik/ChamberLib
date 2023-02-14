
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
    public interface IContentProcessor
    {
        IModel ProcessModel(ModelContent asset, IContentProcessor processor=null);
        ITexture2D ProcessTexture2D(TextureContent asset, IContentProcessor processor=null);
        IFont ProcessFont(FontContent asset, IContentProcessor processor=null);
        ISong ProcessSong(SongContent asset, IContentProcessor processor=null);
        ISoundEffect ProcessSoundEffect(SoundEffectContent asset, IContentProcessor processor=null);
        IShaderStage ProcessShaderStage(ShaderContent asset, IContentProcessor processor=null);
    }
}

