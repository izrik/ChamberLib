
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
    public class CameraAboutTarget : ICamera
    {
        public CameraAboutTarget(string name)
        {
            Name = name;
        }

        Vector3 _target;
        public Vector3 Target
        {
            get { return _target; }
            set
            {
                if (value != _target)
                {
                    _target = value;
                    _mustUpdateView = true;
                }
            }
        }

        float _width = 9;
        public float Width
        {
            get { return _width; }
            set
            {
                _width = value;
                _mustUpdateProjection = true;
            }
        }

        float _height = 6;
        public float Height
        {
            get { return _height; }
            set
            {
                _height = value;
                _mustUpdateProjection = true;
            }
        }

        bool _isPerspective = false;
        public bool IsPerspective
        {
            get { return _isPerspective; }
            set
            {
                _isPerspective = value;
                _mustUpdateProjection = true;
            }
        }
        float _nearPlane = 0.05f;
        public float NearPlane
        {
            get { return _nearPlane; }
            set
            {
                _nearPlane = value;
                _mustUpdateProjection = true;
            }
        }
        float _farPlane = 50;
        public float FarPlane
        {
            get { return _farPlane; }
            set
            {
                _farPlane = value;
                _mustUpdateProjection = true;
            }
        }

        protected virtual Matrix CalcProjection()
        {
            if (IsPerspective)
            {
                return Matrix.CreatePerspective(Width, Height, NearPlane, FarPlane);
            }
            else
            {
                return Matrix.CreateOrthographic(Width, Height, NearPlane, FarPlane);
            }
        }

        public Matrix View { get; protected set; }
        public Matrix Projection { get; protected set; }

        public string Name { get; protected set; }

        //phi=0 corresponds to facing straight ahead 
        //phi=pi/2 corresponds to looking straight up
        //phi=-pi/2 corresponds to looking straight down
        public float _facingPhi;// = (float)(-Math.PI / 4);
        public float FacingPhi
        {
            get { return _facingPhi; }
            set
            {
                if (value != _facingPhi)
                {
                    _facingPhi = value;
                    _mustUpdateView = true;
                }
            }
        }

        //theta=0 corresponds to the +X direction;
        //theta=pi/2 corresponds to the +Z direction
        float _facingTheta;// = 0;
        public float FacingTheta
        {
            get { return _facingTheta; }
            set
            {
                if (value != _facingTheta)
                {
                    _facingTheta = value;
                    _mustUpdateView = true;
                }
            }
        }

        float _distance = 25;
        public float Distance
        {
            get { return _distance; }
            set
            {
                if (value != _distance)
                {
                    _distance = value;
                    _mustUpdateView = true;
                }
            }
        }

        protected bool _mustUpdateView = true;
        protected bool _mustUpdateProjection = true;

        public void ForceUpdate()
        {
            _mustUpdateView = true;
            _mustUpdateProjection = true;
        }

        Vector3 _cache_Target;

        public float MaxFacingPhi = (float)(Math.PI / 2);
        public float MinFacingPhi = (float)(-Math.PI / 2);

        public virtual void Update(GameTime gameTime)
        {
            if (FacingTheta < 0)
            {
                FacingTheta += (float)(2 * Math.PI);
            }
            if (FacingTheta >= (float)(2 * Math.PI))
            {
                FacingTheta -= (float)(2 * Math.PI);
            }
            if (FacingPhi < MinFacingPhi) FacingPhi = (float)(MinFacingPhi);// + 0.001f;
            if (FacingPhi > MaxFacingPhi) FacingPhi = (float)(MaxFacingPhi);// -0.001f;

            if (_mustUpdateView ||
                Target != _cache_Target)
            {
                _cache_Target = Target;
                Vector3 target = _cache_Target;

                var cameraPosition = CalcPositionRelativeToTarget();

                Vector3 up = Vector3.UnitY;
                //Vector3.Normalize(
                //    new Vector3(
                //        (float)Math.Sin(Math.PI + cameraPositionTheta) * (float)Math.Cos(Math.PI / 2 + cameraPositionPhi),
                //        (float)Math.Sin(Math.PI / 2 - cameraPositionPhi),
                //        (float)Math.Cos(Math.PI + cameraPositionTheta) * (float)Math.Cos(Math.PI / 2 + cameraPositionPhi)));

                View = Matrix.CreateLookAt((cameraPosition + target), target, up);
                _mustUpdateView = false;
            }

            if (_mustUpdateProjection)
            {
                Projection = CalcProjection();
                _mustUpdateProjection = false;
            }
        }

        public Vector3 CalcPositionRelativeToTarget()
        {
            float cameraPositionTheta = (float)(Math.PI + FacingTheta);
            float cameraPositionPhi = -FacingPhi;

            Vector3 cameraPosition =
                _distance * new Vector3(
                    (float)Math.Cos(cameraPositionTheta) * (float)Math.Cos(cameraPositionPhi),
                    (float)Math.Sin(cameraPositionPhi),
                    (float)Math.Sin(cameraPositionTheta) * (float)Math.Cos(cameraPositionPhi));

            return cameraPosition;
        }
    }
}
