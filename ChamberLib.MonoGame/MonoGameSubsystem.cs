using System;
using Xna = Microsoft.Xna.Framework;

namespace ChamberLib
{
    public class MonoGameSubsystem : Xna.Game, ISubsystem
    {
        public MonoGameSubsystem(Action initializeMethod, Action loadContentMethod,
            Action<ChamberLib.GameTime> updateMethod, Action<ChamberLib.GameTime> drawMethod)
        {
            graphics = new Xna.GraphicsDeviceManager(this);

            InitializeMethod = initializeMethod;
            LoadContentMethod = loadContentMethod;
            UpdateMethod = updateMethod;
            DrawMethod = drawMethod;
        }

        protected Xna.GraphicsDeviceManager graphics;

        public readonly Action InitializeMethod;
        public readonly Action LoadContentMethod;
        public readonly Action<ChamberLib.GameTime> UpdateMethod;
        public readonly Action<ChamberLib.GameTime> DrawMethod;

        public Renderer _renderer;
        public IRenderer Renderer { get { return _renderer; } }

        public ContentManager _contentManager;
        public IContentManager ContentManager { get { return _contentManager; } }

        public MediaManager _mediaManager;
        public IMediaManager MediaManager { get { return _mediaManager; } }

        protected override bool BeginDraw()
        {
            return base.BeginDraw();
        }
        protected override void Draw(Xna.GameTime gameTime)
        {
            base.Draw(gameTime);

            if (DrawMethod != null)
            {
                DrawMethod(gameTime.ToChamber());
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

            _renderer = new Renderer(GraphicsDevice);

#if MONOMAC
            var rootDirectory = "Content.MacOS";
#else
            var rootDirectory = "Content";
#endif

            _contentManager = new ChamberLib.ContentManager(GraphicsDevice, Content, rootDirectory: rootDirectory);
            _mediaManager = new MediaManager();

            if (InitializeMethod != null)
            {
                InitializeMethod();
            }
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            if (LoadContentMethod != null)
            {
                LoadContentMethod();
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

            if (UpdateMethod != null)
            {
                UpdateMethod(gameTime.ToChamber());
            }
        }

        public string WindowTitle
        {
            get { return Window.Title; }
            set { Window.Title = value; }
        }

        public RectangleI ClientBounds
        {
            get { return Window.ClientBounds.ToChamber(); }
        }

        public bool AllowUserResizing
        {
            get { return Window.AllowUserResizing; }
            set { Window.AllowUserResizing = value; }
        }

        public event EventHandler<EventArgs> ClientSizeChanged
        {
            add
            {
                Window.ClientSizeChanged += value;
            }
            remove
            {
                Window.ClientSizeChanged -= value;
            }
        }
    }
}

