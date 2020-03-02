using System;
namespace ChamberLib
{
    public class Box<T>
        where T : struct
    {
        public Box(T initialValue=default(T))
        {
            Value = initialValue;
        }

        public T Value { get; set; }
    }
}
