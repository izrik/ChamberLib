using System;

namespace ChamberLib.Content
{
    public delegate IModel ModelProcessor(ModelContent asset, IContentProcessor processor);
    public delegate ITexture2D TextureProcessor(TextureContent asset, IContentProcessor processor);
    public delegate IShaderStage ShaderStageProcessor(ShaderContent asset, IContentProcessor processor);
    public delegate IFont FontProcessor(FontContent asset, IContentProcessor processor);
    public delegate ISong SongProcessor(SongContent asset, IContentProcessor processor);
    public delegate ISoundEffect SoundEffectProcessor(SoundEffectContent asset, IContentProcessor processor);
}

