using System;

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

        public static void ToFloatArray(this ChamberLib.Matrix[] matrices, float[] floats)
        {
            int i;
            for (i = 0; i < matrices.Length; i++)
            {
                int n = i * 16;
                floats[n + 0] = matrices[i].M11;
                floats[n + 1] = matrices[i].M12;
                floats[n + 2] = matrices[i].M13;
                floats[n + 3] = matrices[i].M14;
                floats[n + 4] = matrices[i].M21;
                floats[n + 5] = matrices[i].M22;
                floats[n + 6] = matrices[i].M23;
                floats[n + 7] = matrices[i].M24;
                floats[n + 8] = matrices[i].M31;
                floats[n + 9] = matrices[i].M32;
                floats[n + 10] = matrices[i].M33;
                floats[n + 11] = matrices[i].M34;
                floats[n + 12] = matrices[i].M41;
                floats[n + 13] = matrices[i].M42;
                floats[n + 14] = matrices[i].M43;
                floats[n + 15] = matrices[i].M44;
            }
        }
    }
}

