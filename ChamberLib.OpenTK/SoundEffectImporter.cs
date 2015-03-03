using System;
using System.IO;
using FileFormat = ChamberLib.SoundEffect.FileFormat;
using NAudio.Wave;
using NVorbis;
using System.Diagnostics;

namespace ChamberLib
{
    public class SoundEffectImporter
    {
        public SoundEffectContent ImportSoundEffect(string name, string resolvedFilename)
        {
            if (File.Exists(resolvedFilename))
            {
            }
            else if (File.Exists(resolvedFilename + ".wav"))
            {
                resolvedFilename += ".wav";
            }
            else if (File.Exists(resolvedFilename + ".ogg"))
            {
                resolvedFilename += ".ogg";
            }
            else
            {
                throw new FileNotFoundException("The sound file could not be found.", resolvedFilename);
            }

            SoundEffect.FileFormat fileFormat;
            if (resolvedFilename.ToLower().EndsWith(".wav"))
            {
                fileFormat = SoundEffect.FileFormat.Wav;
            }
            else if (resolvedFilename.ToLower().EndsWith(".ogg"))
            {
                fileFormat = SoundEffect.FileFormat.Ogg;
            }
            else
            {
                throw new IOException(string.Format("The file \"{0}\" is of an unknown type", resolvedFilename));
            }

            var stream = File.Open(resolvedFilename, FileMode.Open);

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

                return new SoundEffectContent(name, numChannels, bitsPerSample,
                    samplesPerSecond, audioData);

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
                return new SoundEffectContent(name, numChannels, bitsPerSample,
                    samplesPerSecond, audioData);

            default:
                throw new IOException(string.Format("The stream \"{0}\" is of an unknown type, \"{1}\"", name, fileFormat));

            }
        }
    }
}

