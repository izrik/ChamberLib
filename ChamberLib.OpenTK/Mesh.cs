using System;
using System.Collections.Generic;

namespace ChamberLib
{
    public class Mesh : IMesh
    {
        public List<Part> Parts = new List<Part>();

        public void Draw(IRenderer renderer, Matrix world, Matrix view, Matrix projection, LightingData lighting)
        {
            foreach (var part in Parts)
            {
                part.Draw((Renderer)renderer, world, view, projection, lighting);
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

        public IBone ParentBone
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
}

