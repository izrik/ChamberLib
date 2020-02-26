using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChamberLib
{
    public class ShaderUniforms
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

        public ShaderUniformType GetType(string name)
        {
            return entries[name].Type;
        }

        public bool ContainsName(string name)
        {
            return entries.ContainsKey(name);
        }

        public bool GetValueBool(string name)
        {
            return (bool)entries[name].Value;
        }
        public byte GetValueByte(string name)
        {
            return (byte)entries[name].Value;
        }
        public sbyte GetValueSByte(string name)
        {
            return (sbyte)entries[name].Value;
        }
        public short GetValueShort(string name)
        {
            return (short)entries[name].Value;
        }
        public ushort GetValueUShort(string name)
        {
            return (ushort)entries[name].Value;
        }
        public int GetValueInt(string name)
        {
            return (int)entries[name].Value;
        }
        public uint GetValueUInt(string name)
        {
            return (uint)entries[name].Value;
        }
        public float GetValueSingle(string name)
        {
            return (float)entries[name].Value;
        }
        public double GetValueDouble(string name)
        {
            return (double)entries[name].Value;
        }
        public Vector2 GetValueVector2(string name)
        {
            return (Vector2)entries[name].Value;
        }
        public Vector3 GetValueVector3(string name)
        {
            return (Vector3)entries[name].Value;
        }
        public Vector4 GetValueVector4(string name)
        {
            return (Vector4)entries[name].Value;
        }
        public Matrix GetValueMatrix(string name)
        {
            return (Matrix)entries[name].Value;
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

        public IEnumerable<string> GetUniformNames()
        {
            return entries.Keys;
        }
    }
}
