using System;

namespace src
{
    public interface ITaskExecutor<T> where T : struct, IComparable<T>, IEquatable<T>
    {
        (T[] taskOrder, int executionDuration) Execute(TasksSchedule<T> schedule);
    }
}