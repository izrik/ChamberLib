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
            throw new NotImplementedException();
        }

        public void Draw(Matrix world, Matrix view, Matrix projection)
        {
            throw new NotImplementedException();
        }

        public void EnableDefaultLighting()
        {
            throw new NotImplementedException();
        }

        public void SetAmbientLightColor(Vector3 value)
        {
            throw new NotImplementedException();
        }

        public void SetEmissiveColor(Vector3 value)
        {
            throw new NotImplementedException();
        }

        public void SetDirectionalLight(DirectionalLight light, int index = 0)
        {
            throw new NotImplementedException();
        }

        public void DisableDirectionalLight(int index)
        {
            throw new NotImplementedException();
        }

        public void SetAlpha(float alpha)
        {
            throw new NotImplementedException();
        }

        public void SetTexture(ITexture2D texture)
        {
            throw new NotImplementedException();
        }

        public void SetWorldViewProjection(Matrix transform, Matrix view, Matrix projection)
        {
            throw new NotImplementedException();
        }

        public void SetBoneTransforms(Matrix[] boneTransforms, float verticalOffset, Matrix world)
        {
            throw new NotImplementedException();
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

