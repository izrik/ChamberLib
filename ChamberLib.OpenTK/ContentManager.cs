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

            Processor = new OpenTKContentProcessor(
                    new ContentProcessor(
                        null,
                        null,
                        null,
                        null,
                        null,
                        null));
        }

        public readonly Renderer Renderer;
        public readonly IContentImporter Importer;
        public readonly IContentProcessor Processor;

        readonly Dictionary<string, object> _cache = new Dictionary<string, object>();

        public IModel LoadModel(string name)
        {
            var resolvedFilename = (name);
            if (_cache.ContainsKey(resolvedFilename)) return (IModel)_cache[resolvedFilename];

            var filename = resolvedFilename;
            var modelContent = Importer.ImportModel(filename, Importer);
            var model = new Model(modelContent, Renderer);

            _cache[resolvedFilename] = model;
            return model;
        }

        public ISong LoadSong(string name)
        {
            var resolvedFilename = (name);
            if (_cache.ContainsKey(resolvedFilename)) return (ISong)_cache[resolvedFilename];
            var songContent = Importer.ImportSong(resolvedFilename, Importer);
            var song = new Song(songContent);
            _cache[resolvedFilename] = song;
            return song;
        }

        public ISoundEffect LoadSoundEffect(string name)
        {
            var resolvedFilename = (name);
            if (_cache.ContainsKey(resolvedFilename)) return (ISoundEffect)_cache[resolvedFilename];

            var sec = Importer.ImportSoundEffect(resolvedFilename, null);
            var se = new SoundEffect(sec);

            _cache[resolvedFilename] = se;
            return se;
        }

        public ITexture2D LoadTexture2D(string name)
        {
            var resolvedFilename = (name);
            if (_cache.ContainsKey(resolvedFilename)) return (ITexture2D)_cache[resolvedFilename];

            var filename = (resolvedFilename);

            var textureContent = Importer.ImportTexture2D(filename, null);
            var texture = Processor.ProcessTexture2D(textureContent, Processor);
            _cache[resolvedFilename] = texture;
            return texture;
        }

        public IFont LoadFont(string name)
        {
            var resolvedFilename = (name);
            if (_cache.ContainsKey(resolvedFilename)) return (IFont)_cache[resolvedFilename];
            var fontContent = Importer.ImportFont(resolvedFilename, Importer);
            var font = Processor.ProcessFont(fontContent, Processor);
            _cache[resolvedFilename] = font;
            return font;
        }

        public IShader LoadShader(string name, object bindattrs=null)
        {

            var resolvedFilename = (name);
            if (_cache.ContainsKey(resolvedFilename)) return (IShader)_cache[resolvedFilename];

            var shaderContent = Importer.ImportShader(name, Importer);

            var shader = (ShaderAdapter)Processor.ProcessShader(shaderContent, null, bindattrs);

            shader.Name = name;
            _cache[resolvedFilename] = shader;
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

