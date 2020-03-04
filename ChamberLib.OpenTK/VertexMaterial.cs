using System;
using ChamberLib.Content;

namespace ChamberLib.OpenTK
{
    public class VertexMaterial : Material, IVertexMaterial
    {
        public VertexMaterial(VertexMaterialContent material, ContentResolver resolver, IContentProcessor processor)
            : base(material, resolver, processor)
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
        void IVertexMaterial.Apply(GameTime gameTime, Matrix world,
                                    ComponentCollection components,
                                    IShaderStage vertexShader,
                                    Overrides overrides=default(Overrides))
        {
            ApplyVertexShader(vertexShader, gameTime, world, components,
                overrides);
        }

        public void ApplyVertexShader(IShaderStage vertexShader,
                                        GameTime gameTime, Matrix world,
                                        ComponentCollection components,
                                        Overrides overrides = default(Overrides))
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
