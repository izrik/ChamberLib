
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
using System.Collections.Generic;

namespace ChamberLib
{
    public interface IVertex
    {
        Vector3 GetPosition();
        Vector4 GetBlendIndices();
        Vector4 GetBlendWeights();
        Vector3 GetNormal();
        Vector2 GetTextureCoords();
        Vector4 GetColor();

        void SetPosition(       Vector3 value);
        void SetBlendIndices(   Vector4 value);
        void SetBlendWeights(   Vector4 value);
        void SetNormal(         Vector3 value);
        void SetTextureCoords(  Vector2 value);
        void SetColor(          Vector4 value);

        VertexDescription GetDescription();
    }

    [Flags]
    public enum VertexDescription
    {
        HasPosition=1,
        HasBlendIndices=2,
        HasBlendWeights=4,
        HasNormal=8,
        HasTextureCoords=16,
        HasTextureCoords2=32,
        HasColor=64,
    }

    public struct Vertex_PBiBwNT : IVertex
    {
        public Vector3 Position;
        public Vector4 BlendIndices;
        public Vector4 BlendWeights;
        public Vector3 Normal;
        public Vector2 TextureCoords;

        public Vector3 GetPosition() { return Position; }
        public Vector4 GetBlendIndices() { return BlendIndices; }
        public Vector4 GetBlendWeights() { return BlendWeights; }
        public Vector3 GetNormal() { return Normal; }
        public Vector2 GetTextureCoords() { return TextureCoords; }
        public Vector4 GetColor() { return new Vector4(0, 0, 0, 0); }

        public void SetPosition(       Vector3 value) { Position = value; }
        public void SetBlendIndices(   Vector4 value) { BlendIndices = value; }
        public void SetBlendWeights(   Vector4 value) { BlendWeights = value; }
        public void SetNormal(         Vector3 value) { Normal = value; }
        public void SetTextureCoords(  Vector2 value) { TextureCoords = value; }
        public void SetColor(          Vector4 value) { }

        public VertexDescription GetDescription()
        {
            return 
                VertexDescription.HasPosition | 
                VertexDescription.HasBlendIndices |
                VertexDescription.HasBlendWeights |
                VertexDescription.HasNormal |
                VertexDescription.HasTextureCoords;
        }

        public override string ToString()
        {
            return string.Format(
                "{0} {1} {2} {3} {4}",
                Position,
                BlendIndices,
                BlendWeights,
                Normal,
                TextureCoords);
        }
    }

    public struct Vertex_PBiBwNTC : IVertex
    {
        public Vector3 Position;
        public Vector4 BlendIndices;
        public Vector4 BlendWeights;
        public Vector3 Normal;
        public Vector2 TextureCoords;
        public Vector4 Color;

        public Vector3 GetPosition() { return Position; }
        public Vector4 GetBlendIndices() { return BlendIndices; }
        public Vector4 GetBlendWeights() { return BlendWeights; }
        public Vector3 GetNormal() { return Normal; }
        public Vector2 GetTextureCoords() { return TextureCoords; }
        public Vector4 GetColor() { return Color; }

        public void SetPosition(Vector3      value) { Position = value; }
        public void SetBlendIndices(Vector4  value) { BlendIndices = value; }
        public void SetBlendWeights(Vector4  value) { BlendWeights = value; }
        public void SetNormal(Vector3        value) { Normal = value; }
        public void SetTextureCoords(Vector2 value) { TextureCoords = value; }
        public void SetColor(Vector4         value) { Color = value; }

        public VertexDescription GetDescription()
        {
            return
                VertexDescription.HasPosition |
                VertexDescription.HasBlendIndices |
                VertexDescription.HasBlendWeights |
                VertexDescription.HasNormal |
                VertexDescription.HasTextureCoords|
                VertexDescription.HasColor;
        }

        public override string ToString()
        {
            return string.Format(
                "{0} {1} {2} {3} {4} {5}",
                Position,
                BlendIndices,
                BlendWeights,
                Normal,
                TextureCoords,
                Color);
        }
    }

    public struct Vertex_PN : IVertex
    {
        public Vector3 Position;
        public Vector3 Normal;

        public Vector3 GetPosition() { return Position; }
        public Vector4 GetBlendIndices() { return Vector4.Zero; }
        public Vector4 GetBlendWeights() { return Vector4.Zero; }
        public Vector3 GetNormal() { return Normal; }
        public Vector2 GetTextureCoords() { return Vector2.Zero; }
        public Vector4 GetColor() { return new Vector4(0, 0, 0, 0); }

