
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
using ChamberLib.Content;
using ChamberLib.OpenTK.Content;

namespace ChamberLib.OpenTK.Materials
{
    public class VertexMaterial : IVertexMaterial
    {
        public VertexMaterial(VertexMaterialContent material, ContentResolver resolver, IContentProcessor processor)
        {
            this.Name = material.Name;

            if (material.VertexShader != null)
            {
                var vertex =
                    processor.ProcessShaderStage(material.VertexShader, processor);
                vertex.SetBindAttributes(
                    new[] {
                        "in_position",
                        "in_normal",
                        "in_texture_coords",
                        "in_blend_indices",
                        "in_blend_weights",
                    });
                this.VertexShader = vertex;
            }
        }

        public string Name { get; set; }

        readonly Matrix __Apply_defaultProjection =
            Matrix.CreateOrthographic(2, 2, 0, 1);
        public void Apply(GameTime gameTime, Matrix world,
            ComponentCollection components, IShaderStage vertexShader,
            Overrides overrides=default(Overrides))
        {
            if (vertexShader == null) throw new InvalidOperationException("No vertex shader specified");

            var camera = components?.Get<ICamera>();
            var view = camera?.View ?? Matrix.Identity;
            var projection = camera?.Projection ?? __Apply_defaultProjection;

            vertexShader.SetUniform("worldViewProj", world * view * projection);
            vertexShader.SetUniform("worldView", world * view);
            vertexShader.SetUniform("viewProj", view * projection);
            vertexShader.SetUniform("view", view);
            vertexShader.SetUniform("world", world);
        }

        public void UnApply()
        {
        }

        public IShaderStage VertexShader { get; set; }
    }
}
