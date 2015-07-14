using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChamberLib.Content
{
    public static class Geometry
    {
        public static void GenerateSphere(int numMeridians, int numParallels, out Vector3[] positions, out short[] indexes)
        {
            var positions2 = new List<Vector3>();

            int i;
            int j;

            for (i = 0; i < numMeridians; i++)
            {
                var theta = (float)(i * 2 * Math.PI / numMeridians);
                var costheta = (float)Math.Cos(theta);
                var sintheta = (float)Math.Sin(theta);
                for (j = 0; j < numParallels; j++)
                {
                    var phi = (float)((Math.PI / 2) - ((j + 1) * Math.PI / (numParallels + 1)));
                    var y = (float)Math.Sin(phi);
                    var cosphi = (float)Math.Cos(phi);
                    var x = cosphi * costheta;
                    var z = cosphi * sintheta;
                    var v = new Vector3(x, y, z);
                    positions2.Add(v);
                }
            }

            var n = positions2.Count;
            positions2.Add(Vector3.UnitY);
            positions2.Add(-Vector3.UnitY);
            var indexes2 = new List<int>();

            for (i = 1; i < numMeridians; i++)
            {
                indexes2.Add(numParallels * i);
                indexes2.Add(numParallels * (i - 1));
                indexes2.Add(n);

                indexes2.Add(numParallels * i + (numParallels - 1));
                indexes2.Add(numParallels * (i - 1) + (numParallels - 1));
                indexes2.Add(n + 1);
            }

            indexes2.Add(numParallels * (numMeridians - 1));
            indexes2.Add(numParallels * 0);
            indexes2.Add(n);

            indexes2.Add(numParallels * (numMeridians - 1) + (numParallels - 1));
            indexes2.Add(numParallels * 0 + (numParallels - 1));
            indexes2.Add(n + 1);

            for (i = 1; i < numMeridians; i++)
            {
                for (j = 1; j < numParallels; j++)
                {
                    indexes2.Add(numParallels * (i - 1) + (j - 1));
                    indexes2.Add(numParallels * (i - 1) + (j));
                    indexes2.Add(numParallels * (i) + (j - 1));

                    indexes2.Add(numParallels * (i - 1) + (j));
                    indexes2.Add(numParallels * (i) + (j - 1));
                    indexes2.Add(numParallels * (i) + (j));
                }
            }
            for (j = 1; j < numParallels; j++)
            {
                indexes2.Add(numParallels * (numMeridians - 1) + (j - 1));
                indexes2.Add(numParallels * (numMeridians - 1) + (j));
                indexes2.Add(numParallels * (0) + (j - 1));

                indexes2.Add(numParallels * (numMeridians - 1) + (j));
                indexes2.Add(numParallels * (0) + (j - 1));
                indexes2.Add(numParallels * (0) + (j));
            }

            positions = positions2.ToArray();
            indexes = indexes2.Select(ix => (short)ix).ToArray();
        }

        public static short[] ConvertTriangleListToLineList(short[] triangles)
        {
            var pairs = new HashSet<Tuple<short, short>>();
            Action<short, short> addPair = (a, b) =>
            {
                var min = Math.Min(a, b);
                var max = Math.Max(a, b);
                pairs.Add(new Tuple<short, short>(min, max));
            };

            int i;
            for (i = 0; i < triangles.Length; i += 3)
            {
                addPair(triangles[i], triangles[i + 1]);
                addPair(triangles[i], triangles[i + 2]);
                addPair(triangles[i + 1], triangles[i + 2]);
            }

            var indexes = new short[pairs.Count * 2];
            i = 0;
            foreach (var pair in pairs)
            {
                indexes[2 * i] = pair.Item1;
                indexes[2 * i + 1] = pair.Item2;
                i++;
            }

            return indexes;
        }
    }
}
