
//
// ChamberLib, a cross-platform game engine
// Copyright (C) 2021 izrik and Metaphysics Industries, Inc.
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301
// USA
//

using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using ChamberLib.Content;
using System.Linq;
using ChamberLib.OpenTK.Content;
using ChamberLib.OpenTK.Materials;
using ChamberLib.OpenTK.System;

namespace ChamberLib.OpenTK.Models
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
            Name = mesh.Name;
        }

        public readonly Model ParentModel;

        public List<Part> Parts = new List<Part>();

        public void Draw(GameTime gameTime, Matrix world,
            ComponentCollection components,
            Overrides overrides=default(Overrides))
        {
            if (!ParentModel.IsReady) ParentModel.MakeReady();

            foreach (var part in Parts)
            {
                part.Draw(gameTime, world, components, overrides);
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
                            var ii = part.Indexes.GetIndex(i + part.StartIndex);
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
            FragmentMaterial = resolver.Get(part.FragmentMaterial);
            VertexMaterial = resolver.Get(part.VertexMaterial);
            this.StartIndex = part.StartIndex;
            this.PrimitiveCount = part.PrimitiveCount;
            this.VertexOffset = part.VertexOffset;
        }

        public IndexBuffer Indexes;
        public VertexBuffer Vertexes;
        public int StartIndex;
        public int PrimitiveCount;
        public int VertexOffset;
        public VertexMaterial VertexMaterial;
        public FragmentMaterial FragmentMaterial;

        public RenderBundle RenderBundle;

        public void Draw(GameTime gameTime, Matrix world,
            ComponentCollection components,
            Overrides overrides=default(Overrides))
        {
            var vmaterial = overrides.GetVertexMaterial(VertexMaterial);
            var fmaterial = overrides.GetFragmentMaterial(FragmentMaterial);
            var shader = ShaderProgram.GetShaderProgram(
                (ShaderStage)vmaterial.VertexShader,
                (ShaderStage)fmaterial.FragmentShader);

            vmaterial.Apply(gameTime, world, components, shader.VertexShader,
                overrides);
            fmaterial.Apply(gameTime, world, components, shader.FragmentShader,
                overrides);
            shader.Apply(overrides);
            RenderBundle.Apply();

            RenderBundle.Draw(PrimitiveType.Triangles, PrimitiveCount * 3, StartIndex, VertexOffset);

            RenderBundle.UnApply();
            shader.UnApply();
            fmaterial.UnApply();
            vmaterial.UnApply();
        }

        public void DrawWireframe(GameTime gameTime, Matrix world,
            ComponentCollection components,
            Overrides overrides=default(Overrides))
        {
            var vmaterial = overrides.GetVertexMaterial(VertexMaterial);
            var fmaterial = overrides.GetFragmentMaterial(FragmentMaterial);
            var shader = ShaderProgram.GetShaderProgram(
                (ShaderStage)vmaterial.VertexShader,
                (ShaderStage)fmaterial.FragmentShader);

            vmaterial.Apply(gameTime, world, components, shader.VertexShader,
                overrides);
            fmaterial.Apply(gameTime, world, components, shader.FragmentShader,
                overrides);
            shader.Apply(overrides);
            RenderBundle.Apply();

            RenderBundle.Draw(PrimitiveType.Lines, PrimitiveCount * 2, StartIndex, VertexOffset);

            RenderBundle.UnApply();
            shader.UnApply();
            fmaterial.UnApply();
            vmaterial.UnApply();
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

