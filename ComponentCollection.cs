using System;
using System.Collections.Generic;

namespace ChamberLib
{
    public class ComponentCollection
    {
        public ComponentCollection()
        {
        }

        readonly Dictionary<Type, object> _dictionary = new Dictionary<Type, object>();

        public T Get<T>()
            where T : class
        {
            if (_dictionary.ContainsKey(typeof(T)))
                return (T)_dictionary[typeof(T)];
            return null;
        }

        public void Set<T>(T item)
            where T : class
        {
            _dictionary[typeof(T)] = item;
        }
    }
}
