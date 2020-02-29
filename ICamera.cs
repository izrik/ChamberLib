using System;

namespace ChamberLib
{
    public interface ICamera
    {
        Matrix View { get; }

        Matrix Projection { get; }
        Viewport Viewport { get; }

        Vector3 Project(Vector3 source, Matrix world);
        Vector3 Unproject(Vector3 source, Matrix world);
    }
}
