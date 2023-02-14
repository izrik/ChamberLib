
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
    public class ComponentCollection
    {
        public ComponentCollection()
        {
        }

        readonly Dictionary<object, object> _dictionary = new Dictionary<object, object>();

        public T Get<T>()
            where T : class
        {
            if (_dictionary.ContainsKey(typeof(T)))
                return (T)_dictionary[typeof(T)];
            return null;
        }

        public T Get<T>(string key)
            where T : class
        {
            if (_dictionary.ContainsKey(key))
                return (T)_dictionary[key];
            return null;
        }

        public void Set<T>(T item)
            where T : class
        {
            _dictionary[typeof(T)] = item;
        }

        public object this[string key]
        {
            get { return _dictionary[key]; }
            set { _dictionary[key] = value; }
        }

        public void AddRange(object[] components)
        {
            if (components == null)
                return;
            foreach (var comp in components)
            {
                _dictionary[comp.GetType()] = comp;
            }
        }
    }
}
