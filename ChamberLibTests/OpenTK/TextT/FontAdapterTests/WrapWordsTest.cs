
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
    public class WrapWordsTest
    {
        [Test]
        public void ZeroLinesYieldsZeroLines()
        {
            // given
            var lines = new List<FontAdapter.Span>();

            // precondition
            Assert.IsEmpty(lines);

            // when
            FontAdapter.WrapWords(lines, 50);

            // then
            Assert.IsEmpty(lines);
        }

        [Test]
        public void TypicalLinesWithinMaxWidthSayTheSame()
        {
            // given
            var lines = new List<FontAdapter.Span>
            {
                new FontAdapter.Span("one"),
                new FontAdapter.Span("two"),
                new FontAdapter.Span("three"),
            };

            // precondition
            Assert.AreEqual(3, lines.Count);
            Assert.AreEqual("one", lines[0].Value);
            Assert.AreEqual("two", lines[1].Value);
            Assert.AreEqual("three", lines[2].Value);

            // when
            FontAdapter.WrapWords(lines, 50);

            // then
            Assert.AreEqual(3, lines.Count);
            Assert.AreEqual("one", lines[0].Value);
            Assert.AreEqual("two", lines[1].Value);
            Assert.AreEqual("three", lines[2].Value);
        }

        [Test]
        public void LinesTooLongGetWrapped()
        {
            // given
            var lines = new List<FontAdapter.Span>
            {
                new FontAdapter.Span("one two thr"),
            };

            // precondition
            Assert.AreEqual(1, lines.Count);
            Assert.AreEqual("one two thr", lines[0].Value);

            // when
            FontAdapter.WrapWords(lines, 30);

            // then
            Assert.AreEqual(3, lines.Count);
            Assert.AreEqual("one", lines[0].Value);
            Assert.AreEqual("two", lines[1].Value);
            Assert.AreEqual("thr", lines[2].Value);
        }

        [Test]
        public void WordsGreaterThanMaxWidthTakeAnEntireLine()
        {
            // given
            var lines = new List<FontAdapter.Span>
            {
                new FontAdapter.Span("one twotwotwotwotwo three"),
            };

            // precondition
            Assert.AreEqual(1, lines.Count);
            Assert.AreEqual("one twotwotwotwotwo three", lines[0].Value);

            // when
            FontAdapter.WrapWords(lines, 60);

            // then
            Assert.AreEqual(3, lines.Count);
            Assert.AreEqual("one", lines[0].Value);
            Assert.AreEqual("twotwotwotwotwo", lines[1].Value);
            Assert.AreEqual("three", lines[2].Value);
        }
    }
}
