
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
using ChamberLib.OpenTK.System;
using OpenTK.Graphics.OpenGL;

namespace ChamberLib.OpenTK.Images
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
            Apply(ColorF.Black);
        }
        public void Apply(Color clearColor)
        {
            Apply(clearColor.ToColorF());
        }
        public void Apply(ColorF clearColor)
        {
            if (!IsReady)
            {
                MakeReady();
            }

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, ID);
            GLHelper.CheckError();

            GL.DrawBuffer(DrawBufferMode.ColorAttachment0);
            GLHelper.CheckError();

            Renderer.SetViewport(new Viewport(0, 0, Width, Height), false);

            float one = 1;
            GL.ClearBuffer(ClearBuffer.Depth, 0, ref one); 
            GL.ClearColor(clearColor.R, clearColor.G, clearColor.B,
                clearColor.A);
            GL.Clear(ClearBufferMask.ColorBufferBit |
                ClearBufferMask.DepthBufferBit);
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

