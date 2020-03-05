using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChamberLib
{
    public class ShaderUniforms
    {
        static Dictionary<string, int> tokensByName = new Dictionary<string, int>();
        static List<string> namesByToken = new List<string>();
        public int GetTokenForName(string name)
        {
            if (tokensByName.ContainsKey(name))
                return tokensByName[name];
            int token = tokensByName.Count;
            tokensByName[name] = token;
            namesByToken[token] = name;
            return token;
        }
        public string GetNameFromToken(int token)
        {
            if (token >= namesByToken.Count ||
                token < 0)
            {
                return null;
            }
            return namesByToken[token];
        }

        public struct Entry
        {
            public Entry(string name, int token, ShaderUniformType type, int index)
            {
                Name = name;
                Token = token;
                Type = type;
                Index = index;
            }

            public readonly string Name;
            public readonly int Token;
            public readonly ShaderUniformType Type;
            public readonly int Index;
        }

        protected readonly Dictionary<int, Entry> entries = new Dictionary<int, Entry>();
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
            return GetType(GetTokenForName(name));
        }
        public ShaderUniformType GetType(int token)
        {
            return entries[token].Type;
        }

        public bool ContainsName(string name)
        {
            return ContainsToken(GetTokenForName(name));
        }
        public bool ContainsToken(int token)
        {
            return entries.ContainsKey(token);
        }

        public bool GetValueBool(string name)
        {
            return GetValueBool(GetTokenForName(name));
        }
        public bool GetValueBool(int token)
        {
            return boolValues[entries[token].Index];
        }
        public byte GetValueByte(string name)
        {
            return GetValueByte(GetTokenForName(name));
        }
        public byte GetValueByte(int token)
        {
            return byteValues[entries[token].Index];
        }
        public sbyte GetValueSByte(string name)
        {
            return GetValueSByte(GetTokenForName(name));
        }
        public sbyte GetValueSByte(int token)
        {
            return sbyteValues[entries[token].Index];
        }
        public short GetValueShort(string name)
        {
            return GetValueShort(GetTokenForName(name));
        }
        public short GetValueShort(int token)
        {
            return shortValues[entries[token].Index];
        }
        public ushort GetValueUShort(string name)
        {
            return GetValueUShort(GetTokenForName(name));
        }
        public ushort GetValueUShort(int token)
        {
            return ushortValues[entries[token].Index];
        }
        public int GetValueInt(string name)
        {
            return GetValueInt(GetTokenForName(name));
        }
        public int GetValueInt(int token)
        {
            return intValues[entries[token].Index];
        }
        public uint GetValueUInt(string name)
        {
            return GetValueUInt(GetTokenForName(name));
        }
        public uint GetValueUInt(int token)
        {
            return uintValues[entries[token].Index];
        }
        public float GetValueSingle(string name)
        {
            return GetValueSingle(GetTokenForName(name));
        }
        public float GetValueSingle(int token)
        {
            return singleValues[entries[token].Index];
        }
        public double GetValueDouble(string name)
        {
            return GetValueDouble(GetTokenForName(name));
        }
        public double GetValueDouble(int token)
        {
            return doubleValues[entries[token].Index];
        }
        public Vector2 GetValueVector2(string name)
        {
            return GetValueVector2(GetTokenForName(name));
        }
        public Vector2 GetValueVector2(int token)
        {
            return vector2Values[entries[token].Index];
        }
        public Vector3 GetValueVector3(string name)
        {
            return GetValueVector3(GetTokenForName(name));
        }
        public Vector3 GetValueVector3(int token)
        {
            return vector3Values[entries[token].Index];
        }
        public Vector4 GetValueVector4(string name)
        {
            return GetValueVector4(GetTokenForName(name));
        }
        public Vector4 GetValueVector4(int token)
        {
            return vector4Values[entries[token].Index];
        }
        public Matrix GetValueMatrix(string name)
        {
            return GetValueMatrix(GetTokenForName(name));
        }
        public Matrix GetValueMatrix(int token)
        {
            return matrixValues[entries[token].Index];
        }

        public void SetValue(string name, bool value)
        {
            SetValue(GetTokenForName(name), value);
        }
        public void SetValue(int token, bool value)
        { 
            if (boolValues == null) boolValues = new List<bool>();
            if (entries.ContainsKey(token))
            {
                boolValues[entries[token].Index] = value;
            }
            else
            {
                entries[token] = new Entry(GetNameFromToken(token), token, ShaderUniformType.Bool, boolValues.Count);
                boolValues.Add(value);
            }
        }
        public void SetValue(string name, byte value)
        {
            SetValue(GetTokenForName(name), value);
        }
        public void SetValue(int token, byte value)
        {
            if (byteValues == null) byteValues = new List<byte>();
            if (entries.ContainsKey(token))
            {
                byteValues[entries[token].Index] = value;
            }
            else
            {
                entries[token] = new Entry(GetNameFromToken(token), token, ShaderUniformType.Byte, byteValues.Count);
                byteValues.Add(value);
            }
        }
        public void SetValue(string name, sbyte value)
        {
            SetValue(GetTokenForName(name), value);
        }
        public void SetValue(int token, sbyte value)
        {
            if (sbyteValues == null) sbyteValues = new List<sbyte>();
            if (entries.ContainsKey(token))
            {
                sbyteValues[entries[token].Index] = value;
            }
            else
            {
                entries[token] = new Entry(GetNameFromToken(token), token, ShaderUniformType.SByte, sbyteValues.Count);
                sbyteValues.Add(value);
            }
        }
        public void SetValue(string name, short value)
        {
            SetValue(GetTokenForName(name), value);
        }
        public void SetValue(int token, short value)
        {
            if (shortValues == null) shortValues = new List<short>();
            if (entries.ContainsKey(token))
            {
                shortValues[entries[token].Index] = value;
            }
            else
            {
                entries[token] = new Entry(GetNameFromToken(token), token, ShaderUniformType.Short, shortValues.Count);
                shortValues.Add(value);
            }
        }
        public void SetValue(string name, ushort value)
        {
            SetValue(GetTokenForName(name), value);
        }
        public void SetValue(int token, ushort value)
        {
            if (ushortValues == null) ushortValues = new List<ushort>();
            if (entries.ContainsKey(token))
            {
                ushortValues[entries[token].Index] = value;
            }
            else
            {
                entries[token] = new Entry(GetNameFromToken(token), token, ShaderUniformType.UShort, ushortValues.Count);
                ushortValues.Add(value);
            }
        }
        public void SetValue(string name, int value)
        {
            SetValue(GetTokenForName(name), value);
        }
        public void SetValue(int token, int value)
        {
            if (intValues == null) intValues = new List<int>();
            if (entries.ContainsKey(token))
            {
                intValues[entries[token].Index] = value;
            }
            else
            {
                entries[token] = new Entry(GetNameFromToken(token), token, ShaderUniformType.Int, intValues.Count);
                intValues.Add(value);
            }
        }
        public void SetValue(string name, uint value)
        {
            SetValue(GetTokenForName(name), value);
        }
        public void SetValue(int token, uint value)
        {
            if (uintValues == null) uintValues = new List<uint>();
            if (entries.ContainsKey(token))
            {
                uintValues[entries[token].Index] = value;
            }
            else
            {
                entries[token] = new Entry(GetNameFromToken(token), token, ShaderUniformType.UInt, uintValues.Count);
                uintValues.Add(value);
            }
        }
        public void SetValue(string name, float value)
        {
            SetValue(GetTokenForName(name), value);
        }
        public void SetValue(int token, float value)
        {
            if (singleValues == null) singleValues = new List<float>();
            if (entries.ContainsKey(token))
            {
                singleValues[entries[token].Index] = value;
            }
            else
            {
                entries[token] = new Entry(GetNameFromToken(token), token, ShaderUniformType.Single, singleValues.Count);
                singleValues.Add(value);
            }
        }
        public void SetValue(string name, double value)
        {
            SetValue(GetTokenForName(name), value);
        }
        public void SetValue(int token, double value)
        {
            if (doubleValues == null) doubleValues = new List<double>();
            if (entries.ContainsKey(token))
            {
                doubleValues[entries[token].Index] = value;
            }
            else
            {
                entries[token] = new Entry(GetNameFromToken(token), token, ShaderUniformType.Double, doubleValues.Count);
                doubleValues.Add(value);
            }
        }
        public void SetValue(string name, Vector2 value)
        {
            SetValue(GetTokenForName(name), value);
        }
        public void SetValue(int token, Vector2 value)
        {
            if (vector2Values == null) vector2Values = new List<Vector2>();
            if (entries.ContainsKey(token))
            {
                vector2Values[entries[token].Index] = value;
            }
            else
            {
                entries[token] = new Entry(GetNameFromToken(token), token, ShaderUniformType.Vector2, vector2Values.Count);
                vector2Values.Add(value);
            }
        }
        public void SetValue(string name, Vector3 value)
        {
            SetValue(GetTokenForName(name), value);
        }
        public void SetValue(int token, Vector3 value)
        {
            if (vector3Values == null) vector3Values = new List<Vector3>();
            if (entries.ContainsKey(token))
            {
                vector3Values[entries[token].Index] = value;
            }
            else
            {
                entries[token] = new Entry(GetNameFromToken(token), token, ShaderUniformType.Vector3, vector3Values.Count);
                vector3Values.Add(value);
            }
        }
        public void SetValue(string name, Vector4 value)
        {
            SetValue(GetTokenForName(name), value);
        }
        public void SetValue(int token, Vector4 value)
        {
            if (vector4Values == null) vector4Values = new List<Vector4>();
            if (entries.ContainsKey(token))
            {
                vector4Values[entries[token].Index] = value;
            }
            else
            {
                entries[token] = new Entry(GetNameFromToken(token), token, ShaderUniformType.Vector4, vector4Values.Count);
                vector4Values.Add(value);
            }
        }
        public void SetValue(string name, Matrix value)
        {
            SetValue(GetTokenForName(name), value);
        }
        public void SetValue(int token, Matrix value)
        {
            if (matrixValues == null) matrixValues = new List<Matrix>();
            if (entries.ContainsKey(token))
            {
                matrixValues[entries[token].Index] = value;
            }
            else
            {
                entries[token] = new Entry(GetNameFromToken(token), token, ShaderUniformType.Matrix, matrixValues.Count);
                matrixValues.Add(value);
            }
        }

        public Dictionary<string, Entry>.KeyCollection GetUniformNames()
        {
            throw new NotImplementedException();
        }
    }
}
