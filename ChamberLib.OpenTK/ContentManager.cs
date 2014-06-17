using System;

namespace ChamberLib
{
    public class ContentManager : IContentManager
    {
        public T Load<T>(string name)
        {
            if (typeof(T) == typeof(IModel))
            {
                return (T)(object)new Model();
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
                return (T)(object)new TextureAdapter();
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
    }
}

