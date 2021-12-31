
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

namespace ChamberLib.Content
{
    public class Cache<TInput, TOutput>
    {
        public Cache(Func<TInput, TOutput> next)
        {
            if (next == null) throw new ArgumentNullException("next");

            this.next = next;
        }

        readonly Func<TInput, TOutput> next;

        readonly Dictionary<TInput, TOutput> cache = new Dictionary<TInput, TOutput>();

        public TOutput Call(TInput input)
        {
            if (cache.ContainsKey(input))
                return cache[input];

            var output = next(input);
            cache[input] = output;
            return output;
        }

        public TInput LookupObject(TOutput item)
        {
            foreach (var kvp in cache)
            {
                if (kvp.Value.Equals(item))
                {
                    return kvp.Key;
                }
            }

            return default(TInput);
        }
    }

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

        public TInput LookupObject(TOutput item)
        {
            foreach (var kvp in cache)
            {
                if (kvp.Value.Equals(item))
                {
                    return kvp.Key;
                }
            }

            return default(TInput);
        }
    }

    public class Cache<TInput, TContent, TParam, TOutput>
    {
        public Cache(Func<TInput, TContent, TParam, TOutput> next)
        {
            if (next == null) throw new ArgumentNullException("next");

            this.next = next;
        }

        readonly Func<TInput, TContent, TParam, TOutput> next;

        readonly Dictionary<TInput, TOutput> cache = new Dictionary<TInput, TOutput>();

        public TOutput Call(TInput input, TContent content, TParam param)
        {
            if (cache.ContainsKey(input))
                return cache[input];

            var output = next(input, content, param);
            cache[input] = output;
            return output;
        }

        public TInput LookupObject(TOutput item)
        {
            foreach (var kvp in cache)
            {
                if (kvp.Value.Equals(item))
                {
                    return kvp.Key;
                }
            }

            return default(TInput);
        }
    }

    public class Cache2<TInput0, TInput1, TOutput>
    {
        public Cache2(Func<TInput0, TInput1, TOutput> next)
        {
            if (next == null) throw new ArgumentNullException("next");

            this.next = next;
        }

        readonly Func<TInput0, TInput1, TOutput> next;

        readonly Dictionary<Tuple<TInput0, TInput1>, TOutput> cache = new Dictionary<Tuple<TInput0, TInput1>, TOutput>();

        public TOutput Call(TInput0 input0, TInput1 input1)
        {
            var tuple = new Tuple<TInput0, TInput1>(input0, input1);
            if (cache.ContainsKey(tuple))
                return cache[tuple];

            var output = next(input0, input1);
            cache[tuple] = output;
            return output;
        }

        public Tuple<TInput0, TInput1> LookupObject(TOutput item)
        {
            foreach (var kvp in cache)
            {
                if (kvp.Value.Equals(item))
                {
                    return kvp.Key;
                }
            }

            return default(Tuple<TInput0, TInput1>);
        }
    }

    public class Cache2P<TInput0, TInput1, TParam, TOutput>
    {
        public Cache2P(Func<TInput0, TInput1, TParam, TOutput> next)
        {
            if (next == null) throw new ArgumentNullException("next");

            this.next = next;
        }

        readonly Func<TInput0, TInput1, TParam, TOutput> next;

        readonly Dictionary<Tuple<TInput0, TInput1>, TOutput> cache = new Dictionary<Tuple<TInput0, TInput1>, TOutput>();

        public TOutput Call(TInput0 input0, TInput1 input1, TParam param)
        {
            var tuple = new Tuple<TInput0, TInput1>(input0, input1);
            if (cache.ContainsKey(tuple))
                return cache[tuple];

            var output = next(input0, input1, param);
            cache[tuple] = output;
            return output;
        }

        public Tuple<TInput0, TInput1> LookupObject(TOutput item)
        {
            foreach (var kvp in cache)
            {
                if (kvp.Value.Equals(item))
                {
                    return kvp.Key;
                }
            }

            return default(Tuple<TInput0, TInput1>);
        }
    }

    public class Cache2<TInput0, TInput1, TContent, TOutput>
    {
        public Cache2(Func<TInput0, TInput1, TContent, TOutput> next)
        {
            if (next == null) throw new ArgumentNullException("next");

            this.next = next;
        }

        readonly Func<TInput0, TInput1, TContent, TOutput> next;

        readonly Dictionary<Tuple<TInput0, TInput1>, TOutput> cache = new Dictionary<Tuple<TInput0, TInput1>, TOutput>();

        public TOutput Call(TInput0 input0, TInput1 input1, TContent content)
        {
            var tuple = new Tuple<TInput0, TInput1>(input0, input1);
            if (cache.ContainsKey(tuple))
                return cache[tuple];

            var output = next(input0, input1, content);
            cache[tuple] = output;
            return output;
        }

        public Tuple<TInput0, TInput1> LookupObject(TOutput item)
        {
            foreach (var kvp in cache)
            {
                if (kvp.Value.Equals(item))
                {
                    return kvp.Key;
                }
            }

            return default(Tuple<TInput0, TInput1>);
        }
    }
}

