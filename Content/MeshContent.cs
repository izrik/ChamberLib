﻿using System;
using System.Collections.Generic;

namespace ChamberLib.Content
{
    public class MeshContent
    {
        public List<PartContent> Parts = new List<PartContent>();
        public BoneContent ParentBone;
        public string Name;
    }
}

