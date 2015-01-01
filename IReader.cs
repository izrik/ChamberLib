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

