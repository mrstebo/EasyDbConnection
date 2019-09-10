using System;

namespace EasyDbConnection.Events
{
    public class DbCommandExecutingEventArgs : EventArgs
    {
        public DbCommandExecutingEventArgs(string commandText, DbParam[] parameters)
        {
            CommandText = commandText;
            Parameters = parameters;
        }

        public string CommandText { get; }
        public DbParam[] Parameters { get; }
    }
}