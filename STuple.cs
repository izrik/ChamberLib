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
