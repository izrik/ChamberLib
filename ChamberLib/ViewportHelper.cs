using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XMatrix = Microsoft.Xna.Framework.Matrix;

namespace ChamberLib
{
    public static class ViewportHelper
    {
        public static Matrix GenerateSpriteBatchProjection(this Viewport viewport)
        {
            return
                (XMatrix.CreateTranslation(-0.5f, -0.5f, 0) *
                XMatrix.CreateOrthographicOffCenter(
                        0,
                        viewport.Width,
                        viewport.Height,
                        0,
                        0,
                    1)).ToChamber();
        }

        public static Microsoft.Xna.Framework.Graphics.Viewport ToXna(this ChamberLib.Viewport vp)
        {
            var vp2 = new Microsoft.Xna.Framework.Graphics.Viewport(vp.Bounds.ToXna());

            vp2.MinDepth = vp.MinDepth;
            vp2.MaxDepth = vp.MaxDepth;

            return vp2;
        }

        public static ChamberLib.Viewport ToChamber(this Microsoft.Xna.Framework.Graphics.Viewport vp)
        {
            var vp2 = new ChamberLib.Viewport();

            vp2.Bounds = vp.Bounds.ToChamber();
            vp2.MinDepth = vp.MinDepth;
            vp2.MaxDepth = vp.MaxDepth;

            return vp2;
        }
    }
}
