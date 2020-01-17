using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ChamberLib.Content
{
    public class TextureContent
    {
        public TextureContent(Bitmap bitmap)
        {
            // performance
            if (bitmap == null) throw new ArgumentNullException("bitmap");

            Width = bitmap.Width;
            Height = bitmap.Height;

            var bmpdata = bitmap.LockBits(new Rectangle(0, 0, Width, Height),
                ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            var bytes = new byte[bmpdata.Width * bmpdata.Height * 4];

            try
            {
                Marshal.Copy(bmpdata.Scan0, bytes, 0, bytes.Length); // ignore stride for now
            }
            finally
            {
                bitmap.UnlockBits(bmpdata);
            }

            PixelData = new Color[bitmap.Width * bitmap.Height];
            int i;
            int n = bmpdata.Width * bmpdata.Height * 4;
            for (i = 0; i < n; i+=4)
            {
                // components in the byte array are in the order of A, R, G, B
                var c = new Color(
                                bytes[i + 1],
                                bytes[i + 2],
                                bytes[i + 3],
                                bytes[i]);
                PixelData[i / 4] = c;
            }
        }
        public TextureContent(int width, int height, Color[] pixelData)
        {
            Width = width;
            Height = height;
            PixelData = pixelData;
        }

        public int Width;
        public int Height;
        public Color[] PixelData;
        public PixelFormat? PixelFormat;
    }
}

