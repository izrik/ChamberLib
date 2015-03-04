using System;
using System.IO;

namespace ChamberLib.Content
{
    public class BasicTextureImporter
    {
        public TextureContent ImportTexture(string filename, IContentImporter importer)
        {
            if (File.Exists(filename))
            {
            }
            else if (File.Exists(filename + ".png"))
            {
                filename = filename + ".png";
            }
            else if (File.Exists(filename + ".jpg"))
            {
                filename = filename + ".jpg";
            }
            else if (File.Exists(filename + ".gif"))
            {
                filename = filename + ".gif";
            }
            else if (File.Exists(filename + ".bmp"))
            {
                filename = filename + ".bmp";
            }
            else
            {
                throw new FileNotFoundException("Could not find texture file", filename);
            }

            return BasicTextureLoader.LoadTexture(filename);
        }
    }
}

