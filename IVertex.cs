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

        void SetPosition(       Vector3 value);
        void SetBlendIndices(   Vector4 value);
        void SetBlendWeights(   Vector4 value);
        void SetNormal(         Vector3 value);
        void SetTextureCoords(  Vector2 value);

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

        public void SetPosition(       Vector3 value) { Position = value; }
        public void SetBlendIndices(   Vector4 value) { BlendIndices = value; }
        public void SetBlendWeights(   Vector4 value) { BlendWeights = value; }
        public void SetNormal(         Vector3 value) { Normal = value; }
        public void SetTextureCoords(  Vector2 value) { TextureCoords = value; }

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

    public struct Vertex_PN : IVertex
    {
        public Vector3 Position;
        public Vector3 Normal;

        public Vector3 GetPosition() { return Position; }
        public Vector4 GetBlendIndices() { return Vector4.Zero; }
        public Vector4 GetBlendWeights() { return Vector4.Zero; }
        public Vector3 GetNormal() { return Normal; }
        public Vector2 GetTextureCoords() { return Vector2.Zero; }

        public void SetPosition(       Vector3 value) { Position = value; }
        public void SetBlendIndices(   Vector4 value) { }
        public void SetBlendWeights(   Vector4 value) { }
        public void SetNormal(         Vector3 value) { Normal = value; }
        public void SetTextureCoords(  Vector2 value) { }

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

        public void SetPosition(       Vector3 value) { Position = value; }
        public void SetBlendIndices(   Vector4 value) { }
        public void SetBlendWeights(   Vector4 value) { }
        public void SetNormal(         Vector3 value) { Normal = value; }
        public void SetTextureCoords(  Vector2 value) { TextureCoords = value; }

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

        public void SetPosition(       Vector3 value) { Position = value; }
        public void SetBlendIndices(   Vector4 value) { }
        public void SetBlendWeights(   Vector4 value) { }
        public void SetNormal(         Vector3 value) { Normal = value; }
        public void SetTextureCoords(  Vector2 value) { TextureCoords = value; }

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

