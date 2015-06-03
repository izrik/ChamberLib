using System;

namespace ChamberLib
{
    public interface IShader
    {
        string Name { get; }

        void Apply();
        void UnApply();

        void SetUniform(string name, bool value);
        void SetUniform(string name, byte value);
        void SetUniform(string name, sbyte value);
        void SetUniform(string name, short value);
        void SetUniform(string name, ushort value);
        void SetUniform(string name, int value);
        void SetUniform(string name, uint value);
        void SetUniform(string name, long value);
        void SetUniform(string name, ulong value);
        void SetUniform(string name, float value);
        void SetUniform(string name, double value);
        void SetUniform(string name, Vector2 value);
        void SetUniform(string name, Vector3 value);
        void SetUniform(string name, Vector4 value);
        void SetUniform(string name, Matrix value);

        bool GetUniformBool(string name);
        byte GetUniformByte(string name);
        sbyte GetUniformSByte(string name);
        short GetUniformShort(string name);
        ushort GetUniformUShort(string name);
        int GetUniformInt(string name);
        uint GetUniformUInt(string name);
        long GetUniformLong(string name);
        ulong GetUniformULong(string name);
        float GetUniformSingle(string name);
        double GetUniformDouble(string name);
        Vector2 GetUniformVector2(string name);
        Vector3 GetUniformVector3(string name);
        Vector4 GetUniformVector4(string name);
        Matrix GetUniformMatrix(string name);
    }
}

