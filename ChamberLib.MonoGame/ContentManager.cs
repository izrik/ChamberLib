using System;
using XContentManager = Microsoft.Xna.Framework.Content.ContentManager;
using XTexture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using XModel = Microsoft.Xna.Framework.Graphics.Model;
using XDevice = Microsoft.Xna.Framework.Graphics.GraphicsDevice;
using System.Collections.Generic;

namespace ChamberLib
{
    public class ContentManager : IContentManager
    {
        public ContentManager(XDevice device, XContentManager manager, string rootDirectory=null)
        {
            if (manager == null)
                throw new ArgumentNullException("manager");

            Device = device;
            Manager = manager;

            if (rootDirectory != null)
            {
                Manager.RootDirectory = rootDirectory;
            }
        }

        public XDevice Device;
        public XContentManager Manager;

        readonly Dictionary<string, object> _cache = new Dictionary<string, object>();
        public T Load<T>(string name)
        {
            if (_cache.ContainsKey(name))
            {
                return (T)_cache[name];
            }

            var x = LoadInternal<T>(name);
            _cache[name] = x;
            return x;
        }
        T LoadInternal<T>(string name)
        {
            if (typeof(T) == typeof(IModel))
            {
                return (T)ModelAdapter.GetAdapter(Manager.Load<XModel>(name));
            }
            if (typeof(T) == typeof(ITexture2D))
            {
                return (T)Texture2DAdapter.GetAdapter(Manager.Load<XTexture2D>(name));
            }
            if (typeof(T) == typeof(IFont))
            {
                return (T)SpriteFontAdapter.GetAdapter(Manager.Load<Microsoft.Xna.Framework.Graphics.SpriteFont>(name));
            }
            if (typeof(T) == typeof(ISong))
            {
                return (T)SongAdapter.GetAdapter(Manager.Load<Microsoft.Xna.Framework.Media.Song>(name));
            }
            if (typeof(T) == typeof(ISoundEffect))
            {
                return (T)SoundEffectAdapter.GetAdapter(Manager.Load<Microsoft.Xna.Framework.Audio.SoundEffect>(name));
            }

            return Manager.Load<T>(name);
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
            var texture = new Microsoft.Xna.Framework.Graphics.Texture2D(Device, 1, 1);
            texture.SetData(data.ToXna());
            return Texture2DAdapter.GetAdapter(texture);
        }
    }
}

