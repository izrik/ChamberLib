
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
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL;
using System.Linq;

namespace ChamberLib.OpenTK.System
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
            VertexAttribute[] attributes)
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

        static readonly VertexAttribute[] __FromArray_attirbutes_PBiBwNT = new [] {
            new VertexAttribute(3, VertexAttribPointerType.Float, attributeIndex: 0),
            new VertexAttribute(4, VertexAttribPointerType.Float, attributeIndex: 3),
            new VertexAttribute(4, VertexAttribPointerType.Float, attributeIndex: 4),
            new VertexAttribute(3, VertexAttribPointerType.Float, attributeIndex: 1),
            new VertexAttribute(2, VertexAttribPointerType.Float, attributeIndex: 2)};
        static readonly VertexAttribute[] __FromArray_attirbutes_PBiBwNTC = new[] {
            new VertexAttribute(3, VertexAttribPointerType.Float, attributeIndex: 0),
            new VertexAttribute(4, VertexAttribPointerType.Float, attributeIndex: 3),
            new VertexAttribute(4, VertexAttribPointerType.Float, attributeIndex: 4),
            new VertexAttribute(3, VertexAttribPointerType.Float, attributeIndex: 1),
            new VertexAttribute(2, VertexAttribPointerType.Float, attributeIndex: 2),
            new VertexAttribute(4, VertexAttribPointerType.Float, attributeIndex: 5)};
        static readonly VertexAttribute[] __FromArray_attirbutes_PN = new[] {
            new VertexAttribute(3, VertexAttribPointerType.Float),
            new VertexAttribute(3, VertexAttribPointerType.Float)};
        static readonly VertexAttribute[] __FromArray_attirbutes_PNT = new[] {
            new VertexAttribute(3, VertexAttribPointerType.Float),
            new VertexAttribute(3, VertexAttribPointerType.Float),
            new VertexAttribute(2, VertexAttribPointerType.Float) };
        static readonly VertexAttribute[] __FromArray_attirbutes_PNTC = new[] {
            new VertexAttribute(3, VertexAttribPointerType.Float),
            new VertexAttribute(3, VertexAttribPointerType.Float),
            new VertexAttribute(2, VertexAttribPointerType.Float),
            new VertexAttribute(4, VertexAttribPointerType.Float) };
        static readonly VertexAttribute[] __FromArray_attirbutes_PNTT = new[] {
            new VertexAttribute(3, VertexAttribPointerType.Float),
            new VertexAttribute(3, VertexAttribPointerType.Float),
            new VertexAttribute(2, VertexAttribPointerType.Float),
            new VertexAttribute(2, VertexAttribPointerType.Float) };
        public static MutableVertexBuffer FromArray(IVertex[] vertexes)
        {
            var vb = new MutableVertexBuffer();

            if (vertexes[0] is Vertex_PBiBwNT)
            {
                var size = Marshal.SizeOf(typeof(Vertex_PBiBwNT));
                var data = vertexes.Cast<Vertex_PBiBwNT>().ToArray();

                vb.SetVertexData(data, size, __FromArray_attirbutes_PBiBwNT);
            }
            else if (vertexes[0] is Vertex_PBiBwNTC)
            {
                var size = Marshal.SizeOf(typeof(Vertex_PBiBwNTC));
                var data = vertexes.Cast<Vertex_PBiBwNTC>().ToArray();

                vb.SetVertexData(data, size, __FromArray_attirbutes_PBiBwNTC);
            }
            else
            if (vertexes[0] is Vertex_PN)
            {
                var size = Marshal.SizeOf(typeof(Vertex_PN));
                var data = vertexes.Cast<Vertex_PN>().ToArray();
                vb.SetVertexData(data, size, __FromArray_attirbutes_PN);
            }
            else
            if (vertexes[0] is Vertex_PNT)
            {
                var size = Marshal.SizeOf(typeof(Vertex_PNT));
                var data = vertexes.Cast<Vertex_PNT>().ToArray();
                vb.SetVertexData(data, size, __FromArray_attirbutes_PNT);
            }
            else if (vertexes[0] is Vertex_PNTC)
            {
                var size = Marshal.SizeOf(typeof(Vertex_PNTC));
                var data = vertexes.Cast<Vertex_PNTC>().ToArray();
                vb.SetVertexData(data, size, __FromArray_attirbutes_PNTC);
            }
            else
            if (vertexes[0] is Vertex_PNTT)
            {
                var size = Marshal.SizeOf(typeof(Vertex_PNTT));
                var data = vertexes.Cast<Vertex_PNTT>().ToArray();
                vb.SetVertexData(data, size, __FromArray_attirbutes_PNTT);
            }
            else
            {
                throw new InvalidOperationException();
            }

            return vb;
        }
    }
}

