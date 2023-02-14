
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

using ChamberLib.OpenTK.Text;
using NUnit.Framework;

namespace ChamberLibTests.OpenTK.TextT.FontAdapterTests
{
    [TestFixture]
    public class MeasureLineWidthTest
    {
        [Test]
        public void EmptyStringYieldsZero()
        {
            // given
            var line = new FontAdapter.Span("");

            // when
            var width = FontAdapter.MeasureLineWidth(line);

            // then
            Assert.AreEqual(0, width);
        }

        [Test]
        public void SingleCharYieldsValue()
        {
            // given
            var line = new FontAdapter.Span("a");

            // when
            var width = FontAdapter.MeasureLineWidth(line);

            // then
            Assert.AreEqual(7, width);
        }

        [Test]
        public void MultipleCharsYieldsValue()
        {
            // given
            var line = new FontAdapter.Span("abc");

            // when
            var width = FontAdapter.MeasureLineWidth(line);

            // then
            Assert.AreEqual(27, width);
        }

        [Test]
        public void NewlinesAreTreatedLikeOtherChars()
        {
            // given
            var line = new FontAdapter.Span("a\nc");

            // when
            var width = FontAdapter.MeasureLineWidth(line);

            // then
            Assert.AreEqual(27, width);
        }

        [Test]
        public void CarriageReturnsAreTreatedLikeOtherChars()
        {
            // given
            var line = new FontAdapter.Span("a\rc");

            // when
            var width = FontAdapter.MeasureLineWidth(line);

            // then
            Assert.AreEqual(27, width);
        }

        [Test]
        public void TabsAreTreatedLikeOtherChars()
        {
            // TODO: figure out what to do with tabs

            // given
            var line = new FontAdapter.Span("a\tc");

            // when
            var width = FontAdapter.MeasureLineWidth(line);

            // then
            Assert.AreEqual(27, width);
        }


        [Test]
        public void TrailingWhitespaceIsIgnored()
        {
            // TODO: figure out what to do with tabs

            // given
            var line = new FontAdapter.Span("abc \t\r\n ");

            // when
            var width = FontAdapter.MeasureLineWidth(line);

            // then
            Assert.AreEqual(27, width);
        }

        [Test]
        public void LeadingWhitespaceIsCounted()
        {
            // TODO: figure out what to do with tabs

            // given
            var line = new FontAdapter.Span("\t\r\n abc");

            // when
            var width = FontAdapter.MeasureLineWidth(line);

            // then
            Assert.AreEqual(67, width);
        }

    }
}
