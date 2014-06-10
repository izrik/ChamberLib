using System;
using Xna = Microsoft.Xna.Framework;

namespace ChamberLib
{
    public class MonoGameSubsystem : ISubsystem
    {
        public MonoGameSubsystem(Action initializeMethod, Action loadContentMethod,
            Action<ChamberLib.GameTime> updateMethod, Action<ChamberLib.GameTime> drawMethod)
        {
            InitializeMethod = initializeMethod;
            LoadContentMethod = loadContentMethod;
            UpdateMethod = updateMethod;
            DrawMethod = drawMethod;

            _xnagame = new XnaGame(this);
        }


        public readonly Action InitializeMethod;
        public readonly Action LoadContentMethod;
        public readonly Action<ChamberLib.GameTime> UpdateMethod;
        public readonly Action<ChamberLib.GameTime> DrawMethod;

        internal Renderer _renderer;
        public IRenderer Renderer { get { return _renderer; } }

        internal ContentManager _contentManager;
        public IContentManager ContentManager { get { return _contentManager; } }

        internal MediaManager _mediaManager;
        public IMediaManager MediaManager { get { return _mediaManager; } }

        public IGamePad GetGamePad(PlayerIndex index)
        {
            return GamePad.GetGamePad(index);
        }

        public IKeyboard GetKeyboard()
        {
            return Keyboard.GetKeyboard();
        }

        XnaGame _xnagame;

        class XnaGame : Xna.Game
        {
            public XnaGame(MonoGameSubsystem subsystem)
            {
                if (subsystem == null) throw new ArgumentNullException("subsystem");

                _subsystem = subsystem;

                graphics = new Xna.GraphicsDeviceManager(this);
            }

            public readonly Xna.GraphicsDeviceManager graphics;
            readonly MonoGameSubsystem _subsystem;

            protected override bool BeginDraw()
            {
                return base.BeginDraw();
            }
            protected override void Draw(Xna.GameTime gameTime)
            {
                base.Draw(gameTime);

                if (_subsystem.DrawMethod != null)
                {
                    _subsystem.DrawMethod(gameTime.ToChamber());
                }
            }
            protected override void EndDraw()
            {
                base.EndDraw();
            }

            protected override void BeginRun()
            {
                base.BeginRun();
            }
            protected override void EndRun()
            {
                base.EndRun();
            }

            protected override void Initialize()
            {
                base.Initialize();

                _subsystem._renderer = new Renderer(GraphicsDevice);

#if MONOMAC
                var rootDirectory = "Content.MacOS";
#else
                var rootDirectory = "Content";
#endif

                _subsystem._contentManager = new ChamberLib.ContentManager(GraphicsDevice, Content, rootDirectory: rootDirectory);
                _subsystem._mediaManager = new MediaManager();

                if (_subsystem.InitializeMethod != null)
                {
                    _subsystem.InitializeMethod();
                }
            }

            protected override void LoadContent()
            {
                base.LoadContent();

                if (_subsystem.LoadContentMethod != null)
                {
                    _subsystem.LoadContentMethod();
                }
            }
            protected override void UnloadContent()
            {
                base.UnloadContent();
            }

            protected override void OnActivated(object sender, EventArgs args)
            {
                base.OnActivated(sender, args);
            }
            protected override void OnDeactivated(object sender, EventArgs args)
            {
                base.OnDeactivated(sender, args);
            }
            protected override void OnExiting(object sender, EventArgs args)
            {
                base.OnExiting(sender, args);
            }

            protected override void Update(Xna.GameTime gameTime)
            {
                base.Update(gameTime);

                if (_subsystem.UpdateMethod != null)
                {
                    _subsystem.UpdateMethod(gameTime.ToChamber());
                }
            }
        }

        public void Run()
        {
            _xnagame.Run();
        }

        public void Exit()
        {
            _xnagame.Exit();
        }

        public string WindowTitle
        {
            get { return _xnagame.Window.Title; }
            set { _xnagame.Window.Title = value; }
        }

        public RectangleI ClientBounds
        {
            get { return _xnagame.Window.ClientBounds.ToChamber(); }
        }

        public bool AllowUserResizing
        {
            get { return _xnagame.Window.AllowUserResizing; }
            set { _xnagame.Window.AllowUserResizing = value; }
        }

        public event EventHandler<EventArgs> ClientSizeChanged
        {
            add
            {
                _xnagame.Window.ClientSizeChanged += value;
            }
            remove
            {
                _xnagame.Window.ClientSizeChanged -= value;
            }
        }
    }
}

