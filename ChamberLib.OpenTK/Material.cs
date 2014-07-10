using System;
using OpenTK.Graphics.OpenGL;

namespace ChamberLib
{
    public class Material : IMaterial
    {
        public Vector3 Diffuse { get; set; }
        public ITexture2D Texture { get; set; }

        public void Apply(Renderer renderer, LightingData lighting, Matrix world, Matrix view, Matrix projection)
        {
            renderer.SetMatrices(Matrix.Identity, view, projection);

            GL.LightModel(LightModelParameter.LightModelAmbient,
                new float[] {
                    lighting.AmbientLightColor.X,
                    lighting.AmbientLightColor.Y,
                    lighting.AmbientLightColor.Z,
                    1
                });
            GL.LightModel(LightModelParameter.LightModelLocalViewer, 0);
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);
            GL.Disable(EnableCap.Light1);
            GL.Disable(EnableCap.Light2);
            GL.Disable(EnableCap.Light3);
            GL.Disable(EnableCap.Light4);
            GL.Disable(EnableCap.Light5);
            GL.Disable(EnableCap.Light6);
            GL.Disable(EnableCap.Light7);
            GL.Disable(EnableCap.FragmentLightingSgix);
            GL.Light(LightName.Light0, LightParameter.Ambient, Vector4.Zero.ToOpenTK());
            GL.Light(LightName.Light0, LightParameter.Diffuse, lighting.DirectionalLight.DiffuseColor.ToVector4().ToOpenTK());
            GL.Light(LightName.Light0, LightParameter.Specular, Vector4.Zero.ToOpenTK());
            var position = -lighting.DirectionalLight.Direction;
            GL.Light(LightName.Light0, LightParameter.Position, position.ToVector4(0).ToOpenTK());

            GL.Material(MaterialFace.Front, MaterialParameter.Ambient, Vector4.One.ToOpenTK());
            var diffuse = new Vector4(Diffuse.X, Diffuse.Y, Diffuse.Z, 1);
            GL.Material(MaterialFace.Front, MaterialParameter.Diffuse, diffuse.ToOpenTK());
            GL.Material(MaterialFace.Front, MaterialParameter.Specular, Vector4.Zero.ToOpenTK());
            GL.Material(MaterialFace.Front, MaterialParameter.Shininess, 0);
            GL.Material(MaterialFace.Front, MaterialParameter.Emission, lighting.EmissiveColor.ToOpenTK());

            if (Texture != null)
            {
                ((TextureAdapter)Texture).Bind();
            }

            renderer.SetMatrices(world, view, projection);

        }

        public void UnApply()
        {
            if (Texture != null)
            {
                TextureAdapter.Unbind();
            }
        }
    }
}

