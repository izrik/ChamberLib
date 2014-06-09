using System;
using System.Collections.Generic;
using XSoundEffect = Microsoft.Xna.Framework.Audio.SoundEffect;

namespace ChamberLib
{
    public class SoundEffectAdapter : ISoundEffect
    {
        protected static readonly Dictionary<XSoundEffect, SoundEffectAdapter> _cache = new Dictionary<XSoundEffect, SoundEffectAdapter>();

        public static ISoundEffect GetAdapter(XSoundEffect se)
        {
            if (_cache.ContainsKey(se))
            {
                return _cache[se];
            }

            var adapter = new SoundEffectAdapter(se);
            _cache[se] = adapter;

            return adapter;
        }

        protected SoundEffectAdapter(XSoundEffect se)
        {
            SoundEffect = se;
        }

        public readonly XSoundEffect SoundEffect;

        public void Play()
        {
            SoundEffect.Play();
        }

        public ISoundEffectInstance CreateInstance()
        {
            return SoundEffectInstanceAdapter.GetAdapter(SoundEffect.CreateInstance());
        }
    }
}

