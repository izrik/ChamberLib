using System;
using System.IO;

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

        public T Load<T>(string name)
        {
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
                var model = mi.ImportModel(filename, Renderer);

                return (T)(object)model;
            }
            if (typeof(T) == typeof(ISong))
            {
                return (T)(object)new Song();
            }
            if (typeof(T) == typeof(ISoundEffect))
            {
                return (T)(object)new SoundEffect();
            }
            if (typeof(T) == typeof(ITexture2D))
            {
                return (T)(object)TextureAdapter.LoadTextureFromFile(name);
            }
            if (typeof(T) == typeof(IFont))
            {
                return (T)(object)new FontAdapter();
            }

            return default(T);
        }

        public ITexture2D CreateTexture(int width, int height, Color[] data)
        {
            return null;
        }

        static string GetContentFilename(string name)
        {
            return Path.Combine("Content.OpenTK", name).Replace('\\', '/');
        }
    }
}

