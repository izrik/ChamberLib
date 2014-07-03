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
                mesh.Draw(Renderer, world, view, projection);
            }
        }

        public void EnableDefaultLighting()
        {
        }

        public void SetAmbientLightColor(Vector3 value)
        {
        }

        public void SetEmissiveColor(Vector3 value)
        {
        }

        public void SetDirectionalLight(DirectionalLight light, int index = 0)
        {
        }

        public void DisableDirectionalLight(int index)
        {
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

        public class Bone
        {
            public string Name;
            public int Index;
            public Matrix Transform;

            public Bone Parent;
            public List<Bone> Children = new List<Bone>();
        }

        public class Mesh
        {
            public List<Part> Parts = new List<Part>();

            public void Draw(Renderer renderer, Matrix world, Matrix view, Matrix projection)
            {
                foreach (var part in Parts)
                {
                    part.Draw(renderer, world, view, projection);
                }
            }
        }

        public class Part
        {
            public short[] Indexes;
            public IVertex[] Vertexes;
            public int StartIndex;
            public int PrimitiveCount;
            public int VertexOffset;
            public int NumVertexes;
            public Material Material;

            public void Draw(Renderer renderer, Matrix world, Matrix view, Matrix projection)
            {
                renderer.SetMatrices(world, view, projection);

                Material.Apply();

                renderer.DrawTriangles(Vertexes, Indexes, StartIndex, PrimitiveCount);

                Material.UnApply();
            }
        }

        public class Material
        {
            public Vector3 DiffuseColor;

            public void Apply()
            {
                GL.Color3(DiffuseColor.X, DiffuseColor.Y, DiffuseColor.Z);
            }

            public void UnApply()
            {
            }
        }
    }
}

