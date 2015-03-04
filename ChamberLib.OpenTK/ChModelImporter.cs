using System;
using System.IO;
using ChamberLib;
using System.Collections.Generic;
using ChamberLib.Content;

namespace ChamberLib
{
    public class ChModelImporter
    {
        public ModelContent ImportModel(string filename, IContentManager content)
        {
            if (File.Exists(filename))
            {
            }
            else if (File.Exists(filename + ".chmodel"))
            {
                filename += ".chmodel";
            }
            else
            {
                throw new FileNotFoundException("The file could not be found", filename);
            }

            using (var s = new StreamReader(filename))
            {
                var reader =
                    new RememberingReader(
                        new SkipCommentsReader(
                            new RememberingReader(
                                new TextReaderAdapter(s))));
                int num;
                // Bones
                num = int.Parse(reader.ReadLine().Split(' ')[1]);
                var bones = new List<BoneContent>();
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

                    // TODO: child bone indexes
//                    bones[i].Parent = bones[parentIndexes[i]];
//                    bones[i].Parent.Children.Add(bones[i]);
                }

                // VertexBuffers
                var vbuffers = new List<VertexBufferContent>();
                num = int.Parse(reader.ReadLine().Split(' ')[1]);
                for (i = 0; i < num; i++)
                {
                    var vs = ReadVertexBuffer(reader);
                    var vb = new VertexBufferContent{Vertices=vs};
                    vbuffers.Add(vb);
                }

                // IndexBuffers
                var ibuffers = new List<IndexBufferContent>();
                num = int.Parse(reader.ReadLine().Split(' ')[1]);
                for (i = 0; i < num; i++)
                {
                    var indexes = ReadIndexBuffer(reader);
                    var ib = new IndexBufferContent{Indexes=indexes};
                    ibuffers.Add(ib);
                }

                // Materials
                var materials = new List<MaterialContent>();
                num = int.Parse(reader.ReadLine().Split(' ')[1]);
                for (i = 0; i < num; i++)
                {
                    var material = ReadMaterial(reader, content);
                    materials.Add(material);
                }

                // Meshes
                num = int.Parse(reader.ReadLine().Split(' ')[1]);
                var meshes = new List<MeshContent>();
                for (i = 0; i < num; i++)
                {
                    var mesh = ReadMesh(reader, vbuffers, ibuffers, materials, bones);

                    meshes.Add(mesh);
                }
                var modelRootBone = int.Parse(reader.ReadLine());

                var model = new ModelContent {
                    Bones = bones,
                    Meshes = meshes,
//                    RootBone = (modelRootBone >= 0 ? bones[modelRootBone] : null),
                    RootBoneIndex = modelRootBone,
                    VertexBuffers = vbuffers,
                    IndexBuffers = ibuffers,
                };

                var hasAnimation = bool.Parse(reader.ReadLine());
                if (hasAnimation)
                {
                    var ae = new AnimationExporter();
                    var animdata = ae.ImportAnimationData(reader);
                    model.AnimationData = animdata;
                }

                return model;
            }
        }

        static BoneContent ReadBone(IReader reader, out int parentIndex)
        {
            var name = reader.ReadLine();
            var index = int.Parse(reader.ReadLine());
            parentIndex = int.Parse(reader.ReadLine());
            var tr = ImportExportHelper.ConvertMatrix(reader.ReadLine());
            var bone = new BoneContent {
                Name = name,
//                Index = index,
                Transform = tr,
            };
            return bone;
        }

        static IVertex[] ReadVertexBuffer(IReader reader)
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

        static short[] ReadIndexBuffer(IReader reader)
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

        MaterialContent ReadMaterial(IReader reader, IContentManager content)
        {
            var mat = new MaterialContent();
            mat.DiffuseColor = ImportExportHelper.ConvertVector3(reader.ReadLine());
            mat.EmissiveColor = ImportExportHelper.ConvertVector3(reader.ReadLine());
            mat.SpecularColor = ImportExportHelper.ConvertVector3(reader.ReadLine());
            mat.SpecularPower = float.Parse(reader.ReadLine());
            var texname = reader.ReadLine();
            if (!string.IsNullOrEmpty(texname))
            {
                var resolvedFilename = content.ResolveTextureFilename(texname);
                var ti = new BasicTextureImporter();
                var texture = ti.ImportTexture(resolvedFilename, null);
                mat.Texture = texture;
            }
            var shadername = reader.ReadLine();
            if (!string.IsNullOrEmpty(shadername))
            {
                ShaderContent shader;
                if (shadername == "$basic")
                {
                    shader = BuiltinShaders.BasicShaderContent;
                }
                else if (shadername == "$skinned")
                {
                    shader = BuiltinShaders.SkinnedShaderContent;
                }
                else
                {
                    string vert;
                    string frag;
                    content.ResolveShaderFilenames(shadername, out vert, out frag);

                    shader =
                        BasicShaderLoader.LoadShader(
                            vert,
                            frag,
                            bindattrs: new string[] {
                                "in_position",
                                "in_blend_indexes",
                                "in_blend_weights",
                                "in_normal",
                                "in_texture_coords",
                            });
                }
                mat.Shader = shader;
            }

            return mat;
        }

        static MeshContent ReadMesh(IReader reader, List<VertexBufferContent> vbuffers, List<IndexBufferContent> ibuffers, List<MaterialContent> materials, List<BoneContent> bones)
        {
            var name = reader.ReadLine();
            int parentBone = int.Parse(reader.ReadLine());
            int num2;
            int j;
            // MeshParts
            num2 = int.Parse(reader.ReadLine().Split(' ')[1]);
            var parts = new List<PartContent>();
            for (j = 0; j < num2; j++)
            {
                var part = ReadMeshPart(reader, vbuffers, ibuffers, materials);
                parts.Add(part);
            }
            var mesh = new MeshContent {
                Parts = parts,
                // TODO: mesh parent bone?
//                ParentBone = bones[parentBone],
            };
            return mesh;
        }

        static PartContent ReadMeshPart(IReader reader, List<VertexBufferContent> vbuffers, List<IndexBufferContent>  ibuffers, List<MaterialContent> materials)
        {
            var materialIndex = int.Parse(reader.ReadLine());
            var indexBufferIndex = int.Parse(reader.ReadLine());
            int numvertices = int.Parse(reader.ReadLine());
            var primCount = int.Parse(reader.ReadLine());
            var startIndex = int.Parse(reader.ReadLine());
            var vertexBufferIndex = int.Parse(reader.ReadLine());
            var vertexOffset = int.Parse(reader.ReadLine());
            var part = new PartContent {
                Vertexes = vbuffers[vertexBufferIndex],
                Indexes = ibuffers[indexBufferIndex],
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
