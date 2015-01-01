using System;
using System.IO;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using System.Linq;
using Assimp;

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

        public IModel LoadModel(string name, string relativeTo=null)
        {
            var resolvedFilename = ResolveFilename(name, relativeTo);
            if (_cache.ContainsKey(resolvedFilename)) return (IModel)_cache[resolvedFilename];

            var loader = new AiModelLoader(Renderer);
            String[] importFormats = loader.GetSupportedImportFormats();
            foreach (var ext in importFormats)
            {
                if (File.Exists(resolvedFilename + ext))
                {
                    var filename = resolvedFilename + ext;
                    var model = loader.LoadModel(filename, this);

                    _cache[resolvedFilename] = model;
                    return model;
                }
            }

            if (File.Exists(resolvedFilename + ".chmodel"))
            {
                var filename = resolvedFilename + ".chmodel";
                var mi = new ModelImporter();
                var model = mi.ImportModel(filename, Renderer, this);
                _cache[resolvedFilename] = model;
                return model;
            }

            throw new FileNotFoundException(name);
        }

        public ISong LoadSong(string name, string relativeTo=null)
        {
            var resolvedFilename = ResolveFilename(name, relativeTo);
            if (_cache.ContainsKey(resolvedFilename)) return (ISong)_cache[resolvedFilename];
            var soundEffect = (SoundEffect)LoadSoundEffect(name);
            var song = new Song(soundEffect);
            _cache[resolvedFilename] = song;
            return song;
        }

        public ISoundEffect LoadSoundEffect(string name, string relativeTo=null)
        {
            var resolvedFilename = ResolveFilename(name, relativeTo);
            if (_cache.ContainsKey(resolvedFilename)) return (ISoundEffect)_cache[resolvedFilename];

            if (File.Exists(resolvedFilename))
            {
            }
            else if (File.Exists(resolvedFilename + ".wav"))
            {
                resolvedFilename += ".wav";
            }
            else if (File.Exists(resolvedFilename + ".ogg"))
            {
                resolvedFilename += ".ogg";
            }
            else
            {
                throw new FileNotFoundException("The sound file could not be found.", resolvedFilename);
            }

            SoundEffect.FileFormat format;
            if (resolvedFilename.ToLower().EndsWith(".wav"))
            {
                format = SoundEffect.FileFormat.Wav;
            }
            else if (resolvedFilename.ToLower().EndsWith(".ogg"))
            {
                format = SoundEffect.FileFormat.Ogg;
            }
            else
            {
                throw new IOException(string.Format("The file \"{0}\" is of an unknown type", resolvedFilename));
            }

            var stream = File.Open(resolvedFilename, FileMode.Open);

            var se = new SoundEffect(name, stream, format);
            _cache[resolvedFilename] = se;
            return se;
        }

        public ITexture2D LoadTexture2D(string name, string relativeTo=null)
        {
            var resolvedFilename = ResolveFilename(name, relativeTo);
            if (_cache.ContainsKey(resolvedFilename)) return (ITexture2D)_cache[resolvedFilename];

            string filename;

            if (File.Exists(resolvedFilename))
            {
                filename = resolvedFilename;
            }
            else if (File.Exists(resolvedFilename + ".png"))
            {
                filename = resolvedFilename + ".png";
            }
            else if (File.Exists(resolvedFilename + ".jpg"))
            {
                filename = resolvedFilename + ".jpg";
            }
            else if (File.Exists(resolvedFilename + ".gif"))
            {
                filename = resolvedFilename + ".gif";
            }
            else if (File.Exists(resolvedFilename + ".bmp"))
            {
                filename = resolvedFilename + ".bmp";
            }
            else
            {
                throw new FileNotFoundException("Could not find texture file", resolvedFilename);
            }

            var texture = TextureAdapter.LoadTextureFromFile(filename);
            _cache[resolvedFilename] = texture;
            return texture;
        }

        public IFont LoadFont(string name, string relativeTo=null)
        {
            var resolvedFilename = ResolveFilename(name, relativeTo);
            if (_cache.ContainsKey(resolvedFilename)) return (IFont)_cache[resolvedFilename];
            var font = new FontAdapter();
            _cache[resolvedFilename] = font;
            return font;
        }

        public IShader LoadShader(string name, string relativeTo=null, object bindattrs=null)
        {
            if (name == "$basic")
            {
                return BuiltinShaders.BasicShader;
            }
            if (name == "$skinned")
            {
                return BuiltinShaders.SkinnedShader;
            }

            var resolvedFilename = ResolveFilename(name, relativeTo);
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

            if (name.Contains(","))
            {
                try
                {
                    var parts = name.Split(',');
                    var vert = ResolveFilename(parts[0], relativeTo);
                    var frag = ResolveFilename(parts[1], relativeTo);

                    var vertexShaderSource = File.ReadAllText(vert);
                    var fragmentShaderSource = File.ReadAllText(frag);

                    var shader = new ShaderAdapter(vs: vertexShaderSource, fs: fragmentShaderSource, bindAttributes: bindattrs2);
                    shader.Name = name;
                    _cache[resolvedFilename] = shader;
                    return shader;
                }
                catch (FileNotFoundException e)
                {
                }
            }

            try
            {
                var vert = ResolveFilename(name + ".vert", relativeTo);
                var frag = ResolveFilename(name + ".frag", relativeTo);

                var vertexShaderSource = File.ReadAllText(vert);
                var fragmentShaderSource = File.ReadAllText(frag);

                var shader = new ShaderAdapter(vs: vertexShaderSource, fs: fragmentShaderSource, bindAttributes: bindattrs2);
                shader.Name = name;
                _cache[resolvedFilename] = shader;
                return shader;
            }
            finally
            {
            }
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
        static string ResolveFilename(string filename, string relativeTo)
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

