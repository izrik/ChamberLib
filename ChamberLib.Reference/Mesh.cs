using System;

namespace ChamberLib
{
    public class Mesh : IMesh
    {
        public Mesh()
        {
        }

        #region IMesh implementation

        public void Draw(Matrix world, Matrix view, Matrix projection,
                         LightingData lighting, Overrides overrides=null)
        {
            throw new NotImplementedException();
        }

        public Sphere BoundingSphere
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

        public IBone ParentBone
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

        public string Name
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

