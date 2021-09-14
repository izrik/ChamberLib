using System;
using ChamberLib;

namespace ChamberLib
{
    public class CopyPositionEvent : TimeEvent
    {
        public CopyPositionEvent(float time, Entity from, Entity to)
            : this(time, time, from, to) { }
        public CopyPositionEvent(float startTime, float endTime, Entity from,
            Entity to)
        {
            StartTime = startTime;
            EndTime = endTime;
            From = from;
            To = to;
        }

        public readonly Entity From;
        public readonly Entity To;

        protected override void UpdateTimeEvent(float time)
        {
            To.SetPosition(From.Position);
        }
    }
}
