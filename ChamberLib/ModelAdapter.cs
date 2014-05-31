using System;
using XModel = Microsoft.Xna.Framework.Graphics.Model;
using XMesh = Microsoft.Xna.Framework.Graphics.ModelMesh;
using System.Collections.Generic;

namespace ChamberLib
{
    public class ModelAdapter : IModel
    {
        protected static readonly Dictionary<XModel, IModel> _cache = new Dictionary<XModel, IModel>();

        public static IModel GetAdapter(XModel model)
        {
            if (_cache.ContainsKey(model))
            {
                return _cache[model];
            }

            var adapter = new ModelAdapter(model);
            _cache[model] = adapter;
            return adapter;
        }

        protected ModelAdapter(XModel model)
        {
            Model = model;
        }

        public XModel Model;

        public object Tag
        {
            get { return Model.Tag; }
            set { Model.Tag = value; }
        }

        public IEnumerable<IMesh> GetMeshes()
        {
            foreach (var mesh in Model.Meshes)
            {
                yield return MeshAdapter.GetAdapter(mesh);
            }
        }

        public void Draw(Matrix world, Matrix view, Matrix projection)
        {
            Model.Draw(world.ToXna(), view.ToXna(), projection.ToXna());
        }

        public IBone Root
        {
            get { return BoneAdapter.GetAdapter(Model.Root); }
            set { Model.Root = ((BoneAdapter)value).Bone; }
        }
    }
}

