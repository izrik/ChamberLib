using System;
using System.IO;
using ChamberLib;
using System.Collections.Generic;
using System.Linq;

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
                var materialsset = new HashSet<Material>();
                foreach (var mesh in model.Meshes)
                {
                    foreach (var part in mesh.Parts)
                    {
                        vbuffersset.Add(part.Vertexes);
                        ibuffersset.Add(part.Indexes);
                        materialsset.Add(part.Material);
                    }
                }
                var vbuffers = vbuffersset.ToList();
                var ibuffers = ibuffersset.ToList();
                var materials = materialsset.ToList();
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
                writer.WriteLine("Materials {0}", materials.Count);
                k = 0;
                foreach (var mat in materials)
                {
                    if (ImportExportHelper.WriteComments)
                    {
                        writer.WriteLine("######################");
                        writer.WriteLine("# Material {0,2} ########", k++);
                        writer.WriteLine("######################");
                    }
                    WriteMaterial(writer, mat, content);
                }
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
                    WriteMesh(writer, mesh, model, vbuffers, ibuffers, materials);
                }
                writer.WriteLine(model.Root != null ? model.Bones.IndexOf(model.RootBone) : -1);

                if (ImportExportHelper.WriteComments)
                {
                    writer.WriteLine("######################");
                    writer.WriteLine("# Animation Data    ##", k++);
                    writer.WriteLine("######################");
                }
                if (model.Tag == null)
                {
                    writer.WriteLine(false);
                }
                else if (!(model.Tag is AnimationData))
                {
                    writer.WriteLine(false);
                    writer.WriteLine("# type: {0}", model.Tag.GetType().AssemblyQualifiedName);
                }
                else
                {
                    writer.WriteLine(true);
                    var ad = model.Tag as AnimationData;
                    var ae = new AnimationExporter();
                    int kk = 0;
//                    Action action = () => {
//                        ImportExportHelper.WriteMatrix(writer, model.Bones[k].InverseBindPose, true, "Bone OffsetMatrix");
//                        ImportExportHelper.WriteMatrix(writer, model.Bones[k].InverseBindPose.Inverted(), true, "Bone OffsetMatrix Inverse");
//                        ImportExportHelper.WriteMatrix(writer, model.Bones[k].Transform, true, "Bone Transfrom");
//                        ImportExportHelper.WriteMatrix(writer, model.Bones[k].Transform.Inverted(), true, "Bone Transform Inverse");
//                        kk++;
//                    };
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
            short[] indexData = ib.IndexData;

            writer.WriteLine(indexData.Length);
            writer.WriteLine(16);//ib.IndexElementSize == IndexElementSize.SixteenBits ? "16" : "32");
            writer.WriteLine("");//ib.Name);
            int k = 0;
            foreach (var index in indexData)
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

        void WriteMaterial(TextWriter writer, Material mat, IContentManager content)
        {
            TextureAdapter texture = (TextureAdapter)mat.Texture;
            Vector3 diffuse = mat.Diffuse;
            Vector3 emissive = mat.EmissiveColor;
            Vector3 specularColor = mat.SpecularColor;
            float specularPower = mat.SpecularPower;
            string shadername = (mat.Shader != null ? mat.Shader.Name : "");

            writer.WriteLine(ImportExportHelper.Convert(diffuse));
            writer.WriteLine(ImportExportHelper.Convert(emissive));
            writer.WriteLine(ImportExportHelper.Convert(specularColor));
            writer.WriteLine(specularPower);
            string texname = "";
            if (texture != null)
            {
                texname = content.LookupObjectName(texture);
                if (texname == null)
                {
                }
            }
            writer.WriteLine(texname);

            writer.WriteLine(shadername);
        }

        void WriteMesh(TextWriter writer, Mesh mesh, Model model, List<VertexBuffer> vbuffers, List<IndexBuffer> ibuffers, List<Material> materials)
        {
            writer.WriteLine("mesh name");//mesh.Name);
            writer.WriteLine(mesh.ParentBone != null ? model.Bones.IndexOf((Bone)mesh.ParentBone) : -1);
            writer.WriteLine("MeshParts {0}", mesh.Parts.Count);
            foreach (var part in mesh.Parts)
            {
                WriteMeshPart(writer, part, vbuffers, ibuffers, materials);
            }
        }

        void WriteMeshPart(TextWriter writer, Part part, List<VertexBuffer> vbuffers, List<IndexBuffer> ibuffers, List<Material> materials)
        {
            writer.WriteLine(part.Material != null ? materials.IndexOf(part.Material) : -1);
            writer.WriteLine(part.Indexes != null ? ibuffers.IndexOf(part.Indexes) : -1);
            writer.WriteLine(part.NumVertexes);
            writer.WriteLine(part.PrimitiveCount);
            writer.WriteLine(part.StartIndex);
            writer.WriteLine(part.Vertexes != null ? vbuffers.IndexOf(part.Vertexes) : -1);
            writer.WriteLine(part.VertexOffset);
        }
    }
}

