using System;
using System.Collections.Generic;
using System.Linq;

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

        public void SetAlpha(float alpha)
        {
            var materials = new HashSet<Material>(Meshes.SelectMany(m => m.Parts).Select(p => p.Material));
            foreach (var material in materials)
            {
                material.Alpha = alpha;
            }
        }

        public void SetTexture(ITexture2D texture)
        {
            var materials = new HashSet<Material>(Meshes.SelectMany(m => m.Parts).Select(p => p.Material));
            foreach (var material in materials)
            {
                material.Texture = texture;
            }
        }

        public void SetWorldViewProjection(Matrix transform, Matrix view, Matrix projection)
        {
        }

        public void SetBoneTransforms(Matrix[] boneTransforms, float verticalOffset, Matrix world)
        {
        }

        public object Tag { get; set; }

        public IBone Root
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        public List<Mesh> Meshes = new List<Mesh>();
        public List<Bone> Bones = new List<Bone>();
        public Bone RootBone;

        public List<short[]> _indexBufferContents = new List<short[]>();
        public List<IVertex[]> _vertexBufferContents = new List<IVertex[]>();
        bool IsReady = false;
        void MakeReady()
        {
            if (IsReady) return;

            var vertexBuffers = _vertexBufferContents.Select(array => VertexBuffer.FromArray(array)).ToArray();
            var indexBuffers = _indexBufferContents.Select(array => IndexBuffer.FromArray(array)).ToArray();

            foreach (var mesh in Meshes)
            {
                mesh.MakeReady(vertexBuffers, indexBuffers);
            }

            IsReady = true;
        }
    }
}

