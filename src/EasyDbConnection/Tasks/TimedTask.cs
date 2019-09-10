using System;

namespace EasyDbConnection.Tasks
{
    internal struct TimedTask<T>
    {
        public T Value;
        public TimeSpan TimeTaken;
    }
}