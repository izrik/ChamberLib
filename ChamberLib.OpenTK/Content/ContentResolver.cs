using System;
using System.Collections.Generic;
using ChamberLib.Content;
using ChamberLib.OpenTK.Materials;
using ChamberLib.OpenTK.Models;
using ChamberLib.OpenTK.System;

namespace ChamberLib.OpenTK.Content
{
    public class ContentResolver
    {
        public readonly Dictionary<VertexBufferContent, VertexBuffer> VertexBuffers = new Dictionary<VertexBufferContent, VertexBuffer>();
        public readonly Dictionary<IndexBufferContent, IndexBuffer> IndexBuffers = new Dictionary<IndexBufferContent, IndexBuffer>();
        public readonly Dictionary<BoneContent, Bone> Bones = new Dictionary<BoneContent, Bone>();
        public readonly Dictionary<VertexMaterialContent, VertexMaterial> VertexMaterials = new Dictionary<VertexMaterialContent, VertexMaterial>();
        public readonly Dictionary<FragmentMaterialContent, FragmentMaterial> FragmentMaterials = new Dictionary<FragmentMaterialContent, FragmentMaterial>();
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

        public void Add(VertexMaterialContent from, VertexMaterial to)
        {
            VertexMaterials.Add(from, to);
        }
        public VertexMaterial Get(VertexMaterialContent item)
        {
            return VertexMaterials[item];
        }

        public void Add(FragmentMaterialContent from, FragmentMaterial to)
        {
            FragmentMaterials.Add(from, to);
        }
        public FragmentMaterial Get(FragmentMaterialContent item)
        {
            return FragmentMaterials[item];
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

