
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
    public class DrawLineEntity : Entity2D
    {
        public DrawLineEntity(IRenderer renderer)
        {
            if (renderer == null) throw new ArgumentNullException("renderer");

            _renderer = renderer;
        }
        public DrawLineEntity(IRenderer renderer, Color color, Vector2 p1, Vector2 p2)
            : this(renderer)
        {
            Color = color;
            P1 = p1;
            P2 = p2;
        }

        IRenderer _renderer;

        public Color Color = Color.White;
        public Vector2 P1;
        public Vector2 P2;

        public override void Draw(GameTime gameTime,
            ComponentCollection components)
        {
            var p1 = P1;
            var p2 = P2;
            p1.X *= _renderer.Viewport.Width;
            p1.Y *= _renderer.Viewport.Height;
            p2.X *= _renderer.Viewport.Width;
            p2.Y *= _renderer.Viewport.Height;
            _renderer.DrawLine(Color, p1, p2);
        }

        public override Vector2 Size
        {
            get
            {
                return new Vector2(
                    Math.Abs(P1.X - P2.X),
                    Math.Abs(P1.Y - P2.Y));
            }
        }
    }
}

