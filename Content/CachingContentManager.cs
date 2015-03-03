using System;

namespace ChamberLib.Content
{
    public class CachingContentManager : IContentManager
    {
        public CachingContentManager(IContentManager next)
        {
            if (next == null) throw new ArgumentNullException("next");

            this.next = next;

            models = new Cache<string, IModel>(next.LoadModel);
            textures = new Cache<string, ITexture2D>(next.LoadTexture2D);
            shaders = new Cache<string, object, IShader>(next.LoadShader);
            fonts = new Cache<string, IFont>(next.LoadFont);
            songs = new Cache<string, ISong>(next.LoadSong);
            soundEffects = new Cache<string, ISoundEffect>(next.LoadSoundEffect);
        }

        readonly IContentManager next;

        readonly Cache<string, IModel> models;
        readonly Cache<string, ITexture2D> textures;
        readonly Cache<string, object, IShader> shaders;
        readonly Cache<string, IFont> fonts;
        readonly Cache<string, ISong> songs;
        readonly Cache<string, ISoundEffect> soundEffects;

        public IModel LoadModel(string name)
        {
            return models.Call(name);
        }

        public ITexture2D LoadTexture2D(string name)
        {
            return textures.Call(name);
        }

        public IShader LoadShader(string name, object bindattrs = null)
        {
            return shaders.Call(name, bindattrs);
        }

        public IFont LoadFont(string name)
        {
            return fonts.Call(name);
        }

        public ISong LoadSong(string name)
        {
            return songs.Call(name);
        }

        public ISoundEffect LoadSoundEffect(string name)
        {
            return soundEffects.Call(name);
        }

        public string LookupObjectName(object o)
        {
            return next.LookupObjectName(o);
        }

        public ITexture2D CreateTexture(int width, int height, Color[] data)
        {
            return next.CreateTexture(width, height, data);
        }

        public string ResolveTextureFilename(string name)
        {
            return next.ResolveTextureFilename(name);
        }

        public void ResolveShaderFilenames(string name, out string vertexShaderFilename, out string fragmentShaderFilename)
        {
            next.ResolveShaderFilenames(name, out vertexShaderFilename, out fragmentShaderFilename);
        }
    }
}

