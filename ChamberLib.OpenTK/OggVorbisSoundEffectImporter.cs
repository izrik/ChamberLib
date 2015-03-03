using System;
using System.IO;
using NVorbis;
using System.Diagnostics;
using ChamberLib.Content;

namespace ChamberLib
{
    public class OggVorbisSoundEffectImporter
    {
        public OggVorbisSoundEffectImporter(SoundEffectImporter next=null)
        {
            this.next = next;
        }

        readonly SoundEffectImporter next;

        public SoundEffectContent ImportSoundEffect(string name, string resolvedFilename)
        {
            if (File.Exists(resolvedFilename))
            {
            }
            else if (File.Exists(resolvedFilename + ".ogg"))
            {
                resolvedFilename += ".ogg";
            }
            else if (next != null)
            {
                return next(resolvedFilename, null);
            }
            else
            {
                throw new FileNotFoundException("The sound file could not be found.", resolvedFilename);
            }

            var stream = File.Open(resolvedFilename, FileMode.Open);

            int numChannels;
            int bitsPerSample;
            int samplesPerSecond;
            byte [] audioData;

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
        }
    }
}

