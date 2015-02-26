using System;
using System.Collections.Generic;

namespace ChamberLib.Content
{
    public class BoneContent
    {
        public string Name;
        public Matrix Transform;
        public Matrix InverseBindPose;
        public List<int> ChildBoneIndexes = new List<int>();
    }
}

