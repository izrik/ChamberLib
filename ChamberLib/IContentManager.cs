using System;

namespace ChamberLib
{
    public interface IContentManager
    {
        T Load<T>(string name);

        IModel LoadModel(string name);
        ITexture2D LoadTexture2D(string name);
        IFont LoadFont(string name);
        ISong LoadSong(string name);
        ISoundEffect LoadSoundEffect(string name);

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
    }
}

