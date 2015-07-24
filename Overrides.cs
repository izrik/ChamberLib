using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChamberLib
{
    public class Overrides
    {
        public LightingData? Lighting;
        public IMaterial Material;
        public IShaderProgram ShaderProgram;
        public IShaderStage VertexShader;
        public IShaderStage FragmentShader;
    }
}
