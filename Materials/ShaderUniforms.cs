
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
using System.Linq;
using System.Text;

namespace ChamberLib
{
    public class ShaderUniforms
    {
        static Dictionary<string, int> tokensByName = new Dictionary<string, int>();
        static List<string> namesByToken = new List<string>();
        public static int GetTokenForName(string name)
        {
            if (tokensByName.ContainsKey(name))
                return tokensByName[name];
            int token = tokensByName.Count;
            tokensByName[name] = token;
            namesByToken.Add(name);
            return token;
        }
        public static string GetNameFromToken(int token)
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
            public Entry(string name, int token, ShaderUniformType type, bool isArray, int index)
            {
                Name = name;
                Token = token;
                Type = type;
                IsArray = isArray;
                Index = index;
            }

            public readonly string Name;
            public readonly int Token;
            public readonly ShaderUniformType Type;
            public readonly bool IsArray;
            public readonly int Index;
        }

        protected readonly List<Entry> entries = new List<Entry>();
        protected readonly Dictionary<int, int> entryIndexesByToken = new Dictionary<int, int>();
        protected List<bool> boolValues;
        protected List<byte> byteValues;
        protected List<sbyte> sbyteValues;
        protected List<short> shortValues;
        protected List<ushort> ushortValues;
        protected List<int> intValues;
        protected List<int[]> intArrayValues;
        protected List<uint> uintValues;
        protected List<float> singleValues;
        protected List<double> doubleValues;
        protected List<Vector2> vector2Values;
        protected List<Vector3> vector3Values;
        protected List<Vector3[]> vector3ArrayValues;
        protected List<Vector4> vector4Values;
        protected List<Matrix> matrixValues;
        protected List<Matrix[]> matrixArrayValues;

        public ShaderUniformType GetType(string name)
        {
            return GetType(GetTokenForName(name));
        }
        public ShaderUniformType GetType(int token)
        {
            return entries[entryIndexesByToken[token]].Type;
        }

        public bool GetIsArray(int token)
        {
            return entries[entryIndexesByToken[token]].IsArray;
        }

        public bool ContainsName(string name)
        {
            return ContainsToken(GetTokenForName(name));
        }
        public bool ContainsToken(int token)
        {
            return entryIndexesByToken.ContainsKey(token);
        }

