using System;
using OpenTK.Graphics.OpenGL;
using ChamberLib.Content;

namespace ChamberLib.OpenTK
{
    public abstract class Material
    {
        protected Material(MaterialContent material, ContentResolver resolver, IContentProcessor processor)
        {
        }
    }
}

