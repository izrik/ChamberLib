
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

