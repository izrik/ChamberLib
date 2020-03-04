using System;
using ChamberLib.Content;

namespace ChamberLib.OpenTK
{
    public class VertexMaterial : Material, IVertexMaterial
    {
        public VertexMaterial(VertexMaterialContent material, ContentResolver resolver, IContentProcessor processor)
            : base(material, resolver, processor)
        {
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

        void IVertexMaterial.Apply(GameTime gameTime, Matrix world,
                                    ComponentCollection components,
                                    IShaderStage vertexShader,
                                    Overrides overrides=default(Overrides))
        {
            ApplyVertexShader(vertexShader, gameTime, world, components,
                overrides);
        }
    }
}
