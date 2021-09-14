using System;
using System.Collections.Generic;

namespace ChamberLib
{
    /// <summary>
    /// Keeps track of a number of events that happen at specific times.
    /// Multiple events can occur at the same time.
    /// </summary>
    public class Timeline : IEvents
    {
        public Timeline()
        {
            Reset();
        }

        public void Reset()
        {
            _lastTime = -1;
        }

        public void AddEvent(TimeEvent e)
        {
            if (e == null) throw new ArgumentNullException("e");
            if (e.StartTime > e.EndTime) throw new ArgumentOutOfRangeException("e", "Duration must be non-negative");
            if (e.StartTime < 0) throw new ArgumentOutOfRangeException("e", "StartTime must be non-negative");

            _events.Add(e);

            if (e.EndTime > EndTime)
            {
                EndTime = e.EndTime;
            }
        }

        public void AddRange(IEnumerable<TimeEvent> events)
        {
            foreach (var e in events)
            {
                AddEvent(e);
            }
        }

        HashSet<TimeEvent> _events = new HashSet<TimeEvent>();
        public HashSet<TimeEvent> Events
        {
            get { return _events; }
        }
        public float _lastTime = -1;

        public TimeEvent EarliestEvent
        {
            get
            {
                // TODO: what if more than one start at the same time?

                float? earliestTime = null;
                TimeEvent earliest = null;
                foreach (var e in Events)
                {
                    if (!earliestTime.HasValue ||
                        e.StartTime < earliestTime.Value)
                    {
                        earliestTime = e.StartTime;
                        earliest = e;
                    }
                }

                return earliest;
            }
        }

        public TimeEvent LatestEvent
        {
            get
            {
                // TODO: what if more than one end at the same time?

                float? latestTime = null;
                TimeEvent latest = null;
                foreach (var e in Events)
                {
                    if (!latestTime.HasValue ||
                        e.EndTime < latestTime.Value)
                    {
                        latestTime = e.EndTime;
                        latest = e;
                    }
                }

                return latest;
            }
        }

        public float EndTime { get; protected set; }

        public void Update(float time)
        {
            foreach (var e in _events)
            {
                if (e.StartTime <= time &&
                    e.EndTime > _lastTime)
                {
                    e.Update(Math.Min(time, e.EndTime));
                }
            }

            _lastTime = time;
        }

        public bool HasCompleted => _lastTime >= EndTime;
    }
}
