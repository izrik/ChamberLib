using System;
using XMediaPlayer = Microsoft.Xna.Framework.Media.MediaPlayer;
using XSoundEffect = Microsoft.Xna.Framework.Audio.SoundEffect;
using System.Collections.Generic;

namespace ChamberLib
{
    public class MediaManager : IMediaManager, IDisposable
    {
        public MediaManager()
        {
            XMediaPlayer.MediaStateChanged += HandleMediaStateChanged;
        }

        #region IDisposable implementation
        public void Dispose()
        {
            XMediaPlayer.MediaStateChanged -= HandleMediaStateChanged;
        }
        #endregion


        public float SoundEffectMasterVolume
        {
            get { return XSoundEffect.MasterVolume; }
            set { XSoundEffect.MasterVolume = value; }
        }

        public float MusicVolume
        {
            get { return XMediaPlayer.Volume; }
            set { XMediaPlayer.Volume = value; }
        }

        public void Play(IEnumerable<ISong> songs, int index=0)
        {
            Stop();
            SongQueue = songs;
            _currentIndex = index;
            _currentShuffledIndex = _shuffledQueue.IndexOf(_queue[_currentIndex]);

            State = MediaState.Playing;
            XMediaPlayer.Play(_shuffledQueue[_currentShuffledIndex].ToXna());
        }
        public void Pause()
        {
            State = MediaState.Paused;
            XMediaPlayer.Pause();
        }
        public void Resume()
        {
            State = MediaState.Playing;
            XMediaPlayer.Resume();
        }
        public void Stop()
        {
            State = MediaState.Stopped;
            XMediaPlayer.Stop();
        }
        public bool IsMuted
        {
            get { return XMediaPlayer.IsMuted; }
            set { XMediaPlayer.IsMuted = value; }
        }
        bool _isRepeating = true;
        public bool IsRepeating
        {
            get { return _isRepeating; }
            set { _isRepeating = value; }
        }
        bool _isShuffled = false;
        public bool IsShuffled
        {
            get { return _isShuffled; }
            set
            {
                _isShuffled = value;

                UpdateShuffle();
            }
        }
        public MediaState State
        {
            get;
            protected set;
        }

        int _currentIndex = 0;
        int _currentShuffledIndex = 0;

        readonly List<ISong> _queue = new List<ISong>();
        readonly List<ISong> _shuffledQueue = new List<ISong>();

        public IEnumerable<ISong> SongQueue
        {
            get { return _queue; }
            protected set
            {
                _queue.Clear();
                _queue.AddRange(value);

                UpdateShuffle();
            }
        }

        void UpdateShuffle()
        {
            if (_isShuffled)
            {
                Shuffle(_queue, _shuffledQueue);
                _currentShuffledIndex = _shuffledQueue.IndexOf(_queue[_currentIndex]);
            }
            else
            {
                _shuffledQueue.Clear();
                _shuffledQueue.AddRange(_queue);
                _currentShuffledIndex = _currentIndex;
            }
        }

        static void Shuffle(List<ISong> from, List<ISong> to)
        {
            from = new List<ISong>(from);
            to.Clear();
            var rand = new Random();
            while (from.Count > 0)
            {
                var index = rand.Next(from.Count);
                to.Add(from[index]);
                from.RemoveAt(index);
            }
        }

        void HandleMediaStateChanged(object sender, EventArgs e)
        {
            if (State == MediaState.Playing &&
                XMediaPlayer.State == Microsoft.Xna.Framework.Media.MediaState.Stopped)
            {
                if (_currentShuffledIndex == _shuffledQueue.Count - 1)
                {
                    if (IsRepeating)
                    {
                        if (IsShuffled)
                        {
                            UpdateShuffle();
                            _currentShuffledIndex = 0;
                            _currentIndex = _queue.IndexOf(_shuffledQueue[0]);
                        }
                        else
                        {
                            UpdateShuffle();
                            _currentIndex = 0;
                            _currentShuffledIndex = 0;
                        }

                        XMediaPlayer.Play(_shuffledQueue[_currentShuffledIndex].ToXna());
                    }
                    else
                    {
                        Stop();
                    }
                }
                else
                {
                    XMediaPlayer.Play(_shuffledQueue[_currentShuffledIndex].ToXna());
                }
            }
        }
    }
}

