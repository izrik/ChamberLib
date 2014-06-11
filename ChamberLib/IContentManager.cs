using System;

namespace ChamberLib
{
    public interface IContentManager
    {
        T Load<T>(string name);

        ITexture2D CreateTexture(int width, int height, Color[] data);
    }

    public static class IContentManagerHelper
    {
        public static T LoadIfNotNull<T>(this IContentManager content, string name)
        {
            if (name != null)
            {
                return content.Load<T>(name);
            }

            return default(T);
        }


        public static IModel LoadModel(this IContentManager content, string name)
        {
            return content.Load<IModel>(name);
        }
        public static ITexture2D LoadTexture2D(this IContentManager content, string name)
        {
            return content.Load<ITexture2D>(name);
        }
        public static IFont LoadFont(this IContentManager content, string name)
        {
            return content.Load<IFont>(name);
        }
        public static ISong LoadSong(this IContentManager content, string name)
        {
            return content.Load<ISong>(name);
        }
        public static ISoundEffect LoadSoundEffect(this IContentManager content, string name)
        {
            return content.Load<ISoundEffect>(name);
        }
    }
}

