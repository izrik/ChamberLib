
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
    public class MenuItem<T>
    {
        public MenuItem(T value, Entity2D entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            Value = value;
            Entity = entity;
        }

        public MenuItem(T value, Entity2D entity, Enabler<T> enabler, Color enabledColor, Color disabledColor)
            : this(value, entity)
        {
            Enabler = enabler;
            EnabledColor = enabledColor;
            DisabledColor = disabledColor;
        }

        private bool _isEnabled = true;
        public bool IsEnabled
        {
            get
            {
                if (Enabler != null)
                {
                    return Enabler(this);
                }
                else
                {
                    return _isEnabled;
                }
            }
            set { _isEnabled = value; }
        }
        public T Value { get; protected set; }
        public Entity2D Entity { get; protected set; }

        public Enabler<T> Enabler;
        public Color? EnabledColor;
        public Color? DisabledColor;

        public bool PositionedByParent = false;



        public Vector2 GetSize()
        {
            return Entity.Size;
        }
        public void SetPosition(Vector2 value)
        {
            Entity.Position = value;
        }
        public RectangleF GetBounds()
        {
            return Entity.GetBounds();
        }

        public void Draw(GameTime gameTime, ComponentCollection components)
        {
            if (this.Entity is TextEntity && this.EnabledColor.HasValue && this.DisabledColor.HasValue)
            {
                if (this.IsEnabled)
                {
                    (this.Entity as TextEntity).Color = this.EnabledColor.Value;
                }
                else
                {
                    (this.Entity as TextEntity).Color = this.DisabledColor.Value;
                }
            }

            this.Entity.Draw(gameTime, components);
        }
        
        public void Update(GameTime gameTime, InputSource source,
            ComponentCollection components)
        {
            this.Entity.Update(gameTime, source, components);
        }
    }

    public delegate bool Enabler<T>(MenuItem<T> item);
}
