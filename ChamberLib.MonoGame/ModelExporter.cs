using System;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using ChamberLib;
using XModel = Microsoft.Xna.Framework.Graphics.Model;
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
                foreach (var bone in model.Bones)
                {
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
                foreach (var vb in vbuffers)
                {
                    WriteVertexBuffer(writer, vb);
                }
                writer.WriteLine("IndexBuffers {0}", ibuffers.Count);
                foreach (var ib in ibuffers)
                {
                    WriteIndexBuffer(writer, ib);
                }
                writer.WriteLine("Materials {0}", materials.Count);
                foreach (var mat in materials)
                {
                    WriteMaterial(writer, mat, content);
                }
                writer.WriteLine("Meshes {0}", model.Meshes.Count);
                foreach (var mesh in model.Meshes)
                {
                    WriteMesh(writer, mesh, model, vbuffers, ibuffers, materials);
                }
                writer.WriteLine(model.Root != null ? model.Bones.IndexOf(model.Root) : -1);
            }
        }

        static void WriteBone(StreamWriter writer, ModelBone bone)
        {
            writer.WriteLine(bone.Name);
            writer.WriteLine(bone.Index);
            writer.WriteLine(bone.Parent != null ? bone.Parent.Index : -1);
            writer.WriteLine(ImportExportHelper.Convert(bone.Transform.ToChamber()));
        }

        void WriteVertexBuffer(StreamWriter writer, VertexBuffer vb)
        {
            writer.WriteLine(vb.Name);
            writer.WriteLine(vb.VertexCount);
            var decl = vb.VertexDeclaration;
            bool hasPosition = false;
            bool hasBlendIndices = false;
            bool hasBlendWeights = false;
            bool hasNormal = false;
            bool hasTexCoords = false;
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
            else if (hasPosition && !hasBlendIndices && !hasBlendWeights && hasNormal && hasTexCoords)
            {
                vertexType = 2;
            }
            else
            {
                throw new InvalidOperationException();
            }

            writer.WriteLine(vertexType);
            if (vertexType == 0)
            {
                var vs = new Vertex_PBiBwNT[vb.VertexCount];
                vb.GetData(vs);

                foreach (var v in vs)
                {
                    var values = v.GetValues();
                    foreach (var value in values)
                    {
                        writer.WriteLine(value);
                    }
                }
            }
            else if (vertexType == 1)
            {
                var vs = new Vertex_PN[vb.VertexCount];
                vb.GetData(vs);

                foreach (var v in vs)
                {
                    var values = v.GetValues();
                    foreach (var value in values)
                    {
                        writer.WriteLine(value);
                    }
                }
            }
            else if (vertexType == 2)
            {
                var vs = new Vertex_PNT[vb.VertexCount];
                vb.GetData(vs);

                foreach (var v in vs)
                {
                    var values = v.GetValues();
                    foreach (var value in values)
                    {
                        writer.WriteLine(value);
                    }
                }
            }
        }

        void WriteIndexBuffer(StreamWriter writer, IndexBuffer ib)
        {
            writer.WriteLine(ib.IndexCount);
            writer.WriteLine(ib.IndexElementSize == IndexElementSize.SixteenBits ? "16" : "32");
            writer.WriteLine(ib.Name);
            var indexes = new short[ib.IndexCount];
            ib.GetData(indexes);
            foreach (var index in indexes)
            {
                writer.WriteLine(index);
            }
        }

        void WriteMaterial(StreamWriter writer, Effect mat, IContentManager content)
        {
            Texture2D texture;
            Vector3 diffuse;
            if (mat is BasicEffect)
            {
                var mat2 = (BasicEffect)mat;
                diffuse = mat2.DiffuseColor.ToChamber();
                texture = mat2.Texture;
            }
            else if (mat is SkinnedEffect)
            {
                var mat2 = (SkinnedEffect)mat;
                diffuse = mat2.DiffuseColor.ToChamber();
                texture = mat2.Texture;
            }
            else
            {
                throw new InvalidOperationException();
            }

            writer.WriteLine(ImportExportHelper.Convert(diffuse));
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
        }

        void WriteMesh(StreamWriter writer, ModelMesh mesh, Model model, List<VertexBuffer> vbuffers, List<IndexBuffer> ibuffers, List<Effect> materials)
        {
            writer.WriteLine(mesh.Name);
            writer.WriteLine(mesh.ParentBone != null ? model.Bones.IndexOf(mesh.ParentBone) : -1);
            writer.WriteLine("MeshParts {0}", mesh.MeshParts.Count);
            foreach (var part in mesh.MeshParts)
            {
                WriteMeshPart(writer, part, vbuffers, ibuffers, materials);
            }
        }

        void WriteMeshPart(StreamWriter writer, ModelMeshPart part, List<VertexBuffer> vbuffers, List<IndexBuffer> ibuffers, List<Effect> materials)
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

