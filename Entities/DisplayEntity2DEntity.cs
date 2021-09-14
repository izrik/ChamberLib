
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
    /// <summary>
    /// Simply displays an Entity2D. The Entity2D is not altered or moved.
    /// </summary>
    public class DisplayEntity2DEntity : Entity
    {
        public DisplayEntity2DEntity(Entity2D entity2D)
        {
            Entity2D = entity2D;
        }

        public readonly Entity2D Entity2D;

        public override void Draw(GameTime gameTime,
            ComponentCollection components, Overrides overrides=default)
        {
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
