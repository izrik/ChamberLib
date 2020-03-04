using System;
namespace ChamberLib
{
    public interface IFragmentMaterial : IMaterial
    {
        void Apply(GameTime gameTime,
                    Matrix world,
                    ComponentCollection components,
                    IShaderStage fragmentShader,
                    Overrides overrides=default(Overrides));
    }
}
