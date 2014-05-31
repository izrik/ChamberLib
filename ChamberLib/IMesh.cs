using System;

namespace ChamberLib
{
    public interface IMesh
    {
        Sphere BoundingSphere { get; set; }
        IBone ParentBone { get; set; }

        void Draw();
    }
}

