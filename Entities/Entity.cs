
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

namespace ChamberLib
{
    public abstract class Entity
    {
        static int __id = 0;
        protected Entity()
        {
            Id = __id;
            __id++;
        }

        public int Id { get; private set; }

        public virtual void Update(GameTime gameTime,
            ComponentCollection components) { }
        public virtual void Draw(GameTime gameTime,
            ComponentCollection components,
            Overrides overrides=default(Overrides)) { }

        // Position relative to parent (AttachedTo). If no parent, absolute
        // position in world space
        Vector3 _position = Vector3.Zero;
        public Vector3 LocalPosition
        {
            get { return _position; }
            set
            {
                _position = value;
                InvalidateLocalTransform();
            }
        }
        // Rotation relative to parent (AttachedTo). If no parent, absolute
        // rotation in world space
        Quaternion _rotation = Quaternion.Identity;
        public Quaternion LocalRotation
        {
            get { return _rotation; }
            set
            {
                _rotation = value;
                InvalidateLocalTransform();
            }
        }
        // Scale relative to parent (AttachedTo). If no parent, absolute
        // scale in world space
        Vector3 _scale = Vector3.One;
        public Vector3 LocalScale
        {
            get { return _scale; }
            set
            {
                _scale = value;
                InvalidateLocalTransform();
            }
        }

        // Absolute/global position, rotation, and scale, in world space
        public Vector3 Position
        {
            get { return Transform.DecomposedTranslation; }
        }
        public void SetPosition(Vector3 value)
        {
            if (IsAttached)
            {
                var value_ms = InverseTransform.Transform(value);
                var value_ls = LocalTransform.Transform(value_ms);
                LocalPosition = value_ls;
            }
            else
            {
                LocalPosition = value;
            }
        }
        public Quaternion Rotation
        {
            get { return Transform.DecomposedRotation; }
        }
        public void SetRotation(Quaternion value)
        {
            // TODO: take AttachedTo into account
            LocalRotation = value;
        }
        public Vector3 Scale
        {
            get { return Transform.DecomposedScale; }
        }
        public void SetScale(Vector3 value)
        {
            // TODO: take AttachedTo into account
            LocalScale = value;
        }

        private bool _mustRecalcLocalTransform = true;
        Matrix _localTransform = Matrix.Identity;
        public virtual Matrix LocalTransform
        {
            get
            {
                CheckRecalcLocalTransform();
                return _localTransform;
            }
        }
        protected void InvalidateLocalTransform()
        {
            _mustRecalcLocalTransform = true;
            InvalidateTransform();
        }
        protected void CheckRecalcLocalTransform()
        {
            if (_mustRecalcLocalTransform)
            {
                SetLocalTransform(CalcLocalTransform());
                _mustRecalcLocalTransform = false;
            }
        }
        protected void SetLocalTransform(Matrix value)
        {
            _localTransform = value;
            InvalidateTransform();
        }
        protected virtual Matrix CalcLocalTransform()
        {
            return Matrix.Compose(LocalScale, LocalRotation, LocalPosition);
        }

        Entity _attachedTo = null;
        public Entity AttachedTo
        {
            get { return _attachedTo; }
            private set
            {
                if (_attachedTo != value)
                {
                    if (_attachedTo != null)
                    {
                        _attachedTo.TransformInvalidated -= InvalidateTransform;
                        if (_attachedTo is RiggedEntity)
                            (_attachedTo as RiggedEntity).BoneTransformsInvalidated -= InvalidateTransform;
                    }
                    _attachedTo = value;
                    if (_attachedTo != null)
                    {
                        _attachedTo.TransformInvalidated += InvalidateTransform;
                        if (_attachedTo is RiggedEntity)
                            (_attachedTo as RiggedEntity).BoneTransformsInvalidated += InvalidateTransform;
                    }
                    InvalidateTransform();
                }
            }
        }
        public bool IsAttached
        {
            get { return (AttachedTo != null); }
        }
        string _attachedToBoneName = null;
        public string AttachedToBoneName
        {
            get { return _attachedToBoneName; }
            set
            {
                _attachedToBoneName = value;
                InvalidateTransform();
            }
        }
        public void Attach(Entity attachee, string boneName=null,
            bool retainGlobalPosRotScale=true)
        {
            var pos = Position;
            var rot = Rotation;
            var scale = Scale;

            AttachedTo = attachee;
            AttachedToBoneName = boneName;

            if (retainGlobalPosRotScale)
            {
                SetPosition(pos);
                SetRotation(rot);
                //SetScale(scale);
            }
        }
        public void Detach(bool retainGlobalPosRotScale=true)
        {
            Attach(null, null, retainGlobalPosRotScale);
        }

        private bool _mustRecalcTransform = true;
        protected Matrix _transform = Matrix.Identity;
        public Matrix Transform
        {
            get
            {
                CheckRecalcTransform();
                return _transform;
            }
        }
        private Matrix _inverseTransform = Matrix.Identity;
        public Matrix InverseTransform
        {
            get
            {
                CheckRecalcTransform();
                return _inverseTransform;
            }
        }
        protected void InvalidateTransform()
        {
            _mustRecalcTransform = true;
            TransformInvalidated?.Invoke();
        }
        protected void CheckRecalcTransform()
        {
            if (_mustRecalcTransform)
            {
                SetTransform(CalcTransform());
                _mustRecalcTransform = false;
            }
        }
        protected void SetTransform(Matrix value)
        {
            _transform = value;
            _inverseTransform = value.Inverted();
        }
        protected virtual Matrix CalcTransform()
        {
            if (!IsAttached)
                return LocalTransform;

            var parent = AttachedTo.Transform;
            if (!string.IsNullOrEmpty(AttachedToBoneName) &&
                AttachedTo is RiggedEntity)
            {
                var name = AttachedToBoneName;
                var attachedto = (AttachedTo as RiggedEntity);
                var bonemat = attachedto.GetWorldTransformOfBone(name);
                parent = bonemat * parent;
            }

            return LocalTransform * parent;
        }
        public event Action TransformInvalidated;

        public bool MustBeRemoved { get; protected set; }

        public virtual IModel Model { get; protected set; }
        public virtual void GetAllTransformedMeshBoundingSpheres(ICollection<Sphere> spheres)
        {
            if (Model != null)
            {
                foreach (var mesh in (Model as ChamberLib.OpenTK.Model).GetMeshes())
                {
                    spheres.Add(mesh.BoundingSphere.Transform(Transform));
                }
            }
        }
        public int CountTriangles()
        {
            return ((ChamberLib.OpenTK.Model)Model).CountTriangles();
        }

        public virtual bool CastsShadow => true;
        public virtual bool ShowsHighlight { get; set; } = true;
        public virtual bool ShowsCursor { get; set; } = true;

        //////////////////////////
        /// performance counters

        public int DrawCallTime;
        public int UpdateTime;
    }
}
