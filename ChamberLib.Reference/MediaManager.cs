using System;
using System.Collections.Generic;

namespace ChamberLib
{
    public class MediaManager : IMediaManager
    {
        public MediaManager()
        {
        }

        #region IMediaManager implementation

        public void Play(IEnumerable<ISong> songs, int index=0)
        {
            throw new NotImplementedException();
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Resume()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public float SoundEffectMasterVolume
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public float MusicVolume
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool IsMuted
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool IsRepeating
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool IsShuffled
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public MediaState State
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<ISong> SongQueue
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}

