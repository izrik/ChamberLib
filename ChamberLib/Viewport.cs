using System;


namespace ChamberLib
{
    public struct Viewport
    {
        public Viewport(int x, int y, int width, int height, float minDepth=0, float maxDepth=1)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            MinDepth = minDepth;
            MaxDepth = maxDepth;
        }

        public int Height;
        public int Width;
        public int X;
        public int Y;
        public float MaxDepth;
        public float MinDepth;

        public RectangleI Bounds
        {
            get { return new RectangleI(X, Y, Width, Height); }
            set
            {
                X = value.Left;
                Y = value.Top;
                Width = value.Width;
                Height = value.Height;
            }
        }

        public Vector2 Position
        {
            get { return new Vector2(X, Y); }
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
            v3.Z = (v3.Z * (MaxDepth - MinDepth)) + MinDepth;

            return v3;
        }

        public Vector3 Unproject(Vector3 v, Matrix projection, Matrix view, Matrix world)
        {
            var v2 = new Vector4(
                ((v.X - this.X) / this.Width * 2) - 1,
                1 - ((v.Y - this.Y) / this.Height * 2),
                (v.Z - MinDepth) / (MaxDepth - MinDepth),
                1
            );

            Matrix m = Matrix.Invert(world * view * projection);

            Vector4.Transform(v2, m);

            Vector3 v3 = new Vector3(v2.X / v2.W, v2.Y / v2.W, v2.Z / v2.W);

            return v3;
        }
    }
}

