using System;

namespace ChamberLib
{
    public interface ISubsystem
    {
        IRenderer Renderer { get; }
        IContentManager ContentManager { get; }
        IMediaManager MediaManager { get; }

        IGamePad GetGamePad(PlayerIndex index);
        IKeyboard GetKeyboard();

        string WindowTitle { get; set; }
        bool AllowUserResizing { get; set; }
        RectangleI ClientBounds { get; }
        event EventHandler<EventArgs> ClientSizeChanged;

        void Run();
        void Exit();
    }
}

