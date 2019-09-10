using System.Collections.Generic;
using System.Data;
using EasyDbConnection.Extensions;

namespace EasyDbConnection.Commands
{
    internal class ExecuteScalarCommand
    {
        private readonly IDbConnection _connection;

        public ExecuteScalarCommand(IDbConnection connection)
        {
            _connection = connection;
        }
        
        public object Execute(string commandText, IEnumerable<DbParam> parameters)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = commandText;

                foreach (var parameter in parameters)
                {
                    var p = command.CreateFromDbParam(parameter);
                    
                    command.Parameters.Add(p);
                }

                return command.ExecuteScalar();
            }
        }
    }
}
