using System;
using OpenTK.Graphics.OpenGL;

namespace ChamberLib.OpenTK
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
            ID = GL.Ext.GenRenderbuffer();
            GLHelper.CheckError();
            GL.Ext.BindRenderbuffer(RenderbufferTarget.RenderbufferExt, ID);
            GLHelper.CheckError();
            GL.Ext.RenderbufferStorage(RenderbufferTarget.RenderbufferExt,
                (RenderbufferStorage)All.DepthComponent32, Width, Height);
            GLHelper.CheckError();

            GL.Ext.BindRenderbuffer( RenderbufferTarget.RenderbufferExt, 0);
            GLHelper.CheckError();

            IsReady = true;
        }

        public void Apply()
        {
            if (!IsReady)
            {
                MakeReady();
            }

            GL.Ext.BindRenderbuffer(RenderbufferTarget.RenderbufferExt, ID);
            GLHelper.CheckError();
        }

        public void UnApply()
        {
            GL.Ext.BindRenderbuffer(RenderbufferTarget.RenderbufferExt, 0);
            GLHelper.CheckError();
        }
    }
}

