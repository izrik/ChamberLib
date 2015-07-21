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
            shaders = new Cache<string, object, IShaderProgram>(next.LoadShader);
            shaderStages = new Cache2<string, ShaderType, IShaderStage>(next.LoadShaderStage);
            shaders = new Cache<string, object, IShaderProgram>(next.LoadShader);
            fonts = new Cache<string, IFont>(next.LoadFont);
            songs = new Cache<string, ISong>(next.LoadSong);
            soundEffects = new Cache<string, ISoundEffect>(next.LoadSoundEffect);
        }

        readonly IContentManager next;

        readonly Cache<string, IModel> models;
        readonly Cache<string, ITexture2D> textures;
        readonly Cache<string, object, IShaderProgram> shaders;
        readonly Cache2<string, ShaderType, IShaderStage> shaderStages;
        readonly Cache<string, IFont> fonts;
        readonly Cache<string, ISong> songs;
        readonly Cache<string, ISoundEffect> soundEffects;

        public IContentImporter Importer { get { return next.Importer; } }
        public IContentProcessor Processor { get { return next.Processor; } }

        public IModel LoadModel(string name)
        {
            return models.Call(name);
        }

        public ITexture2D LoadTexture2D(string name)
        {
            return textures.Call(name);
        }

        public IShaderProgram LoadShader(string name, object bindattrs = null)
        {
            return shaders.Call(name, bindattrs);
        }

        public IShaderStage LoadShaderStage(string name, ShaderType type)
        {
            return shaderStages.Call(name, type);
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
            if (o is IModel)
            {
                var s = models.LookupObject((IModel)o);
                if (s != null) return s;
            }
            if (o is ITexture2D)
            {
                var s = textures.LookupObject((ITexture2D)o);
                if (s != null) return s;
            }
            if (o is IShaderProgram)
            {
                var s = shaders.LookupObject((IShaderProgram)o);
                if (s != null) return s;
            }
            if (o is IFont)
            {
                var s = fonts.LookupObject((IFont)o);
                if (s != null) return s;
            }
            if (o is ISong)
            {
                var s = songs.LookupObject((ISong)o);
                if (s != null) return s;
            }
            if (o is ISoundEffect)
            {
                var s = soundEffects.LookupObject((ISoundEffect)o);
                if (s != null) return s;
            }

            return next.LookupObjectName(o);
        }

        public ITexture2D CreateTexture(int width, int height, Color[] data,
            PixelFormat pixelFormat=PixelFormat.Rgba)
        {
            return next.CreateTexture(width, height, data, pixelFormat);
        }

        public IShaderProgram MakeShaderProgram(IShaderStage vertexShader,
            IShaderStage fragmentShader, string[] bindattrs=null)
        {
            return next.MakeShaderProgram(vertexShader, fragmentShader, bindattrs);
        }
    }
}

