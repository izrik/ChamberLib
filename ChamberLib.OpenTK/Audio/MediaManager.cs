
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
using System.Linq;
using ChamberLib.OpenTK.Audio;
using OpenTK.Audio.OpenAL;

namespace ChamberLib.OpenTK.Audio
{
    public class MediaManager : IMediaManager
    {
        List<Song> _songQueue = new List<Song>();
        List<Song> _orderedSongQueue = new List<Song>();
        int _playIndex = 0;

        bool isReady = false;
        int _source;

        public void Play(IEnumerable<ISong> songs, int index = -1)
        {
            Stop();

            SetSongQueue(songs);

            Song songToPlay = index < 0 ? _orderedSongQueue[0] : _songQueue[index];

            PlayInternal(songToPlay);
        }
        public void Pause()
        {
            if (State == MediaState.Playing)
            {
                State = MediaState.Paused;
                AL.SourcePause(_source);
            }
        }
        public void Resume()
        {
            if (State == MediaState.Paused)
            {
                State = MediaState.Playing;
                AL.SourcePlay(_source);
            }
        }
        public void Stop()
        {
            _state = MediaState.Stopped;
            AL.SourceStop(_source);
        }
        float _soundEffectMasterVolume = 1;
        public float SoundEffectMasterVolume
        {
            get { return _soundEffectMasterVolume; }
            set { _soundEffectMasterVolume = value; }
        }
        float _musicVolume = 1;
        public float MusicVolume
        {
            get
            {
                return (IsMuted ? 0 : _musicVolume);
            }
            set { _musicVolume = value; }
        }
        bool _isMuted = false;
        public bool IsMuted
        {
            get { return _isMuted; }
            set { _isMuted = value; }
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
                SetOrderedSongQueue();
            }
        }
        MediaState _state = MediaState.Stopped;
        public MediaState State
        {
            get { return _state; }
            protected set { _state = value; }
        }

        public Song CurrentSong
        {
            get
            {
                if (_playIndex >= _orderedSongQueue.Count)
                {
                    _playIndex = -1;
                }

                if (_playIndex < 0)
                {
                    return null;
                }

                return _orderedSongQueue[_playIndex];
            }
        }

        void SetSongQueue(IEnumerable<ISong> songs)
        {
            _songQueue = songs.Cast<Song>().ToList();
            SetOrderedSongQueue();
            _playIndex = 0;
        }
        void SetOrderedSongQueue()
        {
            var current = CurrentSong;
            _orderedSongQueue.Clear();
            _orderedSongQueue.AddRange(_songQueue);
            if (IsShuffled)
            {
                _orderedSongQueue.Shuffle();
            }

            if (current != null)
            {
                _playIndex = _orderedSongQueue.IndexOf(current);
            }
            else
            {
                _playIndex = 0;
            }
        }

        public IEnumerable<ISong> SongQueue
        {
            get
            {
                return _songQueue.ToList();
            }
        }

        ALSourceState _lastState = ALSourceState.Stopped;
        public void Update(GameTime gameTime)
        {
            int state;
            AL.GetSource(_source, ALGetSourcei.SourceState, out state);
            if (_lastState == ALSourceState.Playing &&
                (ALSourceState)state == ALSourceState.Stopped)
            {
                _playIndex++;
                if (_playIndex >= _orderedSongQueue.Count)
                {
                    if (IsRepeating && _orderedSongQueue.Count > 0)
                    {
                        if (IsShuffled)
                        {
                            _orderedSongQueue.Shuffle();
                        }

                        _playIndex = 0;
                        // play next song
                        PlayInternal(_orderedSongQueue[_playIndex]);
                    }
                    else
                    {
                        // stop
                        Stop();
                    }
                }
                else
                {
                    // play next song
                    PlayInternal(_orderedSongQueue[_playIndex]);
                }
            }

            _lastState = (ALSourceState)state;
        }

        void PlayInternal(Song song)
        {
            MakeReady();
            _playIndex = _orderedSongQueue.IndexOf(song);
            song.Play(_source);
        }

        void MakeReady()
        {
            if (isReady) return;

            // create new AL source
            _source = AL.GenSource();

            isReady = true;
        }
    }
}

