using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using ChamberLib.Content;

namespace ChamberLib
{
    public class ContentManager : IContentManager
    {
        public ContentManager(Renderer renderer)
        {
            if (renderer == null) throw new ArgumentNullException("renderer");

            Renderer = renderer;

            Importer =
                new BuiltinContentImporter(
                    new ResolvingContentImporter(
                        new ContentImporter(
                            new ChModelImporter().ImportModel,
                            new BasicTextureImporter().ImportTexture,
                            new GlslShaderImporter().ImportShader,
                            null,
                            new BasicSongImporter().ImportSong,
                            new OggVorbisSoundEffectImporter(
                                new WaveSoundEffectImporter().ImportSoundEffect).ImportSoundEffect
                        ),
                        basePath: "Content.OpenTK"));

            Processor =
                new CachingContentProcessor(
                    new OpenTKContentProcessor(Renderer));
        }

        public readonly Renderer Renderer;
        public readonly IContentImporter Importer;
        public readonly IContentProcessor Processor;

        readonly Dictionary<string, object> _cache = new Dictionary<string, object>();

        public IModel LoadModel(string name)
        {
            if (_cache.ContainsKey(name)) return (IModel)_cache[name];

            var modelContent = Importer.ImportModel(name, Importer);
            var model = Processor.ProcessModel(modelContent, Processor);

            _cache[name] = model;
            return model;
        }

        public ISong LoadSong(string name)
        {
            if (_cache.ContainsKey(name)) return (ISong)_cache[name];

            var songContent = Importer.ImportSong(name, Importer);
            var song = Processor.ProcessSong(songContent, Processor);

            _cache[name] = song;
            return song;
        }

        public ISoundEffect LoadSoundEffect(string name)
        {
            if (_cache.ContainsKey(name)) return (ISoundEffect)_cache[name];

            var sec = Importer.ImportSoundEffect(name, null);
            var se = Processor.ProcessSoundEffect(sec, Processor);

            _cache[name] = se;
            return se;
        }

        public ITexture2D LoadTexture2D(string name)
        {
            if (_cache.ContainsKey(name)) return (ITexture2D)_cache[name];

            var textureContent = Importer.ImportTexture2D(name, null);
            var texture = Processor.ProcessTexture2D(textureContent, Processor);

            _cache[name] = texture;
            return texture;
        }

        public IFont LoadFont(string name)
        {
            if (_cache.ContainsKey(name)) return (IFont)_cache[name];

            var fontContent = Importer.ImportFont(name, Importer);
            var font = Processor.ProcessFont(fontContent, Processor);

            _cache[name] = font;
            return font;
        }

        public IShader LoadShader(string name, object bindattrs=null)
        {
            if (_cache.ContainsKey(name)) return (IShader)_cache[name];

            var shaderContent = Importer.ImportShader(name, Importer);
            var shader = Processor.ProcessShader(shaderContent, null, bindattrs);

            _cache[name] = shader;
            return shader;
        }

        public string LookupObjectName(object o)
        {
            foreach (var kvp in _cache)
            {
                if (kvp.Value == o)
                {
                    return kvp.Key;
                }
            }

            return null;
        }

        public ITexture2D CreateTexture(int width, int height, Color[] data)
        {
            return TextureAdapter.CreateTexture(width, height, data);
        }
    }
}

