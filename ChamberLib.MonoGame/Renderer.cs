using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Vector2 = ChamberLib.Vector2;
using Vector3 = ChamberLib.Vector3;
using Color = ChamberLib.Color;
using System.Collections.Generic;
using Matrix = ChamberLib.Matrix;
using XMatrix = Microsoft.Xna.Framework.Matrix;
using System.Linq;

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

            _draw3DEffect = new BasicEffect(this);
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

            _drawLineEffect.World = XMatrix.Identity;
            _drawLineEffect.View = XMatrix.Identity;

            var projection = XMatrix.CreateOrthographicOffCenter(0, _device.Viewport.Width, _device.Viewport.Height, 0, 0, 1);
            var halfPixelOffset = XMatrix.CreateTranslation(-0.5f, -0.5f, 0);
            var transform = (halfPixelOffset * projection);

            _drawLineEffect.Projection = transform;
            _drawLineEffect.DiffuseColor = Vector3.Zero.ToXna();
            _drawLineEffect.AmbientLightColor = Vector3.Zero.ToXna();
            _drawLineEffect.EmissiveColor = color.ToVector3().ToXna();

            _drawLineEffect.ApplyFirstPass();

            _device.DrawUserPrimitives(PrimitiveType.LineList, verts, 0, 1);
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

            _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            _spriteBatch.DrawString( sfont, text, position.ToXna(), color.ToXna(), rotation, origin.ToXna(), scale, effects, layerDepth);
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
            _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            foreach (var entry in entries)
            {
                Texture2D texture = ((Texture2DAdapter)entry.Texture).Texture;
                _spriteBatch.Draw(texture, entry.DestinationRectangle.ToXna(), entry.Color.ToXna());
            }
            _spriteBatch.End();
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

        readonly BasicEffect _draw3DEffect;
        public void DrawLines(Vector3 color, Matrix world, Matrix view, Matrix projection, IEnumerable<Vector3> points)
        {
            this.Reset3D();
            var verts = points.Select(v => new VertexPositionNormalTexture(v.ToXna(), Vector3.UnitY.ToXna(), Vector2.Zero.ToXna())).ToArray();
            _draw3DEffect.SetMatrices(world, view, projection);
            _draw3DEffect.DiffuseColor = Vector3.Zero.ToXna();
            _draw3DEffect.EmissiveColor = color.ToXna();
            _draw3DEffect.ApplyFirstPass();
            _device.DrawUserPrimitives(PrimitiveType.LineStrip, verts, 0, verts.Length - 1);
        }
    }
}

