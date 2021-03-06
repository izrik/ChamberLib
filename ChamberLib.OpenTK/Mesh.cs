﻿using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using ChamberLib.Content;
using System.Linq;

namespace ChamberLib.OpenTK
{
    public class Mesh : IMesh
    {
        public Mesh(Model parentModel, MeshContent mesh, ContentResolver resolver)
        {
            if (parentModel == null) throw new ArgumentNullException("parentModel");
            ParentModel = parentModel;

            foreach (var part in mesh.Parts)
            {
                this.Parts.Add(new Part(part, resolver));
            }
            ParentBone = resolver.Bones[mesh.ParentBone];
            Name = mesh.Name;
        }

        public readonly Model ParentModel;

        public List<Part> Parts = new List<Part>();

        public void Draw(GameTime gameTime, Matrix world, Matrix view, Matrix projection,
                            LightingData lighting,
                            Overrides overrides=default(Overrides))
        {
            if (!ParentModel.IsReady) ParentModel.MakeReady();

            foreach (var part in Parts)
            {
                part.Draw(gameTime, world, view, projection, lighting, overrides);
            }
        }

        Sphere _boundingSphere;
        bool _mustCalcBoundingSphere = true;
        public Sphere BoundingSphere
        {
            get
            {
                if (_mustCalcBoundingSphere)
                {
                    // Note that this is a naive algorithm. It calculates _a_
                    // bounding sphere, but not necessarily the _smallest_
                    // bounding sphere.

                    var points = new List<Vector3>();
                    foreach (var part in Parts)
                    {
                        int i;
                        var n = part.PrimitiveCount * 3;
                        for (i = 0; i < n; i++)
                        {
                            var ii = part.Indexes.IndexData[i + part.StartIndex];
                            points.Add(part.Vertexes.VertexData[ii].GetPosition());
                        }
                    }

                    var center = points.Aggregate((a, b) => a + b) / points.Count;
                    var radius = points.Max(v => (v - center).Length());
                    _boundingSphere = new Sphere(center, radius);

                    _mustCalcBoundingSphere = false;
                }

                return _boundingSphere;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IBone ParentBone { get; set; }

        public string Name { get; set; }

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

        public void Draw(GameTime gameTime, Matrix world, Matrix view, Matrix projection,
                            LightingData lighting,
                            Overrides overrides=default(Overrides))
        {
            IMaterial material = overrides.GetMaterial(Material);
            var lighting2 = overrides.GetLighting(lighting);

            material.Apply(gameTime, world, view, projection, lighting, overrides);
            RenderBundle.Apply();

            RenderBundle.Draw(PrimitiveType.Triangles, PrimitiveCount * 3, StartIndex, VertexOffset);

            RenderBundle.UnApply();
            material.UnApply();
        }

        public void DrawWireframe(GameTime gameTime, Matrix world, Matrix view,
            Matrix projection, LightingData lighting,
            Overrides overrides=default(Overrides))
        {
            var material = overrides.GetMaterial(Material);

            material.Apply(gameTime, world, view, projection, lighting);
            RenderBundle.Apply();

            RenderBundle.Draw(PrimitiveType.Lines, PrimitiveCount*2, StartIndex, VertexOffset);

            RenderBundle.UnApply();
            material.UnApply();
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

