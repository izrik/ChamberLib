using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using ChamberLib.Content;

namespace ChamberLib.OpenTK
{
    public class Mesh : IMesh
    {
        public Mesh(MeshContent mesh, ContentResolver resolver)
        {
            foreach (var part in mesh.Parts)
            {
                this.Parts.Add(new Part(part, resolver));
            }
        }

        public List<Part> Parts = new List<Part>();

        public void Draw(IRenderer renderer, Matrix world, Matrix view,
                            Matrix projection, LightingData lighting,
                            IMaterial materialOverride=null)
        {
            foreach (var part in Parts)
            {
                part.Draw((Renderer)renderer, world, view, projection,
                            lighting, materialOverride);
            }
        }

        #region IMesh implementation

        public Sphere BoundingSphere
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

        public IBone ParentBone { get; set; }

        #endregion

        public void MakeReady()
        {
            foreach (var part in Parts)
            {
                part.MakeReady();
            }
        }
    }

    public class Part
    {
        public Part(PartContent part, ContentResolver resolver)
        {
            Indexes = resolver.Get(part.Indexes);
            Vertexes = resolver.Get(part.Vertexes);
            Material = resolver.Get(part.Material);
            this.StartIndex = part.StartIndex;
            this.PrimitiveCount = part.PrimitiveCount;
            this.VertexOffset = part.VertexOffset;
            this.NumVertexes = part.NumVertexes;
        }

        public IndexBuffer Indexes;
        public VertexBuffer Vertexes;
        public int StartIndex;
        public int PrimitiveCount;
        public int VertexOffset;
        public int NumVertexes;
        public Material Material;

        public RenderBundle RenderBundle;

        public void Draw(Renderer renderer, Matrix world, Matrix view,
                            Matrix projection, LightingData lighting,
                            IMaterial materialOverride=null)
        {
            Material.Apply(renderer, lighting, world, view, projection);
            RenderBundle.Apply();

            RenderBundle.Draw(PrimitiveType.Triangles, PrimitiveCount*3, StartIndex, VertexOffset);

            RenderBundle.UnApply();
            Material.UnApply();
        }

        public void MakeReady()
        {
            if (RenderBundle == null)
            {
                RenderBundle = new RenderBundle(Vertexes, Indexes);
            }
        }
    }
}

