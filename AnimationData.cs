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
        public readonly List<Matrix> Transforms;
        public readonly List<Matrix> AbsoluteTransforms;
        public readonly List<int> SkeletonHierarchy;

        public AnimationData(
            Dictionary<string, AnimationSequence> sequences,
            List<Matrix> transforms,
            List<Matrix> absoluteTransforms,
            List<int> skeletonHierarchy)
        {
            Sequences = sequences;
            Transforms = transforms;
            AbsoluteTransforms = absoluteTransforms;
            SkeletonHierarchy = skeletonHierarchy;
        }
    }
}
