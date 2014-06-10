using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;
using ChamberLib;

namespace ChamberLib
{
    public class MusicManager : IDisposable
    {
        public MusicManager()
        {
#if !MONOMAC
            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;
#endif
            MediaPlayer.IsRepeating = false;
        }

        public void PlaySong(ISong song)
        {
            if (song == null)
            {
                MediaPlayer.Stop();
                return;
            }

            if (Songs == null)
            {
                Songs = new ISong[] { song };
            }

            if (Songs.Contains(song))
            {
                MediaPlayer.Play(((SongAdapter)song).Song);
            }
            else
            {
                MediaPlayer.Stop();
            }
        }

        public void PlayNewSong()
        {
            if (Songs == null || Songs.Length < 1)
            {
                PlaySong(null);
                return;
            }

            int index;
            int count = 0;

            do
            {
                index = _rand.Next(Songs.Length);
                count++;
            }
            while (index == _lastIndex && count <= Songs.Length);

            _lastIndex = index;

            PlaySong(index < Songs.Length ? Songs[index] : null);
        }

        public void Stop()
        {
            MediaPlayer.Stop();
        }

        int _lastIndex;
        Random _rand = new Random();
        public ISong[] Songs;

        void MediaPlayer_MediaStateChanged(object sender, EventArgs e)
        {
            if (Enabled && MediaPlayer.State == MediaState.Stopped)
            {
                PlayNewSong();
            }
        }

        bool _enabled = true;
        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                _enabled = value;

                //if (_enabled)
                //{
                //    PlayNewSong();
                //}
                //else
                //{
                //    MediaPlayer.Stop();
                //}
            }
        }

        public bool IsMuted
        {
            get { return MediaPlayer.IsMuted; }
            set { MediaPlayer.IsMuted = value; }
        }

        //public void Play()
        //{
        //}
        //public void Stop()
        //{
        //}
        //public void Pause()
        //{
        //}
        //public void Resume()
        //{
        //}

        #region IDisposable Members

        public void Dispose()
        {
#if !MONOMAC
            MediaPlayer.MediaStateChanged -= MediaPlayer_MediaStateChanged;
#endif
        }

        #endregion
    }
}
