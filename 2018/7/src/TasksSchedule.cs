using System;
using System.Collections.Generic;
using System.Linq;

namespace src
{
    public class TasksSchedule<T> where T : struct, IComparable<T>, IEquatable<T>
    {
        private List<T> _tasksToExecute;
        private Dictionary<T, int> _tasksToGraphIdMapping;
        private bool[,] _graph;

        public int NumberOfTasks { get; }

        public List<T> CompletedTasks { get; }

        public bool AreAllTasksCompleted => CompletedTasks.Count == NumberOfTasks;

        private TasksSchedule(List<(T BlockingTask, T BlockedTask)> inputs)
        {
            CompletedTasks = new List<T>();

            _tasksToExecute = inputs
                .Select(x => x.BlockingTask)
                .Concat(inputs.Select(x => x.BlockedTask))
                .ToHashSet()
                .OrderBy(x => x)
                .ToList();

            NumberOfTasks = _tasksToExecute.Count;

            _tasksToGraphIdMapping = Enumerable.Range(0, NumberOfTasks).ToDictionary(i => _tasksToExecute[i], i => i);

            _graph = CreateGraph(NumberOfTasks, inputs, _tasksToGraphIdMapping);
        }
       
        public static TasksSchedule<T> Create(List<(T BlockingTask, T BlockedTask)> inputs)
        {
            return new TasksSchedule<T>(inputs);
        }

        public T? GetFirstNonBlocked()
        {
            T valueToReturn = _tasksToExecute.FirstOrDefault(x => !IsBlocked(x));
            return EqualityComparer<T>.Default.Equals(valueToReturn, default(T))
                ? (T?)null
                : valueToReturn;
        }

        public void MarkAsExecuting(T task)
        {
            _tasksToExecute.Remove(task);
        }

        public void MarkAsCompleted(T task)
        {
            if (_tasksToExecute.Contains(task))
            {
                _tasksToExecute.Remove(task);
            }
            CompletedTasks.Add(task);
            UnblockBlockedTasks(task);
        }

        private static bool[,] CreateGraph(int numberOfTasks, List<(T BlockingTask, T BlockedTask)> inputs, Dictionary<T, int> tasksToGraphIdMapping)
        {
            bool[,] graph = new bool[numberOfTasks, numberOfTasks];
            foreach ((T blockingTask, T blockedTask) in inputs)
            {
                graph[tasksToGraphIdMapping[blockingTask], tasksToGraphIdMapping[blockedTask]] = true;
            }
            return graph;
        }

        private bool IsBlocked(T task)
        {
            for (int i = 0; i < _graph.GetLength(0); i++)
            {
                if (_graph[i, _tasksToGraphIdMapping[task]])
                {
                    return true;
                }
            }
            return false;
        }

        private void UnblockBlockedTasks(T task)
        {
            for (int j = 0; j < _graph.GetLength(1); j++)
            {
                _graph[_tasksToGraphIdMapping[task], j] = false;
            }
        }
    }
}