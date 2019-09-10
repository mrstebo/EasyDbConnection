using System.Collections.Generic;
using System.Data;
using EasyDbConnection.Extensions;

namespace EasyDbConnection.Commands
{
    internal class ExecuteReaderCommand
    {
        private readonly IDbConnection _connection;

        public ExecuteReaderCommand(IDbConnection connection)
        {
            _connection = connection;
        }

        public IDataReader Execute(string commandText, IEnumerable<DbParam> parameters)
        {
            var command = _connection.CreateCommand();
         
            command.CommandText = commandText;

            foreach (var parameter in parameters)
            {
                var p = command.CreateFromDbParam(parameter);
                
                command.Parameters.Add(p);
            }

            return command.ExecuteReader();
        }
    }
}
