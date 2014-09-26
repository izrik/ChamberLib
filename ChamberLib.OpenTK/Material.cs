using System;
using OpenTK.Graphics.OpenGL;

namespace ChamberLib
{
    public class Material : IMaterial
    {
        public Material()
        {
            Alpha = 1;
        }

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

            var worldViewProjLocation = GL.GetUniformLocation(Shader2.ProgramID, "worldViewProj");
            var worldViewProj = (world * view * projection).ToOpenTK();
            GL.UniformMatrix4(worldViewProjLocation, false, ref worldViewProj);
            GLHelper.CheckError();

            var worldViewLocation = GL.GetUniformLocation(Shader2.ProgramID, "worldView");
            var worldView = (world * view).ToOpenTK();
            GL.UniformMatrix4(worldViewLocation, false, ref worldView);
            GLHelper.CheckError();

            var viewLocation = GL.GetUniformLocation(Shader2.ProgramID, "view");
            var view2 = view.ToOpenTK();
            GL.UniformMatrix4(viewLocation, false, ref view2);
            GLHelper.CheckError();

            var worldLocation = GL.GetUniformLocation(Shader2.ProgramID, "world");
            var world2 = world.ToOpenTK();
            GL.UniformMatrix4(worldLocation, false, ref world2);
            GLHelper.CheckError();

            var useTextureLocation = GL.GetUniformLocation(Shader2.ProgramID, "use_texture");
            GL.Uniform1(useTextureLocation, (Texture == null ? 0 : 1));
            GLHelper.CheckError();

            var materialDiffuseColorLocation = GL.GetUniformLocation(Shader2.ProgramID, "material_diffuse_color");
            GL.Uniform3(materialDiffuseColorLocation, Diffuse.ToOpenTK());
            GLHelper.CheckError();

            var materialEmissiveColorLocation = GL.GetUniformLocation(Shader2.ProgramID, "material_emissive_color");
            GL.Uniform3(materialEmissiveColorLocation, lighting.EmissiveColor.ToOpenTK());
            GLHelper.CheckError();

            var materialSpecularColorLocation = GL.GetUniformLocation(Shader2.ProgramID, "material_specular_color");
            GL.Uniform3(materialSpecularColorLocation, SpecularColor.ToOpenTK());
            GLHelper.CheckError();

            var materialSpecularPowerLocation = GL.GetUniformLocation(Shader2.ProgramID, "material_specular_power");
            GL.Uniform1(materialSpecularPowerLocation, SpecularPower);
            GLHelper.CheckError();

            var materialAlphaLocation = GL.GetUniformLocation(Shader2.ProgramID, "material_alpha");
            GL.Uniform1(materialAlphaLocation, Alpha);
            GLHelper.CheckError();

            var lightAmbientLocation = GL.GetUniformLocation(Shader2.ProgramID, "light_ambient");
            GL.Uniform3(lightAmbientLocation, lighting.AmbientLightColor.ToOpenTK()); 
            GLHelper.CheckError();

            var lightDirectionLocation = GL.GetUniformLocation(Shader2.ProgramID, "light_direction_ws");
            GL.Uniform3(lightDirectionLocation, lighting.DirectionalLight.Direction.Normalized().ToOpenTK());
            GLHelper.CheckError();

            var lightDiffuseColorLocation = GL.GetUniformLocation(Shader2.ProgramID, "light_diffuse_color");
            GL.Uniform3(lightDiffuseColorLocation, lighting.DirectionalLight.DiffuseColor.ToOpenTK()); 
            GLHelper.CheckError();

            var lightSpecularColorLocation = GL.GetUniformLocation(Shader2.ProgramID, "light_specular_color");
            GL.Uniform3(lightSpecularColorLocation, lighting.DirectionalLight.SpecularColor.ToOpenTK()); 
            GLHelper.CheckError();

            var cameraPositionLocation = GL.GetUniformLocation(Shader2.ProgramID, "camera_position_ws");
            var cameraPosition = view.Inverted().ToOpenTK().ExtractTranslation();
            GL.Uniform3(cameraPositionLocation, cameraPosition); 
            GLHelper.CheckError();

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

