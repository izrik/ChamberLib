using System;
using System.Drawing;

namespace ChamberLib.Content
{
    public class TextureContent
    {
        public TextureContent(Bitmap bitmap)
        {
            if (bitmap == null) throw new ArgumentNullException("bitmap");

            this.Bitmap = bitmap;
        }

        public Bitmap Bitmap;
    }
}

