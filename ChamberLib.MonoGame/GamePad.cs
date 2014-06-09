using System;
using System.Collections.Generic;

namespace ChamberLib
{
    public class GamePad : IGamePad
    {
        static Dictionary<PlayerIndex, IGamePad> _cache = new Dictionary<PlayerIndex, IGamePad>();

        public static IGamePad GetGamePad(PlayerIndex index)
        {
            if (_cache.ContainsKey(index))
            {
                return _cache[index];
            }

            var gamepad = new GamePad(index);
            _cache[index] = gamepad;

            return gamepad;
        }

        protected GamePad(PlayerIndex index)
        {
            Index = index;
        }

        public PlayerIndex Index;

        public GamePadState GetState()
        {
            return Microsoft.Xna.Framework.Input.GamePad.GetState(Index.ToXna()).ToChamber();
        }
    }
}

