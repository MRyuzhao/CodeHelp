using CodeHelp.Repository.Impl;
using CodeHelp.Repository.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace CodeHelp.Repository
{
    public static class RepositoryInstaller
    {
        public static void ConfigureContainer(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            SharedWiring(services);
        }

        private static void SharedWiring(IServiceCollection services)
        {
            services.AddScoped<IDataTablesRepository, DataTablesRepository>();
            services.AddScoped<ITableColumnsRepository, TableColumnsRepository>();
        }
    }
}