using System;
namespace ChamberLib
{
    public class CameraFacingThetaOverTimeEvent : TimeEvent
    {
        public CameraFacingThetaOverTimeEvent(float startTime, float endTime,
            CameraAboutTarget camera, Func<float,float> facingThetaFunc)
        {
            StartTime = startTime;
            EndTime = endTime;
            Camera = camera;
            FacingThetaFunc = facingThetaFunc;
        }

        public readonly CameraAboutTarget Camera;
        public readonly Func<float, float> FacingThetaFunc;

        protected override void UpdateTimeEvent(float time)
        {
            Camera.FacingTheta = FacingThetaFunc(time);
        }
    }
}
