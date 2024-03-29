﻿
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

using System.Collections.Generic;
using System.Linq;
using OpenTK.Graphics.OpenGL;
using ChamberLib.Content;
using _OpenTK = global::OpenTK;
using System.Threading;
using ChamberLib.OpenTK.Materials;
using ChamberLib.OpenTK.Math;
using ChamberLib.OpenTK.System;
using _Math = System.Math;

namespace ChamberLib.OpenTK.Text
{
    public partial class FontAdapter : IFont
    {
        static FontAdapter()
        {
            GenerateGlyphs();
        }

        public FontAdapter(FontContent font=null)
        {
        }

        public static readonly float CharacterWidth = 7;
        public static readonly float CharacterHeight = 14;
        public static readonly float LineHeight = 21;//CharacterHeight*1.5f;
        public static readonly float SpaceBetweenChars = 3;
        public static readonly float SpaceBetweenLines = 3;



        static int scaleLocation;
        static int characterSizeLocation;
        static int offsetLocation;
        static int screenWidthLocation;
        static int screenHeightLocation;
        static int fragmentColorLocation;

        static bool IsWordChar(char ch)
        {
            return !char.IsWhiteSpace(ch);
        }

        public struct Span
        {
            //TODO: Move this to its own file and namespace?

            // TODO: see Span<T> and Memory<T> from C# 7.2

            public Span(string s)
                : this(s, 0, s.Length)
            {
            }
            public Span(string s, int start, int length)
            {
                String = s;
                Start = start;
                Length = length;
                _value = null;
            }
            public Span(Span s, int start, int length)
            {
                String = s.String;
                Start = s.Start + start;
                Length = length;
                _value = null;
            }

            public readonly string String;
            public readonly int Start;
            public readonly int Length;

            public int End => Start + Length;

            public char this[int index] => String[index + Start];

            string _value;
            public string Value
            {
                get
                {
                    if (_value == null)
                        _value = String.Substring(Start, Length);
                    return _value;
                }
            }

            public Enumerator GetEnumerator()
            {
                return new Enumerator(this);
            }

            public struct Enumerator
            {
                public Enumerator(Span span)
                {
                    _span = span;
                    i = -1;
                }

                private readonly Span _span;
                private int i;

                public char Current => _span.String[i];

                public bool MoveNext()
                {
                    if (i < 0)
                        i = _span.Start;
                    else
                        i++;

                    if (i >= _span.End) return false;
                    return true;
                }

                public void Reset()
                {
                    i = -1;
                }
            }
        }

        public static void SplitLines(Span s, List<Span> spans)
        {
            int start = 0;
            int end = -1;
            int i;
            spans.Clear();
            for (i = 0; i < s.Length; i++)
            {
                if (s[i] == '\n')
                {
                    spans.Add(new Span(s, start, end - start + 1));
                    start = i + 1;
                    end = i;
                }
                else if (IsWordChar(s[i]))
                {
                    end = i;
                }
            }
            spans.Add(new Span(s, start, end - start + 1));
        }

        public static void SplitWords(Span line, List<Span> words)
        {
            words.Clear();

            if (line.Length < 1) return;

            int i;
            bool prevCharIsWord = IsWordChar(line[0]);
            bool curCharIsWord;
            int start = 0;
            for (i = 0; i < line.Length; i++)
            {
                curCharIsWord = IsWordChar(line[i]);
                if (curCharIsWord && !prevCharIsWord)
                {
                    // start new word
                    start = i;
                }
                else if (!curCharIsWord && prevCharIsWord)
                {
                    // end a word
                    words.Add(new Span(line, start, i - start));
                    start = line.Length;
                }
                prevCharIsWord = curCharIsWord;
            }

            if (prevCharIsWord && start<line.Length)
                words.Add(new Span(line, start, i - start));
        }

        public static float MeasureLineWidth(Span line,
            bool ignoreTrailingWhitespace=true)
        {
            if (line.Length < 1) return 0;

            int end = line.Length - 1;
            if (ignoreTrailingWhitespace)
                while (end > 0 && !IsWordChar(line[end]))
                    end--;
            //int i;
            //float width = 0;
            //for (i = 0; i < end; i++)
            //{
            //    width += 
            //}
            return (end + 1) * CharacterWidth + end * SpaceBetweenChars;

        }

