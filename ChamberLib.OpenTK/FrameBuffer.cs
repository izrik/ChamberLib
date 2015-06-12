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
            ID = GL.Ext.GenFramebuffer();
            GLHelper.CheckError();

            Texture.Apply();
            DepthBuffer.Apply();

            GL.Ext.BindFramebuffer(
                FramebufferTarget.FramebufferExt,
                ID);
            GLHelper.CheckError();
            GL.Ext.FramebufferTexture2D(
                FramebufferTarget.FramebufferExt,
                FramebufferAttachment.ColorAttachment0Ext,
                TextureTarget.Texture2D,
                ((TextureAdapter)Texture).ID,
                0);
            GLHelper.CheckError();
            GL.Ext.FramebufferRenderbuffer(
                FramebufferTarget.FramebufferExt,
                FramebufferAttachment.DepthAttachmentExt,
                RenderbufferTarget.RenderbufferExt,
                DepthBuffer.ID);
            GLHelper.CheckError();

            GL.Ext.BindFramebuffer(FramebufferTarget.FramebufferExt, 0);
            GLHelper.CheckError();

            IsReady = true;
        }

        public void Apply()
        {
            if (!IsReady)
            {
                MakeReady();
            }

            GL.Ext.BindFramebuffer(FramebufferTarget.FramebufferExt, ID);
            GLHelper.CheckError();

            GL.DrawBuffer((DrawBufferMode)FramebufferAttachment.ColorAttachment0Ext);
            GLHelper.CheckError();

            Renderer.Viewport = new Viewport(0, 0, Width, Height);
        }

        public void UnApply()
        {
            GL.Ext.BindFramebuffer(FramebufferTarget.FramebufferExt, 0); // return to visible framebuffer
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

