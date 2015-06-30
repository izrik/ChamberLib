using System;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Linq;

namespace ChamberLib.OpenTK
{
    public partial class Renderer : IRenderer
    {
        public Renderer(OpenTKSubsystem subsystem)
        {
            if (subsystem == null) throw new ArgumentNullException("subsystem");

            _subsystem = subsystem;

            _viewport = new Viewport(0, 0, _subsystem.Window.Width, _subsystem.Window.Height);
        }

        readonly OpenTKSubsystem _subsystem;

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

            var height = (windowed ? _subsystem.Window.Height : value.Height);
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
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GLHelper.CheckError();
        }
    }
}