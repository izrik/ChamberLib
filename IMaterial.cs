﻿using System;

namespace ChamberLib
{
    public interface IMaterial
    {
        string Name { get; }

        void Apply(GameTime gameTime, Matrix world,
                    ComponentCollection components,
                    Overrides overrides=default(Overrides));
        void UnApply();

        Vector3 Diffuse { get; set; }
        Vector3 EmissiveColor { get; set; }
        Vector3 SpecularColor { get; set; }
        float SpecularPower { get; set; }
        float Alpha { get; set; }
        ITexture2D Texture { get; set; }

        IShaderProgram Shader { get; set; }
    }
}

