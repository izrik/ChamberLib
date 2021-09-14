using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChamberLib;

namespace ChamberLib
{
    public class DetachEntityEvent : TimeEvent
    {
        public DetachEntityEvent(float time, Entity entity,
            bool retainGlobalPosRotScale=true)
        {
            Time = time;
            Entity = entity;
            RetainGlobalPosRotScale = retainGlobalPosRotScale;

            StartTime = time;
            EndTime = time;
        }

        public readonly float Time;
        public readonly Entity Entity;
        public readonly bool RetainGlobalPosRotScale;

        protected override void UpdateTimeEvent(float time)
        {
            Entity.Detach(retainGlobalPosRotScale: RetainGlobalPosRotScale);
        }
    }
}
