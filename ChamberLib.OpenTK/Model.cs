using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Graphics.OpenGL;
using System.IO;

namespace ChamberLib
{
    public class Model : IModel
    {
        public Model(Renderer renderer)
        {
            Renderer = renderer;
        }

        public readonly Renderer Renderer;

        #region IModel implementation

        public System.Collections.Generic.IEnumerable<IMesh> GetMeshes()
        {
            return new IMesh[0];
        }

        public void Draw(Matrix world, Matrix view, Matrix projection)
        {
            if (!IsReady)
            {
                MakeReady();
            }

            foreach (var mesh in Meshes)
            {
                mesh.Draw(Renderer, world, view, projection, Lighting);
            }
        }

        public void EnableDefaultLighting()
        {
        }

        LightingData Lighting;
        public void SetAmbientLightColor(Vector3 value)
        {
            Lighting.AmbientLightColor = value;
        }

        public void SetEmissiveColor(Vector3 value)
        {
            Lighting.EmissiveColor = value;
        }

        public void SetDirectionalLight(DirectionalLight light, int index = 0)
        {
            if (index != 0)
                throw new ArgumentOutOfRangeException("index");

            Lighting.DirectionalLight = light;
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

        public void SetWorldViewProjection(Matrix transform, Matrix view, Matrix projection)
        {
        }

        public void SetBoneTransforms(Matrix[] boneTransforms, float verticalOffset, Matrix world)
        {
            foreach (var material in GetAllMaterials())
            {
                if (material.Shader == BuiltinShaders.SkinnedShader)
                {
                    int i;
                    for (i = 0; i < 30 && i < boneTransforms.Length; i++)
                    {
                        var name = string.Format("bones[{0}]", i);
                        BuiltinShaders.SkinnedShader.SetUniform(name, boneTransforms[i]);
                    }

                    break;
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

        #endregion

        public List<Mesh> Meshes = new List<Mesh>();
        public List<Bone> Bones = new List<Bone>();
        public Bone RootBone;

        public List<IndexBuffer> IndexBuffers = new List<IndexBuffer>();
        public List<VertexBuffer> VertexBuffers = new List<VertexBuffer>();

        bool IsReady = false;
        void MakeReady()
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

