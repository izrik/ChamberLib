
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

namespace ChamberLib
{
    public class AnimationPlayer
    {
        public AnimationPlayer(RiggedEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("unit");

            Entity = entity;
        }

        public RiggedEntity Entity { get; protected set; }

        public float _currentTimeValue;
        public float CurrentTimeValue
        {
            get { return _currentTimeValue; }
        }

        public RepeatMode RepeatMode { get; protected set; }
        public AnimationSequence CurrentSequence { get; protected set; }
        public string CurrentSequenceName 
        {
            get { return (CurrentSequence != null ? CurrentSequence.Name : null); }
        }
        public float PlaybackRate { get; protected set; }
        float _lastTime=-1;
        AnimationFrame _lastFrame;

        public void StartAnimationSequence(AnimationSequence sequence, RepeatMode repeatMode, float playbackRate)
        {
            if (sequence == null) throw new ArgumentNullException("sequence");
            if (playbackRate <= 0) throw new ArgumentOutOfRangeException("playbackRate");

            CurrentSequence = sequence;
            _lastTime = 0;
            _lastFrame = null;
            RepeatMode = repeatMode;
            PlaybackRate = playbackRate;
        }

        public void Update(GameTime gameTime)
        {
            if (CurrentSequence != null)
            {
                float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

                deltaTime *= PlaybackRate;

                float time = _lastTime + deltaTime;

                if (time < 0)
                {
                    time = 0;
                }
                else if (time >= CurrentSequence.Duration)
                {
                    if (RepeatMode == RepeatMode.Once)
                    {
                        StopAnimation();

                        OnAnimationCompleted(EventArgs.Empty);
                    }
                    else if (RepeatMode == RepeatMode.Forever)
                    {
                        time %= CurrentSequence.Duration;

                        OnAnimationRepeated(EventArgs.Empty);
                    }
                    else
                    {
                        time = CurrentSequence.Duration;
                    }
                }

                _lastTime = time;

                if (CurrentSequence != null)
                {
                    AnimationFrame frame = SelectAnimationFrame(CurrentSequence, time);

                    if (frame != _lastFrame)
                    {
                        _lastFrame = frame;

                        Entity.SetAnimationFrame(frame);
                    }
                }
            }
        }

        public static AnimationFrame SelectAnimationFrame(AnimationSequence sequence, float time)
        {
            if (sequence == null) throw new ArgumentNullException("sequence");
            if (sequence.Frames == null ||
                sequence.Frames.Length < 1)
            {
                return null;
            }

            AnimationFrame frame = sequence.Frames[0];

            foreach (AnimationFrame f in sequence.Frames)
            {
                if (f.Time > time) break;
                frame = f;
            }

            return frame;
        }

        public void StopAnimation()
        {
            CurrentSequence = null;
            _lastFrame = null;
        }

        public event EventHandler AnimationRepeated;

        void OnAnimationRepeated(EventArgs e)
        {
            if (AnimationRepeated != null)
            {
                AnimationRepeated(this, e);
            }
        }

        public event EventHandler AnimationCompleted;

        public void OnAnimationCompleted(EventArgs e)
        {
            if (AnimationCompleted != null)
            {
                AnimationCompleted(this, e);
            }
        }
    }
}