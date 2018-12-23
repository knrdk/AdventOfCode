using System;
using src;
using NUnit.Framework;

namespace Tests
{
    public class ParserTests
    {
        [Test]
        public void ShouldReturnEmptyRegexForEmptyButValidRegexInput()
        {
            // arrange
            string input = "^$";

            // act
            IRegex result = Parser.Parse(input);

            // assert
            Assert.AreEqual(string.Empty, result.GetLongestNonCyclicWord());
        }

        [Test]
        public void ShouldReturnResultForOneCharacterInput()
        {
            // arrange
            const string expecteResult = "W";
            string input = $"^{expecteResult}$";

            // act
            IRegex result = Parser.Parse(input);

            // assert
            Assert.AreEqual(expecteResult, result.GetLongestNonCyclicWord());
        }

        [Test]
        public void ShouldReturnResultForThreeCharacterInput()
        {
            // arrange
            const string expecteResult = "SNW";
            string input = $"^{expecteResult}$";

            // act
            IRegex result = Parser.Parse(input);

            // assert
            Assert.AreEqual(expecteResult, result.GetLongestNonCyclicWord());
        }

        [Test]
        public void ShouldReturnValueForOneBranch()
        {
            // arrange
            const string expected = "SNTRE";
            string input = $"^SN(E|TRE)$";

            // act
            IRegex result = Parser.Parse(input);

            // assert
            Assert.AreEqual(expected, result.GetLongestNonCyclicWord());
        }

        [Test]
        public void ShouldReturnValueForComplexBranches()
        {
            // arrange
            const string expected = "ESSWWNNNENNWWWSSSSENNNE";
            string input = $"^ESSWWN(E|NNENN(EESS(WNSE|)SSS|WWWSSSSE(SW|NNNE)))$";

            // act
            IRegex result = Parser.Parse(input);

            // assert
            Assert.AreEqual(expected, result.GetLongestNonCyclicWord());
        }

        [Test]
        public void ShouldReturnValueForComplexBranchesSecondTest()
        {
            // arrange
            const string expected = "WSSEESWWWNWNENNEEEENNESSSSWNWSW";
            string input = $"^WSSEESWWWNW(S|NENNEEEENN(ESSSSW(NWSW|SSEN)|WSWWN(E|WWS(E|SS))))$";

            // act
            IRegex result = Parser.Parse(input);

            // assert
            Assert.AreEqual(expected, result.GetLongestNonCyclicWord());
        }
    }
}