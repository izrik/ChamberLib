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

        public string LookupObjectName(object o)
        {
            if (o is IShaderProgram)
            {
                return ((IShaderProgram)o).Name;
            }

            return null;
        }

        public ITexture2D CreateTexture(int width, int height, Color[] data,
            PixelFormat pixelFormat=PixelFormat.Rgba)
        {
            var tc = new TextureContent(width, height, data);
            return Processor.ProcessTexture2D(tc);
        }

        public IShaderProgram MakeShaderProgram(IShaderStage vertexShader,
            IShaderStage fragmentShader, string[] bindattrs=null)
        {
            return Processor.MakeShaderProgram(vertexShader, fragmentShader, bindattrs);
        }
    }
}

