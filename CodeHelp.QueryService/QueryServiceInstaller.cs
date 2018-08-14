using CodeHelp.QueryService.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace CodeHelp.QueryService
{
    public class QueryServiceInstaller
    {
        public static void ConfigureContainer(IServiceCollection services)
        {
            services.AddScoped<IDataTablesQueryService, DataTablesQueryService>();
            services.AddScoped<ITableColumnsQueryService, TableColumnsQueryService>();
        }
    }
}