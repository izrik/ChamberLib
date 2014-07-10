using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;

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
            Lighting.AmbientLightColor = new Vector4(value.X, value.Y, value.Z, 1);
        }

        public void SetEmissiveColor(Vector3 value)
        {
            Lighting.EmissiveColor = new Vector4(value.X, value.Y, value.Z, 1);
        }

        public void SetDirectionalLight(DirectionalLight light, int index = 0)
        {
            if (index != 0)
                throw new ArgumentOutOfRangeException("Index");

            Lighting.DirectionalLight = light;
        }

        public void DisableDirectionalLight(int index)
        {
            if (index == 0)
                throw new ArgumentOutOfRangeException("index");
        }

        public void SetAlpha(float alpha)
        {
        }

        public void SetTexture(ITexture2D texture)
        {
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


    }
}

