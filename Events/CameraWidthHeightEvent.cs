using System;
namespace ChamberLib
{
    public class CameraWidthHeightEvent : TimeEvent
    {
        public CameraWidthHeightEvent(float time, CameraAboutTarget camera,
            float width, float height)
        {
            StartTime = time;
            EndTime = time;
            Camera = camera;
            Width = width;
            Height = height;
        }

        public readonly CameraAboutTarget Camera;
        public readonly float Width;
        public readonly float Height;

        protected override void UpdateTimeEvent(float time)
        {
            Camera.Width = Width;
            Camera.Height = Height;
        }
    }
}
