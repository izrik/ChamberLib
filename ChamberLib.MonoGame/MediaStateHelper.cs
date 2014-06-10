using System;

namespace ChamberLib
{
    public static class MediaStateHelper
    {
        public static ChamberLib.MediaState ToChamber(this Microsoft.Xna.Framework.Media.MediaState state)
        {
            switch (state)
            {
                case Microsoft.Xna.Framework.Media.MediaState.Playing: return ChamberLib.MediaState.Playing;
                case Microsoft.Xna.Framework.Media.MediaState.Paused : return ChamberLib.MediaState.Paused;
                case Microsoft.Xna.Framework.Media.MediaState.Stopped: return ChamberLib.MediaState.Stopped;
            }

            throw new ArgumentException();
        }
    }
}

