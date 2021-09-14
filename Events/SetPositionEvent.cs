using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChamberLib;

namespace ChamberLib
{
    class SetPositionEvent : TimeEvent
    {
        public SetPositionEvent(float time, Entity entity, Vector3 position)
        {
            StartTime = time;
            EndTime = time;
            Entity = entity;
            Position = position;
        }

        public readonly Entity Entity;
        public readonly Vector3 Position;

        protected override void UpdateTimeEvent(float time)
        {
            Entity.SetPosition(Position);
        }
    }
}