        static readonly ThreadLocal<List<Span>> __WrapWords_words =
            new ThreadLocal<List<Span>>(() => new List<Span>());
        public static void WrapWords(List<Span> lines, float maxLineWidth)
        {
            int i;
            for (i = 0; i < lines.Count; i++)
            {
                var line = lines[i];

                var width = MeasureLineWidth(line);

                if (width <= maxLineWidth) continue;

                var words = __WrapWords_words.Value;
                words.Clear();
                SplitWords(line, words);

                int j;
                for (j = 1; j < words.Count; j++)
                {
                    var w = words[j];
                    var ws = new Span(line, 0, w.End - line.Start);
                    var ww = MeasureLineWidth(ws);
                    if (ww > maxLineWidth)
                    {
                        var w1 = words[j - 1];
                        var newPrevLine =
                            new Span(line, 0, w1.End - line.Start);
                        var newNextLine =
                            new Span(line, w.Start - line.Start,
                                line.End - w.Start);
                        lines[i] = newPrevLine;
                        lines.Insert(i + 1, newNextLine);
                        break;
                    }
                }
            }
        }

        readonly ThreadLocal<List<Span>> __MeasureString_lines =
            new ThreadLocal<List<Span>>(() => new List<Span>());
        public Vector2 MeasureString(string text,
            float? wrapWordsToMaxLineWidth=null)
        {
            if (string.IsNullOrEmpty(text)) return Vector2.Zero;
            return MeasureString(new Span(text), wrapWordsToMaxLineWidth);
        }
        public Vector2 MeasureString(Span text,
            float? wrapWordsToMaxLineWidth=null)
        {
            var lines = __MeasureString_lines.Value;
            SplitLines(text, lines);
            if (wrapWordsToMaxLineWidth.HasValue)
                WrapWords(lines,
                    wrapWordsToMaxLineWidth.Value);

            float maxWidth = 0;
            foreach(var line in lines)
            {
                maxWidth = _Math.Max(maxWidth, MeasureLineWidth(line));
            }

            int numlines = lines.Count;
            float height = numlines * LineHeight +
                (numlines - 1) * SpaceBetweenLines;

            return new Vector2(maxWidth, height);
        }

        public static bool IsReady = false;

        public void DrawString(Renderer renderer, string text,
            Vector2 position, Color color, float rotation, float scaleX,
            float scaleY, float? wrapWordsToMaxLineWidth=null,
            int? numCharsToDraw=null)
        {
            if (!IsReady)
            {
                MakeReady();
            }

            renderer.Reset2D();

            Shader.Apply();
            GLHelper.CheckError();
            var ProgramID = ((ShaderProgram)Shader).ProgramID;

            renderData.Apply();

            var p = position;
            var x = p.X;

            // set scale, char size, and color
            GL.Uniform2(scaleLocation, new Vector2(scaleX, scaleY).ToOpenTK());
            GLHelper.CheckError();
            GL.Uniform2(characterSizeLocation, new Vector2(CharacterWidth, CharacterHeight).ToOpenTK());
            GLHelper.CheckError();
            GL.Uniform1(screenWidthLocation, (float)renderer.Viewport.Width);
            GLHelper.CheckError();
            GL.Uniform1(screenHeightLocation, (float)renderer.Viewport.Height);
            GLHelper.CheckError();
            GL.Uniform4(fragmentColorLocation, color.ToVector4().ToOpenTK());
            GLHelper.CheckError();

            var s = new Span(text);
            var lines = __MeasureString_lines.Value;
            SplitLines(s, lines);
            if (wrapWordsToMaxLineWidth.HasValue)
                WrapWords(lines,
                    wrapWordsToMaxLineWidth.Value);

            int numCharsDrawn = 0;
            foreach (var line in lines)
            {
                foreach (var ch in line)
                {
                    GL.Uniform2(offsetLocation, p.ToOpenTK());
                    GLHelper.CheckError();

                    switch (ch)
                    {
                    case '\r':
                        continue;
                    case '\n':
                        continue;
                    default:
                        var glyph = GetGlyph(ch);

                        foreach (var segment in glyph.Segments)
                        {
                            renderData.Draw(
                                PrimitiveType.LineStrip,
                                segment.NumPrimitives + 1,
                                segment.StartIndex);
                            GLHelper.CheckError();

                        }
                        p = new Vector2(p.X + (CharacterWidth + SpaceBetweenChars) * scaleX, p.Y);
                        break;
                    }

                    numCharsDrawn++;
                    if (numCharsToDraw.HasValue &&
                        numCharsDrawn >= numCharsToDraw.Value)
                        break;
                }
                if (numCharsToDraw.HasValue &&
                    numCharsDrawn >= numCharsToDraw.Value)
                    break;
                p = new Vector2(x, p.Y + (LineHeight + SpaceBetweenLines) * scaleY);
            }

            renderData.UnApply();

            Shader.UnApply();
            GLHelper.CheckError();

        }

