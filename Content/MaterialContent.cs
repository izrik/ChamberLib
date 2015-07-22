﻿using System;

namespace ChamberLib.Content
{
    public class MaterialContent
    {
        public string Name = "";
        public Vector3 DiffuseColor;
        public Vector3 EmissiveColor;
        public Vector3 SpecularColor;
        public float SpecularPower;
        public float Alpha = 1;

        public TextureContent Texture;

        public ShaderContent VertexShader;
        public ShaderContent FragmentShader;
    }
}

