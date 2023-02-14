
//
// ChamberLib, a cross-platform game engine
// Copyright (C) 2021 izrik and Metaphysics Industries, Inc.
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301
// USA
//

using System;
using ChamberLib.Content;
using ChamberLib.OpenTK.Content;

namespace ChamberLib.OpenTK.Materials
{
    public class FragmentMaterial : IFragmentMaterial
    {
        public FragmentMaterial(FragmentMaterialContent material,
            ContentResolver resolver, IContentProcessor processor)
            : this(material.DiffuseColor, material.EmissiveColor,
                  material.SpecularColor, material.SpecularPower,
                  material.Alpha,
                  material.Texture != null ?
                    processor.ProcessTexture2D(material.Texture) : null,
                  material.FragmentShader != null ?
                    processor.ProcessShaderStage(material.FragmentShader,
                      processor) : null,
                  material.Name)
        {
        }
        public FragmentMaterial(Vector3 diffuse, Vector3 emissive,
            Vector3 specular, float specularPower, float alpha,
            ITexture2D texture, IShaderStage fragmentShader, string name)
        {
            this.Name = name;

            this.Diffuse = diffuse;
            this.EmissiveColor = emissive;
            this.SpecularColor = specular;
            this.SpecularPower = specularPower;
            this.Alpha = alpha;

            this.Texture = texture;
            this.FragmentShader = fragmentShader;
        }

        public string Name { get; set; }

        public void Apply(GameTime gameTime, Matrix world,
            ComponentCollection components, IShaderStage fragmentShader,
            Overrides overrides=default(Overrides))
        {
            if (fragmentShader == null) throw new InvalidOperationException("No fragment shader specified");

            var ambient = components?.Get<AmbientLight>();
            fragmentShader.SetUniform("light_ambient", ambient?.Color ?? Vector3.Zero);

            var light = components?.Get<DirectionalLight>();
            fragmentShader.SetUniform("light_direction_ws", light?.Direction.Normalized() ?? -Vector3.UnitY);
            fragmentShader.SetUniform("light_diffuse_color", light?.DiffuseColor ?? Vector3.One);
            fragmentShader.SetUniform("light_specular_color", light?.SpecularColor ?? Vector3.One);

            var camera = components?.Get<ICamera>();
            var view = camera?.View ?? Matrix.Identity;
            fragmentShader.SetUniform("camera_position_ws", view.Inverted().Translation);

            fragmentShader.SetUniform("use_texture", (Texture != null));
            fragmentShader.SetUniform("material_diffuse_color", Diffuse);
            fragmentShader.SetUniform("material_emissive_color", EmissiveColor);
            fragmentShader.SetUniform("material_specular_color", SpecularColor);
            fragmentShader.SetUniform("material_specular_power", SpecularPower);
            float alpha = overrides.GetAlpha(Alpha);
            fragmentShader.SetUniform("material_alpha", alpha);

            if (Texture != null)
            {
                Texture.Apply();
            }

            OnApply?.Invoke(gameTime, world, components, fragmentShader,
                overrides);
        }

        public Action<GameTime, Matrix, ComponentCollection, IShaderStage,
            Overrides> OnApply { get; set; }

        public void UnApply()
        {
            if (Texture != null)
            {
                Texture.UnApply();
            }
        }

        public Vector3 Diffuse { get; set; }
        public Vector3 EmissiveColor { get; set; }
        public Vector3 SpecularColor { get; set; }
        public float SpecularPower { get; set; }
        public float Alpha { get; set; }

        public ITexture2D Texture { get; set; }

        public IShaderStage FragmentShader { get; set; }

    }
}
