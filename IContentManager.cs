
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

namespace ChamberLib
{
    public interface IContentManager
    {
        IContentImporter Importer { get; }
        IContentProcessor Processor { get; }

        IModel LoadModel(string name);
        ITexture2D LoadTexture2D(string name);
        IFont LoadFont(string name);
        ISong LoadSong(string name);
        ISoundEffect LoadSoundEffect(string name);
        IShaderStage LoadShaderStage(string name, ShaderType type);

        ITexture2D CreateTexture(int width, int height, Color[] data,
            PixelFormat pixelFormat=PixelFormat.Rgba);
    }

    public static class IContentManagerHelper
    {
        public static IModel LoadModelIfNotNull(this IContentManager content, string name)
        {
            if (name != null)
            {
                return content.LoadModel(name);
            }

            return null;
        }
        public static ITexture2D LoadTexture2DIfNotNull(this IContentManager content, string name)
        {
            if (name != null)
            {
                return content.LoadTexture2D(name);
            }

            return null;
        }
        public static IFont LoadFontIfNotNull(this IContentManager content, string name)
        {
            if (name != null)
            {
                return content.LoadFont(name);
            }

            return null;
        }
        public static ISong LoadSongIfNotNull(this IContentManager content, string name)
        {
            if (name != null)
            {
                return content.LoadSong(name);
            }

            return null;
        }
        public static ISoundEffect LoadSoundEffectIfNotNull(this IContentManager content, string name)
        {
            if (name != null)
            {
                return content.LoadSoundEffect(name);
            }

            return null;
        }
    }
}

