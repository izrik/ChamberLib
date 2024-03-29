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

using System;
using System.Collections.Generic;

namespace ChamberLib
{
    public interface IRenderer
    {
        void DrawLine(Color color, Vector2 v1, Vector2 v2);
        void DrawLines(Color color, Vector2[] vs);
        void DrawLines(Color color, Vector2[] vs, int count);

        void DrawString(IFont font, string text, Vector2 position, Color color,
                        float rotation=0,
                        float scaleX=1,
                        float scaleY=1,
                        float? wrapWordsToMaxLineWidth=null,
                        int? numCharsToDraw=null);

        void DrawImage(DrawImagesEntry entry);
        void DrawImages(params DrawImagesEntry[] entries);

        void Clear(Color color);

        void Reset3D();

        void DrawLines(Vector3 color, Matrix world, Matrix view, Matrix projection, Vector3[] vs);
        void DrawLines(Vector3 color, Matrix world, Matrix view, Matrix projection, Vector3[] vs, int numPoints);

        Viewport Viewport { get; set; }
        void SetViewport(Viewport value, bool windowed=true);
    }

    public struct DrawImagesEntry
    {
        public DrawImagesEntry(ITexture2D texture, RectangleI dest)
            : this(texture, dest, Color.White)
        {
        }
        public DrawImagesEntry(ITexture2D texture, RectangleI dest, Color color)
        {
            Texture = texture;
            DestinationRectangle = dest;
            Color = color;
        }

        public ITexture2D Texture;
        public RectangleI DestinationRectangle;
        public Color Color;
    }

    public static class RendererHelper
    {
        static readonly Vector3[] __DrawLine_points = new Vector3[2];
        public static void DrawLine(this IRenderer renderer, Vector3 color,
            Matrix world, Matrix view, Matrix projection,
            Vector3 p1, Vector3 p2)
        {
            __DrawLine_points[0] = p1;
            __DrawLine_points[1] = p2;
            renderer.DrawLines(color, world, view, projection,
                __DrawLine_points);
        }

        static Vector3[] __DrawBoundingBox_lines = new Vector3[16];
        public static void DrawBoundingBox(this IRenderer renderer,
            Vector3 color, Matrix world, Matrix view, Matrix projection,
            BoundingBox boundingBox)
        {

            var bb = boundingBox;
            var xxx = new Vector3(bb.Max.X, bb.Max.Y, bb.Max.Z);
            var nxx = new Vector3(bb.Min.X, bb.Max.Y, bb.Max.Z);
            var xnx = new Vector3(bb.Max.X, bb.Min.Y, bb.Max.Z);
            var nnx = new Vector3(bb.Min.X, bb.Min.Y, bb.Max.Z);
            var xxn = new Vector3(bb.Max.X, bb.Max.Y, bb.Min.Z);
            var nxn = new Vector3(bb.Min.X, bb.Max.Y, bb.Min.Z);
            var xnn = new Vector3(bb.Max.X, bb.Min.Y, bb.Min.Z);
            var nnn = new Vector3(bb.Min.X, bb.Min.Y, bb.Min.Z);

            __DrawBoundingBox_lines[0] = nnn;
            __DrawBoundingBox_lines[1] = nnx;
            __DrawBoundingBox_lines[2] = nxx;
            __DrawBoundingBox_lines[3] = nxn;
            __DrawBoundingBox_lines[4] = nnn;
            __DrawBoundingBox_lines[5] = xnn;
            __DrawBoundingBox_lines[6] = xnx;
            __DrawBoundingBox_lines[7] = nnx;
            __DrawBoundingBox_lines[8] = xnx;
            __DrawBoundingBox_lines[9] = xxx;
            __DrawBoundingBox_lines[10] = nxx;
            __DrawBoundingBox_lines[11] = nxn;
            __DrawBoundingBox_lines[12] = xxn;
            __DrawBoundingBox_lines[13] = xnn;
            __DrawBoundingBox_lines[14] = xxn;
            __DrawBoundingBox_lines[15] = xxx;

            renderer.DrawLines(color, world, view, projection,
                __DrawBoundingBox_lines);
        }
    }

}

