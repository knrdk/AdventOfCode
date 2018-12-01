using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using src;
using LanguageExt;
using static LanguageExt.Option<int>;

namespace Tests
{
    public class SolverTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Case1()
        {
            // arrange
            Option<int>[] input = new[] { 1, -1 }.Select(Some).ToArray();

            // act
            Option<int> result = Solver.Solve(input);

            // assert
            Option<int> expected = Some(0);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Case2()
        {
            // arrange
            Option<int>[] input = new[] { +3, +3, +4, -2, -4 }.Select(Some).ToArray();

            // act
            Option<int> result = Solver.Solve(input);

            // assert
            Option<int> expected = Some(10);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Case3()
        {
            // arrange
            Option<int>[] input = new[] { -6, +3, +8, +5, -6 }.Select(Some).ToArray();

            // act
            Option<int> result = Solver.Solve(input);

            // assert
            Option<int> expected = Some(5);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Case4()
        {
            // arrange
            Option<int>[] input = new[] { +7, +7, -2, -7, -4 }.Select(Some).ToArray();

            // act
            Option<int> result = Solver.Solve(input);

            // assert
            Option<int> expected = Some(14);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Case5()
        {
            // arrange
            Option<int>[] input = new[] { +1, -2, +3, +1, +1, -2 }.Select(Some).ToArray();

            // act
            Option<int> result = Solver.Solve(input);

            // assert
            Option<int> expected = Some(2);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ItShouldReturnNoneWhenOneOfInputsIsNone()
        {
            // arrange
            Option<int>[] input = new[] { Some(1), None, Some(-1) };

            // act
            Option<int> result = Solver.Solve(input);

            // assert
            Option<int> expected = None;
            Assert.AreEqual(expected, result);
        }
    }
}