using System;
using System.Data;
using System.Diagnostics;
using EasyDbConnection.Commands;
using EasyDbConnection.Events;

namespace EasyDbConnection
{
    public class EasyDbConnection : IEasyDbConnection
    {
        private readonly IDbConnection _connection;

        public EasyDbConnection(IDbConnection connection)
        {
            _connection = connection;
        }

        public void Open()
        {
            if (_connection.State == ConnectionState.Closed)
                _connection.Open();
        }

        public void Close()
        {
            if (_connection.State != ConnectionState.Closed)
                _connection.Close();
        }

        public int ExecuteNonQuery(string commandText, params DbParam[] parameters)
        {
            OnExecuting(new DbCommandExecutingEventArgs(commandText, parameters));

            var command = new ExecuteNonQueryCommand(_connection);
            var result = Execute(() => command.Execute(commandText, parameters));

            OnExecuted(new DbCommandExecutedEventArgs(commandText, parameters, result.TimeTaken));

            return result.Value;
        }

        public object ExecuteScalar(string commandText, params DbParam[] parameters)
        {
            OnExecuting(new DbCommandExecutingEventArgs(commandText, parameters));

            var command = new ExecuteScalarCommand(_connection);
            var result = Execute(() => command.Execute(commandText, parameters));

            OnExecuted(new DbCommandExecutedEventArgs(commandText, parameters, result.TimeTaken));

            return result.Value;
        }

        public IDataReader ExecuteReader(string commandText, params DbParam[] parameters)
        {
            OnExecuting(new DbCommandExecutingEventArgs(commandText, parameters));

            var command = new ExecuteReaderCommand(_connection);
            var result = Execute(() => command.Execute(commandText, parameters));

            OnExecuted(new DbCommandExecutedEventArgs(commandText, parameters, result.TimeTaken));

            return result.Value;
        }

        public event EventHandler<DbCommandExecutingEventArgs> Executing;
        public event EventHandler<DbCommandExecutedEventArgs> Executed;

        protected virtual void OnExecuting(DbCommandExecutingEventArgs e)
        {
            Executing?.Invoke(this, e);
        }

        protected virtual void OnExecuted(DbCommandExecutedEventArgs e)
        {
            Executed?.Invoke(this, e);
        }

        private static TimedResult<T> Execute<T>(Func<T> func)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            return new TimedResult<T> {Value = func(), TimeTaken = stopwatch.Elapsed};
        }

        private struct TimedResult<T>
        {
            public T Value;
            public TimeSpan TimeTaken;
        }
    }
}
