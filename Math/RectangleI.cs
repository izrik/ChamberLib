using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChamberLib
{
    public struct RectangleI
    {
        public RectangleI(int left, int top, int width, int height)
        {
            Left = left;
            Top = top;
            Width = width;
            Height = height;
        }
        public RectangleI(Point2 topLeft, int width, int height)
        {
            Left = topLeft.X;
            Top = topLeft.Y;
            Width = width;
            Height = height;
        }

        public int Left;
        public int Top;
        public int Width;
        public int Height;

        public int Right { get { return Left + Width; } }
        public int Bottom { get { return Top + Height; } }

        public Point2 TopLeft
        {
            get { return new Point2(Left, Top); }
            set { Left = value.X; Top = value.Y; }
        }
        public Point2 TopRight { get { return new Point2(Right, Top); } }
        public Point2 BottomLeft { get { return new Point2(Left, Bottom); } }
        public Point2 BottomRight { get { return new Point2(Right, Bottom); } }

        public Point2 TopCenter { get { return new Point2(Left + Width / 2, Top); } }
        public Point2 BottomCenter { get { return new Point2(Left + Width / 2, Bottom); } }
        public Point2 MiddleLeft { get { return new Point2(Left, Top + Height / 2); } }
        public Point2 MiddleRight { get { return new Point2(Right, Top + Height / 2); } }

        public Point2 Center { get { return new Point2(Left + Width / 2, Top + Height / 2); } }

        public Point2 Size
        {
            get { return new Point2(Width, Height); }
            set { Width = value.X; Height = value.Y; }
        }

        public RectangleI Union(RectangleI other)
        {
            return Union(this, other);
        }

        public static RectangleI Union(RectangleI a, RectangleI b)
        {
            int left = Math.Min(Math.Min(a.Left, a.Right), Math.Min(b.Left, b.Right));
            int top = Math.Min(Math.Min(a.Top, a.Bottom), Math.Min(b.Top, b.Bottom));
            int right = Math.Max(Math.Max(a.Left, a.Right), Math.Max(b.Left, b.Right));
            int bottom = Math.Max(Math.Max(a.Top, a.Bottom), Math.Max(b.Top, b.Bottom));

            return new RectangleI(left, top, right - left, bottom - top);
        }

        public RectangleF ToRectangleF()
        {
            return new RectangleF(Left, Top, Width, Height);
        }
    }
}
