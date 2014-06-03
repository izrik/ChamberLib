using System;
using System.Linq;

namespace ChamberLib
{
    public static class MatrixHelper
    {
        public static Microsoft.Xna.Framework.Matrix ToXna(this ChamberLib.Matrix m)
        {
            return new Microsoft.Xna.Framework.Matrix(
                m.M11, m.M12, m.M13, m.M14,
                m.M21, m.M22, m.M23, m.M24,
                m.M31, m.M32, m.M33, m.M34,
                m.M41, m.M42, m.M43, m.M44);
        }

        public static ChamberLib.Matrix ToChamber(this Microsoft.Xna.Framework.Matrix m)
        {
            return new Matrix(
                m.M11, m.M12, m.M13, m.M14,
                m.M21, m.M22, m.M23, m.M24,
                m.M31, m.M32, m.M33, m.M34,
                m.M41, m.M42, m.M43, m.M44);
        }

        public static Microsoft.Xna.Framework.Matrix[] ToXna(this ChamberLib.Matrix[] m)
        {
            return m.Select(mm => mm.ToXna()).ToArray();
        }

        public static ChamberLib.Matrix[] ToChamber(this Microsoft.Xna.Framework.Matrix[] m)
        {
            return m.Select(mm => mm.ToChamber()).ToArray();
        }
    }
}

