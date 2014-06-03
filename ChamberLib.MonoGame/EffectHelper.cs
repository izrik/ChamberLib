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

        public static void SetMatrices(this IEffectMatrices effect, Matrix world, Matrix view, Matrix projection)
        {
            effect.World = world.ToXna();
            effect.View = view.ToXna();
            effect.Projection = projection.ToXna();
        }
    }
}

