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

            Importer = importer;
            Processor = processor;
        }

        public readonly IContentImporter Importer;
        public readonly IContentProcessor Processor;

        public IModel LoadModel(string name)
        {
            var modelContent = Importer.ImportModel(name, Importer);
            var model = Processor.ProcessModel(modelContent, Processor);
            return model;
        }

        public ISong LoadSong(string name)
        {
            var songContent = Importer.ImportSong(name, Importer);
            var song = Processor.ProcessSong(songContent, Processor);
            return song;
        }

        public ISoundEffect LoadSoundEffect(string name)
        {
            var sec = Importer.ImportSoundEffect(name, null);
            var se = Processor.ProcessSoundEffect(sec, Processor);
            return se;
        }

        public ITexture2D LoadTexture2D(string name)
        {
            var textureContent = Importer.ImportTexture2D(name, null);
            var texture = Processor.ProcessTexture2D(textureContent, Processor);
            return texture;
        }

        public IFont LoadFont(string name)
        {
            var fontContent = Importer.ImportFont(name, Importer);
            var font = Processor.ProcessFont(fontContent, Processor);
            return font;
        }

        public IShader LoadShader(string name, object bindattrs=null)
        {
            var shaderContent = Importer.ImportShader(name, Importer);
            var shader = Processor.ProcessShader(shaderContent, null, bindattrs);
            return shader;
        }

        public string LookupObjectName(object o)
        {
            if (o is IShader)
            {
                return ((IShader)o).Name;
            }

            return null;
        }

        public ITexture2D CreateTexture(int width, int height, Color[] data)
        {
            var tc = new TextureContent(width, height, data);
            return Processor.ProcessTexture2D(tc);
        }
    }
}

