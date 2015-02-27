using System;
using Assimp;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using ChamberLib.Content;

namespace ChamberLib
{
    public class AiModelLoader
    {
        public AssimpContext Importer = new AssimpContext();

        public string[] GetSupportedImportFormats()
        {
            return Importer.GetSupportedImportFormats();
        }

        public ModelContent LoadModel(string filename, IContentManager content)
        {
            var scene = Importer.ImportFile(filename, PostProcessSteps.Triangulate | PostProcessSteps.JoinIdenticalVertices);
//            WriteScene(scene);
            var model = ConvertScene(scene, content, filename);
            return model;
        }

        ModelContent ConvertScene(Scene scene, IContentManager content, string filename)
        {
            // bones
            // meshes
            //  vertices
            //  indices
            // materials
            //  textures
            //  shaders?
            // animations?

            var model = new ModelContent();
            var materials = new List<MaterialContent>();

            foreach (var material in scene.Materials)
            {
                var material2 = new MaterialContent();
                material2.Name = material.Name;
                material2.Alpha = material.Opacity;
                material2.DiffuseColor = material.ColorDiffuse.ToChamberVector().ToVectorXYZ();
                material2.EmissiveColor = material.ColorEmissive.ToChamberVector().ToVectorXYZ();
                material2.Shader = null;
                material2.SpecularColor = material.ColorSpecular.ToChamberVector().ToVectorXYZ();
                material2.SpecularPower = material.Shininess;
                material2.Texture = null;
                if (!string.IsNullOrEmpty(material.TextureDiffuse.FilePath))
                {
                    var texFilename = content.ResolveTextureFilename(material.TextureDiffuse.FilePath);
                    material2.Texture = BasicTextureLoader.LoadTexture(texFilename);
                }
                materials.Add(material2);
            }

            if (scene.MeshCount > 1)
            {
                throw new NotImplementedException();
            }
            IVertex[] vertices = null;
            foreach (var mesh in scene.Meshes)
            {
                var indices = mesh.GetShortIndices();
                vertices = new IVertex[mesh.VertexCount];
                int i;
                for (i = 0; i < mesh.VertexCount; i++)
                {
                    var vv = new Vertex_PBiBwNT();

                    vv.Position = mesh.Vertices[i].ToChamber();
                    vv.Normal = mesh.Normals[i].ToChamber();
                    var uvw = mesh.TextureCoordinateChannels[0][i];
                    vv.TextureCoords = new Vector2(uvw.X, 1-uvw.Y);

                    vertices[i] = vv;
                }

                var part = new PartContent();
                var ib = new IndexBufferContent{Indexes=indices};
                part.Indexes = ib;
                model.IndexBuffers.Add(ib);
                var vb = new VertexBufferContent{Vertices=vertices};
                part.Vertexes = vb;
                model.VertexBuffers.Add(vb);
                part.Material = materials[mesh.MaterialIndex];
                part.NumVertexes = mesh.VertexCount;
                part.PrimitiveCount = mesh.FaceCount;
                var mesh2 = new MeshContent();
                mesh2.Parts.Add(part);
                model.Meshes.Add(mesh2);
            }

            var nodes = Collection.DepthFirstTraversal(scene.RootNode, node => node.Children).ToList();
            var footsteps = nodes.Find(n => n.Name == "Bip01 Footsteps");
            nodes.Remove(footsteps);
            nodes.Add(footsteps);
            var nodesByName = new Dictionary<string, Node>();
            var bonesByNode = new Dictionary<Node, BoneContent>();
            var bonesByName = new Dictionary<string, BoneContent>();
            foreach (var node in nodes)
            {
                nodesByName[node.Name] = node;
                var bone = ConvertNodeToBone(node);
                bonesByNode[node] = bone;
                if (bonesByName.ContainsKey(node.Name))
                {
                    throw new InvalidOperationException("Two nodes have the same name");
                }
                bonesByName[node.Name] = bone;
                model.Bones.Add(bone);
                if (node == scene.RootNode)
                {
                    model.RootBoneIndex = model.Bones.IndexOf(bone);
                }
                if (node.Parent != null && bonesByNode.ContainsKey(node.Parent))
                {
                    // TODO: bone parents and children?
//                    bone.Parent = bonesByNode[node.Parent];
                }
                else if (node.Parent != null)
                {
                    Console.WriteLine("Can't find node's parent: {0}, {1}", node.Name, node.Parent.Name);
                }
            }
            int ibone;
            for (ibone = 0; ibone < model.Bones.Count; ibone++)
            {
//                model.Bones[ibone].Index = ibone;
            }


            foreach (var mesh in scene.Meshes)
            {
                foreach (var bone in mesh.Bones)
                {
                    bonesByName[bone.Name].InverseBindPose = bone.OffsetMatrix.ToChamber();
                }

                var blendIndexes = new List<List<int>>();
                var blendWeights = new List<List<float>>();
                int i;
                for (i = 0; i < mesh.VertexCount; i++)
                {
                    blendIndexes.Add(new List<int>());
                    blendWeights.Add(new List<float>());
                }
                foreach (var bone in mesh.Bones)
                {
                    var bone2 = bonesByName[bone.Name];
                    var bi = model.Bones.IndexOf(bone2);
                    foreach (var vw in bone.VertexWeights)
                    {
                        blendIndexes[vw.VertexID].Add(bi);
                        blendWeights[vw.VertexID].Add(vw.Weight);
                    }
                }
                for (i = 0; i < mesh.VertexCount; i++)
                {
                    var ii = blendIndexes[i];
                    while (ii.Count < 4)
                    {
                        ii.Add(0);
                    }
                    if (ii.Count > 4)
                        throw new InvalidOperationException(string.Format("Too many blend indices for vertex {0}", i));

                    var ww = blendWeights[i];
                    while (ww.Count < 4)
                    {
                        ww.Add(0);
                    }
                    if (ww.Count > 4)
                        throw new InvalidOperationException(string.Format("Two many blend weights for vertex {0}", i));

                    vertices[i].SetBlendIndices(new Vector4(ii[0], ii[1], ii[2], ii[3]));
                    vertices[i].SetBlendWeights(new Vector4(ww[0], ww[1], ww[2], ww[3]));
                }
            }

            foreach (var anim in scene.Animations)
            {

                foreach (var channel in anim.NodeAnimationChannels)
                {
                    int i;
                    for (i = 0; i < channel.PositionKeyCount; i++)
                    {
                        var pos = channel.PositionKeys[i].Value.ToChamber();
                        var rot = channel.RotationKeys[i].Value.ToChamber();
                        var sca = channel.ScalingKeys[i].Value.ToChamber();

                        var transform = Matrix.CreateScale(sca) * rot.ToMatrix() * Matrix.CreateTranslation(pos);
                    }
                }

                break;
            }



            //AnimationData
            if (scene.AnimationCount > 0)
            {
                var sequencesByName = new Dictionary<string, AnimationSequence>();
                var transforms = new List<Matrix>();
                var absoluteTransforms = new List<Matrix>();
                var skeletonHierarchy = new List<int>();
                var boneIndexesByName = new Dictionary<string, int>();
                int i;
                for (i = 0; i < model.Bones.Count; i++)
                {
                    var bone = model.Bones[i];
                    // TODO: skeletonHierarchy
                    //skeletonHierarchy.Add(bone.Parent == null ? -1 : model.Bones.IndexOf((Bone)bone.Parent));
                    boneIndexesByName[bone.Name] = i;
                    transforms.Add(bone.Transform);
                    var inverseBindPose = bone.InverseBindPose;
                    if (inverseBindPose == Matrix.Zero)
                        inverseBindPose = Matrix.Identity;
                    absoluteTransforms.Add(inverseBindPose);
                }

                foreach (var anim in scene.Animations)
                {
                    var tps = anim.TicksPerSecond > 0 ? anim.TicksPerSecond : 30;
                    var tickDuration = 1 / tps;
                    var duration = anim.DurationInTicks * tickDuration;
                    var frames = new List<AnimationFrame>();
                    var ftransforms = new List<Matrix>();
                    int j;
                    for (j = 0; j < model.Bones.Count; j++)
                    {
                        ftransforms.Add(Matrix.Identity);
                    }
                    int numFrames = anim.NodeAnimationChannels[0].PositionKeyCount;// * 2;
                    Vector3 maxPosition = anim.NodeAnimationChannels[0].PositionKeys[0].Value.ToChamber();

                    for (i = 0; i < numFrames; i++)
                    {
                        var ii = i;// / 2;
                        var ir = i % 2;

                        for (j = 0; j < ftransforms.Count; j++)
                        {
                            ftransforms[j] = Matrix.Identity;
                        }
                        var time = anim.NodeAnimationChannels[0].PositionKeys[ii].Time * tickDuration;
                        for (j = 0; j < anim.NodeAnimationChannels.Count; j++)
                        {
                            var channel = anim.NodeAnimationChannels[j];
                            if (channel.PositionKeyCount < 0 || channel.RotationKeyCount < 0 || channel.ScalingKeyCount < 0)
                                continue;

                            var iix = Math.Min(channel.PositionKeys.Count-1, ii);
                            var position = channel.PositionKeys[iix].Value.ToChamber();
                            iix = Math.Min(channel.RotationKeys.Count-1, ii);
                            var rotation = channel.RotationKeys[iix].Value.ToChamber();
                            iix = Math.Min(channel.ScalingKeys.Count-1, ii);
                            var scale = channel.ScalingKeys[iix].Value.ToChamber();

                            if (ir == 1 && ii+1<channel.PositionKeyCount && false)
                            {
                                position = (position + channel.PositionKeys[ii+1].Value.ToChamber()) / 2;

                                var rot2 = channel.RotationKeys[ii + 1].Value.ToChamber();
                                rotation = new Quaternion(
                                    (rotation.X + rot2.X) / 2,
                                    (rotation.Y + rot2.Y) / 2,
                                    (rotation.Z + rot2.Z) / 2,
                                    (rotation.W + rot2.W) / 2);

                                scale = (scale + channel.ScalingKeys[ii+1].Value.ToChamber()) / 2;
                            }

                            if (position.LengthSquared() > maxPosition.LengthSquared())
                                maxPosition = position;

                            var transform = Matrix.CreateScale(scale) * rotation.ToMatrix() * Matrix.CreateTranslation(position);

                            ftransforms[boneIndexesByName[channel.NodeName]] = transform;
                        }

                        var frame = new AnimationFrame((float)time, ftransforms.ToArray());
                        frames.Add(frame);
                    }

                    var name = anim.Name.Replace("AnimStack::", "");
                    var seq = new AnimationSequence((float)duration, frames.ToArray(), name);
                    sequencesByName[seq.Name] = seq;
                }

                var animdata = new AnimationData(
                    sequencesByName,
                    transforms,
                    absoluteTransforms,
                    skeletonHierarchy);
                // TODO: tag and animation
//                model.Tag = animdata;
            }

            model.Filename = filename;

            return model;
        }

