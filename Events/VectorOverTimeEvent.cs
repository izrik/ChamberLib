using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChamberLib;

namespace ChamberLib
{
    public class VectorOverTimeEvent : TimeEvent
    {
        public VectorOverTimeEvent(VectorOverTime vot, Entity entity)
        {
            if (entity == null) throw new ArgumentNullException("position");

            VoT = vot;
            Entity = entity;

            StartTime = vot.StartTime;
            EndTime = vot.EndTime;
        }

        public VectorOverTime VoT { get; protected set; }
        public Entity Entity { get; protected set; }

        protected override void UpdateTimeEvent(float time)
        {
            Entity.SetPosition(VoT.Calculate(time));
        }
    }
}
