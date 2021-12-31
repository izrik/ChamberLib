using System;
using System.IO;
using ChamberLib.Content;

namespace ChamberLib.OpenTK.Audio
{
    public class Song : ISong
    {
        public Song(SongContent songContent)
            : this(new SoundEffect(songContent.Content))
        {
        }
        protected Song(SoundEffect soundEffect)
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

