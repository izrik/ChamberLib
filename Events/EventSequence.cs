using System;
using System.Collections.Generic;

namespace ChamberLib
{
    public class EventSequence : IEvents
    {
        public readonly List<Event> Events = new List<Event>();
        int Index;

        public void Reset()
        {
            Index = 0;
        }

        public void AddEvent(Event e)
        {
            Events.Add(e);
        }

        public bool HasCompleted => Index >= Events.Count;

        float? _eventStartTime = 0;
        public void Update(float time)
        {
            if (HasCompleted) return;
            if (!_eventStartTime.HasValue)
                _eventStartTime = time;
            Events[Index].Update(time - _eventStartTime.Value);
            if (Events[Index].HasCompleted)
            {
                Index++;
                _eventStartTime = null;
            }
        }
    }
}
