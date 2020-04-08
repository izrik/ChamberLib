using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Graphics.OpenGL;
using System.IO;
using ChamberLib.Content;

namespace ChamberLib.OpenTK
{
    public class Model : IModel
    {
        public Model(Content.ModelContent modelContent, IContentProcessor processor)
        {
            var resolver = new ContentResolver();

            foreach (var ib in modelContent.IndexBuffers)
            {
                var ib2 = new IndexBuffer(ib);
                resolver.Add(ib, ib2);
                this.IndexBuffers.Add(ib2);
            }
            foreach (var vb in modelContent.VertexBuffers)
            {
                var vb2 = VertexBuffer.FromArray(vb.Vertices);
                resolver.Add(vb, vb2);
                this.VertexBuffers.Add(vb2);
            }
            foreach (var bone in modelContent.Bones)
            {
                var b2 = new Bone(bone);
                resolver.Add(bone, b2);
                this.Bones.Add(b2);
            }
            this.RootBone = this.Bones[modelContent.RootBoneIndex];
            int i;
            for (i = 0; i < this.Bones.Count; i++)
            {
                this.Bones[i].Index = i;
                foreach (var childIndex in modelContent.Bones[i].ChildBoneIndexes)
                {
                    // TODO: interlink these
                    this.Bones[i].Children.Add(this.Bones[childIndex]);
                    this.Bones[childIndex].Parent = this.Bones[i];
                }
            }
            foreach (var mesh in modelContent.Meshes)
            {
                foreach (var part in mesh.Parts)
                {
                    if (!resolver.VertexMaterials.ContainsKey(part.VertexMaterial))
                    {
                        var mat2 = new VertexMaterial(part.VertexMaterial, resolver, processor);
                        resolver.VertexMaterials.Add(part.VertexMaterial, mat2);
                    }
                    if (!resolver.FragmentMaterials.ContainsKey(part.FragmentMaterial))
                    {
                        var mat2 = new FragmentMaterial(part.FragmentMaterial, resolver, processor);
                        resolver.FragmentMaterials.Add(part.FragmentMaterial, mat2);
                    }
                }
            }
            foreach (var mesh in modelContent.Meshes)
            {
                this.Meshes.Add(new Mesh(this, mesh, resolver));
            }
            this.AnimationData = modelContent.AnimationData;
            this.Filename = modelContent.Filename;
        }

        #region IModel implementation

        public List<Mesh> GetMeshes()
        {
            return Meshes;
        }

        IEnumerable<IMesh> IModel.GetMeshes()
        {
            return GetMeshes();
        }

        public void Draw(GameTime gameTime, Matrix world,
            ComponentCollection components,
            Overrides overrides=default(Overrides))
        {
            if (!IsReady)
            {
                MakeReady();
            }

            foreach (var mesh in Meshes)
            {
                mesh.Draw(gameTime, world, components, overrides);
            }
        }

        public void SetEmissiveColor(Vector3 value)
        {
            // TODO: set the emissive on the material(s)
        }

        public IVertexMaterial GetVertexMaterialByName(string name)
        {
            foreach (var mesh in Meshes)
            {
                foreach (var part in mesh.Parts)
                {
                    if (part.VertexMaterial.Name == name)
                        return part.VertexMaterial;
                }
            }

            return null;
        }

        public IFragmentMaterial GetFragmentMaterialByName(string name)
        {
            foreach (var mesh in Meshes)
            {
                foreach (var part in mesh.Parts)
                {
                    if (part.FragmentMaterial.Name == name)
                        return part.FragmentMaterial;
                }
            }

            return null;
        }

        public void SetAlpha(float alpha)
        {
            foreach (var mesh in Meshes)
            {
                foreach (var part in mesh.Parts)
                {
                    part.FragmentMaterial.Alpha = alpha;
                }
            }
        }

        public void SetTexture(ITexture2D texture)
        {
            foreach (var mesh in Meshes)
            {
                foreach (var part in mesh.Parts)
                {
                    part.FragmentMaterial.Texture = texture;
                }
            }
        }

        static List<string> boneNames = new List<string>();
        static string GetBoneUniformName(int i)
        {
            int j;
            for (j = boneNames.Count; j <= i; j++)
            {
                boneNames.Add(string.Format("bones[{0}]", j));
            }
            return boneNames[i];
        }
        public void SetBoneTransforms(Matrix[] boneTransforms,
            Overrides overrides=default(Overrides))
        {
            if (boneTransforms == null) throw new ArgumentNullException("boneTransforms");

            if (overrides.VertexMaterial != null)
            {
                var material = overrides.GetVertexMaterial(null);
                if (material != null)
                    SetBoneUniformsForMaterial(boneTransforms, material);
            }
            else
            {
                foreach (var mesh in Meshes)
                {
                    foreach (var part in mesh.Parts)
                    {
                        SetBoneUniformsForMaterial(boneTransforms, part.VertexMaterial);
                    }
                }
            }
        }
        void SetBoneUniformsForMaterial(Matrix[] boneTransforms, IVertexMaterial material)
        {
            material.VertexShader.SetUniform("bones[0]", boneTransforms);
        }

        public AnimationData AnimationData { get; set; }

        public IBone Root
        {
            get
            {
                return RootBone;
            }
            set
            {
                RootBone = (Bone)value;
            }
        }

        public IEnumerable<IBone> EnumerateBones()
        {
            foreach (var bone in Bones)
            {
                yield return bone;
            }
        }

        public IEnumerable<Triangle> EnumerateTriangles()
        {
            var set = new HashSet<Triangle>();
            foreach (var mesh in Meshes)
            {
                foreach (var part in mesh.Parts)
                {
                    var vv = part.Vertexes.VertexData;
                    var ii = part.Indexes;
                    int i;
                    int n = part.PrimitiveCount * 3;
                    for (i = 0; i < n; i += 3)
                    {
                        int j = part.StartIndex + i;
                        var t = new Triangle(
                            vv[ii[j + 0]].GetPosition(),
                            vv[ii[j + 1]].GetPosition(),
                            vv[ii[j + 2]].GetPosition());
                        if (!set.Contains(t))
                        {
                            set.Add(t);
                            yield return t;
                        }
                    }
                }
            }
        }

        #endregion

        public List<Mesh> Meshes = new List<Mesh>();
        public List<Bone> Bones = new List<Bone>();
        public Bone RootBone;

        public List<IndexBuffer> IndexBuffers = new List<IndexBuffer>();
        public List<VertexBuffer> VertexBuffers = new List<VertexBuffer>();

        public bool IsReady = false;
        public void MakeReady()
        {
            if (IsReady) return;

            foreach (var mesh in Meshes)
            {
                mesh.MakeReady();
            }

            IsReady = true;
        }
        public string Filename;
    }
}

