using System;
using System.Collections.Generic;
using System.Linq;

namespace ChamberLib
{
    public class Bone : IBone
    {
        public string Name;
        public int Index { get; set; }
        public Matrix Transform { get; set; }

        public IBone Parent { get; set; }
        public List<Bone> Children = new List<Bone>();

        List<IBone> IBone.Children
        {
            get { return this.Children.Select(x => (IBone)x).ToList(); }
        }
    }
}

