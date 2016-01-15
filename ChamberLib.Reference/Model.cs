using System;
using System.Collections.Generic;

namespace ChamberLib
{
    public class Model : IModel
    {
        public Model()
        {
        }

        #region IModel implementation

        public IEnumerable<IMesh> GetMeshes()
        {
            throw new NotImplementedException();
        }

        public void Draw(Matrix world, Matrix view, Matrix projection,
                         Overrides overrides=null)
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

        public void SetDirectionalLight(DirectionalLight light, int index=0)
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

        public void SetBoneTransforms(Matrix[] boneTransforms,
                                      Overrides overrides=null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Triangle> EnumerateTriangles()
        {
            throw new NotImplementedException();
        }

        public object Tag
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

