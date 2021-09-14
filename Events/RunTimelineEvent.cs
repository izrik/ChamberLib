using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChamberLib
{
    public class RunTimelineEvent : TimeEvent
    {
        public RunTimelineEvent(float startTime, Timeline timeline)
        {
            StartTime = startTime;
            Timeline = timeline;
        }

        public Timeline Timeline { get; protected set; }
        public override float EndTime
        {
            get { return Timeline.EndTime + StartTime; }
            protected set { }
        }

        protected override void UpdateTimeEvent(float time)
        {
            Timeline.Update(time - StartTime);
        }
    }
}
