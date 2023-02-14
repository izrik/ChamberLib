
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
    public class ButtonEntity : Entity2D
    {
        public ButtonEntity(Entity2D entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            _entity = entity;
        }

        Entity2D _entity;
        public Entity2D Entity
        {
            get { return _entity; }
        }

        public override Vector2 Position
        {
            get { return _entity.Position; }
            set { _entity.Position = value; }
        }

        public override Vector2 Size
        {
            get { return _entity.Size; }
        }
        public override void SetSize(Vector2 value)
        {
            _entity.SetSize(value);
        }

        public override RectangleF GetBounds()
        {
            return _entity.GetBounds();
        }

        public override void Draw(GameTime gameTime,
            ComponentCollection components)
        {
            _entity.Draw(gameTime, components);
        }

        enum Action
        {
            Press,
        }

        InputMapper<Action> _mapper = GenerateMapper();

        private static InputMapper<Action> GenerateMapper()
        {
            InputMapper<Action> mapper = new InputMapper<Action>();

            mapper.AddMap(Buttons.A, Transition.Pressed, Action.Press);
            mapper.AddMap(Keys.A, Transition.Pressed, Action.Press);

            return mapper;
        }

        public bool RespondsToMouseOnly = false;

        public override void Update(GameTime gameTime, InputSource inputSource,
            ComponentCollection components)
        {
            Pressed = false;
            Down = false;
            _mapper.InputSource = inputSource;

            if ((_mapper.Get(Action.Press) && !RespondsToMouseOnly) ||
                MouseClickInBounds(inputSource))
            {
                Pressed = true;
                if (Press != null)
                {
                    Press(gameTime, components);
                }
            }

            if (MouseDownInBounds(inputSource))
            {
                Down = true;
            }
        }

        public event System.Action<GameTime, ComponentCollection> Press;
        public bool Pressed;
        public bool Down;

        public bool MouseClickInBounds(InputSource source)
        {
            if (!source.IsButtonPressed(MouseButtons.Left)) return false;
            return GetBounds().Contains(source.CurrentMouseState.Position);
        }

        public bool MouseDownInBounds(InputSource source)
        {
            if (!source.IsButtonDown(MouseButtons.Left)) return false;
            return GetBounds().Contains(source.CurrentMouseState.Position);
        }
    }
}
