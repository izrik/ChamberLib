using System;

namespace ChamberLib
{
    public class OpenTKSubsystem : ISubsystem
    {
        public OpenTKSubsystem()
        {
            _renderer = new Renderer();
            _content = new ContentManager();
            _media = new MediaManager();
        }

        readonly Renderer _renderer;
        public IRenderer Renderer { get { return _renderer; } }

        readonly ContentManager _content;
        public IContentManager ContentManager { get { return _content; } }

        readonly MediaManager _media;
        public IMediaManager MediaManager { get { return _media; } }

        public IGamePad GetGamePad(PlayerIndex index)
        {
            return GamePad.GetGamePad(index);
        }
        public IKeyboard GetKeyboard()
        {
            return Keyboard.GetKeyboard();
        }

        public string WindowTitle
        {
            get { return "Window"; }
            set { }
        }

        public bool AllowUserResizing
        {
            get { return false; }
            set { }
        }

        public RectangleI ClientBounds { get { throw new NotImplementedException(); } }

        public event EventHandler<EventArgs> ClientSizeChanged
        {
            add { }
            remove { }
        }

        public void Run()
        {

        }
        public void Exit()
        {

        }
    }
}

