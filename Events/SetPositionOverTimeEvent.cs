using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChamberLib;

namespace ChamberLib
{
    public class SetPositionOverTimeEvent : TimeEvent
    {
        public SetPositionOverTimeEvent(float startTime, float endTime,
            Entity entity, IPositionAtTime positionSource)
            : this(startTime, endTime, entity, positionSource.PositionAtTime) 
        {
        }
        public SetPositionOverTimeEvent(float startTime, float endTime,
            Entity entity, Func<float, Vector3> positionSource)
        {
            StartTime = startTime;
            EndTime = endTime;
            Entity = entity;
            PositionSource = positionSource;
        }

        public Entity Entity { get; protected set; }
        public Func<float, Vector3> PositionSource { get; protected set; }

        protected override void UpdateTimeEvent(float time)
        {
            Entity.SetPosition(PositionSource(time));
        }
    }
}
