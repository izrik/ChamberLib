using System;

namespace ChamberLib
{
    public interface IContentManager
    {
        IModel LoadModel(string name, string relativeTo=null);
        ITexture2D LoadTexture2D(string name, string relativeTo=null);
        IFont LoadFont(string name, string relativeTo=null);
        ISong LoadSong(string name, string relativeTo=null);
        ISoundEffect LoadSoundEffect(string name, string relativeTo=null);
        IShader LoadShader(string name, string relativeTo=null, object bindattrs=null);

        string LookupObjectName(object o);

        ITexture2D CreateTexture(int width, int height, Color[] data);
    }

    public static class IContentManagerHelper
    {
        public static IModel LoadModelIfNotNull(this IContentManager content, string name)
        {
            if (name != null)
            {
                return content.LoadModel(name);
            }

            return null;
        }
        public static ITexture2D LoadTexture2DIfNotNull(this IContentManager content, string name)
        {
            if (name != null)
            {
                return content.LoadTexture2D(name);
            }

            return null;
        }
        public static IFont LoadFontIfNotNull(this IContentManager content, string name)
        {
            if (name != null)
            {
                return content.LoadFont(name);
            }

            return null;
        }
        public static ISong LoadSongIfNotNull(this IContentManager content, string name)
        {
            if (name != null)
            {
                return content.LoadSong(name);
            }

            return null;
        }
        public static ISoundEffect LoadSoundEffectIfNotNull(this IContentManager content, string name)
        {
            if (name != null)
            {
                return content.LoadSoundEffect(name);
            }

            return null;
        }
        public static IShader LoadShaderIfNotNull(this IContentManager content, string name)
        {
            if (name != null)
            {
                return content.LoadShader(name);
            }

            return null;
        }
    }
}

