using System;
using System.Collections.Generic;

namespace ChamberLib
{
    public class ListWrapper<T, U> : IList<U>
        where U : T
    {
        public ListWrapper(List<T> source)
        {
            this.source = source;
        }
        readonly List<T> source;

        #region IList implementation
        public int IndexOf(U item)
        {
            return source.IndexOf(item);
        }
        public void Insert(int index, U item)
        {
            source.Insert(index, item);
        }
        public void RemoveAt(int index)
        {
            source.RemoveAt(index);
        }
        public U this[int index]
        {
            get
            {
                return (U)source[index];
            }
            set
            {
                source[index] = value;
            }
        }
        #endregion

        #region ICollection implementation
        public void Add(U item)
        {
            source.Add(item);
        }
        public void Clear()
        {
            source.Clear();
        }
        public bool Contains(U item)
        {
            return source.Contains(item);
        }
        public void CopyTo(U[] array, int arrayIndex)
        {
            var array2 = new T[array.Length];
            source.CopyTo(array2, arrayIndex);
            int i;
            for (i = arrayIndex; i < array.Length; i++)
            {
                array[i] = (U)array2[i];
            }
        }
        public bool Remove(U item)
        {
            return source.Remove(item);
        }
        public int Count
        {
            get
            {
                return source.Count;
            }
        }
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }
        #endregion

        #region IEnumerable implementation
        public IEnumerator<U> GetEnumerator()
        {
            foreach (var item in source)
            {
                yield return (U)item;
            }
        }
        #endregion

        #region IEnumerable implementation
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return source.GetEnumerator();
        }
        #endregion
    }
}

