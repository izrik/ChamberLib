using System;
using System.IO;
using NAudio.Wave;
using OpenTK.Audio.OpenAL;
using NVorbis;

namespace ChamberLib
{
    public class SoundEffect : ISoundEffect
    {
        public SoundEffect(string name, Stream stream, FileFormat fileFormat)
        {
            if (stream == null) throw new ArgumentNullException("stream");

            Name = name;
            _stream = stream;

            switch (fileFormat)
            {
                case FileFormat.Wav:
                    var wfr = new WaveFileReader(_stream);

                    _numChannels = wfr.WaveFormat.Channels;
                    _bitsPerSample = wfr.WaveFormat.BitsPerSample;
                    _samplesPerSecond = wfr.WaveFormat.SampleRate;

                    int channels = wfr.WaveFormat.Channels;
                    int bitsPerSample = wfr.WaveFormat.BitsPerSample;

                    if (channels != 1 && channels != 2) throw new NotSupportedException("The sound format is not supported");
                    if (bitsPerSample != 8 && bitsPerSample != 16) throw new NotSupportedException("The sound format is not supported");

                    int length = (int)(wfr.BlockAlign * (wfr.Length / wfr.BlockAlign));
                    _audioData = new byte[length];
                    wfr.Read(_audioData, 0, length); //TODO: remaining data might be more than MAX_INT

                    break;

                case FileFormat.Ogg:
                    var reader = new VorbisReader(_stream, false);
                    _numChannels = reader.Channels;
                    _bitsPerSample = 16;
                    _samplesPerSecond = reader.SampleRate;
                    var numSamples = (int)(reader.TotalSamples*_numChannels); //TODO: samples might be more than MAX_INT
                    float[] buffer1 = new float[numSamples];
                    int numSamplesRead = reader.ReadSamples(buffer1, 0, numSamples);
                    if (numSamplesRead != numSamples)
                    {
                        Console.WriteLine("numSamplesRead does not match numSamples - {0}", name);
                    }
                    _audioData = new byte[numSamples * 2];
                    int i;
                    for (i = 0; i < numSamples; i++)
                    {
                        var sample = (short)Math.Max(Math.Min((int)(buffer1[i] * 32767f), short.MaxValue), short.MinValue);
                        _audioData[2 * i + 0] = (byte)(sample & 0xff);
                        _audioData[2 * i + 1] = (byte)((sample & 0xff00) >> 8);
                    }
                    break;

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

