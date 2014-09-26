using System;
using OpenTK.Graphics.OpenGL;

namespace ChamberLib
{
    public class RenderBundle
    {
        public int VertexArrayObjectID;
        public VertexBuffer VertexBuffer;
        public IndexBuffer IndexBuffer;

        public RenderBundle()
            : this(new VertexBuffer(), new IndexBuffer())
        {
        }
        public RenderBundle(VertexBuffer vertexBuffer, IndexBuffer indexBuffer)
        {
            if (indexBuffer == null) throw new ArgumentNullException("indexBuffer");
            if (vertexBuffer == null) throw new ArgumentNullException("vertexBuffer");

            VertexBuffer = vertexBuffer;
            IndexBuffer = indexBuffer;

            VertexArrayObjectID = GL.GenVertexArray();
            GLHelper.CheckError();

            GL.BindVertexArray(0);
            GLHelper.CheckError();
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GLHelper.CheckError();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GLHelper.CheckError();

            BindBuffersToVao();
        }

        public static RenderBundle Create<TVertex, TIndex>(
            TVertex[] vertexData,
            int vertexSizeInBytes,
            VertexAttribPointerType vertexAttributeComponentType,
            int numVertexAttributeComponents,
            TIndex[] indexData)
            where TVertex : struct
            where TIndex : struct
        {
            var bundle = new RenderBundle();

            if (vertexData == null)
            {
                throw new ArgumentNullException("vertexData");
            }
            if (vertexSizeInBytes <= 0)
            {
                throw new ArgumentOutOfRangeException("vertexSizeInBytes", "vertexStrideInBytes must be greater than zero");
            }
            if (numVertexAttributeComponents <= 0)
            {
                throw new ArgumentOutOfRangeException("numVertexAttributeComponents", "numVertexAttributeComponents must be greater than zero");
            }
            if (vertexAttributeComponentType <= 0)
            {
                throw new ArgumentOutOfRangeException("vertexAttributeComponentType", "vertexAttributeComponentType must be greater than zero");
            }
            if (indexData == null)
            {
                throw new ArgumentNullException("indexData");
            }


            bundle.SetVertexData<TVertex>(
                vertexData,
                vertexSizeInBytes,
                vertexAttributeComponentType,
                numVertexAttributeComponents);

            bundle.SetIndexData<TIndex>(indexData);

            return bundle;
        }

        public void Draw(PrimitiveType primitiveType, int count, int firstIndexInTheIndexBuffer, int vertexOffset=0)
        {
            if (vertexOffset < 1)
            {
                GL.DrawElements(
                    primitiveType,
                    count,
                    DrawElementsType.UnsignedShort,
                    new IntPtr(firstIndexInTheIndexBuffer * IndexBuffer.IndexSizeInBytes));
                GLHelper.CheckError();
            }
            else
            {
                GL.DrawElementsBaseVertex(
                    primitiveType,
                    count,
                    DrawElementsType.UnsignedShort,
                    new IntPtr(firstIndexInTheIndexBuffer * IndexBuffer.IndexSizeInBytes),
                    vertexOffset);
            }
        }

        public void Apply()
        {
            GL.BindVertexArray(VertexArrayObjectID);
            GLHelper.CheckError();
        }

        public void UnApply()
        {
            GL.BindVertexArray(0);
            GLHelper.CheckError();
        }

