using System;
using System.IO;
using NAudio.Wave;
using OpenTK.Audio.OpenAL;

namespace ChamberLib
{
    public class SoundEffect : ISoundEffect
    {
        public SoundEffect(string name, Stream stream)
        {
            if (stream == null) throw new ArgumentNullException("stream");

            Name = name;
            _stream = stream;

            _wfr = new WaveFileReader(_stream);

            int channels = _wfr.WaveFormat.Channels;
            int bitsPerSample = _wfr.WaveFormat.BitsPerSample;

            if (channels != 1 && channels != 2) throw new NotSupportedException("The sound format is not supported");
            if (bitsPerSample != 8 && bitsPerSample != 16) throw new NotSupportedException("The sound format is not supported");

            int length = (int)(_wfr.SampleCount * (bitsPerSample / 8) * channels);
            _audioData = new byte[length];
            _wfr.Read(_audioData, 0, length); //TODO: remaining data might be more than MAX_INT
        }

        public readonly string Name;
        Stream _stream;
        WaveFileReader _wfr;
        byte[] _audioData;
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

            int channels = _wfr.WaveFormat.Channels;
            int bitsPerSample = _wfr.WaveFormat.BitsPerSample;
            int samplesPerSecond = _wfr.WaveFormat.SampleRate;

            ALFormat format;
            if (channels == 1 && bitsPerSample == 8)
            {
                format = ALFormat.Mono8;
            }
            else if (channels == 1)
            {
                format = ALFormat.Mono16;
            }
            else if (bitsPerSample == 8)
            {
                format = ALFormat.Stereo8;
            }
            else
            {
                format = ALFormat.Stereo16;
            }

            _source = AL.GenSource();
            _buffer = AL.GenBuffer();
            AL.BufferData(_buffer, format, _audioData, _audioData.Length, samplesPerSecond);

            IsReady = true;
        }

        public ISoundEffectInstance CreateInstance()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

