
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
using ChamberLib.OpenTK.Text;
using NUnit.Framework;

namespace ChamberLibTests.OpenTK.TextT.FontAdapterTests
{
    [TestFixture]
    public class SplitLinesTest
    {
        [Test]
        public void EmptyStringReturnsSingleEmptySpan()
        {
            // given
            var s = new FontAdapter.Span("");
            var lines = new List<FontAdapter.Span>();

            // when
            FontAdapter.SplitLines(s, lines);

            // then
            Assert.AreEqual(1, lines.Count);
            Assert.AreEqual(0, lines[0].Start);
            Assert.AreEqual(0, lines[0].Length);
            Assert.AreEqual("", lines[0].Value);
        }

        [Test]
        public void SingleWordReturnsSingleSpan()
        {
            // given
            var s = new FontAdapter.Span("word");
            var lines = new List<FontAdapter.Span>();

            // when
            FontAdapter.SplitLines(s, lines);

            // then
            Assert.AreEqual(1, lines.Count);
            Assert.AreEqual(0, lines[0].Start);
            Assert.AreEqual(4, lines[0].Length);
            Assert.AreEqual("word", lines[0].Value);
        }

        [Test]
        public void MultipleLinesYieldMultipleSpans()
        {
            // given
            var s = new FontAdapter.Span("one\ntwo\nthree");
            var lines = new List<FontAdapter.Span>();

            // when
            FontAdapter.SplitLines(s, lines);

            // then
            Assert.AreEqual(3, lines.Count);

            Assert.AreEqual(0, lines[0].Start);
            Assert.AreEqual(3, lines[0].Length);
            Assert.AreEqual("one", lines[0].Value);

            Assert.AreEqual(4, lines[1].Start);
            Assert.AreEqual(3, lines[1].Length);
            Assert.AreEqual("two", lines[1].Value);

            Assert.AreEqual(8, lines[2].Start);
            Assert.AreEqual(5, lines[2].Length);
            Assert.AreEqual("three", lines[2].Value);
        }

        [Test]
        public void SingleTrailingNewlineYieldsAdditionalSpan()
        {
            // given
            var s = new FontAdapter.Span("one\ntwo\nthree\n");
            var lines = new List<FontAdapter.Span>();

            // when
            FontAdapter.SplitLines(s, lines);

            // then
            Assert.AreEqual(4, lines.Count);

            Assert.AreEqual(0, lines[0].Start);
            Assert.AreEqual(3, lines[0].Length);
            Assert.AreEqual("one", lines[0].Value);

            Assert.AreEqual(4, lines[1].Start);
            Assert.AreEqual(3, lines[1].Length);
            Assert.AreEqual("two", lines[1].Value);

            Assert.AreEqual(8, lines[2].Start);
            Assert.AreEqual(5, lines[2].Length);
            Assert.AreEqual("three", lines[2].Value);

            Assert.AreEqual(14, lines[3].Start);
            Assert.AreEqual(0, lines[3].Length);
            Assert.AreEqual("", lines[3].Value);
        }

        [Test]
        public void AdditionalTrailingNewlinesYieldAdditionalEmptySpans()
        {
            // given
            var s = new FontAdapter.Span("one\ntwo\nthree\n\n\n");
            var lines = new List<FontAdapter.Span>();

            // when
            FontAdapter.SplitLines(s, lines);

            // then
            Assert.AreEqual(6, lines.Count);

            Assert.AreEqual(0, lines[0].Start);
            Assert.AreEqual(3, lines[0].Length);
            Assert.AreEqual("one", lines[0].Value);

            Assert.AreEqual(4, lines[1].Start);
            Assert.AreEqual(3, lines[1].Length);
            Assert.AreEqual("two", lines[1].Value);

            Assert.AreEqual(8, lines[2].Start);
            Assert.AreEqual(5, lines[2].Length);
            Assert.AreEqual("three", lines[2].Value);

            Assert.AreEqual(14, lines[3].Start);
            Assert.AreEqual(0, lines[3].Length);
            Assert.AreEqual("", lines[3].Value);

            Assert.AreEqual(15, lines[4].Start);
            Assert.AreEqual(0, lines[4].Length);
            Assert.AreEqual("", lines[4].Value);

            Assert.AreEqual(16, lines[5].Start);
            Assert.AreEqual(0, lines[5].Length);
            Assert.AreEqual("", lines[5].Value);
        }

        [Test]
        public void TrailingCarriageReturnsAreIgnored()
        {
            // given
            var s = new FontAdapter.Span("one\r\r\r\ntwo\r\nthree\r");
            var lines = new List<FontAdapter.Span>();

            // when
            FontAdapter.SplitLines(s, lines);

            // then
            Assert.AreEqual(3, lines.Count);

            Assert.AreEqual(0, lines[0].Start);
            Assert.AreEqual(3, lines[0].Length);
            Assert.AreEqual("one", lines[0].Value);

            Assert.AreEqual(7, lines[1].Start);
            Assert.AreEqual(3, lines[1].Length);
            Assert.AreEqual("two", lines[1].Value);

            Assert.AreEqual(12, lines[2].Start);
            Assert.AreEqual(5, lines[2].Length);
            Assert.AreEqual("three", lines[2].Value);
        }

        [Test]
        public void TrailingSpacesAreIgnored()
        {
            // given
            var s = new FontAdapter.Span("one   \ntwo \nthree ");
            var lines = new List<FontAdapter.Span>();

            // when
            FontAdapter.SplitLines(s, lines);

            // then
            Assert.AreEqual(3, lines.Count);

            Assert.AreEqual(0, lines[0].Start);
            Assert.AreEqual(3, lines[0].Length);
            Assert.AreEqual("one", lines[0].Value);

            Assert.AreEqual(7, lines[1].Start);
            Assert.AreEqual(3, lines[1].Length);
            Assert.AreEqual("two", lines[1].Value);

            Assert.AreEqual(12, lines[2].Start);
            Assert.AreEqual(5, lines[2].Length);
            Assert.AreEqual("three", lines[2].Value);
        }

        [Test]
        public void TrailingTabsAreIgnored()
        {
            // given
            var s = new FontAdapter.Span("one\t\t\t\ntwo\t\nthree\t");
            var lines = new List<FontAdapter.Span>();

            // when
            FontAdapter.SplitLines(s, lines);

            // then
            Assert.AreEqual(3, lines.Count);

            Assert.AreEqual(0, lines[0].Start);
            Assert.AreEqual(3, lines[0].Length);
            Assert.AreEqual("one", lines[0].Value);

            Assert.AreEqual(7, lines[1].Start);
            Assert.AreEqual(3, lines[1].Length);
            Assert.AreEqual("two", lines[1].Value);

            Assert.AreEqual(12, lines[2].Start);
            Assert.AreEqual(5, lines[2].Length);
            Assert.AreEqual("three", lines[2].Value);
        }

    }
}
