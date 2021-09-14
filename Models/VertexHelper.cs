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

        public static Vertex_PBiBwNT[] ConvertPBiBwNT(this IVertex[] array)
        {
            return Array.ConvertAll<IVertex, Vertex_PBiBwNT>(array, v => new Vertex_PBiBwNT {
                Position = v.GetPosition(),
                BlendIndices = v.GetBlendIndices(),
                BlendWeights = v.GetBlendWeights(),
                Normal = v.GetNormal(),
                TextureCoords = v.GetTextureCoords(),
            });
        }

        public static Vertex_PBiBwNTC[] ConvertPBiBwNTC(this IVertex[] array)
        {
            return Array.ConvertAll<IVertex, Vertex_PBiBwNTC>(array, v => new Vertex_PBiBwNTC
            {
                Position = v.GetPosition(),
                BlendIndices = v.GetBlendIndices(),
                BlendWeights = v.GetBlendWeights(),
                Normal = v.GetNormal(),
                TextureCoords = v.GetTextureCoords(),
                Color = v.GetColor(),
            });
        }

        public static Vertex_PN[] ConvertPN(this IVertex[] array)
        {
            return Array.ConvertAll<IVertex, Vertex_PN>(array, v => new Vertex_PN {
                Position = v.GetPosition(),
                Normal = v.GetNormal(),
            });
        }

        public static Vertex_PNT[] ConvertPNT(this IVertex[] array)
        {
            return Array.ConvertAll<IVertex, Vertex_PNT>(array, v => new Vertex_PNT {
                Position = v.GetPosition(),
                Normal = v.GetNormal(),
                TextureCoords = v.GetTextureCoords(),
            });
        }

        public static Vertex_PNTC[] ConvertPNTC(this IVertex[] array)
        {
            return Array.ConvertAll<IVertex, Vertex_PNTC>(array, v => new Vertex_PNTC
            {
                Position = v.GetPosition(),
                Normal = v.GetNormal(),
                TextureCoords = v.GetTextureCoords(),
                Color = v.GetColor(),
            });
        }

        public static Vertex_PNTT[] ConvertPNTT(this IVertex[] array)
        {
            return Array.ConvertAll<IVertex, Vertex_PNTT>(array, v => new Vertex_PNTT {
                Position = v.GetPosition(),
                Normal = v.GetNormal(),
                TextureCoords = v.GetTextureCoords(),
            });
        }
    }
}
