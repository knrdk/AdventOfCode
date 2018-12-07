using System;
using System.Collections.Generic;

namespace src
{
    public class SingleThreadTaskExecutor<T> : ITaskExecutor<T> where T : struct, IComparable<T>, IEquatable<T>
    {
        public (T[] taskOrder, int executionDuration) Execute(TasksSchedule<T> schedule)
        {
            while (!schedule.AreAllTasksCompleted)
            {
                T? taskToExecute = schedule.GetFirstNonBlocked();
                schedule.MarkAsCompleted(taskToExecute.Value);
            }
            return (schedule.CompletedTasks.ToArray(), 0);
        }
    }
}