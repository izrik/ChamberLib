using System;
using XModel = Microsoft.Xna.Framework.Graphics.Model;
using XMesh = Microsoft.Xna.Framework.Graphics.ModelMesh;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

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

        protected IEnumerable<Effect> GetEffects()
        {
            return Model.Meshes.SelectMany(m => m.Effects).Distinct();
        }

        public void SetAmbientLightColor(Vector3 color)
        {
            foreach (var effect in GetEffects())
            {
                var iEffectLights = effect as Microsoft.Xna.Framework.Graphics.IEffectLights;
                if (iEffectLights != null)
                {
                    iEffectLights.AmbientLightColor = color.ToXna();
                }
            }
        }

        public void SetEmissiveColor(Vector3 color)
        {
            foreach (var effect in GetEffects())
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

        public void SetDirectionalLight(DirectionalLight light, int index = 0)
        {
            foreach (var effect in GetEffects())
            {
                var effect2 = effect as Microsoft.Xna.Framework.Graphics.IEffectLights;
                if (effect2 != null)
                {
                    Microsoft.Xna.Framework.Graphics.DirectionalLight destLight;
                    if (index == 1)
                    {
                        destLight = effect2.DirectionalLight1;
                    }
                    else if (index == 2)
                    {
                        destLight = effect2.DirectionalLight2;
                    }
                    else
                    {
                        destLight = effect2.DirectionalLight0;
                    }

                    destLight.DiffuseColor = light.DiffuseColor.ToXna();
                    destLight.Direction = light.Direction.ToXna();
                    destLight.Enabled = light.Enabled;
                    destLight.SpecularColor = light.SpecularColor.ToXna();

                    effect2.LightingEnabled = true;
                }
            }
        }
    }
}

