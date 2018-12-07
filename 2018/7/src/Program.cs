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

            Solve1(numberOfTasks, inputs, allTasks.ToList(), tasksToGraphIdMapping);
            Solve2(numberOfTasks, inputs, allTasks.ToList(), tasksToGraphIdMapping);
        }

        private static void Solve1(
            int numberOfTasks,
            List<(char BlockingTask, char BlockedTask)> inputs,
            List<char> allTasks,
            Dictionary<char, int> tasksToGraphIdMapping
            )
        {
            bool[,] graph = CreateGraph(numberOfTasks, inputs, tasksToGraphIdMapping);
            StringBuilder resultBuilder = new StringBuilder(numberOfTasks);
            while (allTasks.Any())
            {
                char? nonBlocked = GetFirstNonBlocked(allTasks, tasksToGraphIdMapping, graph);
                resultBuilder.Append(nonBlocked);
                allTasks.Remove(nonBlocked.Value);
                UnblockBlockedTasks(tasksToGraphIdMapping[nonBlocked.Value], graph);
            }
            Console.WriteLine($"Part1: {resultBuilder}");
        }

        private static void Solve2(
            int numberOfTasks,
            List<(char BlockingTask, char BlockedTask)> inputs,
            List<char> allTasks,
            Dictionary<char, int> tasksToGraphIdMapping
            )
        {
            const int numberOfWorkers = 5;
            Dictionary<char, int> tasksDuration = GetTaskDurations();

            bool[,] graph = CreateGraph(numberOfTasks, inputs, tasksToGraphIdMapping);
            var executingTasks = new List<(char ExecutingTask, int TimeLeft)>();

            int timeElapsed = 0;
            while (allTasks.Any() || executingTasks.Count > 0)
            {                
                // add new Items
                bool isEachTaskBlocked = false;
                while (!isEachTaskBlocked && executingTasks.Count < numberOfWorkers)
                {
                    char? nonBlocked = GetFirstNonBlocked(allTasks, tasksToGraphIdMapping, graph);
                    if (nonBlocked.HasValue)
                    {
                        executingTasks.Add((nonBlocked.Value, tasksDuration[nonBlocked.Value]));
                        allTasks.Remove(nonBlocked.Value);
                    }
                    else
                    {
                        isEachTaskBlocked = true;
                    }
                }

                // do work
                timeElapsed++;

                // decreseTime & remove
                var newExecutingTasks = new List<(char ExecutingTask, int TimeLeft)>();
                foreach ((char taskId, int timeLeft) in executingTasks)
                {
                    int newTimeLeft = timeLeft - 1;
                    if (newTimeLeft == 0)
                    {
                        UnblockBlockedTasks(tasksToGraphIdMapping[taskId], graph);
                    }
                    else
                    {
                        newExecutingTasks.Add((taskId, newTimeLeft));
                    }
                }
                executingTasks = newExecutingTasks;

            }
            Console.WriteLine(timeElapsed);
        }

        private static Dictionary<char, int> GetTaskDurations()
        {
            const int additionalDuration = 60;
            var result = new Dictionary<char, int>();
            for (char c = 'A'; c <= 'Z'; c++)
            {
                int baseCharDuration = c - 64;
                int totalDuration = baseCharDuration + additionalDuration;
                result[c] = totalDuration;
            }
            return result;
        }

        private static bool[,] CreateGraph(int numberOfTasks, List<(char BlockingTask, char BlockedTask)> inputs, Dictionary<char, int> tasksToGraphIdMapping)
        {
            bool[,] graph = new bool[numberOfTasks, numberOfTasks];
            foreach ((char blockingTask, char blockedTask) in inputs)
            {
                graph[tasksToGraphIdMapping[blockingTask], tasksToGraphIdMapping[blockedTask]] = true;
            }
            return graph;
        }

        private static char? GetFirstNonBlocked(List<char> tasks, Dictionary<char, int> mapping, bool[,] graph)
        {
            foreach (char task in tasks)
            {
                if (!IsBlocked(mapping[task], graph))
                {
                    return task;
                }
            }
            return null;
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
