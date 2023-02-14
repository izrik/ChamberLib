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
            if (bitmap == null) throw new ArgumentNullException("bitmap");

            Width = bitmap.Width;
            Height = bitmap.Height;

            var bmpdata = bitmap.LockBits(new Rectangle(0, 0, Width, Height),
                ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            PixelData = new Color[bitmap.Width * bitmap.Height];

            try
            {
                int i;
                int j;
                int n = bmpdata.Width * 4;
                var bytes = new byte[n];
                for (j = 0; j < bmpdata.Height; j++)
                {
                    Marshal.Copy(IntPtr.Add(bmpdata.Scan0, j * bmpdata.Stride), bytes, 0, bytes.Length);
                    for (i = 0; i < n; i += 4)
                    {
                        // BGRA - reverse of Argb above.
                        // Must be a little/big endianness thing.
                        var c = new Color(
                                        r: bytes[i + 2],
                                        g: bytes[i + 1],
                                        b: bytes[i + 0],
                                        a: bytes[i + 3]);
                        PixelData[j * bitmap.Width + i / 4] = c;
                    }
                }
            }
            finally
            {
                bitmap.UnlockBits(bmpdata);
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

