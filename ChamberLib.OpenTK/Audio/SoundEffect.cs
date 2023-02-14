
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
using System.IO;
using OpenTK.Audio.OpenAL;
using ChamberLib.Content;

namespace ChamberLib.OpenTK.Audio
{
    public class SoundEffect : ISoundEffect
    {
        public SoundEffect(SoundEffectContent content)
        {
            Name = content.Name;
            _numChannels = content.NumChannels;
            _bitsPerSample = content.BitsPerSample;
            _samplesPerSecond = content.SamplesPerSecond;
            _audioData = content.AudioData;
        }

        public readonly string Name;
        readonly int _numChannels;
        readonly int _bitsPerSample;
        readonly int _samplesPerSecond;
        readonly byte[] _audioData;
        int _buffer;
        int _source;

        #region ISoundEffect implementation

        public void Play()
        {
            MakeReady();

            Play(_source);
        }
        public void Play(int source)
        {
            MakeReady();

            AL.Source(source, ALSourcei.Buffer, _buffer);
            AL.SourcePlay(source);
        }

        bool IsReady = false;
        void MakeReady()
        {
            if (IsReady) return;

            ALFormat format;
            if (_numChannels == 1 && _bitsPerSample == 8)
            {
                format = ALFormat.Mono8;
            }
            else if (_numChannels == 1)
            {
                format = ALFormat.Mono16;
            }
            else if (_bitsPerSample == 8)
            {
                format = ALFormat.Stereo8;
            }
            else
            {
                format = ALFormat.Stereo16;
            }

            _source = AL.GenSource();
            _buffer = AL.GenBuffer();
            AL.BufferData(_buffer, format, _audioData, _audioData.Length, _samplesPerSecond);

            IsReady = true;
        }

        public ISoundEffectInstance CreateInstance()
        {
            throw new NotImplementedException();
        }

        #endregion

        public enum FileFormat
        {
            Wav,
            Ogg,
        }
    }
}

