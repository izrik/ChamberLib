using System;

namespace ChamberLib
{
    public class MediaManager : IMediaManager
    {
        public float SoundEffectMasterVolume
        {
            get
            {
                return Microsoft.Xna.Framework.Audio.SoundEffect.MasterVolume;
            }
            set
            {
                Microsoft.Xna.Framework.Audio.SoundEffect.MasterVolume = value;
            }
        }

        public float MusicVolume
        {
            get
            {
                return Microsoft.Xna.Framework.Media.MediaPlayer.Volume;
            }
            set
            {
                Microsoft.Xna.Framework.Media.MediaPlayer.Volume = value;
            }
        }


    }
}

