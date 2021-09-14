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
