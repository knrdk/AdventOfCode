using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using src;

namespace Tests
{
    public class ParserTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ItShouldReturnEmptyCollectionForNullInput()
        {
            // arrange
            string[] input = null;

            // act
            IEnumerable<int> result = Parser.Parse(input);

            // assert
            Assert.IsEmpty(result);
        }

        [Test]
        public void ItShouldReturnEmptyCollectionForEmptyInput()
        {
            // arrange
            var input = new string[0];

            // act
            IEnumerable<int> result = Parser.Parse(input);

            // assert
            Assert.IsEmpty(result);
        }

        [Test]
        public void ItShouldWorksForPositiveDigit()
        {
            // arrange
            string input = "+5";

            // act
            int result = Parser.Parse(input);

            // assert
            Assert.AreEqual(5, result);
        }

        [Test]
        public void ItShouldWorksForNegativeDigit()
        {
            // arrange
            string input = "-8";

            // act
            int result = Parser.Parse(input);

            // assert
            Assert.AreEqual(-8, result);
        }

        [TestCase("+0")]
        [TestCase("0")]
        [TestCase("-0")]
        public void ItShouldWorksForZero(string zero)
        {
            // act
            int result = Parser.Parse(zero);

            // assert
            Assert.AreEqual(0, result);
        }

        [TestCase(-54343)]
        [TestCase(-234)]
        [TestCase(-21)]
        [TestCase(-41265243)]
        [TestCase(54343)]
        [TestCase(43)]
        [TestCase(343)]
        [TestCase(464564343)]
        public void ItShouldWorkForMoreThanOneDigitNumber(int expectedNumber)
        {
            // arrange
            char sign = expectedNumber > 0 ? '+' : '-';
            string numberAsText = sign + Math.Abs(expectedNumber).ToString();

            // act
            int result = Parser.Parse(numberAsText);

            // assert
            Assert.AreEqual(expectedNumber, result);
        }

        [Test]
        public void ItShouldWorksForSequence()
        {
            // arrange
            int[] expectedNumbers = new[] { 54, -21, 54342, 0, -2 };
            string[] input = new[] { "+54", "-21", "+54342", "0", "-2" };
        
            // act
            int[] actualNumber = Parser.Parse(input).ToArray();

            // assert
            Assert.AreEqual(expectedNumbers, actualNumber);
        }
    }
}