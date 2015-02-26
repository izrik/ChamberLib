using System;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Linq;

namespace ChamberLib
{
    public class TextureAdapter : ITexture2D
    {
        public TextureAdapter()
        {
            Bitmap = null;
            ID = -1;
            Width = 1;
            Height = 1;
            IsReady = false;
        }
        public TextureAdapter(TextureContent texture)
            : this(texture.Bitmap)
        {
        }
        protected TextureAdapter(Bitmap bitmap)
        {
            Bitmap = bitmap;
            Width = bitmap.Width;
            Height = bitmap.Height;
            ID = -1;
            IsReady = false;
        }

        public readonly Bitmap Bitmap;
        public readonly int Width;
        public readonly int Height;
        public int ID { get; protected set; }
        public bool IsReady { get; protected set; }

        #region ITexture2D implementation

        public Vector2 GetSize()
        {
            return new Vector2(Width, Height);
        }

        static readonly Dictionary<string, TextureAdapter> texturesByName = new Dictionary<string, TextureAdapter>();

        #endregion

        public static ITexture2D CreateTexture(int width, int height, Color[] data)
        {
            var bmp = new Bitmap(width, height);

            if (data.Length > 0)
            {
                var bmpdata = bmp.LockBits(
                              new Rectangle(0, 0, width, height),
                              ImageLockMode.WriteOnly,
                              System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                var bytes = data.SelectMany<Color, byte>(c => new byte[] { c.A, c.R, c.G, c.B }).ToArray();
                Marshal.Copy(bytes, 0, bmpdata.Scan0, Math.Min(width * height * 4, bytes.Length)); // ignore stride for now
                bmp.UnlockBits(bmpdata);
            }

            return new TextureAdapter(bmp);
        }

        protected void MakeReady()
        {
            BitmapData bmp_data = Bitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            try
            {

                int id = GL.GenTexture();
                this.ID = id;
                var lastid = GL.GetInteger(GetPName.TextureBinding2D);
                GL.BindTexture(TextureTarget.Texture2D, id);

                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp_data.Width, bmp_data.Height, 0,
                    OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmp_data.Scan0);
            }
            finally
            {
                Bitmap.UnlockBits(bmp_data);
            }

            // We haven't uploaded mipmaps, so disable mipmapping (otherwise the texture will not appear).
            // On newer video cards, we can use GL.GenerateMipmaps() or GL.Ext.GenerateMipmaps() to create
            // mipmaps automatically. In that case, use TextureMinFilter.LinearMipmapLinear to enable them.
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            IsReady = true;
        }

        public void Apply()
        {
            if (!IsReady)
            {
                MakeReady();
            }

            var lastid = GL.GetInteger(GetPName.TextureBinding2D);
            GL.BindTexture(TextureTarget.Texture2D, this.ID);
        }

        public void UnApply()
        {
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }
    }
}

