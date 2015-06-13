using System;
using OpenTK.Graphics.OpenGL;
using ChamberLib.Content;

using _OpenTK = global::OpenTK;

namespace ChamberLib.OpenTK
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

            if (texture.PixelFormat.HasValue)
            {
                this.PixelFormat = texture.PixelFormat.Value;
            }
            else
            {
                this.PixelFormat = PixelFormat.Rgba;
            }
        }

        public readonly int Width;
        public readonly int Height;
        public Color[] PixelData;
        public int ID { get; protected set; }
        public bool IsReady { get; protected set; }
        public readonly PixelFormat PixelFormat;

        #region ITexture2D implementation

        public Vector2 GetSize()
        {
            return new Vector2(Width, Height);
        }

        #endregion

        protected void MakeReady()
        {
            int id = GL.GenTexture();
            this.ID = id;
            var lastid = GL.GetInteger(GetPName.TextureBinding2D);
            GL.BindTexture(TextureTarget.Texture2D, id);

            int n;
            int i;

            switch (PixelFormat)
            {
            case PixelFormat.Rgba:
                n = PixelData.Length * 4;
                var bytes = new byte[n];
                for (i = 0; i < n; i += 4)
                {
                    var c = PixelData[i / 4];
                    bytes[i] = c.A;
                    bytes[i + 1] = c.R;
                    bytes[i + 2] = c.G;
                    bytes[i + 3] = c.B;
                }

                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, Width, Height, 0,
                    _OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bytes);
                break;
            case PixelFormat.R32f:

                n = PixelData.Length;
                var floats = new float[n];
                for (i = 0; i < n; i++)
                {
                    floats[i] = PixelData[i].R / 255.0f;
                }

                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.R32f, Width, Height, 0,
                    _OpenTK.Graphics.OpenGL.PixelFormat.Red, PixelType.Float, floats);
                break;
            default:
                throw new NotImplementedException(
                    string.Format(
                        "No support for pixel format \"{0}\"",
                        PixelFormat));
            }

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            GL.BindTexture(TextureTarget.Texture2D, 0);

            IsReady = true;
        }

        int _lastTextureSlot;
        public void Apply(int textureSlot=0)
        {
            if (!IsReady)
            {
                MakeReady();
            }

            _lastTextureSlot = textureSlot;

            var unit = (TextureUnit)((int)TextureUnit.Texture0 + textureSlot);
            GL.ActiveTexture(unit);

            var lastid = GL.GetInteger(GetPName.TextureBinding2D);
            GL.BindTexture(TextureTarget.Texture2D, this.ID);
            GL.ActiveTexture(TextureUnit.Texture0);
        }

        public void UnApply()
        {
            GL.ActiveTexture((TextureUnit)((int)TextureUnit.Texture0 + _lastTextureSlot));
            GL.BindTexture(TextureTarget.Texture2D, 0);
            GL.ActiveTexture(TextureUnit.Texture0);
        }
    }
}

