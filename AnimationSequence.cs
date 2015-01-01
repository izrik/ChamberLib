using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChamberLib
{
    public class AnimationSequence
    {
        public float Duration = 0;
        public AnimationFrame[] Frames;
        public string Name = string.Empty;

        public AnimationSequence(float duration, AnimationFrame[] frames, string name)
        {
            Duration = duration;
            Frames = frames;
            Name = name;
        }
    }
}
