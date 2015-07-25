using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChamberLib
{
    public class ShaderUniforms : IEnumerable<ShaderUniforms.Entry>
    {
        public struct Entry
        {
            public Entry(string name, ShaderUniformType type, object value)
            {
                Name = name;
                Type = type;
                Value = value;
            }

            public readonly string Name;
            public readonly ShaderUniformType Type;
            public readonly object Value;
        }

        protected readonly Dictionary<string, Entry> entries = new Dictionary<string, Entry>();

        public Entry GetEntry(string name)
        {
            return entries[name];
        }

        public bool ContainsKey(string name)
        {
            return entries.ContainsKey(name);
        }

        public object GetValue(string name)
        {
            if (entries.ContainsKey(name))
                return entries[name].Value;
            return null;
        }
        public void SetValue(string name, bool value)
        {
            entries[name] = new Entry(name, ShaderUniformType.Bool, value);
        }
        public void SetValue(string name, byte value)
        {
            entries[name] = new Entry(name, ShaderUniformType.Byte, value);
        }
        public void SetValue(string name, sbyte value)
        {
            entries[name] = new Entry(name, ShaderUniformType.SByte, value);
        }
        public void SetValue(string name, short value)
        {
            entries[name] = new Entry(name, ShaderUniformType.Short, value);
        }
        public void SetValue(string name, ushort value)
        {
            entries[name] = new Entry(name, ShaderUniformType.UShort, value);
        }
        public void SetValue(string name, int value)
        {
            entries[name] = new Entry(name, ShaderUniformType.Int, value);
        }
        public void SetValue(string name, uint value)
        {
            entries[name] = new Entry(name, ShaderUniformType.UInt, value);
        }
        public void SetValue(string name, float value)
        {
            entries[name] = new Entry(name, ShaderUniformType.Single, value);
        }
        public void SetValue(string name, double value)
        {
            entries[name] = new Entry(name, ShaderUniformType.Double, value);
        }
        public void SetValue(string name, Vector2 value)
        {
            entries[name] = new Entry(name, ShaderUniformType.Vector2, value);
        }
        public void SetValue(string name, Vector3 value)
        {
            entries[name] = new Entry(name, ShaderUniformType.Vector3, value);
        }
        public void SetValue(string name, Vector4 value)
        {
            entries[name] = new Entry(name, ShaderUniformType.Vector4, value);
        }
        public void SetValue(string name, Matrix value)
        {
            entries[name] = new Entry(name, ShaderUniformType.Matrix, value);
        }

        public static ShaderUniformType GetTypeFromValue(object value)
        {
            if (value == null) throw new ArgumentNullException("value");

            var type = value.GetType();
            if (type == typeof(bool)) return ShaderUniformType.Bool;
            if (type == typeof(byte)) return ShaderUniformType.Byte;
            if (type == typeof(sbyte)) return ShaderUniformType.SByte;
            if (type == typeof(short)) return ShaderUniformType.Byte;
            if (type == typeof(ushort)) return ShaderUniformType.UShort;
            if (type == typeof(int)) return ShaderUniformType.Int;
            if (type == typeof(uint)) return ShaderUniformType.UInt;
            if (type == typeof(float)) return ShaderUniformType.Single;
            if (type == typeof(double)) return ShaderUniformType.Double;
            if (type == typeof(Vector2)) return ShaderUniformType.Vector2;
            if (type == typeof(Vector2)) return ShaderUniformType.Vector3;
            if (type == typeof(Vector4)) return ShaderUniformType.Vector4;
            if (type == typeof(Matrix)) return ShaderUniformType.Matrix;

            throw new ArgumentException("The value's type is not supported: " + type.FullName, "value");
        }

        public IEnumerable<string> GetUniformNames()
        {
            return entries.Keys;
        }

        public IEnumerator<ShaderUniforms.Entry> GetEnumerator()
        {
            return entries.Values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