        static readonly VertexAttribute[] __MakeReady_vattr = new[] {
            new VertexAttribute(2, VertexAttribPointerType.Float)};
        static void MakeReady()
        {
            _OpenTK.Vector2[] vertexData = Vertexes.Select(v => v.ToOpenTK()).ToArray();
            int vertexSizeInBytes = _OpenTK.Vector2.SizeInBytes;
            int[] indexData = Indexes;

            /*
             * Shader
             *
             */
            var name = "$font";

            _DrawString_shader_vert_stage = new ShaderStage(
                _DrawString_shader_vert_source, ShaderType.Vertex, name);
            _DrawString_shader_frag_stage = new ShaderStage(
                _DrawString_shader_frag_source, ShaderType.Fragment, name);

            _DrawString_shader_vert_stage.SetBindAttributes(new[] {
                "in_position" });
            Shader = ShaderProgram.GetShaderProgram(
                _DrawString_shader_vert_stage,
                _DrawString_shader_frag_stage,
                name);

            Shader.Apply();

            scaleLocation = GL.GetUniformLocation(Shader.ProgramID, "scale");
            GLHelper.CheckError();
            characterSizeLocation = GL.GetUniformLocation(Shader.ProgramID, "character_size");
            GLHelper.CheckError();
            offsetLocation = GL.GetUniformLocation(Shader.ProgramID, "offset");
            GLHelper.CheckError();
            screenWidthLocation = GL.GetUniformLocation(Shader.ProgramID, "screenWidth");
            GLHelper.CheckError();
            screenHeightLocation = GL.GetUniformLocation(Shader.ProgramID, "screenHeight");
            GLHelper.CheckError();
            fragmentColorLocation = GL.GetUniformLocation(Shader.ProgramID, "fragment_color");
            GLHelper.CheckError();

            Shader.UnApply();

            vertexBuffer = new MutableVertexBuffer();
            vertexBuffer.SetVertexData(
                vertexData,
                vertexSizeInBytes,
                __MakeReady_vattr);
            indexBuffer = new IndexBuffer(indexData);
            renderData = new RenderBundle(vertexBuffer, indexBuffer);

            IsReady = true;
        }

        static RenderBundle renderData;
        static MutableVertexBuffer vertexBuffer;    // this needs to be Mutable because we're not using IVertex
        static IIndexBuffer indexBuffer;

        static ShaderProgram Shader;

        struct Glyph
        {
            public GlyphSegment[] Segments;
        }
        struct GlyphSegment
        {
            public int StartIndex;
            public int NumPrimitives;
        }

        static Vector2[][] Paths(params Vector2[][] paths)
        {
            return paths;
        }
        static Vector2[] Path(params Vector2[] points)
        {
            return points;
        }

        static Glyph GetGlyph(char ch)
        {
            if (Glyphs.ContainsKey(ch))
            {
                return Glyphs[ch];
            }

            missedCharacters.Add(ch);

            return Glyphs['\0'];
        }

