using System;
using System.IO;
using NAudio.Wave;
using OpenTK.Audio.OpenAL;
using NVorbis;
using System.Diagnostics;

namespace ChamberLib
{
    public class SoundEffect : ISoundEffect
    {
        protected SoundEffect(string name, Stream stream, int numChannels,
                                int bitsPerSample, int samplesPerSecond,
                                byte[] audioData)
        {
            Name = name;
            _stream = stream;
            _numChannels = numChannels;
            _bitsPerSample = bitsPerSample;
            _samplesPerSecond = samplesPerSecond;
            _audioData = audioData;
        }

        public static SoundEffect Create(string name, Stream stream, FileFormat fileFormat)
        {
            if (stream == null) throw new ArgumentNullException("stream");

            int numChannels;
            int bitsPerSample;
            int samplesPerSecond;
            byte [] audioData;

            switch (fileFormat)
            {
            case FileFormat.Wav:
                var wfr = new WaveFileReader(stream);

                numChannels = wfr.WaveFormat.Channels;
                bitsPerSample = wfr.WaveFormat.BitsPerSample;
                samplesPerSecond = wfr.WaveFormat.SampleRate;

                if (numChannels != 1 && numChannels != 2) throw new NotSupportedException("The sound format is not supported");
                if (bitsPerSample != 8 && bitsPerSample != 16) throw new NotSupportedException("The sound format is not supported");

                int length = (int)(wfr.BlockAlign * (wfr.Length / wfr.BlockAlign)); //TODO: length might be more than MAX_INT
                audioData = new byte[length];
                wfr.Read(audioData, 0, length);

                return new SoundEffect(name, stream, numChannels,
                    bitsPerSample, samplesPerSecond, audioData);

            case FileFormat.Ogg:
                var reader = new VorbisReader(stream, false);

                numChannels = reader.Channels;
                bitsPerSample = 16;
                samplesPerSecond = reader.SampleRate;

                if (numChannels != 1 && numChannels != 2) throw new NotSupportedException("The sound format is not supported");
                if (bitsPerSample != 8 && bitsPerSample != 16) throw new NotSupportedException("The sound format is not supported");

                var numSamples = (int)(reader.TotalSamples * numChannels); //TODO: samples might be more than MAX_INT
                float[] buffer1 = new float[numSamples];
                int time1 = Environment.TickCount;
                int numSamplesRead = reader.ReadSamples(buffer1, 0, numSamples);
                int time2 = Environment.TickCount;
                if (numSamplesRead != numSamples)
                {
                    Debug.WriteLine("numSamplesRead does not match numSamples - {0}", name);
                }

                audioData = new byte[numSamples * 2];
                // convert from float to short
                int i;
                for (i = 0; i < numSamples; i++)
                {
                    var sample = (short)Math.Max(Math.Min((int)(buffer1[i] * 32767f), short.MaxValue), short.MinValue);
                    audioData[2 * i + 0] = (byte)(sample & 0xff);
                    audioData[2 * i + 1] = (byte)((sample & 0xff00) >> 8);
                }
                int time3 = Environment.TickCount;
                return new SoundEffect(name, stream, numChannels,
                    bitsPerSample, samplesPerSecond, audioData);

            default:
                throw new IOException(string.Format("The stream \"{0}\" is of an unknown type, \"{1}\"", name, fileFormat));

            }
        }

        public readonly string Name;
        readonly Stream _stream;
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

