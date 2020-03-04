using System;
using ChamberLib.Content;

namespace ChamberLib.OpenTK
{
    public class VertexMaterial : Material
    {
        public VertexMaterial(VertexMaterialContent material, ContentResolver resolver, IContentProcessor processor)
            : base(material, resolver, processor)
        {
        }
    }
}
