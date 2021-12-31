using System;
using OpenTK.Graphics.OpenGL;
using ChamberLib.Content;
using System.Collections.Generic;

namespace ChamberLib.OpenTK.System
{
    public class IndexBuffer : IIndexBuffer
    {
        public IndexBuffer(IndexBufferContent indexData)
        {
            if (indexData == null)
                throw new ArgumentNullException("indexData");

            if (indexData.Indexes32 != null)
                Indexes32 = indexData.Indexes32;
            else if (indexData.Indexes16 != null)
                Indexes16 = indexData.Indexes16;
            else if (indexData.Indexes8 != null)
                Indexes8 = indexData.Indexes8;
            else
                throw new InvalidOperationException("No index data present");
        }
        public IndexBuffer(int[] indexData)
        {
            if (indexData == null)
                throw new ArgumentNullException("indexData");
            Indexes32 = indexData;
        }
        public IndexBuffer(short[] indexData)
        {
            if (indexData == null)
                throw new ArgumentNullException("indexData");
            Indexes16 = indexData;
        }
        public IndexBuffer(byte[] indexData)
        {
            if (indexData == null)
                throw new ArgumentNullException("indexData");
            Indexes8 = indexData;
        }

        public int IndexBufferID;

        public readonly int[] Indexes32;
        public readonly short[] Indexes16;
        public readonly byte[] Indexes8;

        public int IndexSizeInBytes
        {
            get
            {
                if (Indexes32 != null) return sizeof(int);
                if (Indexes16 != null) return sizeof(short);
                if (Indexes8 != null) return sizeof(byte);
                throw new InvalidOperationException("No index data present");
            }
        }

        public DrawElementsType DrawElementsType
        {
            get
            {
                if (Indexes32 != null) return DrawElementsType.UnsignedInt;
                if (Indexes16 != null) return DrawElementsType.UnsignedShort;
                if (Indexes8 != null) return DrawElementsType.UnsignedByte;
                throw new InvalidOperationException("No index data present");
            }
        }

        public int GetIndex(int indexOfIndex)
        {
            if (Indexes32 != null) return Indexes32[indexOfIndex];
            if (Indexes16 != null) return Indexes16[indexOfIndex];
            if (Indexes8 != null) return Indexes8[indexOfIndex];
            throw new InvalidOperationException("No index data present");
        }

        public int this [ int indexOfIndex ]
        {
            get => GetIndex(indexOfIndex);
        }

        public IEnumerable<int> EnumerateIndexes()
        {
            if (Indexes32 != null)
            {
                foreach (var index in Indexes32)
                    yield return index;
            }
            else if (Indexes16 != null)
            {
                foreach (var index in Indexes16)
                    yield return index;
            }
            else if (Indexes8 != null)
            {
                foreach (var index in Indexes8)
                    yield return index;
            }
            else
                throw new InvalidOperationException("No index data present");
        }

        public int Length
        {
            get
            {
                if (Indexes32 != null) return Indexes32.Length;
                if (Indexes16 != null) return Indexes16.Length;
                if (Indexes8 != null) return Indexes8.Length;
                throw new InvalidOperationException("No index data present");
            }
        }

        bool _isReady = false;
        void MakeReady()
        {
            IndexBufferID = GL.GenBuffer();
            GLHelper.CheckError();

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexBufferID);
            GLHelper.CheckError();

            if (Indexes32 != null)
                GL.BufferData<int>(
                    BufferTarget.ElementArrayBuffer,
                    new IntPtr(Indexes32.Length * IndexSizeInBytes),
                    Indexes32, BufferUsageHint.StaticDraw);
            else if (Indexes16 != null)
                GL.BufferData<short>(
                    BufferTarget.ElementArrayBuffer,
                    new IntPtr(Indexes16.Length * IndexSizeInBytes),
                    Indexes16, BufferUsageHint.StaticDraw);
            else if (Indexes8 != null)
                GL.BufferData<byte>(
                    BufferTarget.ElementArrayBuffer,
                    new IntPtr(Indexes8.Length * IndexSizeInBytes),
                    Indexes8, BufferUsageHint.StaticDraw);
            else
                throw new InvalidOperationException("No index data present");

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

