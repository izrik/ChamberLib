using System;
using ChamberLib;

namespace ChamberLib.Events
{
    public class PlayAnimationSequenceEvent : TimeEvent
    {
        public PlayAnimationSequenceEvent(RiggedEntity entity, AnimationSequence sequence, float startTime)
        {
            if (sequence == null) throw new ArgumentNullException("sequence");
            if (startTime < 0) throw new ArgumentOutOfRangeException("startTime", "startTime must be greater than zero");

            Entity = entity;
            StartTime = startTime;
            EndTime = startTime + sequence.Duration;
            AnimationSequence = sequence;
        }

        public RiggedEntity Entity { get; protected set; }
        public AnimationSequence AnimationSequence { get; protected set; }

        public AnimationFrame _lastFrame = null;
        public bool _started = false;


        protected override void UpdateTimeEvent(float time)
        {
            if (!_started)
            {
                Entity.AnimPlayer.StopAnimation();
                
                _started = true;
            }

            AnimationFrame frame = AnimationPlayer.SelectAnimationFrame(AnimationSequence, time - StartTime);

            if (frame != _lastFrame)
            {
                _lastFrame = frame;

                Entity.SetAnimationFrame(frame);
            }
            
            if (time >= EndTime)
            {
                Entity.AnimPlayer.OnAnimationCompleted(EventArgs.Empty);
            }
        }
    }
}