        public bool GetValueBool(string name)
        {
            return GetValueBool(GetTokenForName(name));
        }
        public bool GetValueBool(int token)
        {
            return boolValues[entries[entryIndexesByToken[token]].Index];
        }
        public byte GetValueByte(string name)
        {
            return GetValueByte(GetTokenForName(name));
        }
        public byte GetValueByte(int token)
        {
            return byteValues[entries[entryIndexesByToken[token]].Index];
        }
        public sbyte GetValueSByte(string name)
        {
            return GetValueSByte(GetTokenForName(name));
        }
        public sbyte GetValueSByte(int token)
        {
            return sbyteValues[entries[entryIndexesByToken[token]].Index];
        }
        public short GetValueShort(string name)
        {
            return GetValueShort(GetTokenForName(name));
        }
        public short GetValueShort(int token)
        {
            return shortValues[entries[entryIndexesByToken[token]].Index];
        }
        public ushort GetValueUShort(string name)
        {
            return GetValueUShort(GetTokenForName(name));
        }
        public ushort GetValueUShort(int token)
        {
            return ushortValues[entries[entryIndexesByToken[token]].Index];
        }
        public int GetValueInt(string name)
        {
            return GetValueInt(GetTokenForName(name));
        }
        public int GetValueInt(int token)
        {
            return intValues[entries[entryIndexesByToken[token]].Index];
        }
        public int[] GetValueIntArray(string name)
        {
            return GetValueIntArray(GetTokenForName(name));
        }
        public int[] GetValueIntArray(int token)
        {
            return intArrayValues[entries[entryIndexesByToken[token]].Index];
        }
        public uint GetValueUInt(string name)
        {
            return GetValueUInt(GetTokenForName(name));
        }
        public uint GetValueUInt(int token)
        {
            return uintValues[entries[entryIndexesByToken[token]].Index];
        }
        public float GetValueSingle(string name)
        {
            return GetValueSingle(GetTokenForName(name));
        }
        public float GetValueSingle(int token)
        {
            return singleValues[entries[entryIndexesByToken[token]].Index];
        }
        public double GetValueDouble(string name)
        {
            return GetValueDouble(GetTokenForName(name));
        }
        public double GetValueDouble(int token)
        {
            return doubleValues[entries[entryIndexesByToken[token]].Index];
        }
        public Vector2 GetValueVector2(string name)
        {
            return GetValueVector2(GetTokenForName(name));
        }
        public Vector2 GetValueVector2(int token)
        {
            return vector2Values[entries[entryIndexesByToken[token]].Index];
        }
        public Vector3 GetValueVector3(string name)
        {
            return GetValueVector3(GetTokenForName(name));
        }
        public Vector3 GetValueVector3(int token)
        {
            return vector3Values[entries[entryIndexesByToken[token]].Index];
        }
        public Vector3[] GetValueVector3Array(string name)
        {
            return GetValueVector3Array(GetTokenForName(name));
        }
        public Vector3[] GetValueVector3Array(int token)
        {
            return vector3ArrayValues[entries[entryIndexesByToken[token]].Index];
        }
        public Vector4 GetValueVector4(string name)
        {
            return GetValueVector4(GetTokenForName(name));
        }
        public Vector4 GetValueVector4(int token)
        {
            return vector4Values[entries[entryIndexesByToken[token]].Index];
        }
        public Matrix GetValueMatrix(string name)
        {
            return GetValueMatrix(GetTokenForName(name));
        }
        public Matrix GetValueMatrix(int token)
        {
            return matrixValues[entries[entryIndexesByToken[token]].Index];
        }
        public Matrix[] GetValueMatrixArray(int token)
        {
            return matrixArrayValues[entries[entryIndexesByToken[token]].Index];
        }

        protected void AddEntry(int token, ShaderUniformType type, int valueIndex)
        {
            AddEntry(token, type, false, valueIndex);
        }
        protected void AddEntry(int token, ShaderUniformType type, bool isArray, int valueIndex)
        {
            var index = entries.Count;
            entryIndexesByToken[token] = index;
            var entry = new Entry(GetNameFromToken(token), token, type, isArray, valueIndex);
            entries.Add(entry);
        }

