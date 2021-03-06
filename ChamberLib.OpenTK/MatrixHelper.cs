﻿using System;

using _OpenTK = global::OpenTK;

namespace ChamberLib.OpenTK
{
    public static class MatrixHelper
    {
        public static _OpenTK.Matrix4 ToOpenTK(this ChamberLib.Matrix m)
        {
            return new _OpenTK.Matrix4(
                m.M11, m.M12, m.M13, m.M14,
                m.M21, m.M22, m.M23, m.M24,
                m.M31, m.M32, m.M33, m.M34,
                m.M41, m.M42, m.M43, m.M44);
        }

        public static ChamberLib.Matrix ToChamber(this _OpenTK.Matrix4 m)
        {
            return new ChamberLib.Matrix(
                m.M11, m.M12, m.M13, m.M14,
                m.M21, m.M22, m.M23, m.M24,
                m.M31, m.M32, m.M33, m.M34,
                m.M41, m.M42, m.M43, m.M44);
        }
    }
}

