using System;

namespace EasyDbConnection.Events
{
    public class DbCommandExecutingEventArgs : EventArgs
    {
        public string CommandText { get; }
        public DbParam[] Parameters { get; }

        public DbCommandExecutingEventArgs(string commandText, DbParam[] parameters)
        {
            CommandText = commandText;
            Parameters = parameters;
        }
    }
}