        public void SetVertexData<TVertex>(
            TVertex[] vertexData,
            int vertexSizeInBytes,
            VertexAttribPointerType vertexAttributeComponentType,
            int numVertexAttributeComponents)
            where TVertex : struct
        {
            if (vertexData == null)
            {
                throw new ArgumentNullException("vertexData");
            }
            if (vertexSizeInBytes <= 0)
            {
                throw new ArgumentOutOfRangeException("vertexSizeInBytes", "vertexStrideInBytes must be greater than zero");
            }
            if (numVertexAttributeComponents <= 0)
            {
                throw new ArgumentOutOfRangeException("numVertexAttributeComponents", "numVertexAttributeComponents must be greater than zero");
            }
            if (vertexAttributeComponentType <= 0)
            {
                throw new ArgumentOutOfRangeException("vertexAttributeComponentType", "vertexAttributeComponentType must be greater than zero");
            }

            VertexBuffer.SetVertexData(vertexData, vertexSizeInBytes, vertexAttributeComponentType, numVertexAttributeComponents);

            BindBuffersToVao();
        }
        public void SetVertexData<TVertex>(
            TVertex[] vertexData,
            int vertexSizeInBytes,
            params VertexBuffer.VertexAttribute[] attributes)
            where TVertex : struct
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
                if (attr.OffsetInBytes < 0)
                {
                    throw new ArgumentOutOfRangeException("OffsetInBytes", "OffsetInBytes must not be less than zero");
                }
            }

            VertexBuffer.SetVertexData(vertexData, vertexSizeInBytes, attributes);

            BindBuffersToVao();
        }
        public void SetIndexData<TIndex>(TIndex[] indexData)
            where TIndex : struct
        {
            if (indexData == null)
            {
                throw new ArgumentNullException("indexData");
            }

            IndexBuffer.SetIndexData(indexData);

            BindBuffersToVao();
        }

        void BindBuffersToVao()
        {
            GL.BindVertexArray(VertexArrayObjectID);
            GLHelper.CheckError();

            if (VertexBuffer.VertexBufferID > 0 &&
                VertexBuffer.VertexAttibutes != null &&
                VertexBuffer.VertexAttibutes.Length > 0 &&
                VertexBuffer.VertexSizeInBytes > 0)
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBuffer.VertexBufferID);
                GLHelper.CheckError();

                int offset = 0;
                int k = 0;
                foreach (var attr in VertexBuffer.VertexAttibutes)
                {
                    int attrIndex;
                    if (attr.AttributeIndex.HasValue)
                    {
                        attrIndex = attr.AttributeIndex.Value;
                    }
                    else
                    {
                        attrIndex = k;
                        k++;
                    }

                    GL.EnableVertexAttribArray(attrIndex);
                    GLHelper.CheckError();
                    GL.VertexAttribPointer(
                        attrIndex,
                        attr.NumComponents,
                        attr.VertexAttribPointerType,
                        true,
                        VertexBuffer.VertexSizeInBytes,
                        offset);
                    GLHelper.CheckError();

                    if (!attr.OffsetInBytes.HasValue || attr.OffsetInBytes.Value == 0)
                    {
                        int x;
                        switch (attr.VertexAttribPointerType)
                        {
                            case VertexAttribPointerType.Byte:
                            case VertexAttribPointerType.UnsignedByte:
                                x = 1;
                                break;
                            case VertexAttribPointerType.Short:
                            case VertexAttribPointerType.UnsignedShort:
                                x = 2;
                                break;
                            case VertexAttribPointerType.Int:
                            case VertexAttribPointerType.UnsignedInt:
                            case VertexAttribPointerType.Float:
                                x = 4;
                                break;
                            case VertexAttribPointerType.Double:
                                x = 8;
                                break;
//                            case VertexAttribPointerType.HalfFloat:
//                            case VertexAttribPointerType.Fixed:
//                            case VertexAttribPointerType.UnsignedInt2101010Rev:
//                            case VertexAttribPointerType.Int2101010Rev:
                            default:
                                throw new InvalidOperationException(string.Format("Unknown vertex attribute pointer type: {0}", attr.VertexAttribPointerType));
                        }
                        offset += x * attr.NumComponents;
                    }
                    else
                    {
                        offset += attr.OffsetInBytes.Value;
                    }
                }
            }

            if (IndexBuffer.IndexBufferID > 0)
            {
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexBuffer.IndexBufferID);
                GLHelper.CheckError();
            }

            GL.BindVertexArray(0);
            GLHelper.CheckError();
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GLHelper.CheckError();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GLHelper.CheckError();
        }
    }
}

