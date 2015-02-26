using System;
using System.Drawing;

namespace ChamberLib
{
    public static class BasicTextureLoader
    {
        public static TextureContent LoadTexture(string filename)
        {
            if (String.IsNullOrEmpty(filename)) throw new ArgumentException(filename);

            var bmp = new Bitmap(filename);

            var texture = new TextureContent(bmp);

            return texture;
        }
    }
}

