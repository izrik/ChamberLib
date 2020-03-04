using System;
namespace ChamberLib
{
    public interface IVertexMaterial : IMaterial
    {
        void Apply(GameTime gameTime,
                    Matrix world,
                    ComponentCollection components,
                    IShaderStage vertexShader,
                    Overrides overrides=default(Overrides));
    }
}
