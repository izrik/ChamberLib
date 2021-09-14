using System;
using ChamberLib;

namespace ChamberLib
{
    public class SetRotationEvent : TimeEvent
    {
        public SetRotationEvent(Entity entity, float time, Quaternion rotation)
        {
            Entity = entity;
            StartTime = time;
            EndTime = time;
            Rotation = rotation;
        }

        public readonly Entity Entity;
        public readonly Quaternion Rotation;

        protected override void UpdateTimeEvent(float time)
        {
            Entity.SetRotation(Rotation);
        }
    }
}
