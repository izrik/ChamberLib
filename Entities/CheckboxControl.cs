
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
    public class CheckboxControl : Entity2D
    {
        public CheckboxControl(IRenderer renderer, IFont font)
            : this(new TextEntity("Y", font, Color.White), new TextEntity("N", font, Color.White), renderer, font, string.Empty)
        {
        }
        public CheckboxControl(IRenderer renderer, IFont font, string label)
            : this(new TextEntity("Y", font, Color.White), new TextEntity("N", font, Color.White), renderer, font, label)
        {
        }
        public CheckboxControl(Entity2D checkedEntity, Entity2D uncheckedEntity, IRenderer renderer, IFont font)
            : this(checkedEntity, uncheckedEntity, renderer, font, string.Empty)
        {
        }
        public CheckboxControl(Entity2D checkedEntity, Entity2D uncheckedEntity, IRenderer renderer, IFont font, string label)
        {
            if (checkedEntity == null) throw new ArgumentNullException("checkedEntity");
            if (uncheckedEntity == null) throw new ArgumentNullException("uncheckedEntity");
            if (renderer == null) throw new ArgumentNullException("renderer");
            if (font == null) throw new ArgumentNullException("font");

            if (label == null) label = string.Empty;

            CheckedEntity = checkedEntity;
            UncheckedEntity = uncheckedEntity;
            LabelEntity = new TextEntity(label, font, Color.White);

            SetSize(new Vector2(400,30));
            SetEntityPositions();
        }

        public Entity2D CheckedEntity { get; protected set; }
        public Entity2D UncheckedEntity { get; protected set; }
        public TextEntity LabelEntity { get; protected set; }

        public string Label
        {
            get { return LabelEntity.Text; }
            set { LabelEntity.Text = value; }
        }

        bool _isChecked;
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                if (value != _isChecked)
                {
                    _isChecked = value;

                    OnIsCheckedChanged(EventArgs.Empty);
                }
            }
        }
        protected virtual void OnIsCheckedChanged(EventArgs e)
        {
            if (IsCheckedChanged != null)
            {
                IsCheckedChanged(this, e);
            }
        }
        public event EventHandler IsCheckedChanged;

        enum Action
        {
            Toggle,
        }

        InputMapper<Action> _mapper = GenerateDefaultMapper();

        static InputMapper<Action> GenerateDefaultMapper()
        {
            InputMapper<Action> mapper = new InputMapper<Action>();

            mapper.AddMap(Keys.Left, Transition.Pressed, Action.Toggle);
            mapper.AddMap(Buttons.DPadLeft, Transition.Pressed, Action.Toggle);
            mapper.AddMap(AnalogInput.LeftStickX, -0.5f, AnalogTransition.BecomesLess, Action.Toggle);

            mapper.AddMap(Keys.Right, Transition.Pressed, Action.Toggle);
            mapper.AddMap(Buttons.DPadRight, Transition.Pressed, Action.Toggle);
            mapper.AddMap(AnalogInput.LeftStickX, 0.5f, AnalogTransition.BecomesGreater, Action.Toggle);

            return mapper;
        }

        protected override void OnPositionChanged(EventArgs e)
        {
            SetEntityPositions();
        }

        void SetEntityPositions()
        {
            if (LabelEntity != null &&
                CheckedEntity != null &&
                UncheckedEntity != null)
            {
                float x = this.X;
                float y = this.Y + this.Size.Y / 2;
                LabelEntity.X = x;
                LabelEntity.Y = y - LabelEntity.Size.Y / 2;
                CheckedEntity.X = x + 226;
                CheckedEntity.Y = y - CheckedEntity.Size.Y / 2;
                UncheckedEntity.X = x + 226;
                UncheckedEntity.Y = y - UncheckedEntity.Size.Y / 2;

            }
        }

        Vector2 _size;
        public override Vector2 Size
        {
            get { return _size; }
        }
        public override void SetSize(Vector2 value)
        {
            _size = value;
        }

        public override void Draw(GameTime gameTime,
            ComponentCollection components)
        {
            LabelEntity.Draw(gameTime, components);

            if (IsChecked)
            {
                CheckedEntity.Draw(gameTime, components);
            }
            else
            {
                UncheckedEntity.Draw(gameTime, components);
            }
        }

        public override void Update(GameTime gameTime, InputSource inputSource,
            ComponentCollection components)
        {
            _mapper.InputSource = inputSource;

            if (_mapper.Get(Action.Toggle))
            {
                IsChecked = !IsChecked;
                MenuBase.PlayMenuMove();
            }

            SetEntityPositions();
        }
    }
}

