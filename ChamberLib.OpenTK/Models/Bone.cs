
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

using System.Collections.Generic;
using ChamberLib.Content;

namespace ChamberLib.OpenTK.Models
{
    public class Bone : IBone
    {
        public Bone(BoneContent bone)
        {
            Children = new ListWrapper<IBone, Bone>(this.children);
            this.Name = bone.Name;
            this.Transform = bone.Transform;
            this.InverseBindPose = bone.InverseBindPose;
            // children need to be connected outside of the constructor
        }

        public string Name { get; set; }
        public int Index { get; set; }
        public Matrix Transform { get; set; }
        public Matrix InverseBindPose;

        public IBone Parent { get; set; }
        readonly List<IBone> children = new List<IBone>();
        public IList<Bone> Children;

        List<IBone> IBone.Children
        {
            get { return children; }
        }

        public override string ToString()
        {
            return string.Format("[Bone: Name={0}, Parent={1}, Transform={2}]", Name, (Parent!=null?((Bone)Parent).Name:"null"), Transform);
        }
    }
}

