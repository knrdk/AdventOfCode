using System;
using System.Collections.Generic;

namespace src
{
    public class MultiThreadedTaskExecutor<T> : ITaskExecutor<T> where T : struct, IComparable<T>, IEquatable<T>
    {
        private int _numberOfWorkers;
        private Dictionary<T, int> _tasksDuration;
        
        public MultiThreadedTaskExecutor(int numberOfWorksers, Dictionary<T, int> tasksDuration)
        {
            _numberOfWorkers = numberOfWorksers;
            _tasksDuration = tasksDuration;
        }

        public (T[] taskOrder, int executionDuration) Execute(TasksSchedule<T> schedule)
        {
            var executingTasks = new List<(T ExecutingTask, int TimeLeft)>();

            int timeElapsed = 0;
            while (!schedule.AreAllTasksCompleted)
            {
                TakeNewTaskForExecution(executingTasks, schedule);
                timeElapsed++; // do work
                executingTasks = UpdateTasksAfterUnitOfExecution(executingTasks, schedule);
            }

            return (schedule.CompletedTasks.ToArray(), timeElapsed);
        }

        private void TakeNewTaskForExecution(List<(T ExecutingTask, int TimeLeft)> executingTasks, TasksSchedule<T> schedule)
        {
            bool isEachTaskBlocked = false;
            while (!isEachTaskBlocked && executingTasks.Count < _numberOfWorkers)
            {
                T? nonBlocked = schedule.GetFirstNonBlocked();
                if (nonBlocked.HasValue)
                {
                    executingTasks.Add((nonBlocked.Value, _tasksDuration[nonBlocked.Value]));
                    schedule.MarkAsExecuting(nonBlocked.Value);
                }
                else
                {
                    isEachTaskBlocked = true;
                }
            }
        }

        private List<(T ExecutingTask, int TimeLeft)> UpdateTasksAfterUnitOfExecution(List<(T ExecutingTask, int TimeLeft)> executingTasks, TasksSchedule<T> schedule)
        {
            var newExecutingTasks = new List<(T ExecutingTask, int TimeLeft)>();
            foreach ((T task, int timeLeft) in executingTasks)
            {
                int newTimeLeft = timeLeft - 1;
                if (newTimeLeft == 0)
                {
                    schedule.MarkAsCompleted(task);
                }
                else
                {
                    newExecutingTasks.Add((task, newTimeLeft));
                }
            }
            return newExecutingTasks;
        }
    }
}