using System;

namespace ChamberLib
{
    public class ReferenceSubsystem : ISubsystem
    {
        public ReferenceSubsystem()
        {
        }

        #region ISubsystem implementation

        public event EventHandler<EventArgs> ClientSizeChanged;

        public IGamePad GetGamePad(PlayerIndex index)
        {
            throw new NotImplementedException();
        }

        public IKeyboard GetKeyboard()
        {
            throw new NotImplementedException();
        }

        public void Run()
        {
            throw new NotImplementedException();
        }

        public void Exit()
        {
            throw new NotImplementedException();
        }

        public IRenderTarget CreateRenderTarget(int width, int height,
                                                PixelFormat pixelFormat=PixelFormat.Rgba)
        {
            throw new NotImplementedException();
        }

        public IRenderer Renderer
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IContentManager ContentManager
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IMediaManager MediaManager
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string WindowTitle
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool AllowUserResizing
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public RectangleI ClientBounds
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsActive
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}

