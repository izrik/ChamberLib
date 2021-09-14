using System;
namespace ChamberLib
{
    public class CameraFacingPhiEvent : TimeEvent
    {
        public CameraFacingPhiEvent(float time, CameraAboutTarget camera,
            float phi)
        {
            StartTime = time;
            EndTime = time;
            Camera = camera;
            Phi = phi;
        }

        public readonly CameraAboutTarget Camera;
        public readonly float Phi;

        protected override void UpdateTimeEvent(float time)
        {
            Camera.FacingPhi = Phi;
        }
    }
}
