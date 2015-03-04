using System;
using ChamberLib.Content;

namespace ChamberLib
{
    public class BuiltinContentProcessor : IContentProcessor
    {
        public BuiltinContentProcessor(IContentProcessor next)
        {
            if (next == null) throw new ArgumentNullException("next");

            this.next = next;
        }

        readonly IContentProcessor next;

        public IModel ProcessModel(ModelContent asset, IContentProcessor processor = null)
        {
            return next.ProcessModel(asset, processor);
        }
        public ITexture2D ProcessTexture2D(TextureContent asset, IContentProcessor processor = null)
        {
            return next.ProcessTexture2D(asset, processor);
        }
        public IFont ProcessFont(FontContent asset, IContentProcessor processor = null)
        {
            return new FontAdapter(asset);
        }
        public ISong ProcessSong(SongContent asset, IContentProcessor processor = null)
        {
            return next.ProcessSong(asset, processor);
        }
        public ISoundEffect ProcessSoundEffect(SoundEffectContent asset, IContentProcessor processor = null)
        {
            return next.ProcessSoundEffect(asset, processor);
        }
        public IShader ProcessShader(ShaderContent asset, IContentProcessor processor = null, object bindattrs=null)
        {
            if (asset == BuiltinShaders.BasicShaderContent)
                return BuiltinShaders.BasicShader;

            if (asset == BuiltinShaders.SkinnedShaderContent)
                return BuiltinShaders.SkinnedShader;

            return next.ProcessShader(asset, processor, bindattrs);
        }
    }
}

