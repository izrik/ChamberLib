using System;
using System.Collections.Generic;
using ChamberLib.Content;

namespace ChamberLib.OpenTK
{
    public class ContentResolver
    {
        public readonly Dictionary<VertexBufferContent, VertexBuffer> VertexBuffers = new Dictionary<VertexBufferContent, VertexBuffer>();
        public readonly Dictionary<IndexBufferContent, IndexBuffer> IndexBuffers = new Dictionary<IndexBufferContent, IndexBuffer>();
        public readonly Dictionary<BoneContent, Bone> Bones = new Dictionary<BoneContent, Bone>();
        public readonly Dictionary<MaterialContent, Material> Materials = new Dictionary<MaterialContent, Material>();
        public readonly Dictionary<MeshContent, Mesh> Meshes = new Dictionary<MeshContent, Mesh>();
        public readonly Dictionary<PartContent, Part> Parts = new Dictionary<PartContent, Part>();

        public void Add(VertexBufferContent from, VertexBuffer to)
        {
            VertexBuffers.Add(from, to);
        }
        public VertexBuffer Get(VertexBufferContent item)
        {
            return VertexBuffers[item];
        }

        public void Add(IndexBufferContent from, IndexBuffer to)
        {
            IndexBuffers.Add(from, to);
        }
        public IndexBuffer Get(IndexBufferContent item)
        {
            return IndexBuffers[item];
        }

        public void Add(BoneContent from, Bone to)
        {
            Bones.Add(from, to);
        }
        public Bone Get(BoneContent item)
        {
            return Bones[item];
        }

        public void Add(MaterialContent from, Material to)
        {
            Materials.Add(from, to);
        }
        public Material Get(MaterialContent item)
        {
            return Materials[item];
        }

        public void Add(MeshContent from, Mesh to)
        {
            Meshes.Add(from, to);
        }
        public Mesh Get(MeshContent item)
        {
            return Meshes[item];
        }

        public void Add(PartContent from, Part to)
        {
            Parts.Add(from, to);
        }
        public Part Get(PartContent item)
        {
            return Parts[item];
        }
    }
}

