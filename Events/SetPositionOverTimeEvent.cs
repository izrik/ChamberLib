
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
