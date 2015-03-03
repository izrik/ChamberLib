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
        }

        public readonly Renderer Renderer;

        readonly Dictionary<string, object> _cache = new Dictionary<string, object>();

        public IModel LoadModel(string name)
        {
            var resolvedFilename = ResolveFilename(name);
            if (_cache.ContainsKey(resolvedFilename)) return (IModel)_cache[resolvedFilename];

            if (File.Exists(resolvedFilename + ".chmodel"))
            {
                var filename = resolvedFilename + ".chmodel";
                var mi = new ChModelImporter();
                var modelContent = mi.ImportModel(filename, this);
                var model = new Model(modelContent, Renderer);
                _cache[resolvedFilename] = model;
                return model;
            }

            throw new FileNotFoundException(name);
        }

        public ISong LoadSong(string name)
        {
            var resolvedFilename = ResolveFilename(name);
            if (_cache.ContainsKey(resolvedFilename)) return (ISong)_cache[resolvedFilename];
            var soundEffect = (SoundEffect)LoadSoundEffect(name);
            var song = new Song(soundEffect);
            _cache[resolvedFilename] = song;
            return song;
        }

        public ISoundEffect LoadSoundEffect(string name)
        {
            var resolvedFilename = ResolveFilename(name);
            if (_cache.ContainsKey(resolvedFilename)) return (ISoundEffect)_cache[resolvedFilename];


            var wsei = new WaveSoundEffectImporter();
            SoundEffectImporter wsei2 =
                (string filename, IContentImporter importer) =>
                    wsei.ImportSoundEffect(filename, filename);
            var ovsei = new OggVorbisSoundEffectImporter(wsei2);

            var sec = ovsei.ImportSoundEffect(name, resolvedFilename);
            var se = new SoundEffect(sec);

            _cache[resolvedFilename] = se;
            return se;
        }

        public string ResolveTextureFilename(string name)
        {
            name = ResolveFilename(name);

            string filename;

            if (File.Exists(name))
            {
                filename = name;
            }
            else if (File.Exists(name + ".png"))
            {
                filename = name + ".png";
            }
            else if (File.Exists(name + ".jpg"))
            {
                filename = name + ".jpg";
            }
            else if (File.Exists(name + ".gif"))
            {
                filename = name + ".gif";
            }
            else if (File.Exists(name + ".bmp"))
            {
                filename = name + ".bmp";
            }
            else
            {
                throw new FileNotFoundException("Could not find texture file", name);
            }

            return filename;
        }

        public ITexture2D LoadTexture2D(string name)
        {
            var resolvedFilename = ResolveTextureFilename(name);
            if (_cache.ContainsKey(resolvedFilename)) return (ITexture2D)_cache[resolvedFilename];

            var filename = (resolvedFilename);

            var textureContent = BasicTextureLoader.LoadTexture(filename);
            var texture = new TextureAdapter(textureContent);
            _cache[resolvedFilename] = texture;
            return texture;
        }

        public IFont LoadFont(string name)
        {
            var resolvedFilename = ResolveFilename(name);
            if (_cache.ContainsKey(resolvedFilename)) return (IFont)_cache[resolvedFilename];
            var font = new FontAdapter();
            _cache[resolvedFilename] = font;
            return font;
        }

        public void ResolveShaderFilenames(string name, out string vertexShaderFilename, out string fragmentShaderFilename)
        {
            string vert = null;
            string frag = null;

            if (name.Contains(","))
            {
                var parts = name.Split(',');

                vert = ResolveFilename(parts[0]);
                frag = ResolveFilename(parts[1]);
            }
            else
            {
                vert = frag = ResolveFilename(name);
            }

            if (File.Exists(vert))
            {
                vertexShaderFilename = vert;
            }
            else if (File.Exists(vert + ".vert"))
            {
                vertexShaderFilename = vert + ".vert";
            }
            else
            {
                vertexShaderFilename = vert;
            }

            if (File.Exists(frag))
            {
                fragmentShaderFilename = frag;
            }
            else if (File.Exists(frag + ".frag"))
            {
                fragmentShaderFilename = frag + ".frag";
            }
            else
            {
                fragmentShaderFilename = frag;
            }
        }

        public IShader LoadShader(string name, object bindattrs=null)
        {
            if (name == "$basic")
            {
                return BuiltinShaders.BasicShader;
            }
            if (name == "$skinned")
            {
                return BuiltinShaders.SkinnedShader;
            }

            var resolvedFilename = ResolveFilename(name);
            if (_cache.ContainsKey(resolvedFilename)) return (IShader)_cache[resolvedFilename];

            string[] bindattrs2=null;
            if (bindattrs == null)
            {
            }
            else if (bindattrs is IEnumerable<string>)
            {
                bindattrs2 = (bindattrs as IEnumerable<string>).ToArray();
            }
            else
            {
                throw new InvalidOperationException();
            }

            ShaderContent shaderContent = null;
            string vertexShaderSource = null;
            string fragmentShaderSource = null;

            if (name.Contains(","))
            {
                try
                {
                    var parts = name.Split(',');
                    var vert = ResolveFilename(parts[0]);
                    var frag = ResolveFilename(parts[1]);

                    shaderContent = BasicShaderLoader.LoadShader(vert, frag, bindattrs2);
                }
                catch (FileNotFoundException e)
                {
                }
            }

            try
            {
                var vert = ResolveFilename(name + ".vert");
                var frag = ResolveFilename(name + ".frag");

                shaderContent = BasicShaderLoader.LoadShader(vert, frag, bindattrs2);
            }
            finally
            {
            }

            var shader = new ShaderAdapter(shaderContent);

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

        public static string PathPrefix = "Content.OpenTK";
        static string ResolveFilename(string filename)
        {
            if (Path.IsPathRooted(filename))
                return filename;

            string path = filename;
            if (!string.IsNullOrWhiteSpace(PathPrefix))
            {
                path = Path.Combine(PathPrefix, filename);
            }
            return path.Replace('\\', '/');
        }
    }
}

