using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace src
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = args[0];
            List<(char BlockingTask, char BlockedTask)> inputs = File.ReadLines(fileName).Select(Parse).ToList();

            Part1(inputs);
            Part2(inputs);
        }

        private static void Part1(List<(char BlockingTask, char BlockedTask)> inputs)
        {
            var taskExecutor = new SingleThreadTaskExecutor<char>();
            Solve(inputs, taskExecutor);
        }

        private static void Part2(List<(char BlockingTask, char BlockedTask)> inputs)
        {
            var taskExecutor = new MultiThreadedTaskExecutor<char>(5, GetTaskDurations());
            Solve(inputs, taskExecutor);
        }

        private static void Solve(List<(char BlockingTask, char BlockedTask)> inputs, ITaskExecutor<char> taskExecutor, [CallerMemberName] string partName = "")
        {
            TasksSchedule<char> tasksSchedule = TasksSchedule<char>.Create(inputs);
            (char[] order, int duration) = taskExecutor.Execute(tasksSchedule);

            string orderAsString = new string(order);
            Console.WriteLine($"{partName}: Order {orderAsString}, duration: {duration}");
        }

        private static (char BlockingTask, char BlockedTask) Parse(string line)
        {
            string[] splitted = line.Split(' ');
            return (splitted[1].Single(), splitted[7].Single());
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
    }
}
