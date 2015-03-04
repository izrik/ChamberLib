using System;
using System.IO;
using OpenTK.Audio.OpenAL;
using ChamberLib.Content;

namespace ChamberLib
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

