using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Vector2 = ChamberLib.Vector2;
using Vector3 = ChamberLib.Vector3;
using Color = ChamberLib.Color;

namespace ChamberLib
{
    public class Renderer : IRenderer
    {
        public Renderer(GraphicsDevice device)
        {
            if (device == null) throw new ArgumentNullException("device");

            Device = device;
            SpriteBatch = new SpriteBatch(device);

            _drawLineTexture = new Texture2D(this.Device, 1, 1);
            _drawLineTexture.SetData(new [] { Microsoft.Xna.Framework.Color.White });
            _drawLineEffect = new BasicEffect(this);
        }

        public GraphicsDevice Device { get; protected set; }
        public SpriteBatch SpriteBatch { get; protected set; }

        public static implicit operator GraphicsDevice(Renderer r)
        {
            return r.Device;
        }

        public static implicit operator SpriteBatch(Renderer r)
        {
            return r.SpriteBatch;
        }

        Texture2D _drawLineTexture;
        public void DrawLine(float width, Color color, Vector2 v1, Vector2 v2)
        {
            float angle = (float)Math.Atan2(v2.Y - v1.Y, v2.X - v1.X);
            float length = Vector2.Distance(v1, v2);

            SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            SpriteBatch.Draw(_drawLineTexture, v1.ToXna(), null, color.ToXna(),
                             angle, Vector2.Zero.ToXna(), new Vector2(length, width).ToXna(),
                             SpriteEffects.None, 0);
            SpriteBatch.End();
        }

        BasicEffect _drawLineEffect;
        public void DrawLine(Color color, Vector2 v1, Vector2 v2)
        {
            VertexPositionNormalTexture[] verts = new VertexPositionNormalTexture[]{
                new VertexPositionNormalTexture(new Vector3(v1.X, v1.Y, 0).ToXna(), Vector3.UnitZ.ToXna(), Vector2.Zero.ToXna()),
                new VertexPositionNormalTexture(new Vector3(v2.X, v2.Y, 0).ToXna(), Vector3.UnitZ.ToXna(), Vector2.Zero.ToXna()),
            };

            _drawLineEffect.World = Matrix.Identity;
            _drawLineEffect.View = Matrix.Identity;

            Viewport vp = this.Viewport;
            Matrix projection = Matrix.CreateOrthographicOffCenter(0, vp.Width, vp.Height, 0, 0, 1);
            Matrix halfPixelOffset = Matrix.CreateTranslation(-0.5f, -0.5f, 0);
            Matrix transform = (halfPixelOffset * projection);

            _drawLineEffect.Projection = transform;
            _drawLineEffect.DiffuseColor = Vector3.Zero.ToXna();
            _drawLineEffect.AmbientLightColor = Vector3.Zero.ToXna();
            _drawLineEffect.EmissiveColor = color.ToVector3().ToXna();

            _drawLineEffect.ApplyFirstPass();

            this.DrawUserPrimitives(PrimitiveType.LineList, verts, 0, 1);
        }

        public void Begin()
        {
            SpriteBatch.Begin();
        }

        public void Begin (
            SpriteSortMode sortMode,
            BlendState blendState
            )
        {
            SpriteBatch.Begin(sortMode, blendState);
        }


        public void DrawString (
            IFont font,
            string text,
            Vector2 position,
            Color color,
            float rotation=0,
            Vector2 origin=default(Vector2),
            float scale=1)
        {
            SpriteEffects effects = SpriteEffects.None;
            float layerDepth = 0;
            SpriteFont sfont = ((SpriteFontAdapter)font).SpriteFont;

            SpriteBatch.DrawString( sfont, text, position.ToXna(), color.ToXna(), rotation, origin.ToXna(), scale, effects, layerDepth);
        }

        public void End()
        {
            SpriteBatch.End();
        }

        public void DrawImage(
            ITexture2D texture,
            RectangleI destinationRectangle,
            Color color)
        {
            DrawImages(new DrawImagesEntry(texture, destinationRectangle, color));
        }
        public void DrawImages(params DrawImagesEntry[] entries)
        {
            this.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            foreach (var entry in entries)
            {
                Texture2D texture = ((Texture2DAdapter)entry.Texture).Texture;
                SpriteBatch.Draw(texture, entry.DestinationRectangle.ToXna(), entry.Color.ToXna());
            }
            this.End();
        }

        public DepthStencilState DepthStencilState
        {
            get { return Device.DepthStencilState; }
            set { Device.DepthStencilState = value; }
        }

        public Viewport Viewport
        {
            get { return Device.Viewport; }
            set { Device.Viewport = value; }
        }

        public void Clear(Color color)
        {
            Device.Clear(color.ToXna());
        }

        public void DrawUserPrimitives<T> (
            PrimitiveType primitiveType,
            T[] vertexData,
            int vertexOffset,
            int primitiveCount
            ) where T : struct, IVertexType
        {
            Device.DrawUserPrimitives(primitiveType, vertexData, vertexOffset, primitiveCount);
        }
    }
}

