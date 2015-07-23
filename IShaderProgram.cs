using System;

namespace ChamberLib
{
    public interface IShaderProgram
    {
        string Name { get; }

        void Apply();
        void UnApply();

        IShaderStage VertexShader { get; }
        IShaderStage FragmentShader { get; }

        void SetUniform(string name, bool value);
        void SetUniform(string name, byte value);
        void SetUniform(string name, sbyte value);
        void SetUniform(string name, short value);
        void SetUniform(string name, ushort value);
        void SetUniform(string name, int value);
        void SetUniform(string name, uint value);
        void SetUniform(string name, float value);
        void SetUniform(string name, double value);
        void SetUniform(string name, Vector2 value);
        void SetUniform(string name, Vector3 value);
        void SetUniform(string name, Vector4 value);
        void SetUniform(string name, Matrix value);
    }
}

