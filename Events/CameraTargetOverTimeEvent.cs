using System;
using ChamberLib;

namespace ChamberLib
{
    public class CameraTargetOverTimeEvent : TimeEvent
    {
        public CameraTargetOverTimeEvent(float startTime, float endTime,
            CameraAboutTarget camera, Func<float, Vector3> targetFunc)
        {
            StartTime = startTime;
            EndTime = endTime;
            Camera = camera;
            TargetFunc = targetFunc;
        }

        public readonly CameraAboutTarget Camera;
        public readonly Func<float, Vector3> TargetFunc;

        protected override void UpdateTimeEvent(float time)
        {
            Camera.Target = TargetFunc(time);
        }
    }
}
