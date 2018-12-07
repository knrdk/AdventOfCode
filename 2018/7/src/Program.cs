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

            Solve1(inputs);
            Solve2(inputs);
        }

        private static void Solve1(List<(char BlockingTask, char BlockedTask)> inputs)
        {
            TasksSchedule<char> tasksSchedule = TasksSchedule<char>.Create(inputs);

            StringBuilder resultBuilder = new StringBuilder(tasksSchedule.NumberOfTasks);
            while (!tasksSchedule.IsEmpty)
            {
                char? nonBlocked = tasksSchedule.GetFirstNonBlocked();
                tasksSchedule.MarkAsCompleted(nonBlocked.Value);

                resultBuilder.Append(nonBlocked.Value);
            }
            Console.WriteLine($"Part1: {resultBuilder}");
        }

        private static void Solve2(List<(char BlockingTask, char BlockedTask)> inputs)
        {
            const int numberOfWorkers = 5;
            Dictionary<char, int> tasksDuration = GetTaskDurations();

            TasksSchedule<char> tasksSchedule = TasksSchedule<char>.Create(inputs);
            var executingTasks = new List<(char ExecutingTask, int TimeLeft)>();

            int timeElapsed = 0;
            while (!tasksSchedule.IsEmpty || executingTasks.Count > 0)
            {
                // add new Items
                bool isEachTaskBlocked = false;
                while (!isEachTaskBlocked && executingTasks.Count < numberOfWorkers)
                {
                    char? nonBlocked = tasksSchedule.GetFirstNonBlocked();
                    if (nonBlocked.HasValue)
                    {
                        executingTasks.Add((nonBlocked.Value, tasksDuration[nonBlocked.Value]));
                        tasksSchedule.MarkAsExecuting(nonBlocked.Value);
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
                        tasksSchedule.MarkAsCompleted(taskId);
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

        private static (char BlockingTask, char BlockedTask) Parse(string line)
        {
            string[] splitted = line.Split(' ');
            return (splitted[1].Single(), splitted[7].Single());
        }
    }
}
