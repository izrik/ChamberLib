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
