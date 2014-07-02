using System;
using ChamberLib;
using System.Linq;

namespace ChamberLib
{
    public static class ImportExportHelper
    {

        public static string Convert(Matrix m)
        {
            var values = new float[] { 
                m.M11, m.M12, m.M13, m.M14,
                m.M21, m.M22, m.M23, m.M24,
                m.M31, m.M32, m.M33, m.M34,
                m.M41, m.M42, m.M43, m.M44,
            };
            var s = string.Join(" ", values.Select(f => f.ToString()));
            return s;
        }

        public static Matrix ConvertMatrix(string s)
        {
            var fs = s.Split(' ').Select(ss => float.Parse(ss)).ToArray();

            return new Matrix(
                fs[0], fs[1], fs[2], fs[3],
                fs[4], fs[5], fs[6], fs[7],
                fs[8], fs[9], fs[10], fs[11],
                fs[12], fs[13], fs[14], fs[15]);
        }

        public static string Convert(Vector2 v)
        {
            return string.Format("{0} {1}", v.X, v.Y);
        }

        public static string Convert(Vector3 v)
        {
            return string.Format("{0} {1} {2}", v.X, v.Y, v.Z);
        }

        public static string Convert(Vector4 v)
        {
            return string.Format("{0} {1} {2} {3}", v.X, v.Y, v.Z, v.W);
        }

        public static Vector2 ConvertVector2(string s)
        {
            var fs = s.Split(' ').Select(ss => float.Parse(ss)).ToArray();
            return new Vector2(fs[0], fs[1]);
        }

        public static Vector3 ConvertVector3(string s)
        {
            var fs = s.Split(' ').Select(ss => float.Parse(ss)).ToArray();
            return new Vector3(fs[0], fs[1], fs[2]);
        }

        public static Vector4 ConvertVector4(string s)
        {
            var fs = s.Split(' ').Select(ss => float.Parse(ss)).ToArray();
            return new Vector4(fs[0], fs[1], fs[2], fs[3]);
        }
    }
}

