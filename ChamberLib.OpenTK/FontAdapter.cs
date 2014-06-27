using System;
using System.Collections.Generic;
using System.Linq;

namespace ChamberLib
{
    public class FontAdapter : IFont
    {
        public FontAdapter()
        {
        }

        public static readonly float CharacterWidth = 7;
        public static readonly float CharacterHeight = 14;
        public static readonly float LineHeight = 21;//CharacterHeight*1.5f;
        public static readonly float SpaceBetweenChars = 3;
        public static readonly float SpaceBetweenLines = 3;

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

        public void DrawString(Renderer renderer, string text, Vector2 position, Color color, float rotation, Vector2 origin, float scale)
        {
            var p = position - origin;
            var x = p.X;
            foreach (char ch in text)
            {
                switch (ch)
                {
                    case '\r':
                        continue;
                    case '\n':
                        p = new Vector2(x, p.Y + LineHeight + SpaceBetweenLines);
                        break;
                    default:
                        var paths = GetGlyph(ch);
                        foreach (var path in paths)
                        {
                            var path2 = path.Select(v => v * scale).Select(v => new Vector2(v.X, 1 - v.Y));
                            var path3 = path2.Select(v => p + new Vector2(v.X * CharacterWidth, v.Y * CharacterHeight));

                            renderer.DrawLines(color, path3);
                        }
                        p = new Vector2(p.X + CharacterWidth + SpaceBetweenChars, p.Y);
                        break;
                }
            }
        }

        static Vector2[][] Paths(params Vector2[][] paths)
        {
            return paths;
        }
        static Vector2[] Path(params Vector2[] points)
        {
            return points;
        }

        static Vector2[][] GetGlyph(char ch)
        {
            if (glyphs.ContainsKey(ch))
            {
                return glyphs[ch];
            }

            missedCharacters.Add(ch);

            return glyphs['\0'];
        }

        static readonly HashSet<char> missedCharacters = new HashSet<char>();

        static readonly Dictionary<char, Vector2[][]> glyphs = GenerateGlyphs();

        static Dictionary<char, Vector2[][]> GenerateGlyphs()
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

            var glyphs = new Dictionary<char, Vector2[][]>();

            glyphs.Add('\0', Paths(Path(qx + qy, q3x + qy, q3x + q3y, qx + q3y, qx + qy))); // the 'missing' glyph

