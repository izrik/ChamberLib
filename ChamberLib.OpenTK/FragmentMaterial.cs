using System;
using ChamberLib.Content;

namespace ChamberLib.OpenTK
{
    public class FragmentMaterial : Material
    {
        public FragmentMaterial(FragmentMaterialContent material, ContentResolver resolver, IContentProcessor processor)
            : base(material, resolver, processor)
        {
        }
    }
}
