using System.Data;
using System.Data.SqlClient;

namespace CodeHelp.Data.Dapper
{
    public class SqlDbConnectionFactory : Disposable, IDbConnectionFactory
    {
        private readonly IConnectionStringManager _connectionStringManager;

        public SqlDbConnectionFactory(IConnectionStringManager connectionStringManager)
        {
            _connectionStringManager = connectionStringManager;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionStringManager.ConnectionString);
        }
    }
}