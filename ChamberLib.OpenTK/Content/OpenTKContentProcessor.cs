using ChamberLib.Content;
using ChamberLib.OpenTK.Audio;
using ChamberLib.OpenTK.Images;
using ChamberLib.OpenTK.Materials;
using ChamberLib.OpenTK.Models;
using ChamberLib.OpenTK.Text;

namespace ChamberLib.OpenTK.Content
{
    public class OpenTKContentProcessor : IContentProcessor
    {
        public IModel ProcessModel(ModelContent asset, IContentProcessor processor = null)
        {
            return new Model(asset, processor);
        }
        public ITexture2D ProcessTexture2D(TextureContent asset, IContentProcessor processor = null)
        {
            return new TextureAdapter(asset);
        }
        public IFont ProcessFont(FontContent asset, IContentProcessor processor = null)
        {
            return new FontAdapter(asset);
        }
        public ISong ProcessSong(SongContent asset, IContentProcessor processor = null)
        {
            return new Song(asset);
        }
        public ISoundEffect ProcessSoundEffect(SoundEffectContent asset, IContentProcessor processor = null)
        {
            return new SoundEffect(asset);
        }
        public IShaderStage ProcessShaderStage(ShaderContent asset, IContentProcessor processor = null)
        {
            if (asset == BuiltinShaders.BasicVertexShaderContent)
            {
                return BuiltinShaders.BasicVertexShaderStage;
            }
            if (asset == BuiltinShaders.SkinnedVertexShaderContent)
            {
                return BuiltinShaders.SkinnedVertexShaderStage;
            }
            if (asset == BuiltinShaders.BasicFragmentShaderContent)
            {
                return BuiltinShaders.BasicFragmentShaderStage;
            }

            return new ShaderStage(asset);
        }
    }
}

