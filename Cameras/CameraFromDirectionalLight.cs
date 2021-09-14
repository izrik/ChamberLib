
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
    public class CameraFromDirectionalLight : ICamera
    {
        public CameraFromDirectionalLight(DirectionalLight light)
        {
            if (light == null) throw new ArgumentNullException("light");

            Light = light;
        }

        public readonly DirectionalLight Light;

        Vector3 _position;
        public Vector3 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                _mustRecalcView = true;
            }
        }

        float _farPlaneDistance;
        public float FarPlaneDistance
        {
            get { return _farPlaneDistance; }
            set
            {
                _farPlaneDistance = value;
                _mustRecalcView = true;
                _mustRecalcProj = true;
            }
        }

        float _width;
        public float Width
        {
            get { return _width; }
            set
            {
                _width = value;
                _mustRecalcProj = true;
            }
        }

        float _height;
        public float Height
        {
            get { return _height; }
            set
            {
                _height = value;
                _mustRecalcProj = true;
            }
        }

        #region ICamera implementation

        Matrix _view;
        bool _mustRecalcView = true;
        public Matrix View
        {
            get
            {
                if (_mustRecalcView)
                {
                    _view = CalcView();
                    _mustRecalcView = false;
                }

                return _view;
            }
        }

        Matrix CalcView()
        {
            Vector3 lightz = Vector3.Normalize(Light.Direction);
            Vector3 lightx = Vector3.Normalize(Vector3.Cross(Vector3.UnitY, lightz));
            Vector3 lighty = Vector3.Normalize(Vector3.Cross(lightz, lightx));

            return Matrix.CreateLookAt(Position, Position + lightz, lighty); 
        }

        Matrix _proj;
        bool _mustRecalcProj = true;
        public Matrix Projection
        {
            get
            {
                if (_mustRecalcProj)
                {
                    _proj = CalcProj();
                    _mustRecalcProj = false;
                }

                return _proj;
            }
        }

        Matrix CalcProj()
        {
            return Matrix.CreateOrthographic(Width, Height, 0.05f, FarPlaneDistance);
        }

        #endregion

        List<Sphere> __meshSpheres = new List<Sphere>();
        public void FitViewRegion(List<Entity> entities)
        {
            Vector3 lightz = Vector3.Normalize(Light.Direction);
            Vector3 lightx = Vector3.Normalize(Vector3.Cross(Vector3.UnitY, lightz));
            Vector3 lighty = Vector3.Normalize(Vector3.Cross(lightz, lightx));

            Vector3 mins = Vector3.Zero;
            Vector3 maxs = Vector3.Zero;
            bool first = true;
            foreach (var ent in entities)
            {
                __meshSpheres.Clear();
                ent.GetAllTransformedMeshBoundingSpheres(__meshSpheres);
                foreach (var meshSphere in __meshSpheres)
                {
                    float x = Vector3.Dot(meshSphere.Center, lightx);
                    float y = Vector3.Dot(meshSphere.Center, lighty);
                    float z = Vector3.Dot(meshSphere.Center, lightz);
                    float r = meshSphere.Radius;
                    if (first)
                    {
                        maxs.X = x + r;
                        mins.X = x - r;
                        maxs.Y = y + r;
                        mins.Y = y - r;
                        maxs.Z = z + r;
                        mins.Z = z - r;
                        first = false;
                    }
                    else
                    {
                        if (x + r > maxs.X) maxs.X = x + r;
                        if (x - r < mins.X) mins.X = x - r;
                        if (y + r > maxs.Y) maxs.Y = y + r;
                        if (y - r < mins.Y) mins.Y = y - r;
                        if (z + r > maxs.Z) maxs.Z = z + r;
                        if (z - r < mins.Z) mins.Z = z - r;
                    }
                }
            }

            Vector3 midpoint = (maxs + mins) / 2;
            Vector3 sizes = maxs - mins;
            Vector3 lightPosition =
                midpoint.X * lightx +
                midpoint.Y * lighty +
                mins.Z * lightz;

            Position = lightPosition;

            Width = sizes.X;
            Height = sizes.Y;
            FarPlaneDistance = sizes.Z;
        }
    }
}

