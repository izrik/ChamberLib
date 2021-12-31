using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChamberLib;

namespace ChamberLib.Events
{
    public class StartAnimationSequenceEvent : TimeEvent
    {
        public StartAnimationSequenceEvent(float time, RiggedEntity entity, string sequenceName, RepeatMode repeatMode)
        {
            Entity = entity;
            SequenceName = sequenceName;
            RepeatMode = repeatMode;

            StartTime = time;
            EndTime = time;
        }

        public RiggedEntity Entity { get; protected set; }
        public string SequenceName { get; protected set; }
        public RepeatMode RepeatMode { get; protected set; }

        public float GetAnimationEndTime()
        {
            return Entity.GetAnimationSequenceByName(SequenceName).Duration + StartTime;
        }

        protected override void UpdateTimeEvent(float time)
        {
            Entity.StartAnimationSequence(SequenceName, RepeatMode);
        }
    }
}
