using System;
using System.Collections.Generic;
using System.Linq;

namespace ChamberLib
{
    public class Bone : IBone
    {
        public Bone()
        {
            Children = new ListWrapper<IBone, Bone>(this.children);
        }

        public string Name;
        public int Index { get; set; }
        public Matrix Transform { get; set; }

        public IBone Parent { get; set; }
        readonly List<IBone> children = new List<IBone>();
        public IList<Bone> Children;

        List<IBone> IBone.Children
        {
            get { return children; }
        }
    }
}

