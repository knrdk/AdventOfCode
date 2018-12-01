using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using src;

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
            int[] input = new[] {1, -1};

            // act
            int result = Solver.Solve(input);

            // assert
            int expected = 0;
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Case2()
        {
            // arrange
            int[] input = new[] {+3, +3, +4, -2, -4};

            // act
            int result = Solver.Solve(input);

            // assert
            int expected = 10;
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Case3()
        {
            // arrange
            int[] input = new[] {-6, +3, +8, +5, -6};

            // act
            int result = Solver.Solve(input);

            // assert
            int expected = 5;
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Case4()
        {
            // arrange
            int[] input = new[] {+7, +7, -2, -7, -4};

            // act
            int result = Solver.Solve(input);

            // assert
            int expected = 14;
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Case5()
        {
            // arrange
            int[] input = new[] {+1, -2, +3, +1, +1, -2};

            // act
            int result = Solver.Solve(input);

            // assert
            int expected = 2;
            Assert.AreEqual(expected, result);
        }       
    }
}