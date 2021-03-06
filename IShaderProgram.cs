﻿using System;
using System.Collections.Generic;

namespace ChamberLib
{
    public interface IShaderProgram
    {
        string Name { get; }

        void Apply(Overrides overrides=default(Overrides));
        void UnApply();

        IShaderStage VertexShader { get; }
        IShaderStage FragmentShader { get; }

        IEnumerable<string> BindAttributes { get; }
        void SetBindAttributes(IEnumerable<string> bindattrs);

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

