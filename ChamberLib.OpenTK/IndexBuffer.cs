using System;
using OpenTK.Graphics.OpenGL;

namespace ChamberLib
{
    public class IndexBuffer
    {
        public IndexBuffer()
        {
            IndexBufferID = GL.GenBuffer();
            GLHelper.CheckError();
        }

        public int IndexBufferID;
        public int IndexSizeInBytes;
        public DrawElementsType DrawElementsType;

        public void SetIndexData<T>(T[] indexData)
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
                new IntPtr(indexData.Length * IndexSizeInBytes),
                indexData, BufferUsageHint.StaticDraw);
            GLHelper.CheckError();

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GLHelper.CheckError();
        }

        public static IndexBuffer FromArray(short[] indexes)
        {
            var ib = new IndexBuffer();
            ib.SetIndexData(indexes);
            return ib;
        }
    }
}

