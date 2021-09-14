using System;
namespace ChamberLib
{
    public interface IEvents
    {
        void Update(float time);
        bool HasCompleted { get; }
    }
}
