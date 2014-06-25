using System;

namespace ChamberLib
{
    public class Model : IModel
    {
        public Model()
        {
        }

        #region IModel implementation

        public System.Collections.Generic.IEnumerable<IMesh> GetMeshes()
        {
            return new IMesh[0];
        }

        public void Draw(Matrix world, Matrix view, Matrix projection)
        {
        }

        public void EnableDefaultLighting()
        {
        }

        public void SetAmbientLightColor(Vector3 value)
        {
        }

        public void SetEmissiveColor(Vector3 value)
        {
        }

        public void SetDirectionalLight(DirectionalLight light, int index = 0)
        {
        }

        public void DisableDirectionalLight(int index)
        {
        }

        public void SetAlpha(float alpha)
        {
        }

        public void SetTexture(ITexture2D texture)
        {
        }

        public void SetWorldViewProjection(Matrix transform, Matrix view, Matrix projection)
        {
        }

        public void SetBoneTransforms(Matrix[] boneTransforms, float verticalOffset, Matrix world)
        {
        }

        public object Tag { get; set; }

        public IBone Root
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}

