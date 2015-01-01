using System;
using System.Collections.Generic;
using ChamberLib;
using System.Linq;
using System.IO;

namespace ChamberLib
{
    public class AnimationExporter
    {
        public void ExportAnimationData(AnimationData ad, string filename)
        {
            using (var writer = new StreamWriter(filename))
            {
                ExportAnimationData(ad, writer);
            }
        }
        public void ExportAnimationData(AnimationData ad, TextWriter writer, Action perAbsoluteTransformAction=null, List<IBone> bones=null)
        {
            writer.WriteLine("Sequences {0}", ad.Sequences.Count);
            int k = 1;
            int kk = ad.Sequences.Count;
            foreach (var seq in ad.Sequences.Values)
            {
                writer.WriteLine(seq.Name);
                writer.WriteLine(seq.Duration);
                writer.WriteLine("Frames {0}", seq.Frames.Length);
                int j = 1;
                int jj = seq.Frames.Length;
                foreach (var f in seq.Frames)
                {
                    writer.WriteLine("###################################");
                    writer.WriteLine("# Sequence {0,2}/{1,2}, frame {2,4}/{3,4} #",
                        k,
                        kk,
                        j,
                        jj);
                    writer.WriteLine("###################################");
                    writer.WriteLine(f.Time);
                    writer.WriteLine("Transforms {0}", f.Transforms.Length);
                    foreach (var tr in f.Transforms)
                    {
                        ImportExportHelper.WriteMatrix(writer, tr);
                    }
                    j++;
                }
                k++;
            }
            writer.WriteLine("Transforms {0}", ad.Transforms.Count);
            k = 0;
            foreach (var tr in ad.Transforms)
            {
                if (bones != null)
                {
                    var bone = bones.Find(ib => ib.Index == k);
                    if (bone != null)
                    {
                        writer.WriteLine("#");
                        writer.WriteLine(
                            "# Bone: {0} (Index={1}, has index {2} in the list of bones)", 
                            bone.Name,
                            bone.Index,
                            bones.IndexOf(bone));
                    }
                }
                ImportExportHelper.WriteMatrix(writer, tr);
                k++;
            }
            writer.WriteLine("AbsoluteTransforms {0}", ad.Transforms.Count);
            k = 0;
            foreach (var tr in ad.AbsoluteTransforms)
            {
                if (bones != null)
                {
                    var bone = bones.Find(ib => ib.Index == k);
                    if (bone != null)
                    {
                        writer.WriteLine("#");
                        writer.WriteLine(
                            "# Bone: {0} (Index={1}, has index {2} in the list of bones)", 
                            bone.Name,
                            bone.Index,
                            bones.IndexOf(bone));
                    }
                }
                ImportExportHelper.WriteMatrix(writer, tr);
                ImportExportHelper.WriteMatrix(writer, tr.Inverted(), true, "Transform Inverse");
                if (perAbsoluteTransformAction!=null)
                {
                    perAbsoluteTransformAction();
                }
                k++;
            }
            writer.WriteLine("SkeletonHierarchy {0}", ad.Transforms.Count);
            foreach (var s in ad.SkeletonHierarchy)
            {
                writer.WriteLine(s);
            }
        }

