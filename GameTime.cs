using System;

namespace ChamberLib
{
    public struct GameTime
    {
        public GameTime(TimeSpan totalGameTime=default(TimeSpan), TimeSpan elapsedGameTime=default(TimeSpan))
        {
            TotalGameTime = totalGameTime;
            ElapsedGameTime = elapsedGameTime;
        }

        public TimeSpan TotalGameTime;
        public TimeSpan ElapsedGameTime;
    }
}

