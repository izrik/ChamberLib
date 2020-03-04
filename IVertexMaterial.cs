using System;
namespace ChamberLib
{
    public interface IVertexMaterial : IMaterial
    {
        string Name { get; }

        void Apply(GameTime gameTime,
                    Matrix world,
                    ComponentCollection components,
                    IShaderStage vertexShader,
                    Overrides overrides=default(Overrides));
        void UnApply();

        IShaderStage VertexShader { get; }
    }
}
