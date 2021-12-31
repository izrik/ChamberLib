
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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChamberLib
{
    public struct RectangleF
    {
        public RectangleF(float left, float top, float width, float height)
        {
            Left = left;
            Top = top;
            Width = width;
            Height = height;
        }
        public RectangleF(Vector2 topLeft, float width, float height)
        {
            Left = topLeft.X;
            Top = topLeft.Y;
            Width = width;
            Height = height;
        }

        public float Left;
        public float Top;
        public float Width;
        public float Height;

        public float Right { get { return Left + Width; } }
        public float Bottom { get { return Top + Height; } }

        public Vector2 TopLeft
        {
            get { return new Vector2(Left, Top); }
            set { Left = value.X; Top = value.Y; }
        }
        public Vector2 TopRight { get { return new Vector2(Right, Top); } }
        public Vector2 BottomLeft { get { return new Vector2(Left, Bottom); } }
        public Vector2 BottomRight { get { return new Vector2(Right, Bottom); } }

        public Vector2 TopCenter { get { return new Vector2(Left + Width / 2, Top); } }
        public Vector2 BottomCenter { get { return new Vector2(Left + Width / 2, Bottom); } }
        public Vector2 MiddleLeft { get { return new Vector2(Left, Top + Height / 2); } }
        public Vector2 MiddleRight { get { return new Vector2(Right, Top + Height / 2); } }

        public Vector2 Center { get { return new Vector2(Left + Width / 2, Top + Height / 2); } }

        public Vector2 Size
        {
            get { return new Vector2(Width, Height); }
            set { Width = value.X; Height = value.Y; }
        }

        public RectangleF Union(RectangleF other)
        {
            return Union(this, other);
        }

        public static RectangleF Union(RectangleF a, RectangleF b)
        {
            float left = Math.Min(Math.Min(a.Left, a.Right), Math.Min(b.Left, b.Right));
            float top = Math.Min(Math.Min(a.Top, a.Bottom), Math.Min(b.Top, b.Bottom));
            float right = Math.Max(Math.Max(a.Left, a.Right), Math.Max(b.Left, b.Right));
            float bottom = Math.Max(Math.Max(a.Top, a.Bottom), Math.Max(b.Top, b.Bottom));

            return new RectangleF(left, top, right - left, bottom - top);
        }

        public RectangleF Round()
        {
            int left = Left.RoundToInt();
            int top = Top.RoundToInt();
            int right = Right.RoundToInt();
            int bottom = Bottom.RoundToInt();

            return new RectangleF(left, top, right - left, bottom - top);
        }

        public RectangleI RoundToInt()
        {
            int left = Left.RoundToInt();
            int top = Top.RoundToInt();
            int right = Right.RoundToInt();
            int bottom = Bottom.RoundToInt();

            return new RectangleI(left, top, right - left, bottom - top);
        }

        public override string ToString()
        {
            return string.Format("{{Left:{0} Top:{1} Width:{2} Height:{3}}}", Left, Top, Width, Height);
        }

        public bool Contains(Vector2 v)
        {
            if (v.X < Left) return false;
            if (v.X > Right) return false;
            if (v.Y < Top) return false;
            if (v.Y > Bottom) return false;
            return true;
        }
    }
}
