using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using ChamberLib.OpenTK.Models;
using ChamberLib.OpenTK.System;

namespace ChamberLib.OpenTK
{
    public class ChModelExporter
    {
        public void ExportModel(Model model, string filename, IContentManager content)
        {
            using (var writer = new StreamWriter(filename))
            {
                int k;
                writer.WriteLine("Bones {0}", model.Bones.Count);
                Func<Bone, int> getDepth = null;
                getDepth = b => b.Parent == null ? 0 : getDepth((Bone)b.Parent) + 1;
                foreach (var bone in model.Bones)
                {
                    writer.Write("# ");
                    writer.Write(new String(' ', getDepth(bone) * 2));
                    writer.WriteLine("{0} ({1})", bone.Name, bone.Parent==null ? "()" : ((Bone)bone.Parent).Name);
                }
                k = 0;
                foreach (var bone in model.Bones)
                {
                    if (ImportExportHelper.WriteComments)
                    {
                        writer.WriteLine("######################");
                        writer.WriteLine("# Bone {0,2} ############", k++);
                        writer.WriteLine("######################");
                    }
                    WriteBone(writer, bone);
                }
                var vbuffersset = new HashSet<VertexBuffer>();
                var ibuffersset = new HashSet<IndexBuffer>();
                foreach (var mesh in model.Meshes)
                {
                    foreach (var part in mesh.Parts)
                    {
                        vbuffersset.Add(part.Vertexes);
                        ibuffersset.Add(part.Indexes);
                    }
                }
                var vbuffers = vbuffersset.ToList();
                var ibuffers = ibuffersset.ToList();
                writer.WriteLine("VertexBuffers {0}", vbuffers.Count);
                k = 0;
                foreach (var vb in vbuffers)
                {
                    if (ImportExportHelper.WriteComments)
                    {
                        writer.WriteLine("######################");
                        writer.WriteLine("# VertexBuffer {0,2} ####", k);
                        writer.WriteLine("######################");
                    }
                    WriteVertexBuffer(writer, vb, k);
                    k++;
                }
                writer.WriteLine("IndexBuffers {0}", ibuffers.Count);
                k = 0;
                foreach (var ib in ibuffers)
                {
                    if (ImportExportHelper.WriteComments)
                    {
                        writer.WriteLine("######################");
                        writer.WriteLine("# IndexBuffer {0,2} #####", k);
                        writer.WriteLine("######################");
                    }
                    WriteIndexBuffer(writer, ib, k);
                    k++;
                }
                k = 0;
                writer.WriteLine("Meshes {0}", model.Meshes.Count);
                k = 0;
                foreach (var mesh in model.Meshes)
                {
                    if (ImportExportHelper.WriteComments)
                    {
                        writer.WriteLine("######################");
                        writer.WriteLine("# Mesh {0,2} ############", k++);
                        writer.WriteLine("######################");
                    }
                    WriteMesh(writer, mesh, model, vbuffers, ibuffers);
                }
                writer.WriteLine(model.Root != null ? model.Bones.IndexOf(model.RootBone) : -1);

                if (ImportExportHelper.WriteComments)
                {
                    writer.WriteLine("######################");
                    writer.WriteLine("# Animation Data    ##", k++);
                    writer.WriteLine("######################");
                }
                if (model.AnimationData == null)
                {
                    writer.WriteLine(false);
                }
                else
                {
                    writer.WriteLine(true);
                    var ad = model.AnimationData;
                    var ae = new AnimationExporter();
                    ae.ExportAnimationData(ad, writer, model.Bones.Cast<IBone>().ToList());
                }
            }
        }

        static void WriteBone(TextWriter writer, Bone bone)
        {
            writer.WriteLine(bone.Name);
            writer.WriteLine(bone.Index);
            writer.WriteLine(bone.Parent != null ? bone.Parent.Index : -1);
            ImportExportHelper.WriteMatrix(writer, bone.Transform);

            var mat = bone.InverseBindPose;
            ImportExportHelper.WriteMatrix(writer, mat, isComment: true, prefix: "OffsetMatrix");
        }

        static void WriteVertexBuffer(TextWriter writer, VertexBuffer vb, int bufferNumber)
        {
            writer.WriteLine(string.Format("VertexBuffer{0,4}", bufferNumber));

            int vertexType;
            if (vb.VertexData[0] is Vertex_PBiBwNT)
            {
                vertexType = 0;
            }
            else if (vb.VertexData[0] is Vertex_PN)
            {
                vertexType = 1;
            }
            else if (vb.VertexData[0] is Vertex_PNT)
            {
                vertexType = 2;
            }
            else if (vb.VertexData[0] is Vertex_PNTT)
            {
                vertexType = 3;
            }
            else
            {
                throw new InvalidOperationException(
                    string.Format(
                        "Unknown vertex array type: {0}",
                        vb.VertexData[0].GetType().Name));
            }

            writer.WriteLine(vb.VertexData.Length);
            writer.WriteLine(vertexType);
            int k = 0;
            foreach (var v in vb.VertexData)
            {
                if (k % 100 == 0)
                {
                    if (ImportExportHelper.WriteComments)
                    {
                        writer.WriteLine("################################");
                        writer.WriteLine("# VertexBuffer {0,2}, vertex {1,4} #", bufferNumber, k);
                        writer.WriteLine("################################");
                    }
                }
                k++;

                var values = v.GetValues();
                foreach (var value in values)
                {
                    writer.WriteLine(value);
                }
            }
        }

        void WriteIndexBuffer(TextWriter writer, IndexBuffer ib, int bufferNumber)
        {
            writer.WriteLine(ib.Length);
            writer.WriteLine(16);//ib.IndexElementSize == IndexElementSize.SixteenBits ? "16" : "32");
            writer.WriteLine("");//ib.Name);
            int k = 0;
            foreach (var index in ib.EnumerateIndexes())
            {
                if (k % 100 == 0)
                {
                    if (ImportExportHelper.WriteComments)
                    {
                        writer.WriteLine("##############################");
                        writer.WriteLine("# IndexBuffer {0,2}, index {1,4} #", bufferNumber, k);
                        writer.WriteLine("##############################");
                    }
                }
                k++;

                writer.WriteLine(index);
            }
        }

        void WriteMesh(TextWriter writer, Mesh mesh, Model model, List<VertexBuffer> vbuffers, List<IndexBuffer> ibuffers)
        {
            writer.WriteLine("mesh name");//mesh.Name);
            writer.WriteLine("MeshParts {0}", mesh.Parts.Count);
            foreach (var part in mesh.Parts)
            {
                WriteMeshPart(writer, part, vbuffers, ibuffers);
            }
        }

        void WriteMeshPart(TextWriter writer, Part part, List<VertexBuffer> vbuffers, List<IndexBuffer> ibuffers)
        {
            writer.WriteLine(part.Indexes != null ? ibuffers.IndexOf(part.Indexes) : -1);
            writer.WriteLine(part.PrimitiveCount);
            writer.WriteLine(part.StartIndex);
            writer.WriteLine(part.Vertexes != null ? vbuffers.IndexOf(part.Vertexes) : -1);
            writer.WriteLine(part.VertexOffset);
        }
    }
}

