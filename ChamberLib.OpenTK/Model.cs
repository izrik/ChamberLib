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

            public void Draw(Renderer renderer, Matrix world, Matrix view, Matrix projection, LightingData lighting)
            {
                foreach (var part in Parts)
                {
                    part.Draw(renderer, world, view, projection, lighting);
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

            public void Draw(Renderer renderer, Matrix world, Matrix view, Matrix projection, LightingData lighting)
            {
                Material.Apply(renderer, lighting, world, view, projection);

                renderer.DrawTriangles(Vertexes, Indexes, StartIndex, PrimitiveCount, VertexOffset);

                Material.UnApply();
            }
        }

        public class Material
        {
            public Vector3 DiffuseColor;
            public TextureAdapter Texture;

            public void Apply(Renderer renderer, LightingData lighting, Matrix world, Matrix view, Matrix projection)
            {
                renderer.SetMatrices(Matrix.Identity, view, projection);

                GL.LightModel(LightModelParameter.LightModelAmbient,
                    new float[] {
                        lighting.AmbientLightColor.X,
                        lighting.AmbientLightColor.Y,
                        lighting.AmbientLightColor.Z,
                        1
                    });
                GL.LightModel(LightModelParameter.LightModelLocalViewer, 0);
                GL.Enable(EnableCap.Lighting);
                GL.Enable(EnableCap.Light0);
                GL.Disable(EnableCap.Light1);
                GL.Disable(EnableCap.Light2);
                GL.Disable(EnableCap.Light3);
                GL.Disable(EnableCap.Light4);
                GL.Disable(EnableCap.Light5);
                GL.Disable(EnableCap.Light6);
                GL.Disable(EnableCap.Light7);
                GL.Disable(EnableCap.FragmentLightingSgix);
                GL.Light(LightName.Light0, LightParameter.Ambient, Vector4.Zero.ToOpenTK());
                GL.Light(LightName.Light0, LightParameter.Diffuse, lighting.DirectionalLight.DiffuseColor.ToVector4().ToOpenTK());
                GL.Light(LightName.Light0, LightParameter.Specular, Vector4.Zero.ToOpenTK());
                var position = -lighting.DirectionalLight.Direction;
                GL.Light(LightName.Light0, LightParameter.Position, position.ToVector4(0).ToOpenTK());

                GL.Material(MaterialFace.Front, MaterialParameter.Ambient, Vector4.One.ToOpenTK());
                var diffuse = 
                    /*/
                        Vector4.Zero;
                    /*/
                        new Vector4(DiffuseColor.X, DiffuseColor.Y, DiffuseColor.Z, 1);
                    /**/
                GL.Material(MaterialFace.Front, MaterialParameter.Diffuse, diffuse.ToOpenTK());
                GL.Material(MaterialFace.Front, MaterialParameter.Specular, Vector4.Zero.ToOpenTK());
                GL.Material(MaterialFace.Front, MaterialParameter.Shininess, 0);
                GL.Material(MaterialFace.Front, MaterialParameter.Emission, lighting.EmissiveColor.ToOpenTK());

                if (Texture != null)
                {
                    Texture.Bind();
                }

                renderer.SetMatrices(world, view, projection);

            }

            public void UnApply()
            {
                if (Texture != null)
                {
                    TextureAdapter.Unbind();
                }
            }
        }
    }
}

