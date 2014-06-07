using System;
using XContentManager = Microsoft.Xna.Framework.Content.ContentManager;
using XTexture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using XModel = Microsoft.Xna.Framework.Graphics.Model;

namespace ChamberLib
{
    public class ContentManager : IContentManager
    {
        public ContentManager(XContentManager manager, string rootDirectory=null)
        {
            if (manager == null)
                throw new ArgumentNullException("manager");

            Manager = manager;

            if (rootDirectory != null)
            {
                Manager.RootDirectory = rootDirectory;
            }
        }

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
        public object LoadSong(string name)
        {
            return Manager.Load<Microsoft.Xna.Framework.Media.Song>(name);
        }
    }
}

