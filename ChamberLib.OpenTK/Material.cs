using System;
using OpenTK.Graphics.OpenGL;
using ChamberLib.Content;

namespace ChamberLib
{
    public class Material : IMaterial
    {
        public Material(MaterialContent material, ContentResolver resolver)
        {
            this.Name = material.Name;
            this.Diffuse = material.DiffuseColor;
            this.EmissiveColor = material.EmissiveColor;
            this.SpecularColor = material.SpecularColor;
            this.SpecularPower = material.SpecularPower;
            this.Alpha = material.Alpha;

            if (material.Texture != null)
            {
                if (!resolver.Textures.ContainsKey(material.Texture))
                {
                    var texture = new TextureAdapter(material.Texture);
                    resolver.Add(material.Texture, texture);
                }
                this.Texture = resolver.Get(material.Texture);
            }

            if (material.Shader != null)
            {
                if (!resolver.Shaders.ContainsKey(material.Shader))
                {
                    var shader =
                        new ShaderAdapter(
                            material.Shader,
                            new [] {
                                "in_position",
                                "in_blend_indexes",
                                "in_blend_weights",
                                "in_normal",
                                "in_texture_coords",
                            });
                    resolver.Add(material.Shader, shader);
                }
                this.Shader2 = resolver.Get(material.Shader);
            }
        }

        public string Name = "";

        public Vector3 Diffuse { get; set; }
        public Vector3 EmissiveColor { get; set; }
        public Vector3 SpecularColor { get; set; }
        public float SpecularPower { get; set; }
        public float Alpha { get; set; }
        public ITexture2D Texture { get; set; }

        public IShader Shader { get; set; }
        public ShaderAdapter Shader2
        {
            get { return (ShaderAdapter)Shader; }
            set { Shader = value; }
        }

        public void Apply(Renderer renderer, LightingData lighting, Matrix world, Matrix view, Matrix projection)
        {
            if (Shader == null) throw new InvalidOperationException();

            Shader.Apply();

            Shader2.SetUniform("worldViewProj", world * view * projection);
            Shader2.SetUniform("worldView", world * view);
            Shader2.SetUniform("view", view);
            Shader2.SetUniform("world", world);
            Shader2.SetUniform("use_texture", (Texture != null));
            Shader2.SetUniform("material_diffuse_color", Diffuse);
            Shader2.SetUniform("material_emissive_color", lighting.EmissiveColor);
            Shader2.SetUniform("material_specular_color", SpecularColor);
            Shader2.SetUniform("material_specular_power", SpecularPower);
            Shader2.SetUniform("material_alpha", Alpha);
            Shader2.SetUniform("light_ambient", lighting.AmbientLightColor);
            Shader2.SetUniform("light_direction_ws", lighting.DirectionalLight.Direction.Normalized());
            Shader2.SetUniform("light_diffuse_color", lighting.DirectionalLight.DiffuseColor);
            Shader2.SetUniform("light_specular_color", lighting.DirectionalLight.SpecularColor);
            Shader2.SetUniform("camera_position_ws", view.Inverted().ToOpenTK().ExtractTranslation().ToChamber());

            if (Texture != null)
            {
                Texture.Apply();
            }
        }

        public void UnApply()
        {
            if (Texture != null)
            {
                Texture.UnApply();
            }

            if (Shader != null)
            {
                Shader.UnApply();
            }
        }
    }
}

