using System;

namespace ChamberLib.Content
{
    public interface IContentProcessor
    {
        IModel ProcessModel(ModelContent asset, IContentProcessor processor=null);
        ITexture2D ProcessTexture2D(TextureContent asset, IContentProcessor processor=null);
        IFont ProcessFont(FontContent asset, IContentProcessor processor=null);
        ISong ProcessSong(SongContent asset, IContentProcessor processor=null);
        ISoundEffect ProcessSoundEffect(SoundEffectContent asset, IContentProcessor processor=null);
        IShaderStage ProcessShaderStage(ShaderContent asset, IContentProcessor processor=null);

        IShaderProgram MakeShaderProgram(IShaderStage vertexShader,
            IShaderStage fragmentShader, string[] bindattrs=null);
    }
}

