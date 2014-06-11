using System;
using System.Collections.Generic;

namespace ChamberLib
{
    public class MediaManager : IMediaManager
    {
        public void Play(ISong song)
        {
        }
        public void Play(IEnumerable<ISong> songs, int index = 0)
        {
        }
        public void Pause()
        {
        }
        public void Resume()
        {
        }
        public void Stop()
        {
        }
        public float SoundEffectMasterVolume
        {
            get
            {
                return 1;
            }
            set
            {
            }
        }
        public float MusicVolume
        {
            get
            {
                return 1;
            }
            set
            {
            }
        }
        public bool IsMuted
        {
            get
            {
                return false;
            }
            set
            {
            }
        }
        public bool IsRepeating
        {
            get
            {
                return false;
            }
            set
            {
            }
        }
        public bool IsShuffled
        {
            get
            {
                return false;
            }
            set
            {
            }
        }
        public MediaState State
        {
            get
            {
                return MediaState.Stopped;
            }
        }
        public IEnumerable<ISong> SongQueue
        {
            get
            {
                return new ISong[0];
            }
        }
    }
}

