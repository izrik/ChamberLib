using System;
using OpenTK.Graphics.OpenGL;

namespace ChamberLib.OpenTK
{
    public struct VertexAttribute
    {
        public VertexAttribute(
            int numComponents,
            VertexAttribPointerType type,
            int? offsetInBytes=null,
            int? attributeIndex=null)
        {
            NumComponents=numComponents;
            VertexAttribPointerType = type;
            OffsetInBytes = offsetInBytes;
            AttributeIndex = attributeIndex;
        }

        public readonly int NumComponents;
        public readonly VertexAttribPointerType VertexAttribPointerType;
        public readonly int? OffsetInBytes;
        public readonly int? AttributeIndex;
    }
}

