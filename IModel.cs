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
                var p = tri.Intersects(ray);
                if (!p.HasValue) continue;

                if (!closest.HasValue)
                {
                    closest = p;
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

            return closest;
        }
    }
}

