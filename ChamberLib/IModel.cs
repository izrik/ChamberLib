using System;
using System.Collections.Generic;

namespace ChamberLib
{
    public interface IModel
    {
        object Tag { get; set; }

        IEnumerable<IMesh> GetMeshes();

        void Draw(Matrix world, Matrix view, Matrix projection);

        IBone Root { get; set; }

        void SetAmbientLightColor(Vector3 value);
        void SetEmissiveColor(Vector3 value);

        void SetDirectionalLight(DirectionalLight light, int index=0);
    }
}

