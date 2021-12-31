
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
    public class DirectionalLight
    {
        public DirectionalLight()
        {
        }

        public DirectionalLight(
            Vector3 direction,
            Vector3 diffuseColor,
            Vector3 specularColor,
            bool enabled=true)
        {
            _direction = direction;
            _diffuseColor = diffuseColor;
            _specularColor = specularColor;
            _enabled = enabled;
        }

        Vector3 _diffuseColor;
        public Vector3 DiffuseColor
        {
            get { return _diffuseColor; }
            set
            {
                if (value != _diffuseColor)
                {
                    _diffuseColor = value;
                    OnLightChanged(EventArgs.Empty);
                }
            }
        }

        Vector3 _direction;
        public Vector3 Direction
        {
            get { return _direction; }
            set
            {
                if (value != _direction)
                {
                    _direction = value;
                    OnLightChanged(EventArgs.Empty);
                }
            }
        }

        bool _enabled;
        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                if (value != _enabled)
                {
                    _enabled = value;
                    OnLightChanged(EventArgs.Empty);
                }
            }
        }

        Vector3 _specularColor;
        public Vector3 SpecularColor
        {
            get { return _specularColor; }
            set
            {
                if (value != _specularColor)
                {
                    _specularColor = value;
                    OnLightChanged(EventArgs.Empty);
                }
            }
        }

        public event EventHandler LightChanged;

        protected void OnLightChanged(EventArgs e)
        {
            if (LightChanged != null)
            {
                LightChanged(this, e);
            }
        }
    }
}
