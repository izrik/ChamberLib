using System;
using System.Collections.Generic;

namespace ChamberLib.OpenTK
{
    public class GamePad : IGamePad
    {
        static readonly Dictionary<PlayerIndex, GamePad> _cache = new Dictionary<PlayerIndex, GamePad>();

        static GamePad()
        {
            _cache[PlayerIndex.One] = new GamePad();
            _cache[PlayerIndex.Two] = new GamePad();
            _cache[PlayerIndex.Three] = new GamePad();
            _cache[PlayerIndex.Four] = new GamePad();
        }

        public static GamePad GetGamePad(PlayerIndex index)
        {
            if (_cache.ContainsKey(index))
            {
                return _cache[index];
            }

            var gamepad = new GamePad();
            _cache[index] = gamepad;
            return gamepad;
        }

        public GamePadState GetState()
        {
            return new GamePadState();
        }
    }
}

