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

        public void SetAmbientLightColor(Vector3 color)
        {
            foreach (var mesh in Model.Meshes)
            {
                foreach (var effect in mesh.Effects)
                {
                    var iEffectLights = effect as Microsoft.Xna.Framework.Graphics.IEffectLights;
                    if (iEffectLights != null)
                    {
                        iEffectLights.AmbientLightColor = color.ToXna();
                    }
                }
            }
        }

        public void SetEmissiveColor(Vector3 color)
        {
            foreach (var mesh in Model.Meshes)
            {
                foreach (var effect in mesh.Effects)
                {
                    var effect2 = effect as Microsoft.Xna.Framework.Graphics.BasicEffect;
                    if (effect2 != null)
                    {
                        effect2.EmissiveColor = color.ToXna();
                    }
                    var effect3 = effect as Microsoft.Xna.Framework.Graphics.SkinnedEffect;
                    if (effect3 != null)
                    {
                        effect3.EmissiveColor = color.ToXna();
                    }
                }
            }
        }

    }
}

