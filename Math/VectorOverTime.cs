
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

namespace ChamberLib
{
    public struct VectorOverTime : IPositionAtTime
    {
        public VectorOverTime(Vector3 start, float startTime, Vector3 end, float endTime)
        {
            if (startTime >= endTime) throw new ArgumentOutOfRangeException("endTime must be greater than startTime");

            _start = start;
            _startTime = startTime;
            _end = end;
            _endTime = endTime;

            _deltaTime = _endTime - _startTime;
        }

        Vector3 _start;
        float _startTime;
        Vector3 _end;
        float _endTime;

        float _deltaTime;

        public Vector3 Start { get { return _start; } }
        public float StartTime { get { return _startTime; } }
        public Vector3 End { get { return _end; } }
        public float EndTime { get { return _endTime; } }

        public float Duration { get { return _deltaTime; } }

        public Vector3 Calculate(float time)
        {
            return
                Vector3.Lerp(
                    Start,
                    End,
                    (time - StartTime) / _deltaTime);
        }

        public Vector3[] Iterate(float startTime, float endTime, float step)
        {
            float t;
            //TODO: re-use instead of allocating
            List<Vector3> values = new List<Vector3>();
            for (t = startTime; t <= endTime; t += step)
            {
                values.Add(Calculate(t));
            }

            return values.ToArray();
        }

        public Vector3 PositionAtTime(float time)
        {
            return Calculate(time);
        }
    }
}
