
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
using ChamberLib.Content;
using System.IO;

namespace ChamberLib.OpenTK.Content
{
    public class GlslShaderImporter
    {
        public GlslShaderImporter(ShaderStageImporter next=null)
        {
            this.next = next;
        }

        readonly ShaderStageImporter next;

        public ShaderContent ImportShaderStage(string filename, ShaderType type, IContentImporter importer)
        {
            if (File.Exists(filename))
            {
            }
            else if (type == ShaderType.Vertex && File.Exists(filename + ".vert"))
            {
                filename += ".vert";
            }
            else if (type == ShaderType.Fragment && File.Exists(filename + ".frag"))
            {
                filename += ".frag";
            }
            else if (next != null)
            {
                return next(filename, type, importer);
            }
            else
            {
                throw new FileNotFoundException(
                    string.Format(
                        "The {0} shader file could not be found: {1}",
                        type,
                        filename),
                    filename);
            }

            var source = File.ReadAllText(filename);

            return new ShaderContent(
                source: source,
                name: filename,
                type: type);
        }
    }
}

