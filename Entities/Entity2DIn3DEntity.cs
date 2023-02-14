
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
    /// <summary>
    /// Situates an Entity2D within 3D space. The Entity2D's position on the
    /// screen tracks with its 3D position.
    /// </summary>
    public class Entity2DIn3DEntity : Entity
    {
        public Entity2DIn3DEntity(Entity2D entity2D)
        {
            Entity2D = entity2D;
        }

        public readonly Entity2D Entity2D;

        public override void Draw(GameTime gameTime,
            ComponentCollection components, Overrides overrides=default)
        {
            var renderer = components.Get<IRenderer>();
            var camera = components.Get<ICamera>();
            Vector4 pos = new Vector4(0, 0, 0, 1);
            pos = Vector4.Transform(pos, this.Transform);
            pos = Vector4.Transform(pos, camera.View);
            pos = Vector4.Transform(pos, camera.Projection);

            var hpos = pos.ToVectorXYZ() / pos.W;
            hpos.Y = -hpos.Y;
            var pos2d = (hpos.ToVector2() + Vector2.One) / 2;
            Entity2D.Position = pos2d;

            Entity2D.Draw(gameTime, components);
        }

        public override void Update(GameTime gameTime,
            ComponentCollection components)
        {
            var source = components.Get<InputSource>();
            Entity2D.Update(gameTime, source, components);
        }

        public override bool CastsShadow => false;
    }
}
