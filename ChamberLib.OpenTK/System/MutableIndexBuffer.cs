
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
using OpenTK.Graphics.OpenGL;

namespace ChamberLib.OpenTK.System
{
    public class MutableIndexBuffer : IIndexBuffer
    {
        public MutableIndexBuffer()
        {
            IndexBufferID = GL.GenBuffer();
            GLHelper.CheckError();
        }

        public int IndexBufferID { get; protected set; }
        public int IndexSizeInBytes { get; protected set; }
        public DrawElementsType DrawElementsType { get; protected set; }

        public void SetIndexData<T>(T[] indexData)
            where T : struct
        {
            SetIndexData(indexData, indexData.Length);
        }
        public void SetIndexData<T>(T[] indexData, int numIndexes)
            where T : struct
        {
            if (indexData == null)
            {
                throw new ArgumentNullException("indexData");
            }

            var tindex = typeof(T);
            if (tindex == typeof(int) || tindex == typeof(uint))
            {
                DrawElementsType = DrawElementsType.UnsignedInt;
                IndexSizeInBytes = sizeof(int);
            }
            else if (tindex == typeof(short) || tindex == typeof(ushort))
            {
                DrawElementsType = DrawElementsType.UnsignedShort;
                IndexSizeInBytes = sizeof(short);
            }
            else if (tindex == typeof(byte) || tindex == typeof(sbyte))
            {
                DrawElementsType = DrawElementsType.UnsignedByte;
                IndexSizeInBytes = sizeof(byte);
            }
            else
            {
                throw new ArgumentException(
                    string.Format(
                        "Index type is {0}. The indexes must be of integral " +
                        "type (int, uint, short, ushort, byte, sbyte)",
                        tindex),
                    "T");
            }


            GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexBufferID);
            GLHelper.CheckError();

            GL.BufferData<T>(BufferTarget.ElementArrayBuffer,
                new IntPtr(numIndexes * IndexSizeInBytes),
                indexData, BufferUsageHint.StaticDraw);
            GLHelper.CheckError();

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GLHelper.CheckError();
        }

        public void Apply()
        {
//            if (!_isReady)
//            {
//                MakeReady();
//            }

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexBufferID);
            GLHelper.CheckError();
        }

        public void UnApply()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GLHelper.CheckError();
        }
    }
}

