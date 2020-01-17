using System;

namespace ChamberLib
{
    public class GameTime
    {
        // performance: this should be a struct

        public GameTime(TimeSpan totalGameTime=default(TimeSpan), TimeSpan elapsedGameTime=default(TimeSpan))
        {
            TotalGameTime = totalGameTime;
            ElapsedGameTime = elapsedGameTime;
        }

        public TimeSpan TotalGameTime;
        public TimeSpan ElapsedGameTime;
    }
}

