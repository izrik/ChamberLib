
//
// ChamberLib, a cross-platform game engine
// Copyright (C) 2021 izrik and Metaphysics Industries, Inc.
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301
// USA
//

namespace ChamberLib
{
    public class SpriteEntity : Entity2D
    {
        public SpriteEntity(ITexture2D texture, Color? color=null, Vector2? position=null, Vector2? size=null)
        {
            Color _color = (color.HasValue ? color.Value : Color.White);
            Vector2 _position = (position.HasValue ? position.Value : Vector2.Zero);
            Vector2 _size = Vector2.Zero;
            if (size.HasValue)
                _size = size.Value;
            else if (texture != null)
            {
                _size = texture.GetSize();
                _size.X /= 800.0f;
                _size.Y /= 480.0f;
            }

            Texture = texture;
            Color = _color;
            Position = _position;
            SetSize(_size);
        }

        public ITexture2D Texture { get; set; }
        public Color Color = Color.White;

        static readonly DrawImagesEntry[] __Draw_drawImagesEntry = new DrawImagesEntry[1];
        public override void Draw(GameTime gameTime,
            ComponentCollection components)
        {
            if (Texture == null) return;

            var renderer = components.Get<IRenderer>();
            var bounds = GetBounds();
            bounds.Left *= renderer.Viewport.Width;
            bounds.Width *= renderer.Viewport.Width;
            bounds.Top *= renderer.Viewport.Height;
            bounds.Height *= renderer.Viewport.Height;
            __Draw_drawImagesEntry[0] = new DrawImagesEntry(Texture, bounds.RoundToInt(), Color);
            renderer.DrawImages(__Draw_drawImagesEntry);
        }

        Vector2 _size;
        public override Vector2 Size
        {
            get
            {
                if (Sizing == SizeMode.ResizeEntityToImage)
                {
                    var size = Texture.GetSize();
                    size.X /= 800;
                    size.Y /= 480;
                    return size;
                }
                else
                {
                    return _size;
                }
            }
        }
        public override void SetSize(Vector2 value)
        {
            _size = value;
        }

        public override RectangleF GetBounds()
        {
            return new RectangleF(Position, Size.X, Size.Y);
        }

        public void SetBounds(RectangleF bounds)
        {
            Position = bounds.TopLeft;
            SetSize(bounds.Size);
        }

        public SizeMode Sizing = SizeMode.ResizeImageToEntity;
        public enum SizeMode
        {
            ResizeEntityToImage,
            ResizeImageToEntity,
        }
    }
}
