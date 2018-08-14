using Microsoft.Extensions.Configuration;

namespace CodeHelp.Data.Dapper
{
    public class ConnectionStringManager : IConnectionStringManager
    {
        public ConnectionStringManager(IConfiguration configuration)
        {
            ConnectionString = new DataConfig(configuration).GetConnectionString();
        }

        public ConnectionStringManager(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; }
    }
}