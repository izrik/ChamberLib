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

        public IShader LoadShader(string name, object bindattrs = null)
        {
            var content = Importer.ImportShader(name, Importer);
            var shader = Processor.ProcessShader(content, Processor, bindattrs);
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
            throw new NotImplementedException();
        }
    }
}

