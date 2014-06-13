using System;
using OpenTK;
using OpenTK.Graphics;
using System.Threading.Tasks;
using System.Threading;

namespace ChamberLib
{
    public class OpenTKSubsystem : ISubsystem
    {
        public OpenTKSubsystem(
            int openglMajorVersion=1,
            int openglMinorVersion=0,
            Action onLoadMethod=null,
            Action<GameTime> onRenderFrameMethod=null,
            Action<GameTime> onUpdateFrameMethod=null)
        {
            _renderer = new Renderer();
            _content = new ContentManager();
            _media = new MediaManager();

            _window = new ChamberGameWindow(
                width: 800,
                height: 600,
                major: openglMajorVersion,
                minor: openglMinorVersion,
                onLoadMethod: onLoadMethod,
                onRenderFrameMethod: onRenderFrameMethod,
                onUpdateFrameMethod: onUpdateFrameMethod);
        }

        readonly ChamberGameWindow _window;

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
            get { return _window.Title; }
            set { _window.Title = value; }
        }

        public bool AllowUserResizing
        {
            get { return false; }
            set { }
        }

        public RectangleI ClientBounds { get { throw new NotImplementedException(); } }

        public event EventHandler<EventArgs> ClientSizeChanged
        {
            add { _window.Resize += value; }
            remove { _window.Resize -= value; }
        }

        public void Run()
        {
            _window.Run();
        }
        public void Exit()
        {
            _window.Exit();
        }

        class ChamberGameWindow : GameWindow
        {
            public ChamberGameWindow(
                    int width=800,
                    int height=600,
                    int major=1,
                    int minor=0,
                    Action onLoadMethod=null,
                    Action<GameTime> onRenderFrameMethod=null,
                    Action<GameTime> onUpdateFrameMethod=null)
                : base(width, height, GraphicsMode.Default, "ChamberLib")//, GameWindowFlags.Default, DisplayDevice.Default, major, minor, GraphicsContextFlags.Default)
            {
                OnLoadMethod = onLoadMethod;
                OnRenderFrameMethod = onRenderFrameMethod;
                OnUpdateFrameMethod = onUpdateFrameMethod;
            }

            public readonly Action OnLoadMethod;
            public readonly Action<GameTime> OnRenderFrameMethod;
            public readonly Action<GameTime> OnUpdateFrameMethod;

            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);

                if (OnLoadMethod != null)
                {
                    OnLoadMethod();
                }
            }

            protected override void OnRenderFrame(FrameEventArgs e)
            {
                base.OnRenderFrame(e);

                if (OnRenderFrameMethod != null)
                {
                    OnRenderFrameMethod(e.Time.ToChamber());
                }
            }

            protected override void OnUpdateFrame(FrameEventArgs e)
            {
                base.OnUpdateFrame(e);

                if (OnUpdateFrameMethod != null)
                {
                    OnUpdateFrameMethod(e.Time.ToChamber());
                }
            }
        }
    }
}

