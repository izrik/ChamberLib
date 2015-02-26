using System;
using System.Collections.Generic;

namespace ChamberLib.Content
{
    public class ModelContent
    {
        public List<MeshContent> Meshes = new List<MeshContent>();
        public List<BoneContent> Bones = new List<BoneContent>();
        public int RootBoneIndex;

        public List<IndexBufferContent> IndexBuffers = new List<IndexBufferContent>();
        public List<VertexBufferContent> VertexBuffers = new List<VertexBufferContent>();

        public string Filename;
    }
}

