
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
    public class ProgressBar : Entity2D
    {
        public ProgressBar(IContentManager content)
        {
            if (content == null) throw new ArgumentNullException("content");

            Progress = 0;
            MaxProgress = 10;

            _texture = content.CreateTexture(1, 1, new Color[] { Color.White });
            _sprite = new SpriteEntity(_texture);
        }

        ITexture2D _texture;
        SpriteEntity _sprite;

        public Color Color
        {
            get => _sprite.Color;
            set => _sprite.Color = value;
        }

        public override void Update(GameTime gameTime, InputSource inputSource,
            ComponentCollection components)
        {
            if (_mustUpdateSpriteWidth)
            {
                UpdateSpriteWidth();
            }
        }

        bool _mustUpdateSpriteWidth = true;
        protected void UpdateSpriteWidth()
        {
            float amountComplete = Progress / (float)MaxProgress;
            var width = amountComplete * this.Width;
            _sprite.SetSize(new Vector2(width, _sprite.Height));

            _mustUpdateSpriteWidth = false;
        }

        public override void Draw(GameTime gameTime, 
            ComponentCollection components)
        {
            if (_mustUpdateSpriteWidth)
            {
                UpdateSpriteWidth();
            }

            _sprite.Draw(gameTime, components);
        }

        protected override void OnPositionChanged(EventArgs e)
        {
            base.OnPositionChanged(e);

            _sprite.Position = this.Position;
            _mustUpdateSpriteWidth = true;
        }

        protected void OnSizeChanged(EventArgs e)
        {
            _sprite.SetSize(new Vector2(_sprite.Width, this.Height));
            _mustUpdateSpriteWidth = true;
        }

        int _progress;
        public int Progress
        {
            get { return _progress; }
            set
            {
                _progress = value;
                _mustUpdateSpriteWidth = true;
            }
        }
        int _maxProgress;
        public int MaxProgress
        {
            get { return _maxProgress; }
            set
            {
                _maxProgress = value;
                _mustUpdateSpriteWidth = true;
            }
        }

        Vector2 _size;
        public override Vector2 Size
        {
            get { return _size; }
        }
        public override void SetSize(Vector2 value)
        {
            if (_size != value)
            {
                _size = value;
                OnSizeChanged(new EventArgs());
            }
        }
    }
}
