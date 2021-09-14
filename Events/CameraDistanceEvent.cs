using System;
namespace ChamberLib
{
    public class CameraDistanceEvent : TimeEvent
    {
        public CameraDistanceEvent(float time, CameraAboutTarget camera,
            float distance)
        {
            StartTime = time;
            EndTime = time;
            Camera = camera;
            Distance = distance;
        }

        public readonly CameraAboutTarget Camera;
        public readonly float Distance;

        protected override void UpdateTimeEvent(float time)
        {
            Camera.Distance = Distance;
        }
    }
}
