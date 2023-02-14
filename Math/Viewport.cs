﻿using System;


namespace ChamberLib
{
    public struct Viewport
    {
        public Viewport(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public readonly int Height;
        public readonly int Width;
        public readonly int X;
        public readonly int Y;

        public RectangleI Bounds
        {
            get { return new RectangleI(X, Y, Width, Height); }
        }

        public Vector2 Position
        {
            get { return new Vector2(X, Y); }
        }

        public Vector2 Size
        {
            get { return new Vector2(Width, Height); }
        }

        public RectangleI TitleSafeArea
        {
            get { return new RectangleI(X + 10, Y + 10, Width - 20, Height - 20); }
        }

        public float AspectRatio { get { return Width / (float)Height; } }

        public Vector3 Project(Vector3 v, Matrix projection, Matrix view, Matrix world)
        {
            Vector4 v2 = new Vector4(v.X, v.Y, v.Z, 1);
            v2 = Vector4.Transform(v2, world);
            v2 = Vector4.Transform(v2, view);
            v2 = Vector4.Transform(v2, projection);

            Vector3 v3 = new Vector3(v2.X / v2.W, v2.Y / v2.W, v2.Z / v2.W);

            v3.X = (((v3.X + 1) * 0.5f) * this.Width) + this.X;
            v3.Y = (((1 - v3.Y) * 0.5f) * this.Height) + this.Y;

            return v3;
        }

        public Vector3 Unproject(Vector3 v, Matrix projection, Matrix view, Matrix world)
        {
            var v2 = new Vector4(
                ((v.X - this.X) / this.Width * 2) - 1,
                1 - ((v.Y - this.Y) / this.Height * 2),
                v.Z,
                1
            );

            Matrix m = Matrix.Invert(world * view * projection);

            Vector4.Transform(v2, m);

            Vector3 v3 = new Vector3(v2.X / v2.W, v2.Y / v2.W, v2.Z / v2.W);

            return v3;
        }

        public Vector2 PointToRelativeCoordinates(Point2 pt)
        {
            return PointToRelativeCoordinates(pt.X, pt.Y);
        }
        public Vector2 PointToRelativeCoordinates(int x, int y)
        {
            return new Vector2(
                x / (float)Width,
                y / (float)Height);
        }

        public Point2 PointToPixelCoordinates(Vector2 v)
        {
            return new Point2(
                (v.X * Width).RoundToInt(),
                (v.Y * Height).RoundToInt());
        }
    }
}

