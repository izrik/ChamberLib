using System;
using OpenGL = global::OpenTK.Graphics.OpenGL;

namespace ChamberLib.OpenTK
{
    public static class ShaderTypeHelper
    {
        public static ChamberLib.ShaderType ToChamber(this OpenGL.ShaderType shtype)
        {
            switch (shtype)
            {
            case OpenGL.ShaderType.VertexShader: return ShaderType.Vertex;
            case OpenGL.ShaderType.FragmentShader: return ShaderType.Fragment;
            }

            throw new ArgumentOutOfRangeException("shtype",
                "Unknown shader type: " + shtype.ToString());
        }

        public static OpenGL.ShaderType ToOpenTK(this ChamberLib.ShaderType shtype)
        {
            switch (shtype)
            {
            case ShaderType.Vertex: return OpenGL.ShaderType.VertexShader;
            case ShaderType.Fragment: return OpenGL.ShaderType.FragmentShader;
            }

            throw new ArgumentOutOfRangeException("shtype",
                "Unknown shader type: " + shtype.ToString());
        }
    }
}

