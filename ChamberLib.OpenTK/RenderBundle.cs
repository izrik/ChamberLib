using System;
using OpenTK.Graphics.OpenGL;

namespace ChamberLib.OpenTK
{
    public class RenderBundle
    {
        public int VertexArrayObjectID;
        public IVertexBuffer VertexBuffer;
        public IIndexBuffer IndexBuffer;

        public RenderBundle(IVertexBuffer vertexBuffer, IIndexBuffer indexBuffer)
        {
            if (indexBuffer == null) throw new ArgumentNullException("indexBuffer");
            if (vertexBuffer == null) throw new ArgumentNullException("vertexBuffer");

            VertexBuffer = vertexBuffer;
            IndexBuffer = indexBuffer;
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
            if (!isReady)
            {
                MakeReady();
            }

            GL.BindVertexArray(VertexArrayObjectID);
            GLHelper.CheckError();
        }

        public void UnApply()
        {
            GL.BindVertexArray(0);
            GLHelper.CheckError();
        }


        bool isReady = false;
        void MakeReady()
        {
            VertexBuffer.Apply();
            VertexBuffer.UnApply();

            VertexArrayObjectID = GL.GenVertexArray();
            GLHelper.CheckError();

            GL.BindVertexArray(0);
            GLHelper.CheckError();
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GLHelper.CheckError();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GLHelper.CheckError();

            GL.BindVertexArray(VertexArrayObjectID);
            GLHelper.CheckError();

            VertexBuffer.Apply();

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
//                        case VertexAttribPointerType.HalfFloat:
//                        case VertexAttribPointerType.Fixed:
//                        case VertexAttribPointerType.UnsignedInt2101010Rev:
//                        case VertexAttribPointerType.Int2101010Rev:
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

            IndexBuffer.Apply();



            GL.BindVertexArray(0);
            GLHelper.CheckError();

            VertexBuffer.UnApply();

            IndexBuffer.UnApply();

            isReady = true;
        }
    }
}
