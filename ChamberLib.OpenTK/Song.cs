using System;
using System.IO;

namespace ChamberLib
{
    public class Song : ISong
    {
        public Song(string name, Stream stream)
            : this(new SoundEffect(name, stream))
        {
        }
        public Song(SoundEffect soundEffect)
        {
            if (soundEffect == null) throw new ArgumentNullException("soundEffect");

            this.soundEffect = soundEffect;
        }

        readonly SoundEffect soundEffect;
        public string Name { get { return soundEffect.Name; } }

        public void Play()
        {
            if (soundEffect != null)
            {
                soundEffect.Play();
            }
        }
        public void Play(int source)
        {
            if (soundEffect != null)
            {
                soundEffect.Play(source);
            }
        }
    }
}

