using System;
using System.Data;
using EasyDbConnection.Events;

namespace EasyDbConnection
{
    public interface IEasyDbConnection
    {
        int ExecuteNonQuery(string commandText, params DbParam[] parameters);
        object ExecuteScalar(string commandText, params DbParam[] parameters);
        IDataReader ExecuteReader(string commandText, params DbParam[] parameters);
        
        event EventHandler<DbCommandExecutingEventArgs> Executing;
        event EventHandler<DbCommandExecutedEventArgs> Executed;
    }
}
