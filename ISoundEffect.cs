using System;

namespace ChamberLib
{
    public interface ISoundEffect
    {
        void Play();

        ISoundEffectInstance CreateInstance();
    }
}

