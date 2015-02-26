using System;
using OpenTK.Graphics.OpenGL;
using ChamberLib.Content;

namespace ChamberLib
{
    public class IndexBuffer : IIndexBuffer
    {
        public IndexBuffer(IndexBufferContent indexData)
            : this(indexData.Indexes)
        {
        }
        public IndexBuffer(short[] indexData)
        {
            if (indexData == null)
            {
                throw new ArgumentNullException("indexData");
            }

            IndexData = indexData;
        }

        public int IndexBufferID;
        public int IndexSizeInBytes { get; protected set; }
        public DrawElementsType DrawElementsType;

        public short[] IndexData;

        public static IndexBuffer FromArray(short[] indexes)
        {
            var ib = new IndexBuffer(indexes);
            return ib;
        }

        bool _isReady = false;
        void MakeReady()
        {
            IndexBufferID = GL.GenBuffer();
            GLHelper.CheckError();

            var tindex = IndexData.GetType().GetElementType();// typeof(T);
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

            GL.BufferData<short/*T*/>(BufferTarget.ElementArrayBuffer,
                new IntPtr(IndexData.Length * IndexSizeInBytes),
                IndexData, BufferUsageHint.StaticDraw);
            GLHelper.CheckError();

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GLHelper.CheckError();

            _isReady = true;
        }

        public void Apply()
        {
            if (!_isReady)
            {
                MakeReady();
            }

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

