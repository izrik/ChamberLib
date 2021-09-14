using System;

namespace ChamberLib
{
    public interface ICamera
    {
        Matrix View { get; }

        Matrix Projection { get; }
    }
}
