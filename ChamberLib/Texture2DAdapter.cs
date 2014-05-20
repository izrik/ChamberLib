using System;

namespace ChamberLib
{
    public class Texture2DAdapter : ITexture2D
    {
        public Texture2DAdapter(Microsoft.Xna.Framework.Graphics.Texture2D texture)
        {
            Texture = texture;
        }

        public Microsoft.Xna.Framework.Graphics.Texture2D Texture;

        public Vector2 GetSize()
        {
            return new Vector2(Texture.Width, Texture.Height);
        }
    }
}

