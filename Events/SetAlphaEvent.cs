using System;
using ChamberLib;

namespace ChamberLib.Events
{
    public class SetAlphaEvent : TimeEvent
    {
        public SetAlphaEvent(IFragmentMaterial material, float startTime, float startValue, float endTime, float endValue)
        {
            if (material == null) throw new ArgumentNullException("material");

            this.Material = material;
            this.StartTime = startTime;
            this.StartValue = startValue;
            this.EndTime = endTime;
            this.EndValue = endValue;
        }

        public readonly IFragmentMaterial Material;
        public readonly float StartValue;
        public readonly float EndValue;

        protected override void UpdateTimeEvent(float time)
        {
            this.Material.Alpha = Spline.Calculate(StartTime, StartValue, EndTime, EndValue, time);
        }
    }
}
