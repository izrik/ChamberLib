using System;
using OpenTK.Graphics.OpenGL;

namespace ChamberLib.OpenTK
{
    public class FrameBuffer : IRenderTarget
    {
        public FrameBuffer(IRenderer renderer, ITexture2D texture,
            RenderBuffer depthBuffer)
        {
            Renderer = renderer;
            Texture = texture;
            DepthBuffer = depthBuffer;

            var size = texture.GetSize();
            Width = size.X.RoundToInt();
            Height = size.Y.RoundToInt();
        }

        public readonly IRenderer Renderer;
        public readonly ITexture2D Texture;
        public readonly RenderBuffer DepthBuffer;

        public int Width { get; protected set; }
        public int Height { get; protected set; }

        public int ID { get; protected set; }
        public bool IsReady{ get; protected set; }

        void MakeReady()
        {
            ID = GL.GenFramebuffer();
            GLHelper.CheckError();

            Texture.Apply();
            DepthBuffer.Apply();

            GL.BindFramebuffer(
                FramebufferTarget.Framebuffer,
                ID);
            GLHelper.CheckError();
            GL.FramebufferTexture2D(
                FramebufferTarget.Framebuffer,
                FramebufferAttachment.ColorAttachment0,
                TextureTarget.Texture2D,
                ((TextureAdapter)Texture).ID,
                0);
            GLHelper.CheckError();
            GL.FramebufferRenderbuffer(
                FramebufferTarget.Framebuffer,
                FramebufferAttachment.DepthAttachment,
                RenderbufferTarget.Renderbuffer,
                DepthBuffer.ID);
            GLHelper.CheckError();

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            GLHelper.CheckError();

            IsReady = true;
        }

        public void Apply()
        {
            if (!IsReady)
            {
                MakeReady();
            }

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, ID);
            GLHelper.CheckError();

            GL.DrawBuffer(DrawBufferMode.ColorAttachment0);
            GLHelper.CheckError();

            float one = 1;
            int [] black = new int[]{ 0, 0, 0, 0 };
            GL.ClearBuffer(ClearBuffer.Depth, 0, ref one); 
            GL.ClearBuffer(ClearBuffer.Color, 0, black);

            Renderer.SetViewport(new Viewport(0, 0, Width, Height), false);
        }

        public void UnApply()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0); // return to visible framebuffer
            GLHelper.CheckError();
            GL.DrawBuffer( DrawBufferMode.Back );
            GLHelper.CheckError();
        }

        public ITexture2D AsTexture()
        {
            return Texture;
        }
    }
}

