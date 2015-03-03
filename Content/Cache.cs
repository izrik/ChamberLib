using System;
using System.Collections.Generic;

namespace ChamberLib
{
    public class Cache<TInput, TContent, TOutput>
    {
        public Cache(Func<TInput, TContent, TOutput> next)
        {
            if (next == null) throw new ArgumentNullException("next");

            this.next = next;
        }

        readonly Func<TInput, TContent, TOutput> next;

        readonly Dictionary<TInput, TOutput> cache = new Dictionary<TInput, TOutput>();

        public TOutput Call(TInput input, TContent content)
        {
            if (cache.ContainsKey(input))
                return cache[input];

            var output = next(input, content);
            cache[input] = output;
            return output;
        }
    }
}

