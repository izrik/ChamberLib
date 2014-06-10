using System;
using XContentManager = Microsoft.Xna.Framework.Content.ContentManager;
using XTexture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using XModel = Microsoft.Xna.Framework.Graphics.Model;
using XDevice = Microsoft.Xna.Framework.Graphics.GraphicsDevice;

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

        public T Load<T>(string name)
        {
            if (typeof(T) == typeof(IModel))
            {
                return (T)LoadModel(name);
            }
            if (typeof(T) == typeof(ITexture2D))
            {
                return (T)LoadTexture2D(name);
            }
            if (typeof(T) == typeof(IFont))
            {
                return (T)LoadFont(name);
            }
            if (typeof(T) == typeof(ISong))
            {
                return (T)LoadSong(name);
            }
            if (typeof(T) == typeof(ISoundEffect))
            {
                return (T)LoadSoundEffect(name);
            }

            return Manager.Load<T>(name);
        }

        public IModel LoadModel(string name)
        {
            return ModelAdapter.GetAdapter(Manager.Load<XModel>(name));
        }
        public ITexture2D LoadTexture2D(string name)
        {
            return Texture2DAdapter.GetAdapter(Manager.Load<XTexture2D>(name));
        }
        public IFont LoadFont(string name)
        {
            return SpriteFontAdapter.GetAdapter(Manager.Load<Microsoft.Xna.Framework.Graphics.SpriteFont>(name));
        }
        public ISong LoadSong(string name)
        {
            return SongAdapter.GetAdapter(Manager.Load<Microsoft.Xna.Framework.Media.Song>(name));
        }
        public ISoundEffect LoadSoundEffect(string name)
        {
            return SoundEffectAdapter.GetAdapter(Manager.Load<Microsoft.Xna.Framework.Audio.SoundEffect>(name));
        }

        public ITexture2D CreateTexture(int width, int height, Color[] data)
        {
            var texture = new Microsoft.Xna.Framework.Graphics.Texture2D(Device, 1, 1);
            texture.SetData(data.ToXna());
            return Texture2DAdapter.GetAdapter(texture);
        }
    }
}

