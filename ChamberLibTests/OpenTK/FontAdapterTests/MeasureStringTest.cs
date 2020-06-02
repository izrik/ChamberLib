using System;
using ChamberLib;
using ChamberLib.OpenTK;
using NUnit.Framework;

namespace ChamberLibTests.OpenTK.FontAdapterTests
{
    [TestFixture]
    public class MeasureStringTest
    {
        [Test]
        public void EmptyStringYieldsZero()
        {
            // given
            var fa = new FontAdapter();

            // when
            var size = fa.MeasureString("");

            // then
            Assert.AreEqual(Vector2.Zero, size);
        }

        [Test]
        public void SingleWordYieldsAmount()
        {
            // given
            var fa = new FontAdapter();

            // when
            var size = fa.MeasureString("word");

            // then
            // x => CharacterWidth*4 + SpaceBetweenChars*3 => 28 + 9 => 37
            // y => LineHeight => 21
            Assert.AreEqual(new Vector2(37, 21), size);
        }

        [Test]
        public void MultipleWordsYieldsAmount()
        {
            // given
            var fa = new FontAdapter();

            // when
            var size = fa.MeasureString("one two three");

            // then
            // x => CharacterWidth*13 + SpaceBetweenChars*12 => 91 + 36 => 127
            // y => LineHeight => 21
            Assert.AreEqual(new Vector2(127, 21), size);
        }

        [Test]
        public void WordsOnMultipleLines()
        {
            // given
            var fa = new FontAdapter();

            // when
            var size = fa.MeasureString("one\ntwo\nthree");

            // then
            // x => CharacterWidth*5 + SpaceBetweenChars*4 => 35 + 12 => 47
            // y => LineHeight*3 + SpaceBetweenLines*2 => 63 + 6 => 69
            Assert.AreEqual(new Vector2(47, 69), size);
        }
    }
}
