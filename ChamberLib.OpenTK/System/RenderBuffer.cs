using OpenTK.Graphics.OpenGL;

namespace ChamberLib.OpenTK.System
{
    public class RenderBuffer
    {
        public RenderBuffer(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public readonly int Width;
        public readonly int Height;

        public int ID { get; protected set; }
        public bool IsReady{ get; protected set; }

        void MakeReady()
        {
            ID = GL.GenRenderbuffer();
            GLHelper.CheckError();
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, ID);
            GLHelper.CheckError();
            GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer,
                (RenderbufferStorage)All.DepthComponent32, Width, Height);
            GLHelper.CheckError();

            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);
            GLHelper.CheckError();

            IsReady = true;
        }

        public void Apply()
        {
            if (!IsReady)
            {
                MakeReady();
            }

            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, ID);
            GLHelper.CheckError();
        }

        public void UnApply()
        {
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);
            GLHelper.CheckError();
        }
    }
}

