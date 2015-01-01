using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChamberLib;

namespace ChamberLib
{
    public class AnimationFrame
    {
        public float Time = 0;
        public Matrix[] Transforms;

        public AnimationFrame(float time, Matrix[] transforms)
        {
            Time = time;
            Transforms = transforms;
        }
    }
}
