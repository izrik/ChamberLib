using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChamberLib
{
    public class RunActionEvent : TimeEvent
    {
        public RunActionEvent(float startTime, float endTime,
            Action<float> action)
        {
            StartTime = startTime;
            EndTime = endTime;

            if (action == null) throw new ArgumentNullException("action");

            Action = action;
        }
        public RunActionEvent(float time,
            Action<float> action)
            : this(time, time, action)
        {
        }

        public Action<float> Action { get; protected set; }

        protected override void UpdateTimeEvent(float time)
        {
            Action(time);
        }
    }
}
