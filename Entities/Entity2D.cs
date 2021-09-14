
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

using System;

namespace ChamberLib
{
    public abstract class Entity2D
    {
        public Entity2D()
        {
        }

        public virtual void Begin(GameTime gameTime,
            ComponentCollection components) { }
        public abstract void Draw(GameTime gameTime,
            ComponentCollection components);
        public virtual void Update(GameTime gameTime,
            InputSource inputSource,
            ComponentCollection components) { }

        public bool MustBeRemoved { get; protected set; }

        Vector2 _position = Vector2.Zero;
        public virtual Vector2 Position
        {
            get { return _position; }
            set
            {
                if (value != _position)
                {
                    _position = value;

                    OnPositionChanged(EventArgs.Empty);
                }
            }
        }
        protected virtual void OnPositionChanged(EventArgs e) { }

        public float X
        {
            get { return Position.X; }
            set { Position = new Vector2(value, Y); }
        }
        public float Y
        {
            get { return Position.Y; }
            set { Position = new Vector2(X, value); }
        }

        public abstract Vector2 Size
        {
            get;
        }

        public virtual void SetSize(Vector2 value) { }

        public float Width { get { return Size.X; } }
        public float Height { get { return Size.Y; } }

        public virtual RectangleF GetBounds()
        {
            return new RectangleF(Position, Size.X, Size.Y);
        }
    }
}
