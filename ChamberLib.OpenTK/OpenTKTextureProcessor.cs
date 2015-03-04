using System;
using ChamberLib.Content;

namespace ChamberLib
{
    public class OpenTKTextureProcessor
    {
        public TextureAdapter ProcessTexture(TextureContent textureContent, IContentProcessor processor)
        {
            return new TextureAdapter(textureContent);
        }
    }
}

