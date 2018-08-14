using Microsoft.Extensions.DependencyInjection;

namespace CodeHelp.Data.Dapper
{
    public class DataInstaller
    {
        public static void ConfigureContainer(IServiceCollection services)
        {
            services.AddScoped<IConnectionStringManager, ConnectionStringManager>();
            services.AddScoped<IDbConnectionFactory, SqlDbConnectionFactory>();
            services.AddScoped<ISqlDatabaseProxy, SqlDatabaseProxy>();
        }
    }
}