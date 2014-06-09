using System;
using System.Collections.Generic;
using XSoundEffectInstance = Microsoft.Xna.Framework.Audio.SoundEffectInstance;

namespace ChamberLib
{
    public class SoundEffectInstanceAdapter : ISoundEffectInstance
    {
        protected static readonly Dictionary<XSoundEffectInstance, SoundEffectInstanceAdapter> _cache = new Dictionary<XSoundEffectInstance, SoundEffectInstanceAdapter>();

        public static ISoundEffectInstance GetAdapter(XSoundEffectInstance se)
        {
            if (_cache.ContainsKey(se))
            {
                return _cache[se];
            }

            var adapter = new SoundEffectInstanceAdapter(se);
            _cache[se] = adapter;

            return adapter;
        }

        protected SoundEffectInstanceAdapter(XSoundEffectInstance se)
        {
            SoundEffectInstance = se;
        }

        public readonly XSoundEffectInstance SoundEffectInstance;

        public void Play()
        {
            SoundEffectInstance.Play();
        }

        public void Stop(bool immediate)
        {
            SoundEffectInstance.Stop(immediate);
        }
    }
}

