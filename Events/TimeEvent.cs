using System;
namespace ChamberLib
{
    public abstract class TimeEvent : Event
    {
        public virtual float StartTime { get; protected set; }
        public virtual float EndTime { get; protected set; }
        public virtual float Duration
        {
            get { return EndTime - StartTime; }
            protected set { }
        }

        protected float LastTime = 0;
        public sealed override void Update(float time)
        {
            LastTime = time;
            UpdateTimeEvent(time);
        }

        protected abstract void UpdateTimeEvent(float time);

        public override bool HasCompleted => LastTime > EndTime;
    }
}
