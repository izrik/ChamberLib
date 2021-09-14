using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChamberLib;

namespace ChamberLib
{
    public class AttachEntityEvent : TimeEvent
    {
        public AttachEntityEvent(float time, RiggedEntity entity,
            Entity attachable, string attachedToBoneName,
            bool retainGlobalPosRotScale=true)
        {
            Time = time;
            Entity = entity;
            Attachable = attachable;
            AttachedToBoneName = attachedToBoneName;
            RetainGlobalPosRotScale = retainGlobalPosRotScale;

            StartTime = time;
            EndTime = time;
        }

        public readonly float Time;
        public readonly RiggedEntity Entity;
        public readonly Entity Attachable;
        public readonly string AttachedToBoneName;
        public readonly bool RetainGlobalPosRotScale;

        protected override void UpdateTimeEvent(float time)
        {
            Attachable.Attach(Entity, AttachedToBoneName,
                RetainGlobalPosRotScale);
        }
    }
}
