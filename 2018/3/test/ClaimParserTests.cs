using NUnit.Framework;
using src;

namespace Tests
{
    [TestFixture]
    public class ClaimParserTests
    {
        [Test]
        public void ItShouldWork()
        {
            // arrange 
            string input = "#1 @ 2,3: 4x5";

            // act
            ClaimDto result = ClaimParser.Parse(input);

            // assert
            Assert.AreEqual(2, result.LeftEdgePosition);
            Assert.AreEqual(3, result.TopEdgePosition);
            Assert.AreEqual(4, result.Width);
            Assert.AreEqual(5, result.Height);
        }
    }
}