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
