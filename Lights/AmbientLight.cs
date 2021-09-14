using System;
namespace ChamberLib
{
    public class AmbientLight
    {
        public AmbientLight(Vector3 color=default(Vector3))
        {
            Color = color;
        }

        public Vector3 Color { get; set; }
    }
}
