
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

using System;
using System.Collections.Generic;
using System.Linq;

namespace ChamberLib
{
    public abstract class RiggedEntity : Entity
    {
        protected RiggedEntity()
        {
            AnimPlayer = new AnimationPlayer(this);
        }

        public IBone GetBoneByName(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IBone> EnumerateBones()
        {
            return Model.EnumerateBones();
        }

        public Matrix GetWorldTransformOfBone(string name)
        {
            var bone = Model.EnumerateBones().FirstOrDefault(b => b.Name == name);
            if (bone == null)
            {
                throw new ArgumentOutOfRangeException(
                    string.Format("No bone by the name of \"{0}\"", name));
            }

            return GetWorldTransforms()[bone.Index];
        }


        protected abstract Matrix[] AnimationTransforms { get; }
        protected abstract Matrix[] AnimationAbsoluteTransforms { get; }
        protected abstract int[] AnimationSkeletonHierarchy { get; }

        public void UpdateTransforms(Matrix[] transforms)
        {
            if (transforms == null) throw new ArgumentNullException("transforms");
            if (transforms.Length < 1) throw new ArgumentOutOfRangeException("transforms");

            _transforms = transforms;
            InvalidateBoneTransforms();
        }

        Matrix[] _skinTransforms;
        Matrix[] _worldTransforms;
        Matrix[] _transforms;
        bool _mustRecalcSkinTransforms = true;

        public virtual Matrix[] GetSkinTransforms()
        {
            CheckRecalcBoneTransforms();
            return _skinTransforms;
        }
        public virtual Matrix[] GetWorldTransforms()
        {
            CheckRecalcBoneTransforms();
            return _worldTransforms;
        }
        protected void InvalidateBoneTransforms()
        {
            _mustRecalcSkinTransforms = true;
            BoneTransformsInvalidated?.Invoke();
        }
        protected void CheckRecalcBoneTransforms()
        {
            if (_mustRecalcSkinTransforms)
            {
                if (_transforms == null)
                    InitTransforms();

                CalculateSkinTransforms(
                    _transforms,
                    AnimationAbsoluteTransforms,
                    AnimationSkeletonHierarchy,
                    ref _skinTransforms,
                    ref _worldTransforms);
                _mustRecalcSkinTransforms = false;
            }
        }
        public event Action BoneTransformsInvalidated;

        public static void CalculateSkinTransforms(Matrix[] transforms, Matrix[] absoluteTransforms, int[] skeletonHierarchy, ref Matrix[] skinTransforms, ref Matrix[] worldTransforms)
        {
            if (worldTransforms == null ||
                worldTransforms.Length < transforms.Length)
            {
                worldTransforms = new Matrix[transforms.Length];
            }

            int i;
            for (i = 0; i < transforms.Length; i++)
            {
                var p = skeletonHierarchy[i];
                if (p < 0)
                {
                    worldTransforms[i] = transforms[i];
                }
                else
                {
                    worldTransforms[i] = transforms[i] * worldTransforms[p];
                }
            }

            if (skinTransforms == null ||
                skinTransforms.Length < transforms.Length)
            {
                skinTransforms = new Matrix[transforms.Length];
            }
            for (i = 0; i < transforms.Length; i++)
            {
                if (i >= skinTransforms.Length ||
                    i >= absoluteTransforms.Length ||
                    i >= worldTransforms.Length)
                {
                    break;
                }
                skinTransforms[i] = absoluteTransforms[i] * worldTransforms[i];
            }
        }

        public void InitTransforms()
        {
            if (AnimationTransforms != null && AnimationTransforms.Length > 0)
            {
                UpdateTransforms(AnimationTransforms);
            }
        }

        public abstract AnimationSequence GetAnimationSequenceByName(string name);

        public void StartAnimationSequence(string clipName, RepeatMode repeatMode)
        {
            if (GetAnimationSequenceByName(clipName) == null)
                //throw new ArgumentException(string.Format("No clip by the name \"{0}\"", clipName));
                return;

            StartAnimationSequence(GetAnimationSequenceByName(clipName), repeatMode, 1);
        }
        public void StartAnimationSequence(string clipName, RepeatMode repeatMode, float playbackRate)
        {
            if (GetAnimationSequenceByName(clipName) == null)
                //throw new ArgumentException("No clip by that name");
                return;

            StartAnimationSequence(GetAnimationSequenceByName(clipName), repeatMode, playbackRate);
        }
        public void StartAnimationSequence(AnimationSequence sequence, RepeatMode repeatMode, float playbackRate)
        {
            if (sequence == null) throw new ArgumentNullException("sequence");
            if (playbackRate <= 0) throw new ArgumentOutOfRangeException("playbackRange", "playbackRate must be greater than zero");

            AnimPlayer.StartAnimationSequence(sequence, repeatMode, playbackRate);
        }

        public AnimationPlayer AnimPlayer;

        public void SetAnimationFrame(AnimationFrame frame)
        {
            UpdateTransforms(frame.Transforms);
        }

        public sealed override void Update(GameTime gameTime,
            ComponentCollection components)
        {
            InternalUpdate(gameTime, components);

            if (AnimPlayer != null)
            {
                AnimPlayer.Update(gameTime);
            }
        }
        protected virtual void InternalUpdate(GameTime gameTime,
            ComponentCollection components) { }
    }
}
