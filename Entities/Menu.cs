
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
    public interface IMenu
    {
        int SelectedIndex { get; set; }
        bool Accepted { get; }

        void Update(GameTime gameTime, InputSource source,
            ComponentCollection components);
        void Draw(GameTime gameTime, ComponentCollection components);

    }

    public abstract class MenuBase : Entity2D
    {
        public static ISoundEffect MenuAccept { get; set; }
        public static ISoundEffect MenuCancel { get; set; }
        public static ISoundEffect MenuMove { get; set; }

        public static void PlayMenuAccept()
        {
            if (MenuAccept != null) MenuAccept.Play();
        }
        public static void PlayMenuCancel()
        {
            if (MenuCancel != null) MenuCancel.Play();
        }
        public static void PlayMenuMove()
        {
            if (MenuMove != null) MenuMove.Play();
        }
    }

    public class Menu<T> : MenuBase, IMenu
    {
        public Menu(
            IRenderer renderer,
            ChamberLib.Vector2 position,
            ITexture2D cursor,
            params MenuItem<T>[] items)
            : this(renderer, position, null, null, cursor, null, items)
        {
        }
        public Menu(
            IRenderer renderer,
            ChamberLib.Vector2 position,
            string heading,
            IFont font,
            ITexture2D cursor,
            ITexture2D background,
            params MenuItem<T>[] items)
        {
            if (renderer == null) throw new ArgumentNullException("renderer");
            if (items == null) throw new ArgumentNullException("items");

            if (heading == null) heading = string.Empty;

            _menuCursor = new SpriteEntity(cursor);

            if (font != null)
            {
                _heading = new TextEntity(heading, font);
            }
            if (background != null)
            {
                _background = new SpriteEntity(background);
            }

            _items = items;
            Position = position;
        }

        public enum Action
        {
            Up,
            Down,
            Accept,
            Cancel,
        }

        public static InputMapper<Action> GenerateDefaultMapper(InputSource source)
        {
            InputMapper<Action> mapper = new InputMapper<Action>(source);

            mapper.AddMap(Buttons.DPadUp, Transition.Pressed, Action.Up);
            mapper.AddMap(AnalogInput.LeftStickY, 0.5f, AnalogTransition.BecomesGreater, Action.Up);
            mapper.AddMap(Keys.Up, Transition.Pressed, Action.Up);
            mapper.AddMap(Buttons.DPadDown, Transition.Pressed, Action.Down);
            mapper.AddMap(AnalogInput.LeftStickY, -0.5f, AnalogTransition.BecomesLess, Action.Down);
            mapper.AddMap(Keys.Down, Transition.Pressed, Action.Down);

            return mapper;
        }

        InputMapper<Action> Mapper = GenerateDefaultMapper(null);

        protected override void OnPositionChanged(EventArgs e)
        {
            _recalc = true;
        }

        public override Vector2 Size
        {
            get
            {
                CheckRecalc();
                return _bounds.Size;
            }
        }

        bool _recalc = true;
        RectangleF _bounds;
        RectangleF _itemBounds;
        public override RectangleF GetBounds()
        {
            CheckRecalc();
            return _bounds;
        }

        void CheckRecalc()
        {
            if (_recalc)
            {
                SetEntityMeasurements();
                _recalc = false;
            }
        }

        MenuItem<T>[] _items;
        public MenuItem<T>[] Items
        {
            get { return _items; }
            set
            {
                if (value == null)
                    _items = null;
                else
                    _items = value.CloneArray();
            }
        }

        int _selectedIndex = 0;
        public MenuItem<T> SelectedMenuItem
        {
            get
            {
                if (Items == null || Items.Length < 1)
                    return null;

                return Items[_selectedIndex];
            }
            set { SelectedIndex = Array.IndexOf(Items, value); }
        }
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                if (value != _selectedIndex)
                {
                    _selectedIndex = value;

                    if (_selectedIndex < 0)
                    {
                        _selectedIndex = 0;
                    }
                    if (_selectedIndex >= Items.Length)
                    {
                        _selectedIndex = Items.Length;
                    }

                    _recalc = true;
                }
            }
        }

        public bool Accepted { get; set; }

        private SpriteEntity _menuCursor;
        SpriteEntity _background;

        TextEntity _heading;
        public string HeadingText
        {
            get { return (_heading != null ?  _heading.Text : null); }
            set
            {
                if (_heading != null)
                {
                    _heading.Text = value;
                    _recalc = true;
                }
            }
        }

        bool _isCentered = false;
        public bool IsCentered 
        {
            get { return _isCentered; }
            set
            {
                if (value != _isCentered)
                {
                    foreach (MenuItem<T> item in Items)
                    {
                        if (item.Entity is TextEntity)
                        {
                            (item.Entity as TextEntity).HorizontalAlignment = HorizontalAlignment.Center;
                        }
                    }

                    _recalc = true;
                }
            }
        }

        bool _itemsExtendToFullMenuWidth = true;
        public bool ItemsExtendToFullMenuWidth
        {
            get { return _itemsExtendToFullMenuWidth; }
            set
            {
                _itemsExtendToFullMenuWidth = value;
                _recalc = true;
            }
        }

        public override void Begin(GameTime gameTime, ComponentCollection components)
        {
            _recalc = true;
        }

        public override void Update(GameTime gameTime, InputSource source,
            ComponentCollection components)
        {
            Accepted = false;

            Mapper.InputSource = source;

            if (source.HasMouseMoved())
            {
                var item = GetMenuItemAtPosition(source.CurrentMouseState.Position);
                if (item != null)
                    SelectedMenuItem = item;
            }

            if (source.IsButtonPressed(MouseButtons.Left) &&
                GetBounds().Contains(source.CurrentMouseState.Position))
            {
                Accepted = true;
            }

            if (SelectedIndex > Items.Length - 1)
            {
                SelectedIndex = Items.Length - 1;
            }
            if (SelectedIndex < 0)
            {
                SelectedIndex = 0;
            }
            if (!SelectedMenuItem.IsEnabled)
            {
                int i;
                for (i = SelectedIndex; i < Items.Length; i++)
                {
                    if (Items[i].IsEnabled) break;
                }
                if (i >= Items.Length)
                {
                    for (i = 0; i < SelectedIndex; i++)
                    {
                        if (Items[i].IsEnabled) break;
                    }
                }
                SelectedIndex = i;
            }

            if (Mapper.Get(Action.Up) && SelectedIndex > 0)
            {
                PlayMenuMove();

                int i;
                for (i = SelectedIndex - 1; i >= 0; i--)
                {
                    if (Items[i].IsEnabled)
                    {
                        SelectedIndex = i;
                        break;
                    }
                }
            }
            else if (Mapper.Get(Action.Down) && SelectedIndex < Items.Length - 1)
            {
                PlayMenuMove();

                int i;
                for (i = SelectedIndex + 1; i <= Items.Length - 1; i++)
                {
                    if (Items[i].IsEnabled)
                    {
                        SelectedIndex = i;
                        break;
                    }
                }
            }
            else if (SelectedMenuItem.IsEnabled)
            {
                SelectedMenuItem.Update(gameTime, source, components);
            }
        }

        public override void Draw(GameTime gameTime,
            ComponentCollection components)
        {
            CheckRecalc();

            if (_background != null && _background.Texture != null)
            {
                _background.Draw(gameTime, components);
            }

            if (!string.IsNullOrEmpty(HeadingText))
            {
                _heading.Draw(gameTime, components);
            }

            _menuCursor.Draw(gameTime, components);

            foreach (MenuItem<T> item in Items)
            {
                item.Draw(gameTime, components);
            }
        }

        void SetEntityMeasurements()
        {
            var pos = Position;
            RectangleF bounds = new RectangleF(Position, 0, 0);

            if (!string.IsNullOrEmpty(HeadingText))
            {
                _heading.Position = pos;
                pos.Y += _heading.Size.Y;
                bounds = bounds.Union(_heading.GetBounds());
            }

            foreach (MenuItem<T> item in Items)
            {
                var size = item.GetSize();
                item.SetPosition(pos);
                pos.Y += size.Y;

                bounds = bounds.Union(item.GetBounds());
            }

            _itemBounds = bounds;

            if (SelectedMenuItem != null &&
                _menuCursor != null)
            {
                var ient = SelectedMenuItem.Entity;
                var ibounds = ient.GetBounds();
                var ipos = ibounds.MiddleLeft;
                var cbounds = _menuCursor.GetBounds();
                var cdelta = cbounds.TopLeft - cbounds.MiddleRight;
                _menuCursor.Position = ipos + cdelta;
                bounds = bounds.Union(_menuCursor.GetBounds());
            }

            if (_background != null)
            {
                float deltax = 0.00625f;
                float deltay = 5/480.0f;

                bounds = bounds.Union(
                    new RectangleF(
                        bounds.Left - deltax,
                        bounds.Top - deltay,
                        bounds.Width + deltax + deltax,
                        bounds.Height + deltay + deltay));
                _background.SetBounds(bounds);
            }

            _bounds = bounds;
        }

        MenuItem<T> GetMenuItemAtPosition(Vector2 v)
        {
            foreach(var item in Items)
            {
                var b = item.GetBounds();
                if (ItemsExtendToFullMenuWidth)
                {
                    var r = new RectangleF(_itemBounds.Left, b.Top,
                        _itemBounds.Width, b.Height);
                    b = b.Union(r);
                }
                if (b.Contains(v)) return item;
            }

            return null;
        }
    }
}
