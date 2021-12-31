
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

using ChamberLib;
using ChamberLib.OpenTK.Text;
using NUnit.Framework;

namespace ChamberLibTests.OpenTK.TextT.FontAdapterTests
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
        public void MultipleWordsIncreaseWidth()
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
        public void WordsOnMultipleLinesIncreaseHeight()
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

        [Test]
        public void CarriageReturnsAreIgnored()
        {
            // given
            var fa = new FontAdapter();

            // when
            var size = fa.MeasureString("one\r\ntwo\r\nthree\r");

            // then
            // x => CharacterWidth*5 + SpaceBetweenChars*4 => 35 + 12 => 47
            // y => LineHeight*3 + SpaceBetweenLines*2 => 63 + 6 => 69
            Assert.AreEqual(new Vector2(47, 69), size);
        }

        [Test]
        public void LineWrapModeWithLargeWidthGivesSameResults1()
        {
            // given
            var fa = new FontAdapter();

            // when
            var size = fa.MeasureString("one\r\ntwo\r\nthree\r", 100);

            // then
            // x => CharacterWidth*5 + SpaceBetweenChars*4 => 35 + 12 => 47
            // y => LineHeight*3 + SpaceBetweenLines*2 => 63 + 6 => 69
            Assert.AreEqual(new Vector2(47, 69), size);
        }

        [Test]
        public void LineWrapModeWithLargeWidthGivesSameResults2()
        {
            // given
            var fa = new FontAdapter();

            // when
            var size = fa.MeasureString("one and\r\ntwo\r\nthree\r", 100);

            // then
            // x => CharacterWidth*7 + SpaceBetweenChars*6 => 49 + 18 => 67
            // y => LineHeight*3 + SpaceBetweenLines*2 => 63 + 6 => 69
            Assert.AreEqual(new Vector2(67, 69), size);
        }

        [Test]
        public void LineWrapModeWithSmallerWidthWrapsToNextLine()
        {
            // given
            var fa = new FontAdapter();

            // when
            var size = fa.MeasureString("one and\r\ntwo\r\nthree\r", 55);

            // then
            // x => CharacterWidth*5 + SpaceBetweenChars*4 => 35 + 12 => 47
            // y => LineHeight*4 + SpaceBetweenLines*3 => 84 + 9 => 93
            Assert.AreEqual(new Vector2(47, 93), size);
        }

        [Test]
        public void WhitespaceBeforeWrappedWordIsIgnored()
        {
            // given
            var fa = new FontAdapter();

            // when
            var size = fa.MeasureString("one          and", 55);

            // then
            // x => CharacterWidth*3 + SpaceBetweenChars*2 => 21 + 6 => 27
            // y => LineHeight*2 + SpaceBetweenLines*1 => 42 + 3 => 45
            Assert.AreEqual(new Vector2(27, 45), size);
        }

        [Test]
        public void LineWrapModeTrailingWhitespaceIsIgnored()
        {
            // given
            var fa = new FontAdapter();

            // when
            var size = fa.MeasureString("one and      \r\ntwo\r\nthree\r", 60);

            // then
            // x => CharacterWidth*5 + SpaceBetweenChars*4 => 35 + 12 => 47
            // y => LineHeight*4 + SpaceBetweenLines*3 => 84 + 9 => 93
            Assert.AreEqual(new Vector2(47, 93), size);
        }

        [Test]
        public void LineWrapModeTrailingWhitespaceIsIgnoredUntilEOL()
        {
            // given
            var fa = new FontAdapter();

            // when
            var size =
                fa.MeasureString("one a        \r\n   two\r\nthree\r", 60);

            // then
            // x => CharacterWidth*6 + SpaceBetweenChars*5 => 42 + 15 => 57
            // y => LineHeight*3 + SpaceBetweenLines*2 => 63 + 6 => 69
            Assert.AreEqual(new Vector2(57, 69), size);
        }

        [Test]
        public void WordTooLongForLineIsNotWrapped()
        {
            // given
            var fa = new FontAdapter();

            // when
            var size = fa.MeasureString("onetwothreefour\r\ntwo\r\nthree\r", 55);

            // then
            // x => CharacterWidth*15 + SpaceBetweenChars*14 => 105 + 42 => 147
            // y => LineHeight*3 + SpaceBetweenLines*2 => 63 + 6 => 69
            Assert.AreEqual(new Vector2(147, 69), size);
        }
    }
}
