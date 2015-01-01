using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChamberLib
{
    public static class Collection
    {
        public static void AddRange<T>(ICollection<T> collection, IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                collection.Add(item);
            }
        }

        public static void RemoveRange<T>(ICollection<T> collection, IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                collection.Remove(item);
            }
        }

        public static void AddRange<T, U>(this ICollection<T> collection, IEnumerable<U> items)
            where U : T
        {
            foreach (U item in items)
            {
                collection.Add(item);
            }
        }
        public static void RemoveRange<T, U>(this ICollection<T> collection, IEnumerable<U> items)
            where U : T
        {
            foreach (U item in items)
            {
                collection.Remove(item);
            }
        }
        public static void RemoveKeys<TKey, TValue, U>(this IDictionary<TKey, TValue> dictionary, IEnumerable<U> keys)
            where U : TKey
        {
            foreach (U key in keys)
            {
                dictionary.Remove(key);
            }
        }
        public static void EnqueueRange<T>(this Queue<T> queue, IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                queue.Enqueue(item);
            }
        }

        // This method is useful if you intend to modify a collection while/after iterating over it.
        public static IEnumerable<T> GetEnumeratorOfCopy<T>(this IEnumerable<T> collection)
        {
            var array = collection.ToArray();
            foreach (var item in array)
            {
                yield return item;
            }
        }

        static Random rand = new Random();
        public static void Shuffle<T>(this List<T> list)
        {
            if (list == null) throw new ArgumentNullException("list");

            if (list.Count < 1)
                return;

            int i;
            for (i = 0; i < list.Count; i++)
            {
                var j = rand.Next(i, list.Count);
                var temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }

        public static IEnumerable<T> BreadthFirstTraversal<T>(T start, Func<T, IEnumerable<T>> getChildren)
        {
            var queue = new Queue<T>();
            queue.Enqueue(start);
            while (queue.Count > 0)
            {
                var item = queue.Dequeue();
                yield return item;
                foreach (var child in getChildren(item))
                {
                    queue.Enqueue(child);
                }
            }
        }

        public static IEnumerable<T> DepthFirstTraversal<T>(T start, Func<T, IEnumerable<T>> getChildren)
        {
            yield return start;
            foreach (var child in getChildren(start))
            {
                foreach (var item in DepthFirstTraversal(child, getChildren))
                {
                    yield return item;
                }
            }
        }
    }
}
