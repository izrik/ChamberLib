
//
// ChamberLib, a cross-platform game engine
// Copyright (C) 2021 izrik and Metaphysics Industries, Inc.
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301
// USA
//

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