        public void SetPosition(       Vector3 value) { Position = value; }
        public void SetBlendIndices(   Vector4 value) { }
        public void SetBlendWeights(   Vector4 value) { }
        public void SetNormal(         Vector3 value) { Normal = value; }
        public void SetTextureCoords(  Vector2 value) { }
        public void SetColor(          Vector4 value) { }

        public VertexDescription GetDescription()
        {
            return 
                VertexDescription.HasPosition |
                VertexDescription.HasNormal;
        }

        public override string ToString()
        {
            return string.Format(
                "{0} {1}",
                Position,
                Normal);
        }
    }

    public struct Vertex_PNT : IVertex
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector2 TextureCoords;

        public Vector3 GetPosition() { return Position; }
        public Vector4 GetBlendIndices() { return Vector4.Zero; }
        public Vector4 GetBlendWeights() { return Vector4.Zero; }
        public Vector3 GetNormal() { return Normal; }
        public Vector2 GetTextureCoords() { return TextureCoords; }
        public Vector4 GetColor() { return new Vector4(0, 0, 0, 0); }

        public void SetPosition(       Vector3 value) { Position = value; }
        public void SetBlendIndices(   Vector4 value) { }
        public void SetBlendWeights(   Vector4 value) { }
        public void SetNormal(         Vector3 value) { Normal = value; }
        public void SetTextureCoords(  Vector2 value) { TextureCoords = value; }
        public void SetColor(          Vector4 value) { }

        public VertexDescription GetDescription()
        {
            return 
                VertexDescription.HasPosition |
                VertexDescription.HasNormal |
                VertexDescription.HasTextureCoords;
        }

        public override string ToString()
        {
            return string.Format(
                "{0} {1} {2}",
                Position,
                Normal,
                TextureCoords);
        }
    }

    public struct Vertex_PNTC : IVertex
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector2 TextureCoords;
        public Vector4 Color;

        public Vector3 GetPosition() { return Position; }
        public Vector4 GetBlendIndices() { return Vector4.Zero; }
        public Vector4 GetBlendWeights() { return Vector4.Zero; }
        public Vector3 GetNormal() { return Normal; }
        public Vector2 GetTextureCoords() { return TextureCoords; }
        public Vector4 GetColor() { return Color; }

        public void SetPosition(Vector3      value) { Position = value; }
        public void SetBlendIndices(Vector4  value) { }
        public void SetBlendWeights(Vector4  value) { }
        public void SetNormal(Vector3        value) { Normal = value; }
        public void SetTextureCoords(Vector2 value) { TextureCoords = value; }
        public void SetColor(Vector4         value) { Color = value; }

        public VertexDescription GetDescription()
        {
            return
                VertexDescription.HasPosition |
                VertexDescription.HasNormal |
                VertexDescription.HasTextureCoords |
                VertexDescription.HasColor;
        }

        public override string ToString()
        {
            return string.Format(
                "{0} {1} {2}",
                Position,
                Normal,
                TextureCoords);
        }
    }

    public struct Vertex_PNTT : IVertex
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector2 TextureCoords;
        public Vector2 TextureCoords2;

        public Vector3 GetPosition() { return Position; }
        public Vector4 GetBlendIndices() { return Vector4.Zero; }
        public Vector4 GetBlendWeights() { return Vector4.Zero; }
        public Vector3 GetNormal() { return Normal; }
        public Vector2 GetTextureCoords() { return TextureCoords; }
        public Vector4 GetColor() { return new Vector4(0, 0, 0, 0); }

        public void SetPosition(       Vector3 value) { Position = value; }
        public void SetBlendIndices(   Vector4 value) { }
        public void SetBlendWeights(   Vector4 value) { }
        public void SetNormal(         Vector3 value) { Normal = value; }
        public void SetTextureCoords(  Vector2 value) { TextureCoords = value; }
        public void SetColor(          Vector4 value) { }

        public VertexDescription GetDescription()
        {
            return 
                VertexDescription.HasPosition |
                VertexDescription.HasNormal |
                VertexDescription.HasTextureCoords |
                VertexDescription.HasTextureCoords2;
        }

        public override string ToString()
        {
            return string.Format(
                "{0} {1} {2} {3}",
                Position,
                Normal,
                TextureCoords,
                TextureCoords2);
        }
    }

    public class VertexList
    {
        public readonly List<IVertex> Vertices = new List<IVertex>();
        public VertexDescription Description;
    }
}