        static readonly HashSet<char> missedCharacters = new HashSet<char>();

        static Dictionary<char, Vector2[][]> GeneratePaths()
        {
            var top = Vector2.UnitY;
            var bottom = Vector2.Zero;
            var left = Vector2.Zero;
            var right = Vector2.UnitX;
            var middle = top * 0.5f;
            var center = right * 0.5f;
            var tail = -middle;

            var qx = right * 0.25f;
            var q3x = right * 0.75f;
            var qy = top * 0.25f;
            var q3y = top * 0.75f;

            var topleft = top + left;
            var topcenter = top + center;
            var topright = top + right;
            var middleleft = middle + left;
            var middlecenter = middle + center;
            var middleright = middle + right;
            var bottomleft = bottom + left;
            var bottomcenter = bottom + center;
            var bottomright = bottom + right;
            var tailleft = tail + left;
            var tailright = tail + right;

            var paths = new Dictionary<char, Vector2[][]>();

            paths.Add('\0', Paths(Path(qx + qy, q3x + qy, q3x + q3y, qx + q3y, qx + qy))); // the 'missing' glyph

            paths.Add('A', Paths(Path(bottomleft, topcenter, bottomright), Path(middle + qx, middle + q3x)));
            paths.Add('B', Paths(Path(middleleft, middleright, bottomright, bottomleft, topleft, topcenter, middlecenter)));
            paths.Add('C', Paths(Path(bottomright, bottomleft, topleft, topright)));
            paths.Add('D', Paths(Path(bottomright, bottomleft, topleft, middleright, bottomright)));
            paths.Add('E', Paths(Path(bottomright, bottomleft, topleft, topright), Path(middleleft, middlecenter)));
            paths.Add('F', Paths(Path(bottomleft, topleft, topright), Path(middleleft, middlecenter)));
            paths.Add('G', Paths(Path(topright, topleft, bottomleft, bottomright, middleright, middlecenter)));
            paths.Add('H', Paths(Path(topleft, bottomleft), Path(topright, bottomright), Path(middleleft, middleright)));
            paths.Add('I', Paths(Path(topleft, topright), Path(bottomleft, bottomright), Path(topcenter, bottomcenter)));
            paths.Add('J', Paths(Path(topright, bottomright, bottomleft, middleleft)));
            paths.Add('K', Paths(Path(topleft, bottomleft), Path(topright, middleleft, bottomright)));
            paths.Add('L', Paths(Path(topleft, bottomleft, bottomright)));
            paths.Add('M', Paths(Path(bottomleft, topleft, middlecenter, topright, bottomright)));
            paths.Add('N', Paths(Path(bottomleft, topleft, bottomright, topright)));
            paths.Add('O', Paths(Path(bottomright, bottomleft, topleft, topright, bottomright)));
            paths.Add('P', Paths(Path(bottomleft, topleft, topright, middleright, middleleft)));
            paths.Add('Q', Paths(Path(bottomright, bottomleft, topleft, topright, bottomright), Path(qy + q3x, new Vector2(1.25f, -0.25f))));
            paths.Add('R', Paths(Path(bottomleft, topleft, topright, middleright, middleleft, bottomright)));
            paths.Add('S', Paths(Path(topright, topleft, middleleft, middleright, bottomright, bottomleft)));
            paths.Add('T', Paths(Path(topleft, topright), Path(topcenter, bottomcenter)));
            paths.Add('U', Paths(Path(topleft, bottomleft, bottomright, topright)));
            paths.Add('V', Paths(Path(topleft, bottomcenter, topright)));
            paths.Add('W', Paths(Path(topleft, bottomleft, middlecenter, bottomright, topright)));
            paths.Add('X', Paths(Path(topleft, bottomright), Path(topright, bottomleft)));
            paths.Add('Y', Paths(Path(topleft, middlecenter, topright), Path(middlecenter, bottomcenter)));
            paths.Add('Z', Paths(Path(topleft, topright, bottomleft, bottomright)));
            paths.Add('a', Paths(Path(middle + q3x, middleleft, bottomleft, q3x, middle + q3x, bottomright)));
            paths.Add('b', Paths(Path(topleft, bottomleft, bottomright, middleright, middleleft)));
            paths.Add('c', Paths(Path(middleright, middleleft, bottomleft, bottomright)));
            paths.Add('d', Paths(Path(middleright, middleleft, bottomleft, bottomright, topright)));
            paths.Add('e', Paths(Path(qy, qy + right, middleright, middleleft, bottomleft, bottomright)));
            paths.Add('f', Paths(Path(topright, topcenter, bottomcenter), Path(middleleft, middleright)));
            paths.Add('g', Paths(Path(bottomright, bottomleft, middleleft, middleright, tailright, tailleft)));
            paths.Add('h', Paths(Path(topleft, bottomleft), Path(middleleft, middleright, bottomright)));
            var dot = center + q3y;
            var dotoffset = Vector2.UnitY * 0.05f;
            paths.Add('i', Paths(Path(middlecenter, bottomcenter), Path(dot, dot + dotoffset)));
            paths.Add('j', Paths(Path(middlecenter, tail + center, tailleft), Path(dot, dot + dotoffset)));
            paths.Add('k', Paths(Path(topleft, bottomleft), Path(middleright, qy, bottomright)));
            paths.Add('l', Paths(Path(topcenter, bottomcenter)));
            paths.Add('m', Paths(Path(bottomleft, middleleft, middleright, bottomright), Path(middlecenter, bottomcenter)));
            paths.Add('n', Paths(Path(bottomleft, middleleft, middleright, bottomright)));
            paths.Add('o', Paths(Path(bottomleft, middleleft, middleright, bottomright, bottomleft)));
            paths.Add('p', Paths(Path(tailleft, middleleft, middleright, bottomright, bottomleft)));
            paths.Add('q', Paths(Path(bottomright, bottomleft, middleleft, middleright, tailright)));
            paths.Add('r', Paths(Path(bottomleft, middleleft, middleright)));
            paths.Add('s', Paths(Path(middleright, middleleft, qy, qy + right, bottomright, bottomleft)));
            paths.Add('t', Paths(Path(topcenter, bottomcenter), Path(middleleft, middleright)));
            paths.Add('u', Paths(Path(middleleft, bottomleft, bottomright, middleright)));
            paths.Add('v', Paths(Path(middleleft, bottomcenter, middleright)));
            paths.Add('w', Paths(Path(middleleft, bottomleft, bottomright, middleright), Path(middlecenter, bottomcenter)));
            paths.Add('x', Paths(Path(middleleft, bottomright), Path(middleright, bottomleft)));
            paths.Add('y', Paths(Path(middleleft, bottomcenter), Path(middleright, tailleft)));
            paths.Add('z', Paths(Path(middleleft, middleright, bottomleft, bottomright)));
            paths.Add('0', Paths(Path(topright, topleft, bottomleft, bottomright, topright, bottomleft)));
            paths.Add('1', Paths(Path(q3y + qx, topcenter, bottomcenter)));
            paths.Add('2', Paths(Path(topleft, topcenter, q3y + right, qy + left, bottomleft, bottomright)));
            paths.Add('3', Paths(Path(topleft, topright, bottomright, bottomleft), Path(middlecenter, middleright)));
            paths.Add('4', Paths(Path(topleft, middleleft, middleright), Path(topright, bottomright)));
            paths.Add('5', Paths(Path(topright, topleft, middleleft, middlecenter, qy + right, bottomcenter, bottomleft)));
            paths.Add('6', Paths(Path(topright, topleft, bottomleft, bottomright, middleright, middleleft)));
            paths.Add('7', Paths(Path(topleft, topright, bottomleft)));
            paths.Add('8', Paths(Path(bottomright, bottomleft, topleft, topright, bottomright), Path(middleleft, middleright)));
            paths.Add('9', Paths(Path(middleright, middleleft, topleft, topright, bottomright)));
            paths.Add('.', Paths(Path(bottomcenter, bottomcenter + dotoffset)));
            paths.Add(' ', Paths());
            paths.Add('%', Paths(Path(topright, bottomleft), Path(topleft, q3y, q3y + center, topcenter, topleft), Path(bottomright, qy + right, qy + center, bottomcenter, bottomright)));
            paths.Add(':', Paths(Path(qy + center, qy + center + dotoffset), Path(q3y + center, q3y + center + dotoffset)));
            paths.Add('\'', Paths(Path(topcenter, q3y + center)));
            paths.Add('/', Paths(Path(topright, bottomleft)));
            paths.Add('\\', Paths(Path(topleft, bottomright)));
            paths.Add(',', Paths(Path(bottomcenter, qy + right)));
            paths.Add('@', Paths(Path(qy + q3x, q3y + q3x, q3y + qx, qy + qx, qy + right, topright, topleft, bottomleft, bottomright)));
            paths.Add('=', Paths(Path(q3y + left, q3y + right), Path(qy + left, qy + right)));
            paths.Add('-', Paths(Path(middleleft, middleright)));
            paths.Add('#', Paths(Path(q3y + left, q3y + right), Path(qy + left, qy + right), Path(top + qx, bottom + qx), Path(top + q3x, bottom + q3x)));
            paths.Add('+', Paths(Path(middleleft, middleright), Path(q3y + center, qy + center)));
            paths.Add('(', Paths(Path(topright, q3y + center, qy + center, bottomright)));
            paths.Add(')', Paths(Path(topleft, q3y + center, qy + center, bottomleft)));
            paths.Add('$', Paths(Path(q3y + q3x, q3y + qx, middle + qx, middle + q3x, qy + q3x, qy + qx), Path(topcenter, bottomcenter)));
            paths.Add('^', Paths(Path(q3y + left, topcenter, q3y + right)));
            paths.Add('&', Paths(Path(bottomright, q3y + left, top + qx, q3y + center, qy + left, middleright)));
            paths.Add('!', Paths(Path(topcenter, qy + center), Path(bottomcenter, bottomcenter + dotoffset)));
            paths.Add('~', Paths(Path(middleleft, q3y + qx, qy + q3x, middleright)));
            var o3y = top * 3f / 8f;
            var o5y = top * 5f / 8f;
            paths.Add('*', Paths(Path(q3y + center, qy + center), Path(o5y + q3x, o3y + qx), Path(o5y + qx, o3y + q3x)));
            paths.Add('[', Paths(Path(topright, topcenter, bottomcenter, bottomright)));
            paths.Add(']', Paths(Path(topleft, topcenter, bottomcenter, bottomleft)));
            paths.Add('?', Paths(
                Path(middleleft, topleft, topright, middleright, middlecenter, qy + center),
                Path(bottomcenter, bottomcenter + 2 * dotoffset)));
            paths.Add('<', Paths(Path(right + q3y, middlecenter, right + qy)));
            paths.Add('>', Paths(Path(left + q3y, middlecenter, left + qy)));

            return paths;
        }


        static Vector2[] Vertexes;
        static int[] Indexes;
        static Dictionary<char, Glyph> Glyphs;

        static void GenerateGlyphs()
        {
            var paths = GeneratePaths();
            var glyphs = new Dictionary<char, Glyph>();
            var points = new HashSet<Vector2>();

            foreach (var pathset in paths.Values.ToArray())
            {
                foreach (var path in pathset)
                {
                    points.AddRange(path);
                }
            }

            var vertexes = points.ToList();
            var indexes = new List<int>();

            foreach (var ch in paths.Keys.ToArray())
            {
                var segments = new List<GlyphSegment>();
                foreach (var path in paths[ch])
                {
                    var segment = new GlyphSegment() {
                        StartIndex = indexes.Count,
                        NumPrimitives = path.Length - 1,
                    };
                    segments.Add(segment);
                    indexes.AddRange(path.Select(v => (int)vertexes.IndexOf(v)));
                }
                var glyph = new Glyph {
                    Segments = segments.ToArray()
                };
                glyphs.Add(ch, glyph);
            }

            Glyphs = glyphs;
            Vertexes = vertexes.ToArray();
            Indexes = indexes.ToArray();
        }
    }
}

