
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

namespace ChamberLib
{
    public interface IMediaManager
    {
        float SoundEffectMasterVolume { get; set; }

        float MusicVolume { get; set; }
        bool IsMuted { get; set; }
        bool IsRepeating { get; set; }
        bool IsShuffled { get; set; }
        MediaState State { get; }

        IEnumerable<ISong> SongQueue { get; }

        void Play(IEnumerable<ISong> songs, int index = 0);
        void Pause();
        void Resume();
        void Stop();

    }

    public static class MediaManagerHelper
    {
        public static void Play(this IMediaManager mediaManager, ISong song)
        {
            mediaManager.Play(new [] { song }, 0);
        }
    }
}

