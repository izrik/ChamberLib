using System;
namespace ChamberLib
{
    public class CameraFacingThetaEvent : TimeEvent
    {
        public CameraFacingThetaEvent(float time, CameraAboutTarget camera,
            float theta)
        {
            StartTime = time;
            EndTime = time;
            Camera = camera;
            Theta = theta;
        }

        public readonly CameraAboutTarget Camera;
        public readonly float Theta;

        protected override void UpdateTimeEvent(float time)
        {
            Camera.FacingTheta = Theta;
        }
    }
}
