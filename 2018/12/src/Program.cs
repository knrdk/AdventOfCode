using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace src
{
    class Program
    {
        const int numberOfIterations = 200;
        static void Main(string[] args)
        {            
            string fileName = args[0];
            string[] inputs = File.ReadAllLines(fileName).ToArray();

            bool[] initialState = Convert(inputs[0]);
            string[] a = inputs
                .Skip(1)
                .Where(x => x[9] == '#')
                .Select(x => new string(x.Take(5).ToArray()))
                .ToArray();
            bool[][] positiveRules = a
                .Select(Convert)
                .ToArray();

            var results = new List<(int leftIndex, bool[] state)>();
            bool[] state = initialState;
            int currentLeftIndex = 0;
            for (int iteration = 0; iteration < numberOfIterations; iteration++)
            {
                if (results.Any(x => AreEqual(x.state, state)))
                {
                    DisplayWithScore(iteration, currentLeftIndex, state);
                }
                results.Add((currentLeftIndex, state));

                int nextLeftIndex = currentLeftIndex - 2;
                int nextLength = state.Length + 4;
                bool[] nextState = new bool[nextLength];
                for (int i = nextLeftIndex; i < nextLength + nextLeftIndex; i++)
                {
                    bool[] neighborhood = GetNeighborhood(i, currentLeftIndex, state);
                    nextState[i - nextLeftIndex] = positiveRules.Any(x => AreEqual(x, neighborhood));
                }

                (currentLeftIndex, state) = Reduce(nextLeftIndex, nextState);
            }

            DisplayWithScore(numberOfIterations, currentLeftIndex, state);
        }

        static void DisplayWithScore(int iteration, int leftIndex, bool[] state)
        {
            char[] generationAsChar = state.Select(x => x ? '#' : '.').ToArray();
            string generation = new string(generationAsChar);
            long score = CalculateScore(leftIndex, state);
            Console.WriteLine($"{iteration}, {score}: {generation}");
        }

        static (int leftIndex, bool[] state) Reduce(int currentLeftIndex, bool[] currentState)
        {
            int leftIndex = currentLeftIndex;
            int leftIndexInArray = 0;
            while (leftIndexInArray < currentState.Length && !currentState[leftIndexInArray])
            {
                leftIndexInArray++;
                leftIndex++;
            }

            int rightIndexInArray = currentState.Length - 1;
            while (!currentState[rightIndexInArray])
            {
                rightIndexInArray--;
            }
            rightIndexInArray++;

            if (leftIndexInArray >= rightIndexInArray)
            {
                return (0, new bool[0]);
            }

            bool[] state = currentState
                .Skip(leftIndexInArray)
                .Take(rightIndexInArray - leftIndexInArray)
                .ToArray();
            return (leftIndex, state);
        }

        static bool[] Convert(string input)
        {
            return input.Select(x => x == '#').ToArray();
        }

        static bool[] GetNeighborhood(int position, int currentLeftIndex, bool[] state)
        {
            bool[] result = new bool[5];
            for (int i = 0; i < 5; i++)
            {
                int neighbor = position - 2 + i;
                bool isIn = neighbor >= currentLeftIndex && neighbor < state.Length + currentLeftIndex;
                int neighborIndex = neighbor - currentLeftIndex;
                result[i] = isIn ? state[neighborIndex] : false;
            }
            return result;
        }

        static long CalculateScore(int currentLeftIndex, bool[] state)
        {
            long result = 0;
            for (int i = currentLeftIndex; i < state.Length + currentLeftIndex; i++)
            {
                int index = i - currentLeftIndex;
                result += state[index] ? i : 0;
            }
            return result;
        }

        static bool AreEqual(bool[] a, bool[] b)
        {
            if (a.Length != b.Length)
            {
                return false;
            }
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
