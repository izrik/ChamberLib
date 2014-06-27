using System;
using OpenTK;
using OpenTK.Graphics;
using System.Threading.Tasks;
using System.Threading;
using OpenTK.Graphics.OpenGL;

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
            _content = new ContentManager();
            _media = new MediaManager();

            Window = new ChamberGameWindow(
                width: 800,
                height: 480,
                major: openglMajorVersion,
                minor: openglMinorVersion,
                onLoadMethod: onLoadMethod,
                onRenderFrameMethod: onRenderFrameMethod,
                onUpdateFrameMethod: onUpdateFrameMethod);

            _renderer = new Renderer(this);
        }

        public readonly ChamberGameWindow Window;

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
            get { return Window.Title; }
            set { Window.Title = value; }
        }

        public bool AllowUserResizing
        {
            get { return false; }
            set { }
        }

        public RectangleI ClientBounds { get { throw new NotImplementedException(); } }

        public event EventHandler<EventArgs> ClientSizeChanged
        {
            add { Window.Resize += value; }
            remove { Window.Resize -= value; }
        }

        public void Run()
        {
            Window.Run();
        }
        public void Exit()
        {
            Window.Exit();
        }

        public class ChamberGameWindow : GameWindow
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

                GL.ClearColor(0, 0, 0, 0);

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

                SwapBuffers();
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

