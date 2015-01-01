﻿using System;

namespace ChamberLib
{
    public static class ColorHelper
    {
        public static System.Drawing.Color ToSystemDrawing(this ChamberLib.Color color)
        {
            return System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        public static ChamberLib.Vector3 ToChamberVector(this Assimp.Color3D color)
        {
            return new ChamberLib.Vector3(color.R, color.G, color.B);
        }

        public static ChamberLib.Vector4 ToChamberVector(this Assimp.Color4D color)
        {
            return new ChamberLib.Vector4(color.R, color.G, color.B, color.A);
        }
    }
}

