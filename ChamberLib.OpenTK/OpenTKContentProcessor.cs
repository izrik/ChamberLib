using System;
using ChamberLib.Content;
using System.Collections.Generic;
using System.Linq;

namespace ChamberLib.OpenTK
{
    public class OpenTKContentProcessor : IContentProcessor
    {
        public OpenTKContentProcessor(Renderer renderer)
        {
            this.Renderer = renderer;
        }

        readonly Renderer Renderer;

        public IModel ProcessModel(ModelContent asset, IContentProcessor processor = null)
        {
            return new Model(asset, Renderer, processor);
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
        public IShaderProgram ProcessShader(ShaderContent asset, IContentProcessor processor = null, object bindattrs=null)
        {
            if (asset == BuiltinShaders.BasicShaderContent)
                return BuiltinShaders.BasicShader;

            if (asset == BuiltinShaders.SkinnedShaderContent)
                return BuiltinShaders.SkinnedShader;

            string[] bindattrs2=null;
            if (bindattrs == null)
            {
            }
            else if (bindattrs is IEnumerable<string>)
            {
                bindattrs2 = (bindattrs as IEnumerable<string>).ToArray();
            }
            else
            {
                throw new InvalidOperationException();
            }

            var shader = new ShaderProgram(asset, (String[])bindattrs2);
            return shader;
        }
        public IShaderStage ProcessShaderStage(ShaderContent asset, IContentProcessor processor = null)
        {
            var type = (asset.VertexShaderSource != null ?
                ShaderType.Vertex :
                ShaderType.Fragment);
            var source = asset.VertexShaderSource ?? asset.FragmentShaderSource;
            var shaderStage = new ShaderStage(source, type);
            return shaderStage;
        }

        public IShaderProgram MakeShaderProgram(IShaderStage vertexShader,
            IShaderStage fragmentShader, string[] bindattrs=null)
        {
            if (vertexShader.ShaderType != ShaderType.Vertex)
                throw new ArgumentException("Wrong shader type", "vertexShader");
            if (fragmentShader.ShaderType != ShaderType.Fragment)
                throw new ArgumentException("Wrong shader type", "fragmentShader");

            return new ShaderProgram((ShaderStage)vertexShader, (ShaderStage)fragmentShader, bindattrs);
        }
    }
}

