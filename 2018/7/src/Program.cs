using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace src
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = args[0];
            List<(char BlockingTask, char BlockedTask)> inputs = File.ReadLines(fileName).Select(Parse).ToList();
            List<char> allTasks = inputs
                .Select(x => x.BlockingTask)
                .Concat(inputs.Select(x => x.BlockedTask))
                .ToHashSet()
                .OrderBy(x => x)
                .ToList();

            int numberOfTasks = allTasks.Count;
            Dictionary<char, int> tasksToGraphIdMapping = Enumerable.Range(0, numberOfTasks).ToDictionary(i => allTasks[i], i => i);

            bool[,] graph = new bool[numberOfTasks, numberOfTasks];
            foreach ((char blockingTask, char blockedTask) in inputs)
            {
                graph[tasksToGraphIdMapping[blockingTask], tasksToGraphIdMapping[blockedTask]] = true;
            }

            StringBuilder resultBuilder = new StringBuilder(numberOfTasks);
            while (allTasks.Any())
            {
                char nonBlocked = GetFirstNonBlocked(allTasks, tasksToGraphIdMapping, graph);
                resultBuilder.Append(nonBlocked);
                allTasks.Remove(nonBlocked);
                UnblockBlockedTasks(tasksToGraphIdMapping[nonBlocked], graph);
            }
            Console.WriteLine(resultBuilder.ToString());
        }

        private static char GetFirstNonBlocked(List<char> tasks, Dictionary<char, int> mapping, bool[,] graph)
        {
            foreach (char task in tasks)
            {
                if (!IsBlocked(mapping[task], graph))
                {
                    return task;
                }
            }
            throw new InvalidOperationException();
        }


        private static bool IsBlocked(int vertex, bool[,] graph)
        {
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                if (graph[i, vertex])
                {
                    return true;
                }
            }
            return false;
        }

        private static void UnblockBlockedTasks(int vertex, bool[,] graph)
        {
            for (int j = 0; j < graph.GetLength(1); j++)
            {
                graph[vertex, j] = false;
            }
        }

        private static (char BlockingTask, char BlockedTask) Parse(string line)
        {
            string[] splitted = line.Split(' ');
            return (splitted[1].Single(), splitted[7].Single());
        }
    }
}
