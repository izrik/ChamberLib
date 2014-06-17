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
            throw new NotImplementedException();
        }

        public ISoundEffectInstance CreateInstance()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

