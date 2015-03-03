using System;
using System.IO;
using NAudio.Wave;
using ChamberLib.Content;

namespace ChamberLib
{
    public class WaveSoundEffectImporter
    {
        public WaveSoundEffectImporter(SoundEffectImporter next=null)
        {
            this.next = next;
        }

        readonly SoundEffectImporter next;

        public SoundEffectContent ImportSoundEffect(string name, string resolvedFilename)
        {
            if (File.Exists(resolvedFilename))
            {
            }
            else if (File.Exists(resolvedFilename + ".wav"))
            {
                resolvedFilename += ".wav";
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
        }
    }
}

