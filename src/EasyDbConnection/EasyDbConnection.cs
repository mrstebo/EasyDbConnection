using System;
using System.Data;
using System.Diagnostics;
using EasyDbConnection.Commands;
using EasyDbConnection.Events;
using EasyDbConnection.Tasks;

namespace EasyDbConnection
{
    internal class EasyDbConnection : IEasyDbConnection
    {
        private readonly IDbConnection _connection;

        public EasyDbConnection(IDbConnection connection)
        {
            _connection = connection;
        }

        public int ExecuteNonQuery(string commandText, params DbParam[] parameters)
        {
            OpenConnection();
            
            OnExecuting(new DbCommandExecutingEventArgs(commandText, parameters));

            var command = new ExecuteNonQueryCommand(_connection);
            var result = Execute(() => command.Execute(commandText, parameters));

            OnExecuted(new DbCommandExecutedEventArgs(commandText, parameters, result.TimeTaken));

            return result.Value;
        }

        public object ExecuteScalar(string commandText, params DbParam[] parameters)
        {
            OpenConnection();
            
            OnExecuting(new DbCommandExecutingEventArgs(commandText, parameters));

            var command = new ExecuteScalarCommand(_connection);
            var result = Execute(() => command.Execute(commandText, parameters));

            OnExecuted(new DbCommandExecutedEventArgs(commandText, parameters, result.TimeTaken));

            return result.Value;
        }

        public IDataReader ExecuteReader(string commandText, params DbParam[] parameters)
        {
            OpenConnection();
            
            OnExecuting(new DbCommandExecutingEventArgs(commandText, parameters));

            var command = new ExecuteReaderCommand(_connection);
            var result = Execute(() => command.Execute(commandText, parameters));

            OnExecuted(new DbCommandExecutedEventArgs(commandText, parameters, result.TimeTaken));

            return result.Value;
        }

        public event EventHandler<DbCommandExecutingEventArgs> Executing;
        public event EventHandler<DbCommandExecutedEventArgs> Executed;

        private void OnExecuting(DbCommandExecutingEventArgs e)
        {
            Executing?.Invoke(this, e);
        }

        private void OnExecuted(DbCommandExecutedEventArgs e)
        {
            Executed?.Invoke(this, e);
        }

        private void OpenConnection()
        {
            if (_connection.State == ConnectionState.Closed)
                _connection.Open();
        }
        
        private static TimedTask<T> Execute<T>(Func<T> func)
        {
            return TimedTaskRunner.Execute(func);
        }
    }
}