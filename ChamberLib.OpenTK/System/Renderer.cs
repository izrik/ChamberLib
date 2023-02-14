
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
using ChamberLib.OpenTK.Math;
using OpenTK.Graphics.OpenGL;

namespace ChamberLib.OpenTK.System
{
    public partial class Renderer : IRenderer
    {
        public Renderer(OpenTKSubsystem subsystem)
        {
            if (subsystem == null) throw new ArgumentNullException("subsystem");

            _subsystem = subsystem;

            _viewport = new Viewport(0, 0, GetWidth(), GetHeight());
        }
        public Renderer(Func<int> widthSrc, Func<int> heightSrc)
        {
            if (widthSrc is null) throw new ArgumentNullException(nameof(widthSrc));
            if (heightSrc is null) throw new ArgumentNullException(nameof(heightSrc));

            _widthSrc = widthSrc;
            _heightSrc = heightSrc;

            _viewport = new Viewport(0, 0, GetWidth(), GetHeight());
        }

        readonly OpenTKSubsystem _subsystem;
        readonly Func<int> _widthSrc;
        readonly Func<int> _heightSrc;

        protected int GetWidth()
        {
            if (_widthSrc != null)
                return _widthSrc();
            return _subsystem.Window.Width;
        }
        protected int GetHeight()
        {
            if (_heightSrc != null)
                return _heightSrc();
            return _subsystem.Window.Height;
        }

        #region IRenderer implementation

        public void Clear(Color color)
        {
            GL.ClearColor(color.ToSystemDrawing());
            GLHelper.CheckError();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GLHelper.CheckError();
        }

        public void Reset3D()
        {
            GL.Enable(EnableCap.DepthTest);
            GLHelper.CheckError();
            GL.DepthMask(true);
            GLHelper.CheckError();
        }

        Viewport _viewport;
        public Viewport Viewport
        {
            get
            {
                return _viewport;
            }
            set
            {
                SetViewport(value);
            }
        }
        public void SetViewport(Viewport value, bool windowed=true)
        {
            _viewport = value;

            var height = (windowed ? GetHeight() : value.Height);
            var y = height - value.Y - value.Height;

            GL.Viewport(
                value.X,
                y,
                value.Width,
                value.Height
            );
            GLHelper.CheckError();
        }

        #endregion

        public void Reset2D()
        {
            GL.Disable(EnableCap.DepthTest);
            GLHelper.CheckError();
            GL.Enable(EnableCap.Blend);
            GLHelper.CheckError();
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GLHelper.CheckError();
        }
    }
}