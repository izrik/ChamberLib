using System;
using OpenTK.Graphics.OpenGL;
using System.Runtime.InteropServices;
using System.Linq;

namespace ChamberLib.OpenTK
{
    public class VertexBuffer : IVertexBuffer
    {
        public int VertexBufferID { get; protected set; }
        public int VertexSizeInBytes { get; protected set; }
        public VertexAttribute[] VertexAttibutes { get; protected set; }

        bool IsReady = false;
        public IVertex[] VertexData { get; protected set; }

        public VertexBuffer(
            IVertex[] vertexData,
            int vertexSizeInBytes,
            VertexAttribPointerType vertexAttributeComponentType,
            int numVertexAttributeComponents)
            : this(
                vertexData,
                vertexSizeInBytes,
                new VertexAttribute(
                    numVertexAttributeComponents,
                    vertexAttributeComponentType))
        {
        }
        public VertexBuffer(
            IVertex[] vertexData,
            int vertexSizeInBytes,
            params VertexAttribute[] attributes)
        {
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

            VertexData = vertexData;
            VertexSizeInBytes = vertexSizeInBytes;
            VertexAttibutes = attributes;
        }

        public void MakeReady()
        {
            VertexBufferID = GL.GenBuffer();
            GLHelper.CheckError();

            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferID);
            GLHelper.CheckError();

            if (VertexData[0] is Vertex_PBiBwNT)
            {
                Vertex_PBiBwNT[] vertexArray = VertexData.ConvertPBiBwNT();
                GL.BufferData<Vertex_PBiBwNT>(BufferTarget.ArrayBuffer,
                    new IntPtr(VertexData.Length * VertexSizeInBytes),
                    vertexArray,
                    BufferUsageHint.StaticDraw);
                GLHelper.CheckError();
            }
            else
            if (VertexData[0] is Vertex_PN)
            {
                Vertex_PN[] vertexArray = VertexData.ConvertPN();
                GL.BufferData<Vertex_PN>(BufferTarget.ArrayBuffer,
                    new IntPtr(VertexData.Length * VertexSizeInBytes),
                    vertexArray,
                    BufferUsageHint.StaticDraw);
                GLHelper.CheckError();
            }
            else
            if (VertexData[0] is Vertex_PNT)
            {
                Vertex_PNT[] vertexArray = VertexData.ConvertPNT();
                GL.BufferData<Vertex_PNT>(BufferTarget.ArrayBuffer,
                    new IntPtr(VertexData.Length * VertexSizeInBytes),
                    vertexArray,
                    BufferUsageHint.StaticDraw);
                GLHelper.CheckError();
            }
            else
            if (VertexData[0] is Vertex_PNTT)
            {
                Vertex_PNTT[] vertexArray = VertexData.ConvertPNTT();
                GL.BufferData<Vertex_PNTT>(BufferTarget.ArrayBuffer,
                    new IntPtr(VertexData.Length * VertexSizeInBytes),
                    vertexArray,
                    BufferUsageHint.StaticDraw);
                GLHelper.CheckError();
            }
            else
            {
                throw new InvalidOperationException(
                                string.Format(
                                    "Unknown vertex type: {0}",
                                    VertexData[0].GetType().Name));
            }

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GLHelper.CheckError();

            IsReady = true;
        }

        public static VertexBuffer FromArray(IVertex[] vertexes)
        {
            if (vertexes[0] is Vertex_PBiBwNT)
            {
                var size = Marshal.SizeOf(typeof(Vertex_PBiBwNT));
                return new VertexBuffer(vertexes, size,
                    new VertexAttribute(3, VertexAttribPointerType.Float, attributeIndex: 0),
                    new VertexAttribute(4, VertexAttribPointerType.Float, attributeIndex: 3),
                    new VertexAttribute(4, VertexAttribPointerType.Float, attributeIndex: 4),
                    new VertexAttribute(3, VertexAttribPointerType.Float, attributeIndex: 1),
                    new VertexAttribute(2, VertexAttribPointerType.Float, attributeIndex: 2));
            }

            if (vertexes[0] is Vertex_PN)
            {
                var size = Marshal.SizeOf(typeof(Vertex_PN));
                return new VertexBuffer(vertexes, size,
                    new VertexAttribute(3, VertexAttribPointerType.Float),
                    new VertexAttribute(3, VertexAttribPointerType.Float));
            }

            if (vertexes[0] is Vertex_PNT)
            {
                var size = Marshal.SizeOf(typeof(Vertex_PNT));
                return new VertexBuffer(vertexes, size,
                    new VertexAttribute(3, VertexAttribPointerType.Float),
                    new VertexAttribute(3, VertexAttribPointerType.Float),
                    new VertexAttribute(2, VertexAttribPointerType.Float));
            }

            if (vertexes[0] is Vertex_PNTT)
            {
                var size = Marshal.SizeOf(typeof(Vertex_PNTT));
                return new VertexBuffer(vertexes, size,
                    new VertexAttribute(3, VertexAttribPointerType.Float),
                    new VertexAttribute(3, VertexAttribPointerType.Float),
                    new VertexAttribute(2, VertexAttribPointerType.Float),
                    new VertexAttribute(2, VertexAttribPointerType.Float));
            }

            throw new InvalidOperationException();
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
    }
}