        static BoneContent ConvertNodeToBone(Node node)
        {
            var bone = new BoneContent();
            bone.Transform = node.Transform.ToChamber();
            bone.Name = node.Name;

            return bone;
        }

        public static void WriteScene(TextWriter writer, Scene scene)
        {
            writer.WriteLine("AnimationCount: {0}", scene.AnimationCount);
            int k = 0;
            foreach (var anim in scene.Animations)
            {
                writer.WriteLine("Animation {0} Name: {1}", k, anim.Name);
                writer.WriteLine("Animation {0} DurationInTicks: {1}", k, anim.DurationInTicks);
                writer.WriteLine("Animation {0} MeshAnimationChannelCount: {1}", k, anim.MeshAnimationChannelCount);
                writer.WriteLine("Animation {0} NodeAnimationChannelCount: {1}", k, anim.NodeAnimationChannelCount);
                writer.WriteLine("Animation {0} TicksPerSecond: {1}", k, anim.TicksPerSecond);

                k++;
            }
            writer.WriteLine("MaterialCount: {0}", scene.MaterialCount);
            k = 0;
            foreach (var mat in scene.Materials)
            {
                writer.WriteLine("Material {0} BlendMode: {1}", k, mat.BlendMode);
                writer.WriteLine("Material {0} BumpScaling: {1}", k, mat.BumpScaling);
                writer.WriteLine("Material {0} ColorAmbient: {1}", k, mat.ColorAmbient);
                writer.WriteLine("Material {0} ColorDiffuse: {1}", k, mat.ColorDiffuse);
                writer.WriteLine("Material {0} ColorEmissive: {1}", k, mat.ColorEmissive);
                writer.WriteLine("Material {0} ColorReflective: {1}", k, mat.ColorReflective);
                writer.WriteLine("Material {0} ColorSpecular: {1}", k, mat.ColorSpecular);
                writer.WriteLine("Material {0} ColorTransparent: {1}", k, mat.ColorTransparent);
                writer.WriteLine("Material {0} IsTwoSided: {1}", k, mat.IsTwoSided);
                writer.WriteLine("Material {0} IsWireFrameEnabled: {1}", k, mat.IsWireFrameEnabled);
                writer.WriteLine("Material {0} Name: {1}", k, mat.Name);
                writer.WriteLine("Material {0} Opacity: {1}", k, mat.Opacity);
                writer.WriteLine("Material {0} Reflectivity: {1}", k, mat.Reflectivity);
                writer.WriteLine("Material {0} ShadingMode: {1}", k, mat.ShadingMode);
                writer.WriteLine("Material {0} Shininess: {1}", k, mat.Shininess);
                writer.WriteLine("Material {0} ShininessStrength: {1}", k, mat.ShininessStrength);
                writer.WriteLine("Material {0} HasTextureAmbient: {1}", k, mat.HasTextureAmbient);
                writer.WriteLine("Material {0} HasTextureDiffuse: {1}", k, mat.HasTextureDiffuse);
                writer.WriteLine("Material {0} HasTextureDisplacement: {1}", k, mat.HasTextureDisplacement);
                writer.WriteLine("Material {0} HasTextureEmissive: {1}", k, mat.HasTextureEmissive);
                writer.WriteLine("Material {0} HasTextureHeight: {1}", k, mat.HasTextureHeight);
                writer.WriteLine("Material {0} HasTextureLightMap: {1}", k, mat.HasTextureLightMap);
                writer.WriteLine("Material {0} HasTextureNormal: {1}", k, mat.HasTextureNormal);
                writer.WriteLine("Material {0} HasTextureOpacity: {1}", k, mat.HasTextureOpacity);
                writer.WriteLine("Material {0} HasTextureReflection: {1}", k, mat.HasTextureReflection);
                writer.WriteLine("Material {0} HasTextureSpecular: {1}", k, mat.HasTextureSpecular);
                writer.WriteLine("Material {0} PropertyCount: {1}", k, mat.PropertyCount);
                k++;
            }

            var nodes = new List<Node>();
            var visit = new Queue<Node>();
            visit.Enqueue(scene.RootNode);
            while (visit.Count > 0)
            {
                var node = visit.Dequeue();
                nodes.Add(node);
                foreach (var child in node.Children)
                {
                    visit.Enqueue(child);
                }
            }
            var nodesByName = new Dictionary<string, Node>();
            foreach (var node in nodes)
            {
                nodesByName[node.Name] = node;
            }

            writer.WriteLine("MeshCount: {0}", scene.MeshCount);
            k = 0;
            foreach (var mesh in scene.Meshes)
            {
                int j = 0;
                foreach (var bone in mesh.Bones)
                {
                    writer.WriteLine("Mesh {0} Bone {1} Name: {2} [{3}]", k, j, bone.Name, nodes.IndexOf(nodesByName[bone.Name]));
                    writer.WriteLine("Mesh {0} Bone {1} OffsetMatrix: {2}", k, j, bone.OffsetMatrix);
                    writer.WriteLine("Mesh {0} Bone {1} OffsetMatrix Translation: {2}", k, j, bone.OffsetMatrix.ToChamber().Translation);
                    writer.WriteLine("Mesh {0} Bone {1} OffsetMatrix Rotation: {2}", k, j, bone.OffsetMatrix.ToChamber().DecomposedRotation);
                    writer.WriteLine("Mesh {0} Bone {1} OffsetMatrix Scale: {2}", k, j, bone.OffsetMatrix.ToChamber().Scale);
                    writer.WriteLine("Mesh {0} Bone {1} VertexWeightCount: {2}", k, j, bone.VertexWeightCount);
                    j++;
                }
                writer.WriteLine("Mesh {0} FaceCount: {1}", k, mesh.FaceCount);
                writer.WriteLine("Mesh {0} MaterialIndex: {1}, \"{2}\"", k, mesh.MaterialIndex, scene.Materials[mesh.MaterialIndex].Name);
                writer.WriteLine("Mesh {0} Name: {1}", k, mesh.Name);
                for (j = 0; j < mesh.Vertices.Count; j++)
                {
                    writer.WriteLine("Mesh {0} Vertex {1}: {2} {3}", k, j, mesh.Vertices[j], mesh.Normals[j]);
                }
                writer.WriteLine("Mesh {0} PrimitiveType: {1}", k, mesh.PrimitiveType);
                k++;
            }

            writer.WriteLine("Nodes: {0}", nodes.Count);
            k = 0;
            foreach (var node in nodes)
            {
                int j = 0;
                foreach (var child in node.Children)
                {
                    writer.WriteLine("Node {0} Child {1}: {2}", k, j, nodes.IndexOf(child));
                    j++;
                }
                j = 0;
                foreach (var meshIndex in node.MeshIndices)
                {
                    writer.WriteLine("Node {0} Mesh Index {1}: {2}, \"{3}\"", k, j, meshIndex, scene.Meshes[meshIndex].Name);
                    j++;
                }
                writer.WriteLine("Node {0} Name: {1}", k, node.Name);
                writer.WriteLine("Node {0} Transform: {1}", k, node.Transform);
                writer.WriteLine("Node {0} Transform Translation: {1}", k, node.Transform.ToChamber().Translation);
                writer.WriteLine("Node {0} Transform Rotation: {1}", k, node.Transform.ToChamber().DecomposedRotation);
                writer.WriteLine("Node {0} Transform Scale: {1}", k, node.Transform.ToChamber().Scale);

                k++;
            }
        }
    }
}

