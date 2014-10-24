using System;
using System.IO;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using System.Linq;

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
        public T Load<T>(string name, object param=null)
        {
            if (_cache.ContainsKey(name))
            {
                return (T)_cache[name];
            }

            var x = LoadInternal<T>(name, param);
            _cache[name] = x;

            var xl = (x as ILoadable);
            if (xl != null)
            {
                xl.LoadContents(this);
            }

            return x;
        }
        T LoadInternal<T>(string name, object param)
        {
            var contentName = GetContentFilename(name);
            if (typeof(T) == typeof(IModel))
            {
                name = GetContentFilename(name);
                string filename;
                if (File.Exists(name + ".chmodel"))
                {
                    filename = name + ".chmodel";
                }
                else
                {
                    throw new FileNotFoundException(name);
                }

                var mi = new ModelImporter();
                var model = mi.ImportModel(filename, Renderer, this);

                return (T)(object)model;
            }
            if (typeof(T) == typeof(ISong))
            {
                return (T)(object)new Song();
            }
            if (typeof(T) == typeof(ISoundEffect))
            {
                if (File.Exists(contentName))
                {
                }
                if (File.Exists(contentName + ".wav"))
                {
                    contentName += ".wav";
                }
                else
                {
                    throw new FileNotFoundException("The sound file could not be found.", contentName);
                }

                if (contentName.ToLower().EndsWith(".wav"))
                {
                    // type = wav
                }
                else
                {
                    throw new IOException(string.Format("The file \"{0}\" is of an unknown type", contentName));
                }

                var stream = File.Open(contentName, FileMode.Open);

                return (T)(object)new SoundEffect(stream);
            }
            if (typeof(T) == typeof(ITexture2D))
            {
                return (T)(object)TextureAdapter.LoadTextureFromFile(name);
            }
            if (typeof(T) == typeof(IFont))
            {
                return (T)(object)new FontAdapter();
            }
            if (typeof(T) == typeof(IShader))
            {
                if (name == "$basic")
                {
                    return (T)(object)BuiltinShaders.BasicShader;
                }
                if (name == "$skinned")
                {
                    return (T)(object)BuiltinShaders.SkinnedShader;
                }

                string[] bindattrs=null;
                if (param == null)
                {
                }
                else if (param is IEnumerable<string>)
                {
                    bindattrs = (param as IEnumerable<string>).ToArray();
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
                        var vert = GetContentFilename(parts[0]);
                        var frag = GetContentFilename(parts[1]);

                        var vertexShaderSource = File.ReadAllText(vert);
                        var fragmentShaderSource = File.ReadAllText(frag);

                        var shader = new ShaderAdapter(vs: vertexShaderSource, fs: fragmentShaderSource, bindAttributes: bindattrs);
                        shader.Name = name;
                        return (T)(object)shader;
                    }
                    catch (FileNotFoundException e)
                    {
                    }
                }

                try
                {
                    var vert = GetContentFilename(name + ".vert");
                    var frag = GetContentFilename(name + ".frag");

                    var vertexShaderSource = File.ReadAllText(vert);
                    var fragmentShaderSource = File.ReadAllText(frag);

                    var shader = new ShaderAdapter(vs: vertexShaderSource, fs: fragmentShaderSource, bindAttributes: bindattrs);
                    shader.Name = name;
                    return (T)(object)shader;
                }
                finally
                {
                }
            }

            return default(T);
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

        static string GetContentFilename(string name)
        {
            return Path.Combine("Content.OpenTK", name).Replace('\\', '/');
        }
    }
}

