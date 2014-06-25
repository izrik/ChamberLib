using System;

namespace ChamberLib
{
    public class SoundEffect : ISoundEffect
    {
        public SoundEffect()
        {
        }

        #region ISoundEffect implementation

        public void Play()
        {
        }

        public ISoundEffectInstance CreateInstance()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

