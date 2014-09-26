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

        void Play(ISong song);
        void Play(IEnumerable<ISong> songs, int index = 0);
        void Pause();
        void Resume();
        void Stop();

    }
}