            glyphs.Add('A', Paths(Path(bottomleft, topcenter, bottomright), Path(middle + qx, middle + q3x)));
            glyphs.Add('B', Paths(Path(middleleft, middleright, bottomright, bottomleft, topleft, topcenter, middlecenter)));
            glyphs.Add('C', Paths(Path(bottomright, bottomleft, topleft, topright)));
            glyphs.Add('D', Paths(Path(bottomright, bottomleft, topleft, middleright, bottomright)));
            glyphs.Add('E', Paths(Path(bottomright, bottomleft, topleft, topright), Path(middleleft, middlecenter)));
            glyphs.Add('F', Paths(Path(bottomleft, topleft, topright), Path(middleleft, middlecenter)));
            glyphs.Add('G', Paths(Path(topright, topleft, bottomleft, bottomright, middleright, middlecenter)));
            glyphs.Add('H', Paths(Path(topleft, bottomleft), Path(topright, bottomright), Path(middleleft, middleright)));
            glyphs.Add('I', Paths(Path(topleft, topright), Path(bottomleft, bottomright), Path(topcenter, bottomcenter)));
            glyphs.Add('J', Paths(Path(topright, bottomright, bottomleft, middleleft)));
            glyphs.Add('K', Paths(Path(topleft, bottomleft), Path(topright, middleleft, bottomright)));
            glyphs.Add('L', Paths(Path(topleft, bottomleft, bottomright)));
            glyphs.Add('M', Paths(Path(bottomleft, topleft, middlecenter, topright, bottomright)));
            glyphs.Add('N', Paths(Path(bottomleft, topleft, bottomright, topright)));
            glyphs.Add('O', Paths(Path(bottomright, bottomleft, topleft, topright, bottomright)));
            glyphs.Add('P', Paths(Path(bottomleft, topleft, topright, middleright, middleleft)));
            glyphs.Add('Q', Paths(Path(bottomright, bottomleft, topleft, topright, bottomright), Path(qy + q3x, new Vector2(1.25f, -0.25f))));
            glyphs.Add('R', Paths(Path(bottomleft, topleft, topright, middleright, middleleft, bottomright)));
            glyphs.Add('S', Paths(Path(topright, topleft, middleleft, middleright, bottomright, bottomleft)));
            glyphs.Add('T', Paths(Path(topleft, topright), Path(topcenter, bottomcenter)));
            glyphs.Add('U', Paths(Path(topleft, bottomleft, bottomright, topright)));
            glyphs.Add('V', Paths(Path(topleft, bottomcenter, topright)));
            glyphs.Add('W', Paths(Path(topleft, bottomleft, middlecenter, bottomright, topright)));
            glyphs.Add('X', Paths(Path(topleft, bottomright), Path(topright, bottomleft)));
            glyphs.Add('Y', Paths(Path(topleft, middlecenter, topright), Path(middlecenter, bottomcenter)));
            glyphs.Add('Z', Paths(Path(topleft, topright, bottomleft, bottomright)));
            glyphs.Add('a', Paths(Path(middle + q3x, middleleft, bottomleft, q3x, middle + q3x, bottomright)));
            glyphs.Add('b', Paths(Path(topleft, bottomleft, bottomright, middleright, middleleft)));
            glyphs.Add('c', Paths(Path(middleright, middleleft, bottomleft, bottomright)));
            glyphs.Add('d', Paths(Path(middleright, middleleft, bottomleft, bottomright, topright)));
            glyphs.Add('e', Paths(Path(qy, qy + right, middleright, middleleft, bottomleft, bottomright)));
            glyphs.Add('f', Paths(Path(topright, topcenter, bottomcenter), Path(middleleft, middleright)));
            glyphs.Add('g', Paths(Path(bottomright, bottomleft, middleleft, middleright, tailright, tailleft)));
            glyphs.Add('h', Paths(Path(topleft, bottomleft), Path(middleleft, middleright, bottomright)));
            var dot = center + q3y;
            var dotoffset = Vector2.UnitY * 0.05f;
            glyphs.Add('i', Paths(Path(middlecenter, bottomcenter), Path(dot, dot + dotoffset)));
            glyphs.Add('j', Paths(Path(middlecenter, tail + center, tailleft), Path(dot, dot + dotoffset)));
            glyphs.Add('k', Paths(Path(topleft, bottomleft), Path(middleright, qy, bottomright)));
            glyphs.Add('l', Paths(Path(topcenter, bottomcenter)));
            glyphs.Add('m', Paths(Path(bottomleft, middleleft, middleright, bottomright), Path(middlecenter, bottomcenter)));
            glyphs.Add('n', Paths(Path(bottomleft, middleleft, middleright, bottomright)));
            glyphs.Add('o', Paths(Path(bottomleft, middleleft, middleright, bottomright, bottomleft)));
            glyphs.Add('p', Paths(Path(tailleft, middleleft, middleright, bottomright, bottomleft)));
            glyphs.Add('q', Paths(Path(bottomright, bottomleft, middleleft, middleright, tailright)));
            glyphs.Add('r', Paths(Path(bottomleft, middleleft, middleright)));
            glyphs.Add('s', Paths(Path(middleright, middleleft, qy, qy + right, bottomright, bottomleft)));
            glyphs.Add('t', Paths(Path(topcenter, bottomcenter), Path(middleleft, middleright)));
            glyphs.Add('u', Paths(Path(middleleft, bottomleft, bottomright, middleright)));
            glyphs.Add('v', Paths(Path(middleleft, bottomcenter, middleright)));
            glyphs.Add('w', Paths(Path(middleleft, bottomleft, bottomright, middleright), Path(middlecenter, bottomcenter)));
            glyphs.Add('x', Paths(Path(middleleft, bottomright), Path(middleright, bottomleft)));
            glyphs.Add('y', Paths(Path(middleleft, bottomcenter), Path(middleright, tailleft)));
            glyphs.Add('z', Paths(Path(middleleft, middleright, bottomleft, bottomright)));
            glyphs.Add('0', Paths(Path(topright, topleft, bottomleft, bottomright, topright, bottomleft)));
            glyphs.Add('1', Paths(Path(q3y + qx, topcenter, bottomcenter)));
            glyphs.Add('2', Paths(Path(topleft, topcenter, q3y + right, qy + left, bottomleft, bottomright)));
            glyphs.Add('3', Paths(Path(topleft, topright, bottomright, bottomleft), Path(middlecenter, middleright)));
            glyphs.Add('4', Paths(Path(topleft, middleleft, middleright), Path(topright, bottomright)));
            glyphs.Add('5', Paths(Path(topright, topleft, middleleft, middlecenter, qy + right, bottomcenter, bottomleft)));
            glyphs.Add('6', Paths(Path(topright, topleft, bottomleft, bottomright, middleright, middleleft)));
            glyphs.Add('7', Paths(Path(topleft, topright, bottomleft)));
            glyphs.Add('8', Paths(Path(bottomright, bottomleft, topleft, topright, bottomright), Path(middleleft, middleright)));
            glyphs.Add('9', Paths(Path(middleright, middleleft, topleft, topright, bottomright)));
            glyphs.Add('.', Paths(Path(bottomcenter, bottomcenter + dotoffset)));
            glyphs.Add(' ', Paths());
            glyphs.Add('%', Paths(Path(topright, bottomleft), Path(topleft, q3y, q3y + center, topcenter, topleft), Path(bottomright, qy + right, qy + center, bottomcenter, bottomright)));
            glyphs.Add(':', Paths(Path(qy + center, qy + center + dotoffset), Path(q3y + center, q3y + center + dotoffset)));
            glyphs.Add('\'', Paths(Path(topcenter, q3y + center)));
            glyphs.Add('/', Paths(Path(topright, bottomleft)));
            glyphs.Add('\\', Paths(Path(topleft, bottomright)));
            glyphs.Add(',', Paths(Path(bottomcenter, qy + right)));
            glyphs.Add('@', Paths(Path(qy + q3x, q3y + q3x, q3y + qx, qy + qx, qy + right, topright, topleft, bottomleft, bottomright)));
            glyphs.Add('=', Paths(Path(q3y + left, q3y + right), Path(qy + left, qy + right)));
            glyphs.Add('-', Paths(Path(middleleft, middleright)));
            glyphs.Add('#', Paths(Path(q3y + left, q3y + right), Path(qy + left, qy + right), Path(top + qx, bottom + qx), Path(top + q3x, bottom + q3x)));
            glyphs.Add('+', Paths(Path(middleleft, middleright), Path(q3y + center, qy + center)));
            glyphs.Add('(', Paths(Path(topright, q3y + center, qy + center, bottomright)));
            glyphs.Add(')', Paths(Path(topleft, q3y + center, qy + center, bottomleft)));
            glyphs.Add('$', Paths(Path(q3y + q3x, q3y + qx, middle + qx, middle + q3x, qy + q3x, qy + qx), Path(topcenter, bottomcenter)));
            glyphs.Add('^', Paths(Path(q3y + left, topcenter, q3y + right)));
            glyphs.Add('&', Paths(Path(bottomright, q3y + left, top + qx, q3y + center, qy + left, middleright)));
            glyphs.Add('!', Paths(Path(topcenter, qy + center), Path(bottomcenter, bottomcenter + dotoffset)));
            glyphs.Add('~', Paths(Path(middleleft, q3y + qx, qy + q3x, middleright)));
            var o3y = top * 3f / 8f;
            var o5y = top * 5f / 8f;
            glyphs.Add('*', Paths(Path(q3y + center, qy + center), Path(o5y + q3x, o3y + qx), Path(o5y + qx, o3y + q3x)));
            glyphs.Add('[', Paths(Path(topright, topcenter, bottomcenter, bottomright)));
            glyphs.Add(']', Paths(Path(topleft, topcenter, bottomcenter, bottomleft)));

            return glyphs;
        }
    }
}

