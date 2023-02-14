
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
    public class ContentManager : IContentManager
    {
        public ContentManager(IContentImporter importer, IContentProcessor processor)
        {
            if (importer == null) throw new ArgumentNullException("importer");
            if (processor == null) throw new ArgumentNullException("processor");

            _importer = importer;
            _processor = processor;
        }

        public readonly IContentImporter _importer;
        public readonly IContentProcessor _processor;

        public IContentImporter Importer { get { return _importer; } }
        public IContentProcessor Processor { get { return _processor; } }

        public IModel LoadModel(string name)
        {
            var content = Importer.ImportModel(name, Importer);
            var model = Processor.ProcessModel(content, Processor);
            return model;
        }

        public ITexture2D LoadTexture2D(string name)
        {
            var content = Importer.ImportTexture2D(name, Importer);
            var texture = Processor.ProcessTexture2D(content, Processor);
            return texture;
        }

        public IFont LoadFont(string name)
        {
            var content = Importer.ImportFont(name, Importer);
            var font = Processor.ProcessFont(content, Processor);
            return font;
        }

        public ISong LoadSong(string name)
        {
            var content = Importer.ImportSong(name, Importer);
            var song = Processor.ProcessSong(content, Processor);
            return song;
        }

        public ISoundEffect LoadSoundEffect(string name)
        {
            var content = Importer.ImportSoundEffect(name, Importer);
            var soundEffect = Processor.ProcessSoundEffect(content, Processor);
            return soundEffect;
        }

        public IShaderStage LoadShaderStage(string name, ShaderType type)
        {
            var content = Importer.ImportShaderStage(name, type, Importer);
            var shaderStage = Processor.ProcessShaderStage(content, Processor);
            return shaderStage;
        }

        public ITexture2D CreateTexture(int width, int height, Color[] data,
            PixelFormat pixelFormat=PixelFormat.Rgba)
        {
            var tc = new TextureContent(width, height, data);
            return Processor.ProcessTexture2D(tc);
        }
    }
}

