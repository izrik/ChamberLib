using System;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.IO;

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

        static string GetContentFilename(string name)
        {
            return Path.Combine("Content.OpenTK", name).Replace('\\', '/');
        }

        public static ITexture2D LoadTextureFromFile(string name)
        {
            if (String.IsNullOrEmpty(name)) throw new ArgumentException(name);

            string contentName = GetContentFilename(name);

            if (texturesByName.ContainsKey(contentName))
            {
                return texturesByName[contentName];
            }

            string filename;

            if (File.Exists(contentName))
            {
                filename = contentName;
            }
            else if (File.Exists(contentName + ".png"))
            {
                filename = contentName + ".png";
            }
            else if (File.Exists(contentName + ".jpg"))
            {
                filename = contentName + ".jpg";
            }
            else if (File.Exists(contentName + ".gif"))
            {
                filename = contentName + ".gif";
            }
            else if (File.Exists(contentName + ".bmp"))
            {
                filename = contentName + ".bmp";
            }
            else
            {
                throw new FileNotFoundException("Could not fine texture file", contentName);
            }

            var bmp = new Bitmap(filename);

            var texture = new TextureAdapter(bmp);

            texturesByName[contentName] = texture;

            return texture;
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

        public void Bind()
        {
            if (!IsReady)
            {
                MakeReady();
            }

            var lastid = GL.GetInteger(GetPName.TextureBinding2D);
            GL.BindTexture(TextureTarget.Texture2D, this.ID);
        }

        public static void Unbind()
        {
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }
    }
}

