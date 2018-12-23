using System;
using src;
using NUnit.Framework;

namespace Tests
{
    public class ConstRegexTests
    {
        [Test]
        public void ShouldReturnCorrectLongestNonCyclicWordForEmptyRegex()
        {
            // arrange
            ConstRegex regex = new ConstRegex(string.Empty);

            // act
            string longestWord = regex.GetLongestNonCyclicWord();

            // assert
            Assert.AreEqual(string.Empty, longestWord);
        }

        [Test]
        public void ShouldThrowExceptionWhenPassingNullToConstructor()
        {
            // act & assert assert
            Assert.Throws<ArgumentNullException>(() => new ConstRegex(null));
        }

        [TestCase("W")]
        [TestCase("WNWN")]
        public void ShouldReturnCorrectLongestNonCyclicWordForRegex(string input)
        {
            // arrange
            ConstRegex regex = new ConstRegex(input);

            // act
            string longestWord = regex.GetLongestNonCyclicWord();

            // assert
            Assert.AreEqual(input, longestWord);
        }
    }
}