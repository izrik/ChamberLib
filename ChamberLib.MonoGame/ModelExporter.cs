using System;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using ChamberLib;
using XModel = Microsoft.Xna.Framework.Graphics.Model;
using XMatrix = Microsoft.Xna.Framework.Matrix;
using Xna = Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace ChamberLib
{
    public class ModelExporter
    {
        public void ExportModel(XModel model, string filename, IContentManager content)
        {
            using (var writer = new StreamWriter(filename))
            {
                writer.WriteLine("Bones {0}", model.Bones.Count);
                if (ImportExportHelper.WriteComments)
                {
                    Func<ModelBone, int> getDepth = null;
                    getDepth = b => b.Parent == null ? 0 : getDepth((ModelBone)b.Parent) + 1;
                    foreach (var bone in model.Bones)
                    {
                        writer.Write("# ");
                        writer.Write(new String(' ', getDepth(bone) * 2));
                        writer.WriteLine("{0} ({1})", bone.Name, bone.Parent == null ? "()" : bone.Parent.Name);
                    }
                }
                int k;
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
                var materialsset = new HashSet<Effect>();
                foreach (var mesh in model.Meshes)
                {
                    materialsset.AddRange(mesh.Effects);
                    foreach (var part in mesh.MeshParts)
                    {
                        vbuffersset.Add(part.VertexBuffer);
                        ibuffersset.Add(part.IndexBuffer);
                        materialsset.Add(part.Effect);
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
                writer.WriteLine(model.Root != null ? model.Bones.IndexOf(model.Root) : -1);

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
                    ae.ExportAnimationData(ad, writer, bones: model.Bones.Select(BoneAdapter.GetAdapter).ToList());
                }
            }
        }

        static void WriteMatrix(TextWriter writer, XMatrix mat)
        {
            ImportExportHelper.WriteMatrix(writer, mat.ToChamber());
        }

        static void WriteBone(TextWriter writer, ModelBone bone)
        {
            writer.WriteLine(bone.Name);
            writer.WriteLine(bone.Index);
            writer.WriteLine(bone.Parent != null ? bone.Parent.Index : -1);
            WriteMatrix(writer, bone.Transform);
        }

        void WriteVertexBuffer(TextWriter writer, VertexBuffer vb, int bufferNumber)
        {
            writer.WriteLine(vb.Name);
            writer.WriteLine(vb.VertexCount);
            var decl = vb.VertexDeclaration;
            bool hasPosition = false;
            bool hasBlendIndices = false;
            bool hasBlendWeights = false;
            bool hasNormal = false;
            bool hasTexCoords = false;
            bool hasTexCoords2 = false;
            foreach (var elem in decl.GetVertexElements())
            {
                switch (elem.VertexElementUsage)
                {
                    case VertexElementUsage.Position:
                        hasPosition = true;
                        break;
                    case VertexElementUsage.BlendIndices:
                        hasBlendIndices = true;
                        break;
                    case VertexElementUsage.BlendWeight:
                        hasBlendWeights = true;
                        break;
                    case VertexElementUsage.Normal:
                        hasNormal = true;
                        break;
                    case VertexElementUsage.TextureCoordinate:
                        if (hasTexCoords)
                            hasTexCoords2 = true;
                        hasTexCoords = true;
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }
            int vertexType;
            if (hasPosition && hasBlendIndices && hasBlendWeights && hasNormal && hasTexCoords)
            {
                vertexType = 0;
            }
            else if (hasPosition && !hasBlendIndices && !hasBlendWeights && hasNormal && !hasTexCoords)
            {
                vertexType = 1;
            }
            else if (hasPosition && !hasBlendIndices && !hasBlendWeights && hasNormal && hasTexCoords && !hasTexCoords2)
            {
                vertexType = 2;
            }
            else if (hasPosition && !hasBlendIndices && !hasBlendWeights && hasNormal && hasTexCoords && hasTexCoords2)
            {
                vertexType = 3;
            }
            else
            {
                throw new InvalidOperationException();
            }

            writer.WriteLine(vertexType);
            IVertex[] vertices = null;
            if (vertexType == 0)
            {
                var vs = new Vertex_PBiBwNT[vb.VertexCount];
                vb.GetData(vs);
                vertices = Array.ConvertAll(vs, (v) => (IVertex)v);
            }
            else if (vertexType == 1)
            {
                var vs = new Vertex_PN[vb.VertexCount];
                vb.GetData(vs);
                vertices = Array.ConvertAll(vs, (v) => (IVertex)v);
            }
            else if (vertexType == 2)
            {
                var vs = new Vertex_PNT[vb.VertexCount];
                vb.GetData(vs);
                vertices = Array.ConvertAll(vs, (v) => (IVertex)v);
            }
            else if (vertexType == 3)
            {
                var vs = new Vertex_PNTT[vb.VertexCount];
                vb.GetData(vs);
                vertices = Array.ConvertAll(vs, (v) => (IVertex)v);
            }

            int k = 0;
            foreach (var v in vertices)
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
            writer.WriteLine(ib.IndexCount);
            writer.WriteLine(ib.IndexElementSize == IndexElementSize.SixteenBits ? "16" : "32");
            writer.WriteLine(ib.Name);
            var indexes = new short[ib.IndexCount];
            ib.GetData(indexes);
            int k = 0;
            foreach (var index in indexes)
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

        void WriteMaterial(TextWriter writer, Effect mat, IContentManager content)
        {
            Texture2D texture;
            Vector3 diffuse;
            Vector3 emissive;
            Vector3 specularColor;
            float specularPower;
            string shadername = "";
            if (mat is BasicEffect)
            {
                var mat2 = (BasicEffect)mat;
                diffuse = mat2.DiffuseColor.ToChamber();
                emissive = mat2.EmissiveColor.ToChamber();
                specularColor = mat2.SpecularColor.ToChamber();
                specularPower = mat2.SpecularPower;
                texture = mat2.Texture;
                shadername = "$basic";
            }
            else if (mat is SkinnedEffect)
            {
                var mat2 = (SkinnedEffect)mat;
                diffuse = mat2.DiffuseColor.ToChamber();
                emissive = mat2.EmissiveColor.ToChamber();
                specularColor = mat2.SpecularColor.ToChamber();
                specularPower = mat2.SpecularPower;
                texture = mat2.Texture;
                shadername = "$skinned";
            }
            else
            {
                throw new InvalidOperationException();
            }

            writer.WriteLine(ImportExportHelper.Convert(diffuse));
            writer.WriteLine(ImportExportHelper.Convert(emissive));
            writer.WriteLine(ImportExportHelper.Convert(specularColor));
            writer.WriteLine(specularPower);
            string texname = "";
            if (texture != null)
            {
                var texadapter = Texture2DAdapter.GetAdapter(texture);
                if (texadapter != null)
                {
                    texname = content.LookupObjectName(texadapter);
                    if (texname == null)
                    {
                    }
                }
            }
            writer.WriteLine(texname);

            writer.WriteLine(shadername);
        }

        void WriteMesh(TextWriter writer, ModelMesh mesh, Model model, List<VertexBuffer> vbuffers, List<IndexBuffer> ibuffers, List<Effect> materials)
        {
            writer.WriteLine(mesh.Name);
            writer.WriteLine(mesh.ParentBone != null ? model.Bones.IndexOf(mesh.ParentBone) : -1);
            writer.WriteLine("MeshParts {0}", mesh.MeshParts.Count);
            foreach (var part in mesh.MeshParts)
            {
                WriteMeshPart(writer, part, vbuffers, ibuffers, materials);
            }
        }

        void WriteMeshPart(TextWriter writer, ModelMeshPart part, List<VertexBuffer> vbuffers, List<IndexBuffer> ibuffers, List<Effect> materials)
        {
            writer.WriteLine(part.Effect != null ? materials.IndexOf(part.Effect) : -1);
            writer.WriteLine(part.IndexBuffer != null ? ibuffers.IndexOf(part.IndexBuffer) : -1);
            writer.WriteLine(part.NumVertices);
            writer.WriteLine(part.PrimitiveCount);
            writer.WriteLine(part.StartIndex);
            writer.WriteLine(part.VertexBuffer != null ? vbuffers.IndexOf(part.VertexBuffer) : -1);
            writer.WriteLine(part.VertexOffset);
        }
    }
}

