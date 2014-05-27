using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Vector2 = ChamberLib.Vector2;
using Vector3 = ChamberLib.Vector3;
using Color = ChamberLib.Color;
using System.Collections.Generic;

namespace ChamberLib
{
    public class Renderer : IRenderer
    {
        public Renderer(GraphicsDevice device)
        {
            if (device == null) throw new ArgumentNullException("device");

            _device = device;
            _spriteBatch = new SpriteBatch(device);

            _drawLineTexture = new Texture2D(this._device, 1, 1);
            _drawLineTexture.SetData(new [] { Microsoft.Xna.Framework.Color.White });
            _drawLineEffect = new BasicEffect(this);
        }

        GraphicsDevice _device;
        SpriteBatch _spriteBatch;

        public static implicit operator GraphicsDevice(Renderer r)
        {
            return r._device;
        }

        public static implicit operator SpriteBatch(Renderer r)
        {
            return r._spriteBatch;
        }

        Texture2D _drawLineTexture;
        public void DrawLine(float width, Color color, Vector2 v1, Vector2 v2)
        {
            float angle = (float)Math.Atan2(v2.Y - v1.Y, v2.X - v1.X);
            float length = Vector2.Distance(v1, v2);

            _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            _spriteBatch.Draw(_drawLineTexture, v1.ToXna(), null, color.ToXna(),
                             angle, Vector2.Zero.ToXna(), new Vector2(length, width).ToXna(),
                             SpriteEffects.None, 0);
            _spriteBatch.End();
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

            Matrix projection = Matrix.CreateOrthographicOffCenter(0, _device.Viewport.Width, _device.Viewport.Height, 0, 0, 1);
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
            _spriteBatch.Begin();
        }

        public void Begin (
            SpriteSortMode sortMode,
            BlendState blendState
            )
        {
            _spriteBatch.Begin(sortMode, blendState);
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

            _spriteBatch.DrawString( sfont, text, position.ToXna(), color.ToXna(), rotation, origin.ToXna(), scale, effects, layerDepth);
        }

        public void End()
        {
            _spriteBatch.End();
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
                _spriteBatch.Draw(texture, entry.DestinationRectangle.ToXna(), entry.Color.ToXna());
            }
            this.End();
        }

        public void Reset3D()
        {
            _device.DepthStencilState = DepthStencilState.Default;
        }

        public Viewport Viewport
        {
            get { return _device.Viewport.ToChamber(); }
            set { _device.Viewport = value.ToXna(); }
        }

        public void Clear(Color color)
        {
            _device.Clear(color.ToXna());
        }

        public void DrawUserPrimitives<T> (
            PrimitiveType primitiveType,
            T[] vertexData,
            int vertexOffset,
            int primitiveCount
            ) where T : struct, IVertexType
        {
            _device.DrawUserPrimitives(primitiveType, vertexData, vertexOffset, primitiveCount);
        }

        public void DrawCircleXZ(Vector3 color, Matrix? world=null, Matrix? view=null, Matrix? projection=null)
        {
            if (_circleEffect == null) InitCircle();

            if (!world.HasValue)
                world = Matrix.Identity;

            if (!view.HasValue)
                view = Matrix.Identity;

            if (!projection.HasValue)
                projection = Matrix.Identity;

            _circleEffect.World = world.Value;
            _circleEffect.View = view.Value;
            _circleEffect.Projection = projection.Value;
            _circleEffect.EmissiveColor = color.ToXna();
            _circleEffect.ApplyFirstPass();

            this.DrawUserPrimitives(PrimitiveType.LineList, _circle, 0, _circle.Length / 2);
        }



        BasicEffect _circleEffect;
        VertexPositionNormalTexture[] _circle;

        protected void InitCircle()
        {
            if (_circle != null && _circleEffect != null) return;

            //unit id circle
            _circleEffect = new BasicEffect(this._device);
            _circleEffect.AmbientLightColor = Vector3.Zero.ToXna();
            _circleEffect.DiffuseColor = Vector3.Zero.ToXna();
            _circleEffect.DirectionalLight0.DiffuseColor = Vector3.Zero.ToXna();
            _circleEffect.DirectionalLight1.DiffuseColor = Vector3.Zero.ToXna();
            _circleEffect.DirectionalLight2.DiffuseColor = Vector3.Zero.ToXna();
            int n = 16;
            int i;
            float r = 0.4f;
            List<VertexPositionNormalTexture> pts = new List<VertexPositionNormalTexture>();
            pts.Add(new VertexPositionNormalTexture(new Vector3(r, 0.01f, 0).ToXna(), Vector3.Zero.ToXna(), Vector2.Zero.ToXna()));
            for (i = 0; i < n; i++)
            {
                float theta = (float)(2 * Math.PI * i / (float)n);
                float x = (float)(r * Math.Cos(theta));
                float z = (float)(r * Math.Sin(theta));
                VertexPositionNormalTexture v = new VertexPositionNormalTexture(new Vector3(x, 0.01f, z).ToXna(), Vector3.Zero.ToXna(), Vector2.Zero.ToXna());
                pts.Add(v);
                pts.Add(v);
            }
            pts.Add(pts[0]);
            _circle = pts.ToArray();
        }
    }
}

