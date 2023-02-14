
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
using ChamberLib.OpenTK.Text;

namespace ChamberLib.OpenTK.System
{
    public partial class Renderer
    {
        public void DrawString(IFont font, string text, Vector2 position,
            Color color,
            float rotation=0,
            float scaleX=1,
            float scaleY=1,
            float? wrapWordsToMaxLineWidth=null,
            int? numCharsToDraw=null)
        {
            var font2 = (FontAdapter)font;
            font2.DrawString(this, text, position, color, rotation,
                scaleX, scaleY, wrapWordsToMaxLineWidth, numCharsToDraw);
        }

    }
}

