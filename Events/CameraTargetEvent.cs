using System;
using ChamberLib;

namespace ChamberLib
{
    public class CameraTargetEvent : TimeEvent
    {
        public CameraTargetEvent(float time, CameraAboutTarget camera,
            Vector3 target)
        {
            StartTime = time;
            EndTime = time;
            Camera = camera;
            Target = target;
        }

        public readonly CameraAboutTarget Camera;
        public readonly Vector3 Target;

        protected override void UpdateTimeEvent(float time)
        {
            Camera.Target = Target;
        }
    }
}
