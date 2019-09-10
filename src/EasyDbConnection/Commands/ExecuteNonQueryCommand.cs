using System.Collections.Generic;
using System.Data;
using EasyDbConnection.Extensions;

namespace EasyDbConnection.Commands
{
    internal class ExecuteNonQueryCommand
    {
        private readonly IDbConnection _connection;

        public ExecuteNonQueryCommand(IDbConnection connection)
        {
            _connection = connection;
        }

        public int Execute(string commandText, IEnumerable<DbParam> parameters)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = commandText;

                foreach (var parameter in parameters)
                {
                    var p = command.CreateFromDbParam(parameter); 
                    
                    command.Parameters.Add(p);
                }

                return command.ExecuteNonQuery();
            }
        }
    }
}
