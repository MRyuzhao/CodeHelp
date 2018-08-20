using Microsoft.Extensions.Configuration;

namespace CodeHelp.Data.Dapper
{
    public class DataConfig
    {
        public IConfiguration Configuration { get; }

        public DataConfig(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private static string GetConnectionStringKey()
        {
            //return "ZOS";
            return "OperationPlatformDB";
        }

        public string GetConnectionString()
        {
            var connectionStringKey = GetConnectionStringKey();

            return Configuration.GetSection("ConnectionStrings")?[connectionStringKey];
        }
    }
}