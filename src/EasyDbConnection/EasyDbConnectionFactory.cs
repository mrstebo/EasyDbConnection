using System.Data;

namespace EasyDbConnection
{
    public static class EasyDbConnectionFactory
    {
        public static IEasyDbConnection Create(IDbConnection connection)
        {
            return new EasyDbConnection(connection);
        }
    }
}