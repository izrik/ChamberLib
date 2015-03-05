using System;
using OpenTK.Graphics.OpenGL;
using ChamberLib.Content;

namespace ChamberLib
{
    public class TextureAdapter : ITexture2D
    {
        public TextureAdapter(TextureContent texture)
        {
            Width = texture.Width;
            Height = texture.Height;
            PixelData = texture.PixelData;
            ID = -1;
            IsReady = false;
        }

        public readonly int Width;
        public readonly int Height;
        public Color[] PixelData;
        public int ID { get; protected set; }
        public bool IsReady { get; protected set; }

        #region ITexture2D implementation

        public Vector2 GetSize()
        {
            return new Vector2(Width, Height);
        }

        #endregion

        protected void MakeReady()
        {
            int n = PixelData.Length * 4;
            var bytes = new byte[n];
            int i;
            for (i = 0; i < n; i+=4)
            {
                var c = PixelData[i/4];
                bytes[i] = c.A;
                bytes[i + 1] = c.R;
                bytes[i + 2] = c.G;
                bytes[i + 3] = c.B;
            }

            int id = GL.GenTexture();
            this.ID = id;
            var lastid = GL.GetInteger(GetPName.TextureBinding2D);
            GL.BindTexture(TextureTarget.Texture2D, id);


            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, Width, Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bytes);

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

