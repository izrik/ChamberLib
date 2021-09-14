
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

namespace ChamberLib
{
    public class SettableActionSource<T> : IActionSource<T>
    {
        Dictionary<T, bool> _bools = new Dictionary<T, bool>();
        Dictionary<T, float> _floats = new Dictionary<T, float>();

        public virtual bool Get(T key)
        {
            if (_bools.ContainsKey(key))
            {
                return _bools[key];
            }
            else
            {
                return false;
            }
        }

        public virtual void Set(T key, bool value)
        {
            _bools[key] = value;
        }

        public virtual float GetAnalog(T key)
        {
            if (_floats.ContainsKey(key))
            {
                return _floats[key];
            }
            else
            {
                return 0;
            }
        }

        public virtual void SetAnalog(T key, float value)
        {
            _floats[key] = value;
        }

        public virtual void Remove(T key)
        {
            _bools.Remove(key);
        }

        public virtual void RemoveAnalog(T key)
        {
            _floats.Remove(key);
        }

        public virtual void ClearAll()
        {
            _bools.Clear();
            _floats.Clear();
        }
    }
}

