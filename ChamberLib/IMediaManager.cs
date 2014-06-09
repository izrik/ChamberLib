using System;

namespace ChamberLib
{
    public interface IMediaManager
    {
        float SoundEffectMasterVolume { get; set; }

        float MusicVolume { get; set; }
    }
}

