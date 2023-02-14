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
                    if (!resolver.Materials.ContainsKey(part.Material))
                    {
                        var mat2 = new Material(part.Material, resolver, processor);
                        resolver.Materials.Add(part.Material, mat2);
                    }
                }
            }
            foreach (var mesh in modelContent.Meshes)
            {
                this.Meshes.Add(new Mesh(this, mesh, resolver));
            }
            this.Tag = modelContent.AnimationData;
            this.Filename = modelContent.Filename;
        }

        #region IModel implementation

        public System.Collections.Generic.IEnumerable<IMesh> GetMeshes()
        {
            return Meshes;
        }

        public void Draw(GameTime gameTime, Matrix world, Matrix view, Matrix projection,
                            Overrides overrides=default(Overrides))
        {
            if (!IsReady)
            {
                MakeReady();
            }

            var lighting = overrides.GetLighting(Lighting).Value;

            foreach (var mesh in Meshes)
            {
                mesh.Draw(gameTime, world, view, projection, lighting, overrides);
            }
        }

        public LightingData _lighting;
        public LightingData Lighting { get { return _lighting; } }
        public void SetAmbientLightColor(Vector3 value)
        {
            _lighting.AmbientLightColor = value;
        }

        public void SetEmissiveColor(Vector3 value)
        {
            _lighting.EmissiveColor = value;
        }

        public void SetDirectionalLight(DirectionalLight light, int index = 0)
        {
            if (index != 0)
                throw new ArgumentOutOfRangeException("index");

            _lighting.DirectionalLight = light;
        }

        public void DisableDirectionalLight(int index)
        {
            if (index == 0)
                throw new ArgumentOutOfRangeException("index");
        }

        public IEnumerable<Material> GetAllMaterials()
        {
            var materials = new HashSet<Material>(Meshes.SelectMany(m => m.Parts).Select(p => p.Material));
            foreach (var material in materials)
            {
                yield return material;
            }
        }

        public IMaterial GetMaterialByName(string name)
        {
            foreach (var material in GetAllMaterials())
            {
                if (material.Name == name)
                {
                    return material;
                }
            }

            return null;
        }

        public void SetAlpha(float alpha)
        {
            foreach (var material in GetAllMaterials())
            {
                material.Alpha = alpha;
            }
        }

        public void SetTexture(ITexture2D texture)
        {
            foreach (var material in GetAllMaterials())
            {
                material.Texture = texture;
            }
        }

        public void SetBoneTransforms(Matrix[] boneTransforms,
            Overrides overrides=default(Overrides))
        {
            if (boneTransforms == null) throw new ArgumentNullException("boneTransforms");

            IEnumerable<IMaterial> materials;
            if (overrides.Material != null)
            {
                materials = new[] { overrides.Material };
            }
            else
            {
                materials = GetAllMaterials();
            }

            foreach (var material in materials)
            {
                int i;
                for (i = 0; i < boneTransforms.Length; i++)
                {
                    var name = string.Format("bones[{0}]", i);
                    material.Shader.SetUniform(name, boneTransforms[i]);
                }
            }
        }

        public object Tag { get; set; }

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
                    var ii = part.Indexes.IndexData;
                    int i;
                    int n= part.PrimitiveCount*3;
                    for (i=0;i<n;i+=3)
                    {
                        int j = part.StartIndex+i;
                        var t = new Triangle(
                            vv[ii[j]].GetPosition(),
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

