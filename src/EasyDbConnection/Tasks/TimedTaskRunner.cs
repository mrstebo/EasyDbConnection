using System;
using System.Diagnostics;

namespace EasyDbConnection.Tasks
{
    internal static class TimedTaskRunner
    {
        public static TimedTask<T> Execute<T>(Func<T> func)
        {
            var stopwatch = new Stopwatch();
            
            stopwatch.Start();
            
            return new TimedTask<T>
            {
                Value = func(),
                TimeTaken = stopwatch.Elapsed
            };
        }
    }
}