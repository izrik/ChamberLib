using System;

namespace ChamberLib
{
    public static class MatrixHelper
    {
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

