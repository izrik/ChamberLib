
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
    public class TextEntity : Entity2D
    {
        public TextEntity(string text, IFont font, Color? color=null,
                            float rotationInRadians=0, Vector2 position=default(Vector2), float scale=1)
        {
            if (text == null) throw new ArgumentNullException("text");
            if (font == null) throw new ArgumentNullException("font");

            if (!color.HasValue)
            {
                color = Color.White;
            }

            Position = position;
            Text = text;
            Font = font;
            Color = color.Value;
            RotationInRadians = rotationInRadians;
            Scale = scale;
        }

        IFont _font;
        public IFont Font
        {
            get { return _font; }
            protected set
            {
                _font = value;
                _recalc = true;
            }
        }

        string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                _recalc = true;
            }
        }

        public int? NumCharsToDraw { get; set; } = null;

        Color _color;
        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                _recalc = true;
            }
        }

        float _scale = 1;
        public float Scale
        {
            get { return _scale; }
            set
            {
                _scale = value;
                _recalc = true;
            }
        }

        bool _recalc = true;

        // TODO: proper box model

        // Padding scales with the text
        float _paddingLeft;
        public float PaddingLeft
        {
            get { return _paddingLeft; }
            set
            {
                _paddingLeft = value;
                _recalc = true;
            }
        }
        float _paddingTop;
        public float PaddingTop
        {
            get { return _paddingTop; }
            set
            {
                _paddingTop = value;
                _recalc = true;
            }
        }
        float _paddingRight;
        public float PaddingRight
        {
            get { return _paddingRight; }
            set
            {
                _paddingRight = value;
                _recalc = true;
            }
        }
        float _paddingBottom;
        public float PaddingBottom
        {
            get { return _paddingBottom; }
            set
            {
                _paddingBottom = value;
                _recalc = true;
            }
        }

        public void SetPadding(float padding)
        {
            SetPadding(padding, padding, padding, padding);
        }
        public void SetPadding(float leftRight, float topBottom)
        {
            SetPadding(leftRight, topBottom, leftRight, topBottom);
        }
        public void SetPadding(float left, float top, float right, float bottom)
        {
            PaddingLeft = left;
            PaddingTop = top;
            PaddingRight = right;
            PaddingBottom = bottom;
        }

        float? _maxUnscaledLineWidth = null;
        public float? MaxUnscaledLineWidth
        {
            get => _maxUnscaledLineWidth;
            set
            {
                _maxUnscaledLineWidth = value;
                _recalc = true;
            }
        }

        public override Vector2 Position
        {
            get { return base.Position; }
            set
            {
                base.Position = value;
                _recalc = true;
            }
        }

        public override Vector2 Size
        {
            get
            {
                CheckCalc();
                return _bounds.Size;
            }
        }

        RectangleF _bounds;
        Vector2 _drawPosition;
        Vector2 _usize;
        public override RectangleF GetBounds()
        {
            CheckCalc();
            return _bounds;
        }

        void CheckCalc()
        {
            if (_recalc)
            {
                var tm = CalcMeasurements(Text, Font,
                    Position, HorizontalAlignment, VerticalAlignment, Scale,
                    PaddingLeft, PaddingTop, PaddingRight, PaddingBottom,
                    MaxUnscaledLineWidth);

                _bounds = tm.Bounds;
                _drawPosition = tm.DrawPosition;
                _usize = tm.UnpaddedSize;

                _recalc = false;
            }
        }

        public struct TextMeasurements
        {
            public RectangleF Bounds;
            public Vector2 DrawPosition;
            public Vector2 UnpaddedSize;

        }
        public static TextMeasurements CalcMeasurements(string text,
            IFont font, Vector2 position,
            HorizontalAlignment halign, VerticalAlignment valign, float scale,
            float paddingLeft, float paddingTop, float paddingRight,
            float paddingBottom, float? maxUnscaledLineWidth=null)
        {

            var tsize = CalcTextUnscaledSize(text, font, maxUnscaledLineWidth);
            tsize.X /= 800f;
            tsize.Y /= 480f;
            var psize = new Vector2(
                tsize.X + paddingLeft + paddingRight,
                tsize.Y + paddingTop + paddingBottom);
            var ssize = psize * scale;
            var size = ssize;

            var boundsTopLeft = CalcBoundsTopLeft(position,
                halign, valign, size);

            var tm = new TextMeasurements() {
                Bounds = new RectangleF(boundsTopLeft, size.X, size.Y),
                DrawPosition = boundsTopLeft +
                    new Vector2(paddingLeft, paddingTop),
                UnpaddedSize = tsize * scale
            };

            return tm;
        }

        Vector2[] __bounds = new Vector2[7];
        static public bool __show_bounds = false;
        public override void Draw(GameTime gameTime,
            ComponentCollection components)
        {
            CheckCalc();

            var drawPosition = _drawPosition;
            var scale = new Vector2(Scale, Scale);
            var renderer = components.Get<IRenderer>();
            var vp = renderer.Viewport;
            var maxUnscaledLineWidth = MaxUnscaledLineWidth;
            if (true)
            {
                drawPosition.X *= vp.Width;
                drawPosition.Y *= vp.Height;
                scale.X *= vp.Width / 800.0f;
                scale.Y *= vp.Height / 480.0f;
                maxUnscaledLineWidth *= vp.Width;
            }

            if (__show_bounds)
            {
                var bounds = GetBounds();
                bounds = new RectangleF(
                    bounds.Left * vp.Width,
                    bounds.Top * vp.Height,
                    bounds.Width * vp.Width,
                    bounds.Height * vp.Height);
                __bounds[0] = bounds.TopLeft;
                __bounds[1] = bounds.TopRight;
                __bounds[2] = bounds.BottomRight;
                __bounds[3] = bounds.BottomLeft;
                __bounds[4] = bounds.TopLeft;
                renderer.DrawLines(Color.Yellow, __bounds, 5);

                var bounds2 = new RectangleF(
                    _drawPosition.X * vp.Width,
                    _drawPosition.Y * vp.Height,
                    _usize.X * vp.Width,
                    _usize.Y * vp.Height);
                __bounds[0] = bounds2.TopLeft;
                __bounds[1] = bounds2.TopRight;
                __bounds[2] = bounds2.BottomRight;
                __bounds[3] = bounds2.BottomLeft;
                __bounds[4] = bounds2.TopLeft;
                renderer.DrawLines(Color.Red, __bounds, 5);

                __bounds[0] = Vector2.Zero;
                __bounds[1] = Vector2.One;
                __bounds[2] = Vector2.UnitX;
                __bounds[3] = Vector2.UnitY;
                __bounds[4] = -Vector2.One;
                __bounds[5] = -Vector2.UnitY;
                __bounds[6] = -Vector2.UnitX;
                renderer.DrawLines(Color.Magenta, __bounds, 7);

                //var tm = CalcMeasurements(Text, Font,
                //    Position, HorizontalAlignment, VerticalAlignment, Scale,
                //    PaddingLeft, PaddingTop, PaddingRight, PaddingBottom);
            }

            renderer.DrawString(
                Font,
                Text,
                drawPosition,
                Color,
                RotationInRadians,
                scale.X,
                scale.Y,
                maxUnscaledLineWidth,
                NumCharsToDraw);
        }

        public static Vector2 CalcTextUnscaledSize(string text, IFont font,
            float? maxUnscaledLineWidth)
        {
            var unscaledTextSize = font.MeasureString(text,
                wrapWordsToMaxLineWidth: maxUnscaledLineWidth);
            return unscaledTextSize + Vector2.UnitY;
        }

        public static Vector2 CalcBoundsTopLeft(Vector2 position,
            HorizontalAlignment horizontalAlignment,
            VerticalAlignment verticalAlignment, Vector2 size)
        {
            float dx;
            float dy;

            switch (horizontalAlignment)
            {
                case HorizontalAlignment.Left: dx = 0; break;
                case HorizontalAlignment.Center: dx = -size.X / 2; break;
                case HorizontalAlignment.Right: dx = -size.X; break;
                default:
                    throw new InvalidOperationException(
                        string.Format(
                            "Invalid value for HorizontalAlignment property: {0}",
                    horizontalAlignment.ToString()));
            }

            switch (verticalAlignment)
            {
                case VerticalAlignment.Top: dy = 0; break;
                case VerticalAlignment.Center: dy = -size.Y / 2; break;
                case VerticalAlignment.Bottom: dy = -size.Y; break;
                default:
                    throw new InvalidOperationException(
                        string.Format(
                            "Invalid value for VerticalAlignment property: {0}",
                    verticalAlignment.ToString()));
            }

            return position + new Vector2(dx, dy);
        }

        HorizontalAlignment _halign = HorizontalAlignment.Left;
        public HorizontalAlignment HorizontalAlignment
        {
            get { return _halign; }
            set
            {
                _halign = value;
                _recalc = true;
            }
        }
        public bool IsCenteredHorizontally
        {
            get { return HorizontalAlignment == HorizontalAlignment.Center; }
        }

        VerticalAlignment _valign = VerticalAlignment.Top;
        public VerticalAlignment VerticalAlignment
        {
            get { return _valign; }
            set
            {
                _valign = value;
                _recalc = true;
            }
        }
        public bool IsCenteredVertically
        {
            get { return VerticalAlignment == VerticalAlignment.Center; }
        }

        public float RotationInRadians { get; set; }
        public float RotationInDegrees
        {
            get { return (float)(RotationInRadians * 180.0 / Math.PI); }
            set { RotationInRadians = (float)(value * Math.PI / 180.0); }
        }
    }
}
