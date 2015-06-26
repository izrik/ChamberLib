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

        void EnableDefaultLighting();

        void SetAmbientLightColor(Vector3 value);
        void SetEmissiveColor(Vector3 value);
        void SetDirectionalLight(DirectionalLight light, int index=0);
        void DisableDirectionalLight(int index);

        void SetAlpha(float alpha);

        void SetTexture(ITexture2D texture);

        void SetWorldViewProjection(Matrix transform, Matrix view, Matrix projection);

        void SetBoneTransforms(Matrix[] boneTransforms,
            IMaterial materialOverride=null);
    }
}

