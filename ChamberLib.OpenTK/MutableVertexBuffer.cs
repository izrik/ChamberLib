using System;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL;
using System.Linq;

namespace ChamberLib
{
    public class MutableVertexBuffer : IVertexBuffer
    {
        public int VertexBufferID { get; protected set; }
        public int VertexSizeInBytes { get; protected set; }
        public VertexAttribute[] VertexAttibutes { get; protected set; }

        bool IsReady = false;

        public void SetVertexData<T>(
            T[] vertexData,
            int vertexSizeInBytes,
            VertexAttribPointerType vertexAttributeComponentType,
            int numVertexAttributeComponents)
            where T : struct
        {
            SetVertexData(
                vertexData,
                vertexSizeInBytes,
                new VertexAttribute(
                    numVertexAttributeComponents,
                    vertexAttributeComponentType));
        }
        public void SetVertexData<T>(
            T[] vertexData,
            int vertexSizeInBytes,
            params VertexAttribute[] attributes)
            where T : struct
        {
            if (!IsReady)
            {
                MakeReady();
            }

            if (vertexData == null)
            {
                throw new ArgumentNullException("vertexData");
            }
            if (vertexSizeInBytes <= 0)
            {
                throw new ArgumentOutOfRangeException("vertexSizeInBytes", "vertexSizeInBytes must be greater than zero");
            }
            foreach (var attr in attributes)
            {
                if (attr.NumComponents <= 0)
                {
                    throw new ArgumentOutOfRangeException("NumComponents", "NumComponents must be greater than zero");
                }
                if (attr.VertexAttribPointerType <= 0)
                {
                    throw new ArgumentOutOfRangeException("VertexAttribPointerType", "VertexAttribPointerType must be greater than zero");
                }
                if (attr.OffsetInBytes.HasValue && attr.OffsetInBytes.Value < 0)
                {
                    throw new ArgumentOutOfRangeException("OffsetInBytes", "OffsetInBytes must be greater than or equal to zero");
                }
                if (attr.AttributeIndex.HasValue && attr.AttributeIndex.Value < 0)
                {
                    throw new ArgumentOutOfRangeException("AttributeIndex", "AttributeIndex must be greater than or equal to zero");
                }
            }

            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferID);
            GLHelper.CheckError();

            GL.BufferData<T>(BufferTarget.ArrayBuffer,
                new IntPtr(vertexData.Length * vertexSizeInBytes),
                vertexData,
                BufferUsageHint.StaticDraw);
            GLHelper.CheckError();

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GLHelper.CheckError();

            VertexSizeInBytes = vertexSizeInBytes;
            VertexAttibutes = attributes;
        }

        public void MakeReady()
        {
            VertexBufferID = GL.GenBuffer();
            GLHelper.CheckError();

            IsReady = true;
        }

        public void Apply()
        {
            if (!IsReady)
            {
                MakeReady();
            }

            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferID);
            GLHelper.CheckError();
        }

        public void UnApply()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GLHelper.CheckError();
        }

        public static MutableVertexBuffer FromArray(IVertex[] vertexes)
        {
            var vb = new MutableVertexBuffer();

            if (vertexes[0] is Vertex_PBiBwNT)
            {
                var size = Marshal.SizeOf(typeof(Vertex_PBiBwNT));
                var data = vertexes.Cast<Vertex_PBiBwNT>().ToArray();

                vb.SetVertexData(data, size,
                    new VertexAttribute(3, VertexAttribPointerType.Float, attributeIndex: 0),
                    new VertexAttribute(4, VertexAttribPointerType.Float, attributeIndex: 3),
                    new VertexAttribute(4, VertexAttribPointerType.Float, attributeIndex: 4),
                    new VertexAttribute(3, VertexAttribPointerType.Float, attributeIndex: 1),
                    new VertexAttribute(2, VertexAttribPointerType.Float, attributeIndex: 2));
            }
            else
            if (vertexes[0] is Vertex_PN)
            {
                var size = Marshal.SizeOf(typeof(Vertex_PN));
                var data = vertexes.Cast<Vertex_PN>().ToArray();
                vb.SetVertexData(data, size,
                    new VertexAttribute(3, VertexAttribPointerType.Float),
                    new VertexAttribute(3, VertexAttribPointerType.Float));
            }
            else
            if (vertexes[0] is Vertex_PNT)
            {
                var size = Marshal.SizeOf(typeof(Vertex_PNT));
                var data = vertexes.Cast<Vertex_PNT>().ToArray();
                vb.SetVertexData(data, size,
                    new VertexAttribute(3, VertexAttribPointerType.Float),
                    new VertexAttribute(3, VertexAttribPointerType.Float),
                    new VertexAttribute(2, VertexAttribPointerType.Float));
            }
            else
            if (vertexes[0] is Vertex_PNTT)
            {
                var size = Marshal.SizeOf(typeof(Vertex_PNTT));
                var data = vertexes.Cast<Vertex_PNTT>().ToArray();
                vb.SetVertexData(data, size, 
                    new VertexAttribute(3, VertexAttribPointerType.Float),
                    new VertexAttribute(3, VertexAttribPointerType.Float),
                    new VertexAttribute(2, VertexAttribPointerType.Float),
                    new VertexAttribute(2, VertexAttribPointerType.Float));
            }
            else
            {
                throw new InvalidOperationException();
            }

            return vb;
        }
    }
}