        public void SetValue(string name, bool value)
        {
            SetValue(GetTokenForName(name), value);
        }
        public void SetValue(int token, bool value)
        { 
            if (boolValues == null) boolValues = new List<bool>();
            if (entryIndexesByToken.ContainsKey(token))
            {
                boolValues[entries[entryIndexesByToken[token]].Index] = value;
            }
            else
            {
                AddEntry(token, ShaderUniformType.Bool, boolValues.Count);
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
            if (entryIndexesByToken.ContainsKey(token))
            {
                byteValues[entries[entryIndexesByToken[token]].Index] = value;
            }
            else
            {
                AddEntry(token, ShaderUniformType.Byte, byteValues.Count);
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
            if (entryIndexesByToken.ContainsKey(token))
            {
                sbyteValues[entries[entryIndexesByToken[token]].Index] = value;
            }
            else
            {
                AddEntry(token, ShaderUniformType.SByte, sbyteValues.Count);
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
            if (entryIndexesByToken.ContainsKey(token))
            {
                shortValues[entries[entryIndexesByToken[token]].Index] = value;
            }
            else
            {
                AddEntry(token, ShaderUniformType.Short, shortValues.Count);
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
            if (entryIndexesByToken.ContainsKey(token))
            {
                ushortValues[entries[entryIndexesByToken[token]].Index] = value;
            }
            else
            {
                AddEntry(token, ShaderUniformType.UShort, ushortValues.Count);
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
            if (entryIndexesByToken.ContainsKey(token))
            {
                intValues[entries[entryIndexesByToken[token]].Index] = value;
            }
            else
            {
                AddEntry(token, ShaderUniformType.Int, intValues.Count);
                intValues.Add(value);
            }
        }
        public void SetValue(string name, int[] value)
        {
            SetValue(GetTokenForName(name), value);
        }
        public void SetValue(int token, int[] value)
        {
            if (intArrayValues == null) intArrayValues = new List<int[]>();
            if (entryIndexesByToken.ContainsKey(token))
            {
                intArrayValues[entries[entryIndexesByToken[token]].Index] = value;
            }
            else
            {
                AddEntry(token, ShaderUniformType.Int, true, intArrayValues.Count);
                intArrayValues.Add(value);
            }
        }
        public void SetValue(string name, uint value)
        {
            SetValue(GetTokenForName(name), value);
        }
        public void SetValue(int token, uint value)
        {
            if (uintValues == null) uintValues = new List<uint>();
            if (entryIndexesByToken.ContainsKey(token))
            {
                uintValues[entries[entryIndexesByToken[token]].Index] = value;
            }
            else
            {
                AddEntry(token, ShaderUniformType.UInt, uintValues.Count);
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
            if (entryIndexesByToken.ContainsKey(token))
            {
                singleValues[entries[entryIndexesByToken[token]].Index] = value;
            }
            else
            {
                AddEntry(token, ShaderUniformType.Single, singleValues.Count);
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
            if (entryIndexesByToken.ContainsKey(token))
            {
                doubleValues[entries[entryIndexesByToken[token]].Index] = value;
            }
            else
            {
                AddEntry(token, ShaderUniformType.Double, doubleValues.Count);
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
            if (entryIndexesByToken.ContainsKey(token))
            {
                vector2Values[entries[entryIndexesByToken[token]].Index] = value;
            }
            else
            {
                AddEntry(token, ShaderUniformType.Vector2, vector2Values.Count);
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
            if (entryIndexesByToken.ContainsKey(token))
            {
                vector3Values[entries[entryIndexesByToken[token]].Index] = value;
            }
            else
            {
                AddEntry(token, ShaderUniformType.Vector3, vector3Values.Count);
                vector3Values.Add(value);
            }
        }
        public void SetValue(string name, Vector3[] value)
        {
            SetValue(GetTokenForName(name), value);
        }
        public void SetValue(int token, Vector3[] value)
        {
            if (vector3ArrayValues == null) vector3ArrayValues = new List<Vector3[]>();
            if (entryIndexesByToken.ContainsKey(token))
            {
                vector3ArrayValues[entries[entryIndexesByToken[token]].Index] = value;
            }
            else
            {
                AddEntry(token, ShaderUniformType.Vector3, true, vector3ArrayValues.Count);
                vector3ArrayValues.Add(value);
            }
        }
        public void SetValue(string name, Vector4 value)
        {
            SetValue(GetTokenForName(name), value);
        }
        public void SetValue(int token, Vector4 value)
        {
            if (vector4Values == null) vector4Values = new List<Vector4>();
            if (entryIndexesByToken.ContainsKey(token))
            {
                vector4Values[entries[entryIndexesByToken[token]].Index] = value;
            }
            else
            {
                AddEntry(token, ShaderUniformType.Vector4, vector4Values.Count);
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
            if (entryIndexesByToken.ContainsKey(token))
            {
                matrixValues[entries[entryIndexesByToken[token]].Index] = value;
            }
            else
            {
                AddEntry(token, ShaderUniformType.Matrix, matrixValues.Count);
                matrixValues.Add(value);
            }
        }
        public void SetValue(string name, Matrix[] value)
        {
            SetValue(GetTokenForName(name), value);
        }
        public void SetValue(int token, Matrix[] value)
        {
            if (matrixArrayValues == null) matrixArrayValues = new List<Matrix[]>();
            if (entryIndexesByToken.ContainsKey(token))
            {
                matrixArrayValues[entries[entryIndexesByToken[token]].Index] = value;
            }
            else
            {
                AddEntry(token, ShaderUniformType.Matrix, true, matrixArrayValues.Count);
                matrixArrayValues.Add(value);
            }
        }

        public Dictionary<string, Entry>.KeyCollection GetUniformNames()
        {
            throw new NotImplementedException();
        }

        public List<Entry> GetEntries()
        {
            return entries;
        }
    }
}
