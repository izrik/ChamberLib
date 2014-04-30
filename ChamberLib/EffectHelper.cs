using System;
using Microsoft.Xna.Framework.Graphics;

namespace ChamberLib
{
    public static class EffectHelper
    {
        public static void ApplyFirstPass(this Effect effect)
        {
            effect.CurrentTechnique.Passes[0].Apply();
        }
    }
}

