using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Graphics.OpenGL;
using System.Runtime.InteropServices;
using ChamberLib.Content;

using _OpenTK = global::OpenTK;

namespace ChamberLib.OpenTK
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

        public Vector2 MeasureString(string text)
        {
            int numlines = 1;
            int maxchars = 0;
            int linechars = 0;
            foreach (char ch in text)
            {
                switch (ch)
                {
                    case '\r':
                        continue;
                    case '\n':
                        numlines++;
                        maxchars = Math.Max(maxchars, linechars);
                        linechars = 0;
                        break;
                    default:
                        linechars++;
                        break;
                }
            }
            maxchars = Math.Max(maxchars, linechars);

            return 
                new Vector2(
                maxchars * CharacterWidth + (maxchars - 1) * SpaceBetweenChars,
                numlines * LineHeight + (numlines - 1) * SpaceBetweenLines);
        }

        public static bool IsReady = false;

        public void DrawString(Renderer renderer, string text, Vector2 position, Color color, float rotation, Vector2 origin, float scaleX, float scaleY)
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

            var p = position - origin;
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

            foreach (char ch in text)
            {
                // set p
                GL.Uniform2(offsetLocation, p.ToOpenTK());
                GLHelper.CheckError();

                switch (ch)
                {
                    case '\r':
                        continue;
                    case '\n':
                        p = new Vector2(x, p.Y + (LineHeight + SpaceBetweenLines) * scaleY);
                        break;
                    default:
                        var glyph = GetGlyph(ch);

                        foreach (var segment in glyph.Segments)
                        {
                            renderData.Draw(
                                PrimitiveType.LineStrip,
                                segment.NumPrimitives+1,
                                segment.StartIndex);
                            GLHelper.CheckError();

                        }
                        p = new Vector2(p.X + (CharacterWidth + SpaceBetweenChars) * scaleX, p.Y);
                        break;
                }
            }

            renderData.UnApply();

            Shader.UnApply();
            GLHelper.CheckError();

        }

        static void MakeReady()
        {
            _OpenTK.Vector2[] vertexData = Vertexes.Select(v => v.ToOpenTK()).ToArray();
            int vertexSizeInBytes = _OpenTK.Vector2.SizeInBytes;
            short[] indexData = Indexes;
            VertexAttribPointerType vertexAttributeComponentType = VertexAttribPointerType.Float;
            int numVertexAttributeComponents = 2;

            /*
             * Shader
             *
             */
            var name = "$font";

            _DrawString_shader_vert_stage = new ShaderStage(
                _DrawString_shader_vert_source, ShaderType.Vertex, name);
            _DrawString_shader_frag_stage = new ShaderStage(
                _DrawString_shader_frag_source, ShaderType.Fragment, name);

            Shader = ShaderProgram.MakeShaderProgram(
                _DrawString_shader_vert_stage,
                _DrawString_shader_frag_stage,
                name);
            Shader.SetBindAttributes(new[] { "in_position" });

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
                vertexAttributeComponentType,
                numVertexAttributeComponents);
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
        static short[] Indexes;
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
            var indexes = new List<short>();

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
                    indexes.AddRange(path.Select(v => (short)vertexes.IndexOf(v)));
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

