using System;
using System.Collections.Generic;

namespace ChamberLib
{
    public interface IModel
    {
        AnimationData AnimationData { get; set; }

        IEnumerable<IMesh> GetMeshes();

        void Draw(GameTime gameTime, Matrix world,
            ComponentCollection components,
            Overrides overrides=default(Overrides));

        IBone Root { get; set; }
        IEnumerable<IBone> EnumerateBones();

        void SetEmissiveColor(Vector3 value);

        void SetAlpha(float alpha);

        void SetTexture(ITexture2D texture);

        void SetBoneTransforms(Matrix[] boneTransforms,
            Overrides overrides=default(Overrides));

        IEnumerable<Triangle> EnumerateTriangles();

        IVertexMaterial GetVertexMaterialByName(string name);
        IFragmentMaterial GetFragmentMaterialByName(string name);
    }

    public static class ModelHelper
    {
        public static Vector3? IntersectClosest(this IModel model, Ray rayInModelSpace)
        {
            Vector3? closest = null;
            float closestDist = -1;

            foreach (var tri in model.EnumerateTriangles())
            {
                if (tri.IsDegenerate()) continue;

                IntersectClosest_IntersectRaySingleTriangle(tri, ref rayInModelSpace, ref closest, ref closestDist);
            }

            return closest;
        }

        public static Vector3?[] IntersectClosest(this IModel model, Ray[] raysInModelSpace)
        {
            Vector3?[] closest = new Vector3?[raysInModelSpace.Length];
            float[] closestDist = new float[raysInModelSpace.Length];

            foreach (var tri in model.EnumerateTriangles())
            {
                int i;
                for (i = 0; i < raysInModelSpace.Length; i++)
                {
                    var rayInModelSpace = raysInModelSpace[i];
                    IntersectClosest_IntersectRaySingleTriangle(tri, ref rayInModelSpace, ref closest[i], ref closestDist[i]);
                }
            }

            return closest;
        }

        public static Vector3?[,] IntersectClosest(this IModel model, Ray[,] raysInModelSpace)
        {
            Vector3?[,] closest = new Vector3?[raysInModelSpace.GetLength(0), raysInModelSpace.GetLength(1)];
            float[,] closestDist = new float[raysInModelSpace.GetLength(0), raysInModelSpace.GetLength(1)];

            foreach (var tri in model.EnumerateTriangles())
            {
                int i;
                int j;
                for (i = 0; i < raysInModelSpace.GetLength(0); i++)
                {
                    for (j = 0; j < raysInModelSpace.GetLength(1); j++)
                    {
                        var rayInModelSpace = raysInModelSpace[i, j];
                        IntersectClosest_IntersectRaySingleTriangle(tri, ref rayInModelSpace, ref closest[i, j], ref closestDist[i, j]);
                    }
                }
            }

            return closest;
        }

        private static void IntersectClosest_IntersectRaySingleTriangle(Triangle triInModelSpace, ref Ray rayInModelSpace, ref Vector3? closest, ref float closestDist)
        {
            var p = triInModelSpace.Intersects(rayInModelSpace);
            if (!p.HasValue) return;

            if (!closest.HasValue)
            {
                closest = p;
                closestDist = (p.Value - rayInModelSpace.Position).LengthSquared();
            }
            else
            {
                var dist = (p.Value - rayInModelSpace.Position).LengthSquared();
                if (dist < closestDist)
                {
                    closest = p;
                    closestDist = dist;
                }
            }
        }
    }
}

