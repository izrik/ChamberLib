using NUnit.Framework;
using ChamberLib;

namespace ChamberLibTests
{
    [TestFixture]
    public class TimelineTest
    {
        [Test]
        public void Create_HasNoEvents()
        {
            // when
            var tl = new Timeline();
            
            // then
            Assert.IsEmpty(tl.Events);
            Assert.AreEqual(0, tl.EndTime);
        }
    }
}
