using System;

namespace ChamberLib
{
    public struct Sphere
    {
        public Sphere(Vector3 center, float radius)
        {
            Center = center;
            Radius = radius;
        }

        public Vector3 Center;
        public float Radius;

        public Sphere Transform(Matrix m)
        {
            Vector3 center = Vector3.Transform(this.Center, m);


            var xr = m.Row1.ToVectorXYZ().LengthSquared();
            var yr = m.Row2.ToVectorXYZ().LengthSquared();
            var zr = m.Row3.ToVectorXYZ().LengthSquared();

            var rr = Math.Max(xr, Math.Max(yr, zr));

            float radius = (float)(Math.Sqrt(rr) * this.Radius);

            return new Sphere(center, radius);
        }

        public ContainmentType Contains(Frustum f)
        {
            throw new NotImplementedException();
        }
    }
}

