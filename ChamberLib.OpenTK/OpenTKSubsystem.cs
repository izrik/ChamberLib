using System;
using OpenTK;
using OpenTK.Graphics;
using System.Threading.Tasks;
using System.Threading;
using OpenTK.Graphics.OpenGL;
using OpenTK.Audio;
using ChamberLib.Content;

namespace ChamberLib
{
    public class OpenTKSubsystem : ISubsystem
    {
        public OpenTKSubsystem(
            int openglMajorVersion,
            int openglMinorVersion,
            Action onLoadMethod=null,
            Action<GameTime> onRenderFrameMethod=null,
            Action<GameTime> onUpdateFrameMethod=null)
        {

            // audio playback
            _media = new MediaManager();
            _audioContext = new AudioContext();

            // window management
            Window = new ChamberGameWindow(
                width: 800,
                height: 480,
                major: openglMajorVersion,
                minor: openglMinorVersion,
                onLoadMethod: Load + onLoadMethod,
                onRenderFrameMethod: Render + onRenderFrameMethod,
                onUpdateFrameMethod: Update + onUpdateFrameMethod);

            // renderer
            _renderer = new Renderer(this);

            // content management
            var importer =
                new BuiltinContentImporter(
                    new ResolvingContentImporter(
                        new ContentImporter(
                            new ChModelImporter().ImportModel,
                            new BasicTextureImporter().ImportTexture,
                            new GlslShaderImporter().ImportShader,
                            null,
                            new BasicSongImporter().ImportSong,
                            new OggVorbisSoundEffectImporter(
                                new WaveSoundEffectImporter().ImportSoundEffect).ImportSoundEffect
                        ),
                        basePath: "Content.OpenTK"));

            var processor =
                new CachingContentProcessor(
                    new OpenTKContentProcessor(_renderer));

            _content = new ContentManager(importer, processor);
            _cachingContent = new CachingContentManager(_content);
        }

        public readonly ChamberGameWindow Window;

        readonly Renderer _renderer;
        public IRenderer Renderer { get { return _renderer; } }

        readonly ContentManager _content;
        readonly CachingContentManager _cachingContent;
        public IContentManager ContentManager { get { return _cachingContent; } }

        readonly MediaManager _media;
        public IMediaManager MediaManager { get { return _media; } }

        readonly AudioContext _audioContext;
        public AudioContext AudioContext { get { return _audioContext; } }

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

        public bool IsActive
        {
            get { return Window.Focused; }
        }

        public void Run()
        {
            Window.Run();
        }

        void Load()
        {
        }

        void Update(GameTime gameTime)
        {
            _media.Update(gameTime);
        }

        void Render(GameTime gameTime)
        {
        }

        public void Exit()
        {
            Window.Exit();
        }

        public class ChamberGameWindow : GameWindow
        {
            public ChamberGameWindow(
                    int width,
                    int height,
                    int major,
                    int minor,
                    Action onLoadMethod=null,
                    Action<GameTime> onRenderFrameMethod=null,
                    Action<GameTime> onUpdateFrameMethod=null)
                : base(
                    width, height,
                    GraphicsMode.Default,
                    "ChamberLib",
                    GameWindowFlags.Default,
                    DisplayDevice.Default,
                    major, minor,
                    GraphicsContextFlags.Default)
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

                GL.Enable(EnableCap.DepthTest);

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
                    OnRenderFrameMethod(_gameTime);
                }

                SwapBuffers();
            }

            float _totalSeconds = 0;
            GameTime _gameTime;

            protected override void OnUpdateFrame(FrameEventArgs e)
            {
                base.OnUpdateFrame(e);

                _totalSeconds += (float)e.Time;
                _gameTime = 
                    new GameTime(
                        TimeSpan.FromSeconds(_totalSeconds),
                        TimeSpan.FromSeconds(e.Time));

                if (OnUpdateFrameMethod != null)
                {
                    OnUpdateFrameMethod(_gameTime);
                }
            }
        }
    }
}

