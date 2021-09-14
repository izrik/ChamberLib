
namespace ChamberLib
{
    public abstract class Event
    {
        public abstract void Update(float time);
        public abstract bool HasCompleted { get; }
    }
}
