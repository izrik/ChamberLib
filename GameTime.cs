using System;

namespace ChamberLib
{
    public class GameTime
    {
        public static readonly GameTime ZERO = new GameTime();

        public GameTime(TimeSpan totalGameTime=default(TimeSpan), TimeSpan elapsedGameTime=default(TimeSpan))
        {
            TotalGameTime = totalGameTime;
            ElapsedGameTime = elapsedGameTime;
        }

        public TimeSpan TotalGameTime;
        public TimeSpan ElapsedGameTime;
    }
}

