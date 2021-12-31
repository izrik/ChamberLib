
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

