using System;

namespace ChamberLib
{
    public static class MatrixHelper
    {
        public static OpenTK.Matrix4 ToOpenTK(this ChamberLib.Matrix m)
        {
            return new OpenTK.Matrix4(
                m.M11, m.M12, m.M13, m.M14,
                m.M21, m.M22, m.M23, m.M24,
                m.M31, m.M32, m.M33, m.M34,
                m.M41, m.M42, m.M43, m.M44);
        }

        public static ChamberLib.Matrix ToChamber(this OpenTK.Matrix4 m)
        {
            return new ChamberLib.Matrix(
                m.M11, m.M12, m.M13, m.M14,
                m.M21, m.M22, m.M23, m.M24,
                m.M31, m.M32, m.M33, m.M34,
                m.M41, m.M42, m.M43, m.M44);
        }

        public static ChamberLib.Matrix ToChamber(this Assimp.Matrix4x4 m)
        {
            return new ChamberLib.Matrix(
                m.A1, m.B1, m.C1, m.D1,
                m.A2, m.B2, m.C2, m.D2,
                m.A3, m.B3, m.C3, m.D3,
                m.A4, m.B4, m.C4, m.D4);
        }
    }
}

