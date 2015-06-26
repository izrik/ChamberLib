using System;

namespace ChamberLib
{
    public interface IMesh
    {
        Sphere BoundingSphere { get; set; }
        IBone ParentBone { get; set; }
        string Name { get; set; }

        void Draw(Matrix world, Matrix view, Matrix projection,
                    LightingData lighting, IMaterial materialOverride=null);
    }
}

