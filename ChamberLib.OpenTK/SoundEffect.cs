using System;
using System.IO;
using NAudio.Wave;
using OpenTK.Audio.OpenAL;

namespace ChamberLib
{
    public class SoundEffect : ISoundEffect
    {
        public SoundEffect(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException("stream");

            this.stream = stream;
        }

        Stream stream;
        byte[] audioData;
        int buffer;
        int source;

        #region ISoundEffect implementation

        public void Play()
        {
            MakeReady();

            AL.Source(source, ALSourcei.Buffer, buffer);
            AL.SourcePlay(source);
        }

        bool IsReady = false;
        void MakeReady()
        {
            if (IsReady) return;


            buffer = AL.GenBuffer();
            source = AL.GenSource();

            var wfr = new WaveFileReader(stream);
            int channels = wfr.WaveFormat.Channels;
            int bitsPerSample = wfr.WaveFormat.BitsPerSample;
            int samplesPerSecond = wfr.WaveFormat.SampleRate;

            int dataLength = (int)((bitsPerSample / 8) * wfr.SampleCount);
            audioData = new byte[dataLength];
            wfr.Read(audioData, 0, dataLength); //TODO: remaining data might be more than MAX_INT

            if (channels != 1 && channels != 2) throw new NotSupportedException("The sound format is not supported");
            if (bitsPerSample != 8 && bitsPerSample != 16) throw new NotSupportedException("The sound format is not supported");
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

            AL.BufferData(buffer, format, audioData, audioData.Length, samplesPerSecond);

            IsReady = true;
        }

        public ISoundEffectInstance CreateInstance()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

