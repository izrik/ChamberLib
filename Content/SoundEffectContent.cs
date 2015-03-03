using System;

namespace ChamberLib
{
    public class SoundEffectContent
    {
        public SoundEffectContent()
        {
        }
        public SoundEffectContent(string name, int numChannels,
            int bitsPerSample, int samplesPerSecond,
            byte[] audioData)
        {
            Name = name;
            NumChannels = numChannels;
            BitsPerSample = bitsPerSample;
            SamplesPerSecond = samplesPerSecond;
            AudioData = audioData;
        }
        public string Name;
        public int NumChannels;
        public int BitsPerSample;
        public int SamplesPerSecond;
        public byte[] AudioData;
    }
}

