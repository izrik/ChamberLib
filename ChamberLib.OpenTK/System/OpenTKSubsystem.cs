﻿
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
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Audio;
using ChamberLib.Content;
using ChamberLib.OpenTK.Audio;
using ChamberLib.OpenTK.Content;
using ChamberLib.OpenTK.Images;
using ChamberLib.OpenTK.Input;
using ChamberLib.OpenTK.Math;
using ChamberLib.OpenTK.System;

namespace ChamberLib.OpenTK.System
{
    public class OpenTKSubsystem : ISubsystem
    {
        public OpenTKSubsystem(
            int openglMajorVersion,
            int openglMinorVersion,
            Action onLoadMethod=null,
            Action<GameTime> onRenderFrameMethod=null,
            Action<GameTime> onUpdateFrameMethod=null,
            IContentManager contentManager=null,
            IContentImporter contentImporter=null,
            IContentProcessor contentProcessor=null)
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
            if (contentManager == null)
            {
                if (contentImporter == null)
                {
                    var glsl = new GlslShaderImporter();
                    var wav = new WaveSoundEffectImporter();
                    var ogg = new OggVorbisSoundEffectImporter(
                        wav.ImportSoundEffect);
                    contentImporter =
                        new BuiltinContentImporter(
                            new ResolvingContentImporter(
                                new ContentImporter(
                                    new ChModelImporter().ImportModel,
                                    new BasicTextureImporter().ImportTexture,
                                    glsl.ImportShaderStage,
                                    null,
                                    new BasicSongImporter().ImportSong,
                                    ogg.ImportSoundEffect
                                ),
                                basePath: "Content.OpenTK"));
                }

                if (contentProcessor == null)
                {
                    contentProcessor =
                        new CachingContentProcessor(
                            new OpenTKContentProcessor());
                }

                contentManager = new CachingContentManager(
                    new ContentManager(contentImporter, contentProcessor));
            }
            ContentManager = contentManager;
        }

        public readonly ChamberGameWindow Window;
        GameWindow ISubsystem.GameWindow
        {
            get { return Window; }
        }

        readonly Renderer _renderer;
        public IRenderer Renderer { get { return _renderer; } }

        public IContentManager ContentManager { get; protected set; }

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
        public IMouse GetMouse()
        {
            return Mouse.GetMouse(Window, Renderer);
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

        public RectangleI ClientBounds { get { return Window.ClientRectangleI; } }

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

            public RectangleI ClientRectangleI
            {
                get { return this.ClientRectangle.ToChamber(); }
            }
        }

        public IRenderTarget CreateRenderTarget(int width, int height,
            PixelFormat pixelFormat=PixelFormat.Rgba)
        {
            var colorTexture = ContentManager.CreateTexture(width, height,
                new Color[] { Color.Black }, pixelFormat);

            var depthBuffer = new RenderBuffer(width, height);

            var fbo = new FrameBuffer(Renderer, colorTexture, depthBuffer);

            return fbo;
        }
    }
}

