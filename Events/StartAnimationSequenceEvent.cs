
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
