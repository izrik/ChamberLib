
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
using System.IO;
using System.Collections.Generic;

namespace ChamberLib
{
    public interface IReader
    {
        string ReadLine();
    }

    public class TextReaderAdapter : IReader
    {
        public TextReaderAdapter(TextReader source)
        {
            Source = source;
        }

        public readonly TextReader Source;

        public string ReadLine()
        {
            return Source.ReadLine();
        }
    }

    public class SkipCommentsReader : IReader
    {
        public SkipCommentsReader(IReader source)
        {
            Source = source;
        }

        public readonly IReader Source;

        public string ReadLine()
        {
            var line = Source.ReadLine();
            while (line.Trim().StartsWith("#"))
            {
                line = Source.ReadLine();
            }

            return line;
        }
    }

    public class RememberingReader : IReader
    {
        public readonly IReader Source;
        public readonly List<string> Lines = new List<string>();

        public RememberingReader(IReader source)
        {
            Source = source;
        }

        public string ReadLine()
        {
            var line = Source.ReadLine();
            Lines.Add(line);
            return line;
        }
    }
}

