
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
    public struct STuple<T1, T2> : IEquatable<STuple<T1, T2>>
    {
        public STuple(T1 item1, T2 item2)
        {
            Item1 = item1;
            Item2 = item2;
        }

        public readonly T1 Item1;
        public readonly T2 Item2;

        public bool Equals(STuple<T1, T2> other)
        {
            return Item1.Equals(other.Item1) && Item2.Equals(other.Item2);
        }

        public override bool Equals(object obj)
        {
            if (obj is STuple<T1, T2>)
                return Equals((STuple<T1, T2>)obj);
            return false;
        }

        public override int GetHashCode()
        {
            return (Item1.GetHashCode() * 113) ^ Item2.GetHashCode();
        }
    }
}
