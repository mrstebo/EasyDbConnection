using System;

namespace EasyDbConnection.Events
{
    public class DbCommandExecutedEventArgs : EventArgs
    {
        public string CommandText { get; }
        public DbParam[] Parameters { get; }
        public TimeSpan TimeTaken { get; }

        public DbCommandExecutedEventArgs(string commandText, DbParam[] parameters, TimeSpan timeTaken)
        {
            CommandText = commandText;
            Parameters = parameters;
            TimeTaken = timeTaken;
        }
    }
}