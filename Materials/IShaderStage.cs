
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

namespace ChamberLib
{
    public interface IShaderStage
    {
        string Name { get; }
        string Source { get; }
        ShaderType ShaderType { get; }

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
        void SetUniform(string name, Matrix[] value);
    }
}

