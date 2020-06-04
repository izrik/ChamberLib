using System;
using System.Collections.Generic;
using ChamberLib.OpenTK;
using NUnit.Framework;

namespace ChamberLibTests.OpenTK.FontAdapterTests
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