        public AnimationData ImportAnimationData(string filename)
        {
            using (var sreader = new StreamReader(filename))
            {
                var reader = 
                    new RememberingReader(
                        new SkipCommentsReader(
                            new RememberingReader(
                                new TextReaderAdapter(sreader))));
                return ImportAnimationData(reader);
            }
        }
        public AnimationData ImportAnimationData(IReader reader)
        {
            var ad = new AnimationData2();
            string line;

            line = reader.ReadLine();
            if (!line.StartsWith("Sequences "))
                throw new InvalidOperationException();
            int numseq = int.Parse(line.Split(' ')[1]);
            int i;
            int numtr;
            for (i = 0; i < numseq; i++)
            {
                var seq = new AnimationSequence2();
                ad.Sequences.Add(seq);

                seq.Name = reader.ReadLine();
                seq.Duration = float.Parse(reader.ReadLine());
                line = reader.ReadLine();
                if (!line.StartsWith("Frames "))
                    throw new InvalidOperationException();
                var numf = int.Parse(line.Split(' ')[1]);
                int j;
                for (j = 0; j < numf; j++)
                {
                    var f = new AnimationFrame2();
                    seq.Frames.Add(f);
                    f.Time = float.Parse(reader.ReadLine());
                    numtr = int.Parse(reader.ReadLine().Split(' ')[1]);
                    int k;
                    for (k = 0; k < numtr; k++)
                    {
                        f.Transforms.Add(ImportExportHelper.ConvertMatrix(reader.ReadLine()));
                    }
                }
            }

            numtr = int.Parse(reader.ReadLine().Split(' ')[1]);
            for (i = 0; i < numtr; i++)
            {
                ad.Transforms.Add(ImportExportHelper.ConvertMatrix(reader.ReadLine()));
            }

            numtr = int.Parse(reader.ReadLine().Split(' ')[1]);
            for (i = 0; i < numtr; i++)
            {
                ad.AbsoluteTransforms.Add(ImportExportHelper.ConvertMatrix(reader.ReadLine()));
            }

            numtr = int.Parse(reader.ReadLine().Split(' ')[1]);
            for (i = 0; i < numtr; i++)
            {
                ad.SkeletonHierarchy.Add(int.Parse(reader.ReadLine()));
            }

            return Convert(ad);
        }

        class AnimationData2
        {
            public List<AnimationSequence2> Sequences = new List<AnimationSequence2>();
            public List<Matrix> Transforms = new List<Matrix>();
            public List<Matrix> AbsoluteTransforms = new List<Matrix>();
            public List<int> SkeletonHierarchy = new List<int>();
        }

        class AnimationSequence2
        {
            public float Duration = 0;
            public List<AnimationFrame2> Frames = new List<AnimationFrame2>();
            public string Name = string.Empty;
        }

        public class AnimationFrame2
        {
            public float Time = 0;
            public List<Matrix> Transforms = new List<Matrix>();
        }

        AnimationData2 Convert(AnimationData ad)
        {
            var ad2 = new AnimationData2();
            ad2.SkeletonHierarchy.AddRange(ad.SkeletonHierarchy);
            ad2.AbsoluteTransforms.AddRange(ad.AbsoluteTransforms);
            ad2.Transforms.AddRange(ad.Transforms);
            foreach (var seq in ad.Sequences.Values)
            {
                ad2.Sequences.Add(Convert(seq));
            }
            return ad2;
        }

        AnimationSequence2 Convert(AnimationSequence seq)
        {
            var seq2 = new AnimationSequence2();
            seq2.Name = seq.Name;
            seq2.Duration = seq.Duration;
            foreach (var frame in seq.Frames)
            {
                seq2.Frames.Add(Convert(frame));
            }
            return seq2;
        }

        AnimationFrame2 Convert(AnimationFrame f)
        {
            var f2 = new AnimationFrame2();
            f2.Time = f.Time;
            foreach (var tr in f.Transforms)
            {
                f2.Transforms.Add(tr);
            }
            return f2;
        }

        AnimationData Convert(AnimationData2 ad2)
        {
            var sequences = new Dictionary<string, AnimationSequence>();
            foreach (var seq2 in ad2.Sequences)
            {
                var seq = Convert(seq2);

                if (sequences.ContainsKey(seq.Name)) throw new InvalidOperationException("Already have a sequence by that name.");

                sequences[seq.Name] = seq;
            }

            var trs = new List<Matrix>();
            foreach (var tr in ad2.Transforms)
            {
                trs.Add(tr);
            }

            var atrs = new List<Matrix>();
            foreach (var tr in ad2.AbsoluteTransforms)
            {
                atrs.Add(tr);
            }

            var skels = new List<int>();
            foreach (var skel in ad2.SkeletonHierarchy)
            {
                skels.Add(skel);
            }

            return new AnimationData(sequences, trs, atrs, skels);
        }

        AnimationSequence Convert(AnimationSequence2 seq2)
        {
            var fs = new List<AnimationFrame>();
            foreach (var f2 in seq2.Frames)
            {
                fs.Add(Convert(f2));
            }
            return new AnimationSequence(seq2.Duration, fs.ToArray(), seq2.Name);
        }

        AnimationFrame Convert(AnimationFrame2 f2)
        {
            return new AnimationFrame(f2.Time, f2.Transforms.ToArray());
        }
    }
}

