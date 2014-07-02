using System;
using ChamberLib;
using System.Collections.Generic;

using VDesc = ChamberLib.VertexDescription;

namespace ChamberLib
{
    public static class VertexHelper
    {
        public static int GetVertexStrideInBytes(this IVertex vertex)
        {
            var desc = vertex.GetDescription();
            int size = 0;
            if ((desc & VDesc.HasPosition     ) == VDesc.HasPosition     ) size += 12;
            if ((desc & VDesc.HasBlendIndices ) == VDesc.HasBlendIndices ) size += 16;
            if ((desc & VDesc.HasBlendWeights ) == VDesc.HasBlendWeights ) size += 16;
            if ((desc & VDesc.HasNormal       ) == VDesc.HasNormal       ) size += 12;
            if ((desc & VDesc.HasTextureCoords) == VDesc.HasTextureCoords) size += 8;
            return size;
        }

        public static void Populate(this IVertex vertex, float[] values)
        {
            var desc = vertex.GetDescription();
            int i = 0;
            if ((desc & VDesc.HasPosition) == VDesc.HasPosition)
            { 
                var v = new Vector3(values[i], values[i + 1], values[i + 2]);
                i += 3;
                vertex.SetPosition(v);
            }
            if ((desc & VDesc.HasBlendIndices) == VDesc.HasBlendIndices)
            { 
                var v = new Vector4(values[i], values[i + 1], values[i + 2], values[i + 3]);
                i += 4;
                vertex.SetBlendIndices(v);
            }
            if ((desc & VDesc.HasBlendWeights) == VDesc.HasBlendWeights)
            { 
                var v = new Vector4(values[i], values[i + 1], values[i + 2], values[i + 3]);
                i += 4;
                vertex.SetBlendWeights(v);
            }
            if ((desc & VDesc.HasNormal) == VDesc.HasNormal)
            { 
                var v = new Vector3(values[i], values[i + 1], values[i + 2]);
                i += 3;
                vertex.SetNormal(v);
            }
            if ((desc & VDesc.HasTextureCoords) == VDesc.HasTextureCoords)
            { 
                var v = new Vector2(values[i], values[i + 1]);
                i += 2;
                vertex.SetTextureCoords(v);
            }
        }

        public static float[] GetValues(this IVertex vertex)
        {
            var desc = vertex.GetDescription();
            var values = new List<float>();
            if ((desc & VDesc.HasPosition) == VDesc.HasPosition)
            {
                var v = vertex.GetPosition();
                values.Add(v.X);
                values.Add(v.Y);
                values.Add(v.Z);
            }
            if ((desc & VDesc.HasBlendIndices) == VDesc.HasBlendIndices)
            {
                var v = vertex.GetBlendIndices();
                values.Add(v.X);
                values.Add(v.Y);
                values.Add(v.Z);
                values.Add(v.W);
            }
            if ((desc & VDesc.HasBlendWeights) == VDesc.HasBlendWeights)
            { 
                var v = vertex.GetBlendWeights();
                values.Add(v.X);
                values.Add(v.Y);
                values.Add(v.Z);
                values.Add(v.W);
            }
            if ((desc & VDesc.HasNormal) == VDesc.HasNormal)
            { 
                var v = vertex.GetNormal();
                values.Add(v.X);
                values.Add(v.Y);
                values.Add(v.Z);
            }
            if ((desc & VDesc.HasTextureCoords) == VDesc.HasTextureCoords)
            { 
                var v = vertex.GetTextureCoords();
                values.Add(v.X);
                values.Add(v.Y);
            }
            return values.ToArray();
        }
    }
}
