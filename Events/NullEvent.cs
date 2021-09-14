using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChamberLib
{
    public class NullEvent : TimeEvent
    {
        public NullEvent(float time, string label=null)
        {
            StartTime = time;
            EndTime = time;
            Label = label ?? "";
        }

        public readonly string Label;

        protected override void UpdateTimeEvent(float time) { }
    }
}
