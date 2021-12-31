using System.Collections.Generic;
using ChamberLib.OpenTK.Text;
using NUnit.Framework;

namespace ChamberLibTests.OpenTK.TextT.FontAdapterTests
{
    [TestFixture]
    public class SplitWordsTest
    {
        [Test]
        public void EmptyLineYieldsZeroSpans()
        {
            // given
            var line = new FontAdapter.Span("");
            var words = new List<FontAdapter.Span>();

            // when
            FontAdapter.SplitWords(line, words);

            // then
            Assert.AreEqual(0, words.Count);
        }

        [Test]
        public void SingleWordYieldsSingleSpan()
        {
            // given
            var line = new FontAdapter.Span("one");
            var words = new List<FontAdapter.Span>();

            // when
            FontAdapter.SplitWords(line, words);

            // then
            Assert.AreEqual(1, words.Count);
            Assert.AreEqual(0, words[0].Start);
            Assert.AreEqual(3, words[0].Length);
            Assert.AreEqual("one", words[0].Value);
        }

        [Test]
        public void MultipleWordsYieldMultipleSpans()
        {
            // given
            var line = new FontAdapter.Span("one two three");
            var words = new List<FontAdapter.Span>();

            // when
            FontAdapter.SplitWords(line, words);

            // then
            Assert.AreEqual(3, words.Count);

            Assert.AreEqual(0, words[0].Start);
            Assert.AreEqual(3, words[0].Length);
            Assert.AreEqual("one", words[0].Value);

            Assert.AreEqual(4, words[1].Start);
            Assert.AreEqual(3, words[1].Length);
            Assert.AreEqual("two", words[1].Value);

            Assert.AreEqual(8, words[2].Start);
            Assert.AreEqual(5, words[2].Length);
            Assert.AreEqual("three", words[2].Value);
        }

        [Test]
        public void TrailingWhitespaceIsIgnored()
        {
            // given
            var line = new FontAdapter.Span("one two three  \t\r\n  ");
            var words = new List<FontAdapter.Span>();

            // when
            FontAdapter.SplitWords(line, words);

            // then
            Assert.AreEqual(3, words.Count);

            Assert.AreEqual(0, words[0].Start);
            Assert.AreEqual(3, words[0].Length);
            Assert.AreEqual("one", words[0].Value);

            Assert.AreEqual(4, words[1].Start);
            Assert.AreEqual(3, words[1].Length);
            Assert.AreEqual("two", words[1].Value);

            Assert.AreEqual(8, words[2].Start);
            Assert.AreEqual(5, words[2].Length);
            Assert.AreEqual("three", words[2].Value);
        }

        [Test]
        public void LeadingWhitespaceIsIgnored()
        {
            // given
            var line = new FontAdapter.Span("  \t\r\n  one two three");
            var words = new List<FontAdapter.Span>();

            // when
            FontAdapter.SplitWords(line, words);

            // then
            Assert.AreEqual(3, words.Count);

            Assert.AreEqual(7, words[0].Start);
            Assert.AreEqual(3, words[0].Length);
            Assert.AreEqual("one", words[0].Value);

            Assert.AreEqual(11, words[1].Start);
            Assert.AreEqual(3, words[1].Length);
            Assert.AreEqual("two", words[1].Value);

            Assert.AreEqual(15, words[2].Start);
            Assert.AreEqual(5, words[2].Length);
            Assert.AreEqual("three", words[2].Value);
        }
    }
}
