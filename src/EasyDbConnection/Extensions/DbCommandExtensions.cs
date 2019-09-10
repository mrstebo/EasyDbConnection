using System.Data;

namespace EasyDbConnection.Extensions
{
    internal static class DbCommandExtensions
    {
        public static IDbDataParameter CreateFromDbParam(this IDbCommand command, DbParam parameter)
        {
            var p = command.CreateParameter();

            p.ParameterName = parameter.ParameterName;
            p.DbType = parameter.DbType;
            p.Value = parameter.Value;

            return p;
        }
    }
}