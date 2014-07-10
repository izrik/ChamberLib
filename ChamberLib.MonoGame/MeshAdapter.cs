using System;
using XMesh = Microsoft.Xna.Framework.Graphics.ModelMesh;
using System.Collections.Generic;

namespace ChamberLib
{
    public class MeshAdapter : IMesh
    {
        protected static readonly Dictionary<XMesh, MeshAdapter> _cache = new Dictionary<XMesh, MeshAdapter>();

        public static IMesh GetAdapter(XMesh mesh)
        {
            if (_cache.ContainsKey(mesh))
            {
                return _cache[mesh];
            }

            var adapter = new MeshAdapter(mesh);
            _cache[mesh] = adapter;

            return adapter;
        }

        protected MeshAdapter(XMesh mesh)
        {
            Mesh = mesh;
        }

        public XMesh Mesh;

        public Sphere BoundingSphere
        {
            get { return Mesh.BoundingSphere.ToChamber(); }
            set { Mesh.BoundingSphere = value.ToXna(); }
        }

        public void Draw(IRenderer renderer, Matrix world, Matrix view, Matrix projection, LightingData lighting)
        {
            Mesh.Draw();
        }

        public IBone ParentBone
        {
            get { return BoneAdapter.GetAdapter(Mesh.ParentBone); }
            set { Mesh.ParentBone = ((BoneAdapter)value).Bone; }
        }
    }
}

