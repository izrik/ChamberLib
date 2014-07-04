using System;
using System.IO;
using ChamberLib;
using System.Collections.Generic;

namespace ChamberLib
{
    public class ModelImporter
    {
        class RememberingReader : IDisposable
        {
            public readonly StreamReader Reader;
            public readonly List<string> Lines = new List<string>();

            public RememberingReader(string filename)
            {
                Reader = new StreamReader(filename);
            }

            #region IDisposable implementation

            public void Dispose()
            {
                Reader.Dispose();
            }

            #endregion

            public string ReadLine()
            {
                var line = Reader.ReadLine();
                Lines.Add(line);
                return line;
            }
        }

        public Model ImportModel(string filename, Renderer renderer, IContentManager content)
        {
            using (var reader = new RememberingReader(filename))
            {
                int num;
                // Bones
                num = int.Parse(reader.ReadLine().Split(' ')[1]);
                var bones = new List<Model.Bone>();
                var parentIndexes = new List<int>();
                int i;
                for (i = 0; i < num; i++)
                {
                    int parentIndex;
                    var bone = ReadBone(reader, out parentIndex);
                    bones.Add(bone);
                    parentIndexes.Add(parentIndex);
                }
                for (i = 0; i < num; i++)
                {
                    if (parentIndexes[i] < 0) continue;

                    bones[i].Parent = bones[parentIndexes[i]];
                    bones[i].Parent.Children.Add(bones[i]);
                }

                // VertexBuffers
                var vbuffers = new List<IVertex[]>();
                num = int.Parse(reader.ReadLine().Split(' ')[1]);
                for (i = 0; i < num; i++)
                {
                    var vs = ReadVertexBuffer(reader);
                    vbuffers.Add(vs);
                }

                // IndexBuffers
                var ibuffers = new List<short[]>();
                num = int.Parse(reader.ReadLine().Split(' ')[1]);
                for (i = 0; i < num; i++)
                {
                    var indexes = ReadIndexBuffer(reader);
                    ibuffers.Add(indexes);
                }

                // Materials
                var materials = new List<Model.Material>();
                num = int.Parse(reader.ReadLine().Split(' ')[1]);
                for (i = 0; i < num; i++)
                {
                    var material = ReadMaterial(reader, content);
                    materials.Add(material);
                }

                // Meshes
                num = int.Parse(reader.ReadLine().Split(' ')[1]);
                var meshes = new List<Model.Mesh>();
                var meshParentBones = new List<int>();
                for (i = 0; i < num; i++)
                {
                    int parentBone;
                    var mesh = ReadMesh(reader, vbuffers, ibuffers, materials, out parentBone);
                    meshes.Add(mesh);
                    meshParentBones.Add(parentBone);
                }
                var modelRootBone = int.Parse(reader.ReadLine());

                var model = new Model(renderer) {
                    Bones = bones,
                    Meshes = meshes,
                    RootBone = (modelRootBone >= 0 ? bones[modelRootBone] : null),
                };

                return model;
            }
        }

        static Model.Bone ReadBone(RememberingReader reader, out int parentIndex)
        {
            var name = reader.ReadLine();
            var index = int.Parse(reader.ReadLine());
            parentIndex = int.Parse(reader.ReadLine());
            var tr = ImportExportHelper.ConvertMatrix(reader.ReadLine());
            var bone = new Model.Bone {
                Name = name,
                Index = index,
                Transform = tr,
            };
            return bone;
        }

        static IVertex[] ReadVertexBuffer(RememberingReader reader)
        {
            var vertBufferName = reader.ReadLine();
            int numvertices2 = int.Parse(reader.ReadLine());
            int vertexType = int.Parse(reader.ReadLine());
            if (vertexType < 0 || vertexType > 3)
                throw new InvalidOperationException();
            var vs = new IVertex[numvertices2];
            int k;
            for (k = 0; k < numvertices2; k++)
            {
                IVertex v;
                float[] values;
                if (vertexType == 0)
                {
                    v = new Vertex_PBiBwNT();
                    values = new float[16];
                }
                else if (vertexType == 1)
                {
                    v = new Vertex_PN();
                    values = new float[6];
                }
                else if (vertexType == 2 || vertexType == 3)
                {
                    v = new Vertex_PNT();
                    values = new float[8];
                }
                else
                {
                    throw new InvalidOperationException();
                }
                int m;
                for (m = 0; m < values.Length; m++)
                {
                    values[m] = float.Parse(reader.ReadLine());
                }
                v.Populate(values);
                vs[k] = v;
            }
            return vs;
        }

        static short[] ReadIndexBuffer(RememberingReader reader)
        {
            var numIndexes = int.Parse(reader.ReadLine());
            var indexSize = int.Parse(reader.ReadLine());
            var indexBufferName = reader.ReadLine();
            var indexes = new short[numIndexes];
            int k;
            for (k = 0; k < numIndexes; k++)
            {
                indexes[k] = short.Parse(reader.ReadLine());
            }
            return indexes;
        }

        Model.Material ReadMaterial(RememberingReader reader, IContentManager content)
        {
            var mat = new Model.Material();
            mat.DiffuseColor = ImportExportHelper.ConvertVector3(reader.ReadLine());
            var texname = reader.ReadLine();
            if (!string.IsNullOrEmpty(texname))
            {
                var texture = content.Load<ITexture2D>(texname);
                mat.Texture = (TextureAdapter)texture;
            }

            return mat;
        }

        static Model.Mesh ReadMesh(RememberingReader reader, List<IVertex[]> vbuffers, List<short[]> ibuffers, List<Model.Material> materials, out int parentBone)
        {
            var name = reader.ReadLine();
            parentBone = int.Parse(reader.ReadLine());
            int num2;
            int j;
            // MeshParts
            num2 = int.Parse(reader.ReadLine().Split(' ')[1]);
            var parts = new List<Model.Part>();
            for (j = 0; j < num2; j++)
            {
                var part = ReadMeshPart(reader, vbuffers, ibuffers, materials);
                parts.Add(part);
            }
            var mesh = new Model.Mesh() {
                Parts = parts,
            };
            return mesh;
        }

        static Model.Part ReadMeshPart(RememberingReader reader, List<IVertex[]> vbuffers, List<short[]> ibuffers, List<Model.Material> materials)
        {
            var materialIndex = int.Parse(reader.ReadLine());
            var indexBufferIndex = int.Parse(reader.ReadLine());
            int numvertices = int.Parse(reader.ReadLine());
            var primCount = int.Parse(reader.ReadLine());
            var startIndex = int.Parse(reader.ReadLine());
            var vertexBufferIndex = int.Parse(reader.ReadLine());
            var vertexOffset = int.Parse(reader.ReadLine());
            var part = new Model.Part() {
                Indexes = (indexBufferIndex >= 0 ? ibuffers[indexBufferIndex] : null),
                Vertexes = (vertexBufferIndex >= 0 ? vbuffers[vertexBufferIndex] : null),
                StartIndex = startIndex,
                VertexOffset = vertexOffset,
                NumVertexes = numvertices,
                PrimitiveCount = primCount,
                Material = (materialIndex >= 0 ? materials[materialIndex] : null),
            };
            return part;
        }
    }
}
