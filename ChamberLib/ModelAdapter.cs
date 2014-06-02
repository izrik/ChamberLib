using System;
using XModel = Microsoft.Xna.Framework.Graphics.Model;
using XMesh = Microsoft.Xna.Framework.Graphics.ModelMesh;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using XMatrix = Microsoft.Xna.Framework.Matrix;

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

        public void DisableDirectionalLight(int index)
        {
            foreach (var effect in GetEffects())
            {
                var effect2 = effect as Microsoft.Xna.Framework.Graphics.IEffectLights;
                if (effect2 != null)
                {
                    Microsoft.Xna.Framework.Graphics.DirectionalLight destLight;
                    if (index == 1)
                    {
                        effect2.DirectionalLight1.Enabled = false;
                    }
                    else if (index == 2)
                    {
                        effect2.DirectionalLight2.Enabled = false;
                    }
                    else
                    {
                        effect2.DirectionalLight0.Enabled = false;
                    }
                }
            }
        }

        public void SetWorldViewProjection(Matrix world, Matrix view, Matrix projection)
        {
            var world2 = world.ToXna();
            var view2 = view.ToXna();
            var projection2 = projection.ToXna();

            foreach (var effect in GetEffects())
            {
                var effect2 = effect as Microsoft.Xna.Framework.Graphics.IEffectMatrices;
                if (effect2 != null)
                {
                    effect2.World = world2;
                    effect2.View = view2;
                    effect2.Projection = projection2;
                }
                else
                {
                    effect.Parameters["World"].SetValue(world2);
                    effect.Parameters["View"].SetValue(view2);
                    effect.Parameters["Projection"].SetValue(projection2);
                }
            }
        }

        public void SetAlpha(float alpha)
        {
            foreach (var effect in GetEffects())
            {
                var skinnedEffect = effect as SkinnedEffect;
                var basicEffect = effect as BasicEffect;
                if (skinnedEffect != null)
                {
                    skinnedEffect.Alpha = alpha;
                }
                else if (basicEffect != null)
                {
                    basicEffect.Alpha = alpha;
                }
            }
        }

        public void SetTexture(ITexture2D texture)
        {
            var texture2 = ((Texture2DAdapter)texture).Texture;
            foreach (var effect in GetEffects())
            {
                var skinnedEffect = effect as SkinnedEffect;
                var basicEffect = effect as BasicEffect;
                if (skinnedEffect != null)
                {
                    skinnedEffect.Texture = texture2;
                }
                else if (basicEffect != null)
                {
                    basicEffect.Texture = texture2;
                }
            }
        }

        public void SetBoneTransforms(Matrix[] boneTransforms, float verticalOffset, Matrix world)
        {
            Matrix[] boneTransformsCopy = null;
            XMatrix[] boneTransformsCopy2 = null;

            if (boneTransforms != null)
            {
                boneTransformsCopy = boneTransforms.ToArray();
                boneTransformsCopy[0] = Matrix.CreateTranslation(0, verticalOffset, 0) * boneTransformsCopy[0];
                boneTransformsCopy2 = boneTransformsCopy.ToXna();
            }

            foreach (var mesh in this.GetMeshes())
            {
                foreach (Effect effect in ((MeshAdapter)mesh).Mesh.Effects)
                {
                    var skinnedEffect = effect as SkinnedEffect;
                    var basicEffect = effect as BasicEffect;
                    if (skinnedEffect != null)
                    {
                        if (boneTransforms != null)
                        {
                            skinnedEffect.SetBoneTransforms(boneTransformsCopy2);
                        }
                    }
                    else if (basicEffect != null)
                    {
                        if (boneTransforms != null)
                        {
                            basicEffect.World = (boneTransforms[mesh.ParentBone.Index] * world).ToXna();
                        }
                        else
                        {
                            basicEffect.World = world.ToXna();
                        }
                    }
                }
            }
        }

        public void EnableDefaultLighting()
        {
            foreach (var effect in GetEffects())
            {
                var effect2 = effect as IEffectLights;
                if (effect2 != null)
                {
                    effect2.EnableDefaultLighting();
                }
            }
        }
    }
}

