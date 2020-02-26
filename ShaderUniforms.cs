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
            public Entry(string name, ShaderUniformType type, int index)
            {
                Name = name;
                Type = type;
                Index = index;
            }

            public readonly string Name;
            public readonly ShaderUniformType Type;
            public readonly int Index;
        }

        protected readonly Dictionary<string, Entry> entries = new Dictionary<string, Entry>();
        protected List<bool> boolValues;
        protected List<byte> byteValues;
        protected List<sbyte> sbyteValues;
        protected List<short> shortValues;
        protected List<ushort> ushortValues;
        protected List<int> intValues;
        protected List<uint> uintValues;
        protected List<float> singleValues;
        protected List<double> doubleValues;
        protected List<Vector2> vector2Values;
        protected List<Vector3> vector3Values;
        protected List<Vector4> vector4Values;
        protected List<Matrix> matrixValues;

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
            return boolValues[entries[name].Index];
        }
        public byte GetValueByte(string name)
        {
            return byteValues[entries[name].Index];
        }
        public sbyte GetValueSByte(string name)
        {
            return sbyteValues[entries[name].Index];
        }
        public short GetValueShort(string name)
        {
            return shortValues[entries[name].Index];
        }
        public ushort GetValueUShort(string name)
        {
            return ushortValues[entries[name].Index];
        }
        public int GetValueInt(string name)
        {
            return intValues[entries[name].Index];
        }
        public uint GetValueUInt(string name)
        {
            return uintValues[entries[name].Index];
        }
        public float GetValueSingle(string name)
        {
            return singleValues[entries[name].Index];
        }
        public double GetValueDouble(string name)
        {
            return doubleValues[entries[name].Index];
        }
        public Vector2 GetValueVector2(string name)
        {
            return vector2Values[entries[name].Index];
        }
        public Vector3 GetValueVector3(string name)
        {
            return vector3Values[entries[name].Index];
        }
        public Vector4 GetValueVector4(string name)
        {
            return vector4Values[entries[name].Index];
        }
        public Matrix GetValueMatrix(string name)
        {
            return matrixValues[entries[name].Index];
        }

        public void SetValue(string name, bool value)
        {
            if (boolValues == null) boolValues = new List<bool>();
            if (entries.ContainsKey(name))
            {
                boolValues[entries[name].Index] = value;
            }
            else
            {
                entries[name] = new Entry(name, ShaderUniformType.Bool, boolValues.Count);
                boolValues.Add(value);
            }
        }
        public void SetValue(string name, byte value)
        {
            if (byteValues == null) byteValues = new List<byte>();
            if (entries.ContainsKey(name))
            {
                byteValues[entries[name].Index] = value;
            }
            else
            {
                entries[name] = new Entry(name, ShaderUniformType.Byte, byteValues.Count);
                byteValues.Add(value);
            }
        }
        public void SetValue(string name, sbyte value)
        {
            if (sbyteValues == null) sbyteValues = new List<sbyte>();
            if (entries.ContainsKey(name))
            {
                sbyteValues[entries[name].Index] = value;
            }
            else
            {
                entries[name] = new Entry(name, ShaderUniformType.SByte, sbyteValues.Count);
                sbyteValues.Add(value);
            }
        }
        public void SetValue(string name, short value)
        {
            if (shortValues == null) shortValues = new List<short>();
            if (entries.ContainsKey(name))
            {
                shortValues[entries[name].Index] = value;
            }
            else
            {
                entries[name] = new Entry(name, ShaderUniformType.Short, shortValues.Count);
                shortValues.Add(value);
            }
        }
        public void SetValue(string name, ushort value)
        {
            if (ushortValues == null) ushortValues = new List<ushort>();
            if (entries.ContainsKey(name))
            {
                ushortValues[entries[name].Index] = value;
            }
            else
            {
                entries[name] = new Entry(name, ShaderUniformType.UShort, ushortValues.Count);
                ushortValues.Add(value);
            }
        }
        public void SetValue(string name, int value)
        {
            if (intValues == null) intValues = new List<int>();
            if (entries.ContainsKey(name))
            {
                intValues[entries[name].Index] = value;
            }
            else
            {
                entries[name] = new Entry(name, ShaderUniformType.Int, intValues.Count);
                intValues.Add(value);
            }
        }
        public void SetValue(string name, uint value)
        {
            if (uintValues == null) uintValues = new List<uint>();
            if (entries.ContainsKey(name))
            {
                uintValues[entries[name].Index] = value;
            }
            else
            {
                entries[name] = new Entry(name, ShaderUniformType.UInt, uintValues.Count);
                uintValues.Add(value);
            }
        }
        public void SetValue(string name, float value)
        {
            if (singleValues == null) singleValues = new List<float>();
            if (entries.ContainsKey(name))
            {
                singleValues[entries[name].Index] = value;
            }
            else
            {
                entries[name] = new Entry(name, ShaderUniformType.Single, singleValues.Count);
                singleValues.Add(value);
            }
        }
        public void SetValue(string name, double value)
        {
            if (doubleValues == null) doubleValues = new List<double>();
            if (entries.ContainsKey(name))
            {
                doubleValues[entries[name].Index] = value;
            }
            else
            {
                entries[name] = new Entry(name, ShaderUniformType.Double, doubleValues.Count);
                doubleValues.Add(value);
            }
        }
        public void SetValue(string name, Vector2 value)
        {
            if (vector2Values == null) vector2Values = new List<Vector2>();
            if (entries.ContainsKey(name))
            {
                vector2Values[entries[name].Index] = value;
            }
            else
            {
                entries[name] = new Entry(name, ShaderUniformType.Vector2, vector2Values.Count);
                vector2Values.Add(value);
            }
        }
        public void SetValue(string name, Vector3 value)
        {
            if (vector3Values == null) vector3Values = new List<Vector3>();
            if (entries.ContainsKey(name))
            {
                vector3Values[entries[name].Index] = value;
            }
            else
            {
                entries[name] = new Entry(name, ShaderUniformType.Vector3, vector3Values.Count);
                vector3Values.Add(value);
            }
        }
        public void SetValue(string name, Vector4 value)
        {
            if (vector4Values == null) vector4Values = new List<Vector4>();
            if (entries.ContainsKey(name))
            {
                vector4Values[entries[name].Index] = value;
            }
            else
            {
                entries[name] = new Entry(name, ShaderUniformType.Vector4, vector4Values.Count);
                vector4Values.Add(value);
            }
        }
        public void SetValue(string name, Matrix value)
        {
            if (matrixValues == null) matrixValues = new List<Matrix>();
            if (entries.ContainsKey(name))
            {
                matrixValues[entries[name].Index] = value;
            }
            else
            {
                entries[name] = new Entry(name, ShaderUniformType.Matrix, matrixValues.Count);
                matrixValues.Add(value);
            }
        }

        public IEnumerable<string> GetUniformNames()
        {
            return entries.Keys;
        }
    }
}
