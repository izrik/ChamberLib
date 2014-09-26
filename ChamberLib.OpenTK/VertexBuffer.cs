using System;
using OpenTK.Graphics.OpenGL;
using System.Runtime.InteropServices;
using System.Linq;

namespace ChamberLib
{
    public class VertexBuffer
    {
        public struct VertexAttribute
        {
            public VertexAttribute(
                int numComponents,
                VertexAttribPointerType type,
                int? offsetInBytes=null,
                int? attributeIndex=null)
            {
                NumComponents=numComponents;
                VertexAttribPointerType = type;
                OffsetInBytes = offsetInBytes;
                AttributeIndex = attributeIndex;
            }

            public readonly int NumComponents;
            public readonly VertexAttribPointerType VertexAttribPointerType;
            public readonly int? OffsetInBytes;
            public readonly int? AttributeIndex;
        }

        public int VertexBufferID;
        public int VertexSizeInBytes;
        public VertexAttribute[] VertexAttibutes;

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

        public static VertexBuffer FromArray(IVertex[] vertexes)
        {
            var vb = new VertexBuffer();

            if (vertexes[0] is Vertex_PBiBwNT)
            {
                var size = Marshal.SizeOf(typeof(Vertex_PBiBwNT));
                var data = vertexes.Cast<Vertex_PBiBwNT>().ToArray();

                vb.SetVertexData(data, size, 
                    new VertexBuffer.VertexAttribute(3, VertexAttribPointerType.Float, attributeIndex: 0),
                    new VertexBuffer.VertexAttribute(4, VertexAttribPointerType.Float, attributeIndex: 3),
                    new VertexBuffer.VertexAttribute(4, VertexAttribPointerType.Float, attributeIndex: 4),
                    new VertexBuffer.VertexAttribute(3, VertexAttribPointerType.Float, attributeIndex: 1),
                    new VertexBuffer.VertexAttribute(2, VertexAttribPointerType.Float, attributeIndex: 2));
            }
            else if (vertexes[0] is Vertex_PN)
            {
                var size = Marshal.SizeOf(typeof(Vertex_PN));
                var data = vertexes.Cast<Vertex_PN>().ToArray();
                vb.SetVertexData(data, size, 
                    new VertexBuffer.VertexAttribute(3, VertexAttribPointerType.Float),
                    new VertexBuffer.VertexAttribute(3, VertexAttribPointerType.Float));
            }
            else if (vertexes[0] is Vertex_PNT)
            {
                var size = Marshal.SizeOf(typeof(Vertex_PNT));
                var data = vertexes.Cast<Vertex_PNT>().ToArray();
                vb.SetVertexData(data, size, 
                    new VertexBuffer.VertexAttribute(3, VertexAttribPointerType.Float),
                    new VertexBuffer.VertexAttribute(3, VertexAttribPointerType.Float),
                    new VertexBuffer.VertexAttribute(2, VertexAttribPointerType.Float));
            }
            else if (vertexes[0] is Vertex_PNTT)
            {
                var size = Marshal.SizeOf(typeof(Vertex_PNTT));
                var data = vertexes.Cast<Vertex_PNTT>().ToArray();
                vb.SetVertexData(data, size, 
                    new VertexBuffer.VertexAttribute(3, VertexAttribPointerType.Float),
                    new VertexBuffer.VertexAttribute(3, VertexAttribPointerType.Float),
                    new VertexBuffer.VertexAttribute(2, VertexAttribPointerType.Float),
                    new VertexBuffer.VertexAttribute(2, VertexAttribPointerType.Float));
            }
            else
            {
                throw new InvalidOperationException();
            }

            return vb;
        }
    }
}

