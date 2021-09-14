using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChamberLib;

namespace ChamberLib
{
    public class AnimationData
    {
        public readonly Dictionary<string, AnimationSequence> Sequences;
        public readonly Matrix[] Transforms;
        public readonly Matrix[] AbsoluteTransforms;
        public readonly int[] SkeletonHierarchy;

        public AnimationData(
            Dictionary<string, AnimationSequence> sequences,
            List<Matrix> transforms,
            List<Matrix> absoluteTransforms,
            List<int> skeletonHierarchy)
        {
            Sequences = sequences;
            Transforms = transforms.ToArray();
            AbsoluteTransforms = absoluteTransforms.ToArray();

            // TODO: check for cycles
            // TODO: make sure it's monotonic increasing, or create a lookup table
            SkeletonHierarchy = skeletonHierarchy.ToArray();
        }
    }
}
