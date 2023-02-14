
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
    public class DrawLine3DEntity : Entity
    {
        public DrawLine3DEntity(IRenderer renderer, Vector3 a, Vector3 b,
            Color? color=null)
        {
            if (renderer == null) throw new ArgumentNullException("renderer");

            _renderer = renderer;
            this.a = a;
            this.b = b;
            if (!color.HasValue)
            {
                color = Color.White;
            }
            this.Color = color.Value;
        }

        IRenderer _renderer;

        public Vector3 a;
        public Vector3 b;
        public Color Color = Color.White;

        public override void Draw(GameTime gameTime, 
            ComponentCollection components,
            Overrides overrides=default(Overrides))
        {
            var camera = components.Get<ICamera>();
            _renderer.DrawLine(Color.ToVector3(), Matrix.Identity, camera.View,
                camera.Projection, a, b);
        }
    }
}
