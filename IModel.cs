using System;
using System.Collections.Generic;

namespace ChamberLib
{
    public interface IModel
    {
        object Tag { get; set; }

        IEnumerable<IMesh> GetMeshes();

        void Draw(Matrix world, Matrix view, Matrix projection,
            IMaterial materialOverride=null,
            LightingData? lightingOverride=null);

        IBone Root { get; set; }

        void SetAmbientLightColor(Vector3 value);
        void SetEmissiveColor(Vector3 value);
        void SetDirectionalLight(DirectionalLight light, int index=0);
        void DisableDirectionalLight(int index);

        void SetAlpha(float alpha);

        void SetTexture(ITexture2D texture);

        void SetBoneTransforms(Matrix[] boneTransforms,
            IMaterial materialOverride=null);

        IEnumerable<Triangle> EnumerateTriangles();
    }

    public static class ModelHelper
    {
        public static Vector3? IntersectClosest(this IModel model, Ray ray)
        {
            Vector3? closest = null;
            float closestDist = -1;

            foreach (var tri in model.EnumerateTriangles())
            {
                IntersectClosest_IntersectRaySingleTriangle(tri, ref ray, ref closest, ref closestDist);
            }

            return closest;
        }

        public static Vector3?[] IntersectClosest(this IModel model, Ray[] rays)
        {
            Vector3?[] closest = new Vector3?[rays.Length];
            float[] closestDist = new float[rays.Length];

            foreach (var tri in model.EnumerateTriangles())
            {
                int i;
                for (i = 0; i < rays.Length; i++)
                {
                    var ray = rays[i];
                    IntersectClosest_IntersectRaySingleTriangle(tri, ref ray, ref closest[i], ref closestDist[i]);
                }
            }

            return closest;
        }

        public static Vector3?[,] IntersectClosest(this IModel model, Ray[,] rays)
        {
            Vector3?[,] closest = new Vector3?[rays.GetLength(0), rays.GetLength(1)];
            float[,] closestDist = new float[rays.GetLength(0), rays.GetLength(1)];

            foreach (var tri in model.EnumerateTriangles())
            {
                int i;
                int j;
                for (i = 0; i < rays.GetLength(0); i++)
                {
                    for (j = 0; j < rays.GetLength(1); j++)
                    {
                        var ray = rays[i, j];
                        IntersectClosest_IntersectRaySingleTriangle(tri, ref ray, ref closest[i, j], ref closestDist[i, j]);
                    }
                }
            }

            return closest;
        }

        private static void IntersectClosest_IntersectRaySingleTriangle(Triangle tri, ref Ray ray, ref Vector3? closest, ref float closestDist)
        {
            var p = tri.Intersects(ray);
            if (!p.HasValue) return;

            if (!closest.HasValue)
            {
                closest = p;
                closestDist = (p.Value - ray.Position).LengthSquared();
            }
            else
            {
                var dist = (p.Value - ray.Position).LengthSquared();
                if (dist < closestDist)
                {
                    closest = p;
                    closestDist = dist;
                }
            }
        }
    }
}

